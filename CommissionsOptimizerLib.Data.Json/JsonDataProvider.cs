using CommissionsOptimizerLib.Core.Interfaces;
using CommissionsOptimizerLib.Core.Models;
using System.Text.Json;

namespace CommissionsOptimizerLib.Data.Json;

public sealed class JsonDataProvider : IDataProvider
{
    private readonly IReadOnlyList<Commission> commissions;
    private readonly IReadOnlyList<TrekkerData> trekkers;

    public IReadOnlyList<Commission> GetCommissionsData() => commissions;
    public IReadOnlyList<TrekkerData> GetTrekkersData() => trekkers;

    public static async Task<JsonDataProvider> CreateAsync(string commissionsDataFilePath, string trekkersDataFilePath, JsonSerializerOptions options)
    {
        var commissions = await GetListOfDataAsync<Commission>(commissionsDataFilePath, options);
        var trekkers = await GetListOfDataAsync<TrekkerData>(trekkersDataFilePath, options);

        return new JsonDataProvider(commissions, trekkers);
    }

    private JsonDataProvider(IReadOnlyList<Commission> commissions, IReadOnlyList<TrekkerData> trekkers)
    {
        this.commissions = commissions;
        this.trekkers = trekkers;
    }

    private static async Task<List<T>> GetListOfDataAsync<T>(string filePath, JsonSerializerOptions options, CancellationToken token = default)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found.", filePath);

        using var stream = File.OpenRead(filePath);
        var result = await JsonSerializer.DeserializeAsync<List<T>>(stream, options, token);

        return result ?? [];
    }
}
