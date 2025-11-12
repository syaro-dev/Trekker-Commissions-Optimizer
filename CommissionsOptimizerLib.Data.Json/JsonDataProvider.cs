using CommissionsOptimizerLib.Core.Interfaces;
using CommissionsOptimizerLib.Core.Models;
using System.Text.Json;

namespace CommissionsOptimizerLib.Data.Json;

public sealed class JsonDataProvider(string CommissionsDataFilePath, string TrekkersDataFilePath) : IDataProvider
{
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public async Task<List<Commission>> GetCommissionsDataAsync(CancellationToken token = default) =>
        await GetDataAsync<Commission>(CommissionsDataFilePath, _options, token);

    public async Task<List<TrekkerData>> GetTrekkersDataAsync(CancellationToken token = default) =>
        await GetDataAsync<TrekkerData>(TrekkersDataFilePath, _options, token);

    private static async Task<List<T>> GetDataAsync<T>(string filePath, JsonSerializerOptions options, CancellationToken token = default)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found.", filePath);

        using var stream = File.OpenRead(filePath);
        var result = await JsonSerializer.DeserializeAsync<List<T>>(stream, options, token);

        return result ?? [];
    }
}
