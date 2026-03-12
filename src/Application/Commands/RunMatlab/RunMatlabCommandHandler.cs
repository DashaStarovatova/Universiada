using MediatR;
using Application.Interfaces;
using Domain.Interfaces;
using Domain;


public class RunMatlabHandler : IRequestHandler<RunMatlabCommand>
{
    private readonly IMatlabRunner _runner;
    private readonly IResultRepository _resultRepo;

    public RunMatlabHandler(IMatlabRunner runner, IResultRepository resultRepo)
    {
        _runner = runner;
        _resultRepo = resultRepo;
    }

    public async Task Handle(
        RunMatlabCommand command,
        CancellationToken token)
    {

        MatlabResult[] matlabResult = [];


        matlabResult = await _runner.RunMatlabScript(
        command.KeyRate,
        command.IsRefresh,
        command.TeamId);


        if (matlabResult != null && matlabResult.Length > 0)
        {
            MatlabResult lastItem = matlabResult.Last();
            var result = new Result(
                command.TeamId,
                lastItem.period,
                lastItem.zzobs_dNFX,
                lastItem.zzobs_dPC,
                lastItem.zzobs_dRFX,
                lastItem.zzobs_dY,
                lastItem.zzobs_r_G,
                command.AnswerId

            );

            await _resultRepo.AddAsync(result);
            await _resultRepo.SaveAsync();

        }


    }
}