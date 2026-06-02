using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using Application.Services;
using Domain;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services;

public class MatlabResolver : IAnswersResolver
{
    private readonly IServiceProvider provider;
    private readonly ConcurrentQueue<Answer> _answers;
    private readonly string _historyFilePath = @"C:\Users\Darya\Desktop\testUniversiada\letsgo\";
    private readonly string _scriptName = "game1_oneDesicion";
    private readonly string _matlabExeFile = @"C:\Users\Darya\Desktop\Matlab new\bin\matlab.exe";
    private readonly string _workDirectory = @"C:\Users\Darya\Desktop\testUniversiada\";

    public MatlabResolver(IServiceProvider serviceProvider)
    {
        provider = serviceProvider;
        _answers = new ConcurrentQueue<Answer>();
    }

    public void AddToQueue(Answer answer)
    {
        _answers.Enqueue(answer);
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        while (cancellationToken.IsCancellationRequested is false)
        {
            await foreach (var answer in _answers.ToAsyncEnumerable())
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                using var scope = provider.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IResultRepository>();

                var historyPath = $"{_historyFilePath}{answer.TeamId}/{answer.TeamId}.mat";

                var matlabCommand = $"game1_oneDesicion({answer.AnswerValue.ToString(CultureInfo.InvariantCulture)}, 0, '{historyPath}')";

                var processInfo = new ProcessStartInfo()
                {
                    FileName = _matlabExeFile,
                    Arguments = $"-batch \"{matlabCommand}\"",
                    WorkingDirectory = _workDirectory,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                using var process = new Process()
                {
                    StartInfo = processInfo
                };

                process.Start();

                var output = await process.StandardOutput.ReadToEndAsync(cancellationToken);

                await process.WaitForExitAsync(cancellationToken);

                var results = JsonSerializer.Deserialize<MatlabResult[]>(output);

                if (results is null || results.Length == 0)
                {
                    continue;
                }

                var lastItem = results.Last();
                var result = new Result(
                    answer.TeamId,
                    lastItem.period,
                    lastItem.zzobs_dNFX,
                    lastItem.zzobs_dPC,
                    lastItem.zzobs_dRFX,
                    lastItem.zzobs_dY,
                    lastItem.zzobs_r_G,
                    answer.Id
                );

                await repository.AddAsync(result);
                await repository.SaveAsync();
            }
        }
    }
}
