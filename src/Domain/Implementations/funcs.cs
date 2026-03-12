using System.Text;


// Создаём модель - результат работы матлаба
public class MatlabResult
{
    public string period { get; set; } = "";
    public double zzobs_dNFX { get; set; }
    public double zzobs_dPC { get; set; }
    public double zzobs_dRFX { get; set; }
    public double zzobs_dY { get; set; }
    public double zzobs_r_G { get; set; }
};


public class JsonToCsv
{
    public static void Convert<T>(T[] jsonModel, string outputPath) // T - любой type
    {
        if (jsonModel == null || jsonModel.Length == 0) return; // если нет данных, выходим из метода
        
        var properties = typeof(T).GetProperties(); // получаем все свойства класса MatlabResult
        
        using (var writer = new StreamWriter(outputPath, false, Encoding.UTF8)) 
        // Создание потока для записи в файл. false - не дописывать, а перезаписать файл
        // StreamWriter — класс для записи текста в файл
        {
            // 1. Заголовки
            writer.WriteLine(string.Join(";", properties.Select(p => p.Name))); // writer.WriteLine(string.Join(";", properties.Select(p => p.Name)));

            // 2. Данные
            foreach (var item in jsonModel)
            {
                var rowValues = new string[properties.Length]; // Создание массива для значений одной строки, фикс. длины
                
                for (int i = 0; i < properties.Length; i++)
                {
                    var value = properties[i].GetValue(item);
                    rowValues[i] = value?.ToString() ?? "";
                }
                
                writer.WriteLine(string.Join(";", rowValues));
            }
        }
    }
}
