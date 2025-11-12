using CommissionsOptimizerLib.Core.Models;
using System.Text.Json;

namespace CommissionsOptimizerLib.Data.Json;

public readonly struct JsonDataSerializer
{
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public static async Task SaveCommissionsDataAsync(string filePath, List<Commission> commissions, CancellationToken token = default)
        => await SaveDataAsync(filePath, commissions, token);

    public static async Task SaveTrekkersDataAsync(string filePath, List<TrekkerData> trekkers, CancellationToken token = default)
        => await SaveDataAsync(filePath, trekkers, token);

    private static async Task SaveDataAsync<T>(string filePath, List<T> data, CancellationToken token = default)
    {
        using var stream = File.OpenWrite(filePath);
        await JsonSerializer.SerializeAsync(stream, data, _options, token);
    }
}
