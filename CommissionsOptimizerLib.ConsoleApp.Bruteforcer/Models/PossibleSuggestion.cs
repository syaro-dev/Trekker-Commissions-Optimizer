using CommissionsOptimizerLib.ConsoleApp.Bruteforcer.Helpers;
using CommissionsOptimizerLib.Core.Models;

namespace CommissionsOptimizerLib.ConsoleApp.Bruteforcer.Models;

internal sealed record PossibleSuggestion
{
    private readonly IReadOnlyList<ValidTeam> validTeams;

    public int MemberCount { get; }

    public PossibleSuggestion(params ValidTeam[] ValidTeams)
    {
        validTeams = ValidTeams;
        MemberCount = ValidTeams.Sum(x => x.TeamComp.Count);
    }

    /// <summary>
    /// Get teams, sorted by vigor efficiency (descending)
    /// </summary>
    public IEnumerable<ValidTeam> GetTeamsSortedByEfficiency()
        => validTeams.OrderByDescending(x => x.VigorEfficiency);

    public Suggestion ToSuggestion()
        => new([.. validTeams.Select(x => x.ToCommissionGroup())]);
}
