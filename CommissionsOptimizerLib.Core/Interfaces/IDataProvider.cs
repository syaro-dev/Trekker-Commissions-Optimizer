using CommissionsOptimizerLib.Core.Models;

namespace CommissionsOptimizerLib.Core.Interfaces;

public interface IDataProvider
{
    public IReadOnlyList<Commission> GetCommissionsData();
    public IReadOnlyList<TrekkerData> GetTrekkersData();
}
