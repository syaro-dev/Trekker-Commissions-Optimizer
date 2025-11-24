using CommissionsOptimizerLib.Core.Enums;
using CommissionsOptimizerLib.Core.Models;

namespace CommissionsOptimizerLib.ConsoleApp.Bruteforcer.Models;

internal sealed class UnorderedSuggestions
{
    // Ordered from high priority to low priority
    private readonly Dictionary<RewardType, long> multipliers;
    private readonly bool hyperFocus;
    private readonly PriorityQueue<PossibleSuggestion, float> pq = new();
    private readonly object _lock = new();

    public UnorderedSuggestions(OptimizerOptions options)
    {
        var materialsSelect = options.MaterialsSelect;
        multipliers = materialsSelect.Select(x =>
            new
            {
                x.Material,
                Mult = (long)Math.Pow(100, x.Priority)
            })
            .ToDictionary(x => x.Material, x => x.Mult);

        hyperFocus = options.HyperFocus;
    }

    /// <summary>
    /// Adds a suggestion.
    /// </summary>
    public void Suggest(PossibleSuggestion suggestion)
    {
        float priority = 0;
        var usedPriority = new HashSet<RewardType>();
        foreach (var team in suggestion.GetTeamsSortedByEfficiency())
        {
            long multiplier;

            if (multipliers.TryGetValue(team.RewardType, out var m) && !usedPriority.Contains(team.RewardType))
            {
                multiplier = m;

                if (!hyperFocus)
                    // Mark as used — next time will be default (1)
                    usedPriority.Add(team.RewardType);
            }
            else
            {
                // Neutral priority
                multiplier = 1;            
            }

            priority += team.VigorEfficiency * multiplier;
        }

        // keep critical section minimal
        lock (_lock)
        {
            if (pq.Count < 3)
            {
                pq.Enqueue(suggestion, priority);
                return;
            }

            // peek returns smallest-priority item (min-heap)
            pq.TryPeek(out var existing, out var minPriority);

            if (priority < minPriority)
            {
                // Worse; reject
                return;
            }

            if (priority == minPriority)
            {
                // Tie-breaker: lower MemberCount wins
                if (suggestion.MemberCount >= existing!.MemberCount)
                {
                    // New one loses the tie
                    return;
                }
            }

            // replace min item with new better item
            pq.Dequeue();
            pq.Enqueue(suggestion, priority);
        }
    }

    public IEnumerable<Suggestion> CompileResults()
        => pq.UnorderedItems.Select(x => (x.Element, x.Priority))
                            .OrderByDescending(x => x.Priority)
                            .Select(x => x.Element.ToSuggestion());
}
