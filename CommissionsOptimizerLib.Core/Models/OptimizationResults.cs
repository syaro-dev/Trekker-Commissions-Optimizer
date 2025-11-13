namespace CommissionsOptimizerLib.Core.Models;

public sealed record OptimizationResults
{
    public required List<CommissionGroup> Results { get; set; }
}
