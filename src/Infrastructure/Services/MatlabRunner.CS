using System.Text.Json;
using System.Diagnostics;
using Application.Interfaces;
namespace Infrastructure.Services;
using System.Globalization;

class MatlabRunner : IMatlabRunner
{
    private readonly string _matlabExeFile;
    private readonly string _workDirectory;
    private readonly string _scriptName;
    private readonly string _historyFilePath;

    // Указание путей к приложению/раб.директории/скрипту
    public MatlabRunner()
    {
        _matlabExeFile = @"C:\Users\Darya\Desktop\Matlab new\bin\matlab.exe";
        _workDirectory = @"C:\Users\Darya\Desktop\testUniversiada";
        _scriptName = "game1_oneDesicion";
        _historyFilePath = @"C:\Users\Darya\Desktop\testUniversiada\letsgo";
    }

    // Для пользователей
    public async Task<MatlabResult[]> RunMatlabScript(double keyRate, int isRefresh, Guid teamID)
    {
        // 0. Формируем путь до файла-истории
        string historyFilePath = Path.Combine(_historyFilePath, teamID.ToString(), teamID.ToString() + ".mat");

        // 1. Формируем команду для MATLAB
        string matlabCommand = $"{_scriptName}({keyRate.ToString(CultureInfo.InvariantCulture)}, {isRefresh}, '{historyFilePath}')";

        // 2. Создаем настройки для запуска MATLAB как внешней программы
        ProcessStartInfo startInfo = new ProcessStartInfo // ProcessStartInfo - это тип данных (класс) для хранения настроек запуска процесса.
        {
            FileName = _matlabExeFile, // полный путь к matlab.exe
            Arguments = $"-batch \"{matlabCommand}\"", // Запускает MATLAB без графического интерфейса (только консоль). Выполняет команду и сразу завершает работу. Для MATLAB R2019a+
            WorkingDirectory = _workDirectory,
            UseShellExecute = false, // запускаем процесс напрямую, а не через оболочку Windows. Это нужно, чтобы перенаправлять ввод/вывод.
            RedirectStandardOutput = true, // перехватываем текстовый вывод MATLAB (всё, что он печатает в консоль) в нашу программу на C#.
            CreateNoWindow = true // скрываем окно консоли, процесс работает в фоне без видимого окна
        };

        // 3. Запускаем MATLAB, читаем его вывод и возвращаем результат
        using (Process process = new Process { StartInfo = startInfo })
        // using - автоматически закроет и удалит процесс после выполнения блока (даже при ошибке).
        {
            process.Start(); // запускает MATLAB с заданными настройками
            string output = await process.StandardOutput.ReadToEndAsync();  // асинхронно читает весь текстовый вывод MATLAB (то, что обычно видно в консоли).
            await process.WaitForExitAsync(); // ждёт, пока MATLAB завершит работу
            // 2. Десереализуем
            // Десереализация — это преобразование JSON/XML/другого формата обратно в объекты C#
            // Сериализация - обратный процесс
            MatlabResult[]? results = JsonSerializer.Deserialize<MatlabResult[]>(output);
            if (results == null || results.Length == 0)
            {
                // Просто возвращаем пустой массив
                results = Array.Empty<MatlabResult>();
            }

            return results;
        }


    }


};

