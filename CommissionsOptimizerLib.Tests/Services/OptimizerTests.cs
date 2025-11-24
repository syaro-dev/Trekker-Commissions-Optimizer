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

        var mockOptions = new Mock<OptimizerOptions>();

        // Act
        var results = optimizer.CalculateCommissions(playerTrekkerData, mockOptions.Object);

        // Test
        throw new NotImplementedException("Test not complete yet");
        //Assert.Equal(expected, results, EqualityComparers.OptimizationResults);
    }
}
