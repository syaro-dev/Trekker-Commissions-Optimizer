using CommissionsOptimizerLib.Data.Json;
using CommissionsOptimizerLib.Tests.Helpers;
using System.Text.Json;

namespace CommissionsOptimizerLib.Tests.Data;

public class JsonDataSerializerTests
{
    private readonly static JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

    [Fact]
    public async Task Should_Write_Commissions_Data()
    {
        // Arrange
        var temp_commissionsData = DummyData.Commissions;
        string temp_filePath = DummyData.TEST_COMMISSIONS_FILE_PATH;
        string testToJson = JsonSerializer.Serialize(temp_commissionsData, options);

        // Act
        await JsonDataSerializer.SaveCommissionsDataAsync(temp_filePath, temp_commissionsData);

        // Test
        Assert.True(File.Exists(temp_filePath));
        string fileContent = File.ReadAllText(temp_filePath);
        Assert.Equal(testToJson, fileContent);
    }

    [Fact]
    public async Task Should_Write_Trekkers_Data()
    {
        // Arrange
        var temp_trekkersData = DummyData.Trekkers;
        string temp_filePath = DummyData.TEST_TREKKERS_FILE_PATH;
        string testToJson = JsonSerializer.Serialize(temp_trekkersData, options);

        // Act
        await JsonDataSerializer.SaveTrekkersDataAsync(temp_filePath, temp_trekkersData);

        // Test
        Assert.True(File.Exists(temp_filePath));
        string fileContent = File.ReadAllText(temp_filePath);
        Assert.Equal(testToJson, fileContent);
    }

    [Fact]
    public async Task Should_Read_Json_Data()
    {
        // Arrange
        var dataProvider = await JsonDataProvider.CreateAsync(DummyData.TEST_COMMISSIONS_FILE_PATH, DummyData.TEST_TREKKERS_FILE_PATH, options);
        var commissions = DummyData.Commissions;
        var trekkers = DummyData.Trekkers;

        // Act
        var commissionsResult = dataProvider.GetCommissionsData();
        var trekkersResult = dataProvider.GetTrekkersData();

        // Test
        Assert.Equal(commissions, commissionsResult, EqualityComparers.Commissions);
        Assert.Equal(trekkers, trekkersResult, EqualityComparers.Trekkers);
    }
}