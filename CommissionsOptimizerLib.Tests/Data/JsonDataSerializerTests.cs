using CommissionsOptimizerLib.Data.Json;
using CommissionsOptimizerLib.Tests.Helpers;
using System.Text.Json;

namespace CommissionsOptimizerLib.Tests.Data;

public class JsonDataSerializerTests
{
    private readonly static JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
    private readonly static JsonDataProvider dataProvider = new(DummyData.TEST_COMMISSIONS_FILE_PATH, DummyData.TEST_TREKKERS_FILE_PATH);

    [Fact]
    public async Task Should_Write_Commissions_Data()
    {
        // Arrange
        var temp_commissionsData = DummyData.Commissions;
        string temp_filePath = DummyData.TEST_COMMISSIONS_FILE_PATH;

        // Act
        await JsonDataSerializer.SaveCommissionsDataAsync(temp_filePath, temp_commissionsData);

        // Test
        Assert.True(File.Exists(temp_filePath));
        string fileContent = File.ReadAllText(temp_filePath);
        string testToJson = JsonSerializer.Serialize(temp_commissionsData, options);
        Assert.Equal(testToJson, fileContent);
    }

    [Fact]
    public async Task Should_Write_Trekkers_Data()
    {
        // Arrange
        var temp_trekkersData = DummyData.Trekkers;
        string temp_filePath = DummyData.TEST_TREKKERS_FILE_PATH;

        // Act
        await JsonDataSerializer.SaveTrekkersDataAsync(temp_filePath, temp_trekkersData);

        // Test
        Assert.True(File.Exists(temp_filePath));
        string fileContent = File.ReadAllText(temp_filePath);
        string testToJson = JsonSerializer.Serialize(temp_trekkersData, options);
        Assert.Equal(testToJson, fileContent);
    }

    [Fact]
    public async Task Should_Read_Commissions_Data()
    {
        // Arrange

        // Act
        var commissions = await dataProvider.GetCommissionsDataAsync();

        // Test
        Assert.Equal(DummyData.Commissions, commissions, EqualityComparers.Commissions);
    }

    [Fact]
    public async Task Should_Read_Trekkers_Data()
    {
        // Arrange

        // Act
        var trekkers = await dataProvider.GetTrekkersDataAsync();

        // Test
        Assert.Equal(DummyData.Trekkers, trekkers, EqualityComparers.Trekkers);
    }
}