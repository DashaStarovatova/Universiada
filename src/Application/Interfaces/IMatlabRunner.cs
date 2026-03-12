namespace Application.Interfaces;
public interface IMatlabRunner
{
    Task<MatlabResult[]> RunMatlabScript(double keyRate, int isRefresh, Guid  teamID);

}


