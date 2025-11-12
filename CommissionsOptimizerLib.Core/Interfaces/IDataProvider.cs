using CommissionsOptimizerLib.Core.Models;

namespace CommissionsOptimizerLib.Core.Interfaces;

public interface IDataProvider
{
    public Task<List<Commission>> GetCommissionsDataAsync(CancellationToken token = default);
    public Task<List<TrekkerData>> GetTrekkersDataAsync(CancellationToken token = default);
}
