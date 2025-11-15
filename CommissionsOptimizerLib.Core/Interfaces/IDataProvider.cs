using CommissionsOptimizerLib.Core.Models;

namespace CommissionsOptimizerLib.Core.Interfaces;

public interface IDataProvider
{
    public IReadOnlyList<Commission> GetCommissionsData();
    public IReadOnlyList<TrekkerData> GetTrekkersData();

    public Commission? GetCommissionDataFromID(string id);
    public Commission? GetCommissionDataFromName(string name);

    public TrekkerData? GetTrekkerDataFromID(string id);
    public TrekkerData? GetTrekkerDataFromName(string name);
}
