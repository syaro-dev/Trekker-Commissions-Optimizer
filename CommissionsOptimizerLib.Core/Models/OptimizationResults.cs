namespace CommissionsOptimizerLib.Core.Models;

public sealed record OptimizationResults(OptimizerOptions Options, IReadOnlyList<Suggestion> Top3Results)
{

}
