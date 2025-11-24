using CommissionsOptimizerLib.Core.Enums;
using CommissionsOptimizerLib.Core.Interfaces;
using CommissionsOptimizerLib.Core.Models;

namespace CommissionsOptimizerLib.Core.Services;

public class CommissionsOptimizerService(IDataProvider DataProvider) : ICommissionsOptimizer
{
    public OptimizationResults CalculateCommissions(List<PlayerTrekkerData> playerTrekkers, OptimizerOptions options, CancellationToken cancellationToken = default)
    {
        var commissions = DataProvider.GetCommissionsData();
        var trekkers = DataProvider.GetTrekkersData();

        throw new NotImplementedException("Optimizer not implemented");
    }
}
