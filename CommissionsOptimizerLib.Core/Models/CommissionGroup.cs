namespace CommissionsOptimizerLib.Core.Models;

public sealed record CommissionGroup
{
    public required Commission Commission { get; set; }
    public required List<TrekkerData> TrekkersToSend { get; set; }
}
