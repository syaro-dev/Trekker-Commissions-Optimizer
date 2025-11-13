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

    public static async Task<List<Commission>> LoadCommissionsDataAsync(string filePath, CancellationToken token = default)
        => await LoadDataAsync<Commission>(filePath, token);

    public static async Task<List<TrekkerData>> LoadTrekkersDataAsync(string filePath, CancellationToken token = default)
        => await LoadDataAsync<TrekkerData>(filePath, token);

    private static async Task SaveDataAsync<T>(string filePath, List<T> data, CancellationToken token = default)
    {
        try
        {
            using var stream = File.OpenWrite(filePath);
            await JsonSerializer.SerializeAsync(stream, data, _options, token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private static async Task<List<T>> LoadDataAsync<T>(string filePath, CancellationToken token = default)
    {
        try
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}", filePath);

            using var stream = File.OpenRead(filePath);
            var result = await JsonSerializer.DeserializeAsync<List<T>>(stream, _options, token);

            return result ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return [];
        }
    }
}
