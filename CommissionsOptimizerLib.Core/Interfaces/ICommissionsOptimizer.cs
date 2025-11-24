using CommissionsOptimizerLib.Core.Models;

namespace CommissionsOptimizerLib.Core.Interfaces;

public interface ICommissionsOptimizer
{
    public OptimizationResults CalculateCommissions(List<PlayerTrekkerData> playerTrekkers, OptimizerOptions options, CancellationToken cancellationToken);
}
