using CommissionsOptimizerLib.Core.Enums;
using CommissionsOptimizerLib.Core.Interfaces;
using CommissionsOptimizerLib.Core.Models;
using CommissionsOptimizerLib.Core.Services;
using CommissionsOptimizerLib.Tests.Helpers;
using Moq;

namespace CommissionsOptimizerLib.Tests.Services;

public class OptimizerTests
{
    [Fact]
    public void First_Easy_Test()
    {
        // Arrange
        var playerTrekkerData = DummyData.PlayerTrekkers;
        var selectedMaterials = new List<RewardType>()
        {
            RewardType.TrekkerMats_Grotesque,
            RewardType.TrekkerMats_Duloos,
            RewardType.DiscMats_Lampflower,
            RewardType.DiscMats_Grotesque,
        };

        var mockDataService = new Mock<IDataProvider>();
        mockDataService.Setup(ds => ds.GetTrekkersData()).Returns(() => DummyData.Trekkers);
        mockDataService.Setup(ds => ds.GetCommissionsData()).Returns(() => DummyData.Commissions);
        var optimizer = new CommissionsOptimizerService(mockDataService.Object);

        var expected = new OptimizationResults()
        {
            Results =
            [
                new CommissionGroup()
                {
                    Commission = DummyData.Commissions[3],
                    TrekkersToSend =
                    [
                        DummyData.Trekkers[0],
                        DummyData.Trekkers[1],
                    ]
                },
                new CommissionGroup()
                {
                    Commission = DummyData.Commissions[5],
                    TrekkersToSend =
                    [
                        DummyData.Trekkers[4],
                        DummyData.Trekkers[8],
                        DummyData.Trekkers[5],
                    ]
                },
                new CommissionGroup()
                {
                    Commission = DummyData.Commissions[2],
                    TrekkersToSend =
                    [
                        DummyData.Trekkers[2],
                        DummyData.Trekkers[10],
                        DummyData.Trekkers[11],
                    ]
                },
                new CommissionGroup()
                {
                    Commission = DummyData.Commissions[1],
                    TrekkersToSend =
                    [
                        DummyData.Trekkers[7],
                        DummyData.Trekkers[9],
                    ]
                },
            ]
        };

        // Act
        var results = optimizer.CalculateCommissions(playerTrekkerData, selectedMaterials, 40);

        // Test
        Assert.Equal(expected, results, EqualityComparers.OptimizationResults);
    }
}
