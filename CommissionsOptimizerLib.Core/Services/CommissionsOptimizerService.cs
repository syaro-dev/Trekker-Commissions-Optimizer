using CommissionsOptimizerLib.Core.Enums;
using CommissionsOptimizerLib.Core.Interfaces;
using CommissionsOptimizerLib.Core.Models;

namespace CommissionsOptimizerLib.Core.Services;

public class CommissionsOptimizerService(IDataProvider DataProvider) : ICommissionsOptimizer
{
    public OptimizationResults CalculateCommissions(List<PlayerTrekkerData> playerTrekkers, List<RewardType> selectedMaterials, int tyrantLevel)
    {
        var commissions = DataProvider.GetCommissionsData();
        var trekkers = DataProvider.GetTrekkersData();

        return new OptimizationResults()
        {
            Results =
            [
                new CommissionGroup()
                {
                    Commission = commissions[3],
                    TrekkersToSend =
                    [
                        trekkers[0],
                        trekkers[1],
                    ]
                },
                new CommissionGroup()
                {
                    Commission = commissions[5],
                    TrekkersToSend =
                    [
                        trekkers[4],
                        trekkers[8],
                        trekkers[5],
                    ]
                },
                new CommissionGroup()
                {
                    Commission = commissions[2],
                    TrekkersToSend =
                    [
                        trekkers[2],
                        trekkers[10],
                        trekkers[11],
                    ]
                },
                new CommissionGroup()
                {
                    Commission = commissions[1],
                    TrekkersToSend =
                    [
                        trekkers[7],
                        trekkers[9],
                    ]
                },
            ]
        };
    }
}
