using CommissionsOptimizerLib.Core.Enums;
using CommissionsOptimizerLib.Core.Models;

namespace CommissionsOptimizerLib.Core.Interfaces;

public interface ICommissionsOptimizer
{
    public OptimizationResults CalculateCommissions(List<PlayerTrekkerData> playerTrekkers, List<RewardType> selectedMaterials, int tyrantLevel);
}
