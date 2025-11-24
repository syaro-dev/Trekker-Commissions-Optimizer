namespace CommissionsOptimizerLib.Core.Models;

public sealed record CommissionGroup(Commission Commission, IReadOnlyList<TrekkerData> TrekkersToSend, float AverageRewards);
