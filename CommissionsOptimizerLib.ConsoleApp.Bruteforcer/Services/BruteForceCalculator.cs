using CommissionsOptimizerLib.ConsoleApp.Bruteforcer.Helpers;
using CommissionsOptimizerLib.ConsoleApp.Bruteforcer.Models;
using CommissionsOptimizerLib.Core.Enums;
using CommissionsOptimizerLib.Core.Interfaces;
using CommissionsOptimizerLib.Core.Models;

namespace CommissionsOptimizerLib.ConsoleApp.Bruteforcer.Services;

internal class BruteForceCalculator(IDataProvider dataProvider) : ICommissionsOptimizer
{
    private long checksDone;
    public long ChecksDone => checksDone;

    public OptimizationResults CalculateCommissions(List<PlayerTrekkerData> playerTrekkers, OptimizerOptions options, CancellationToken cancellationToken = default)
    {
        Dictionary<PlayerTrekkerData, int> trekkerToIntMask = [];
        for (int i = 0; i < playerTrekkers.Count; i++)
        {
            trekkerToIntMask.Add(playerTrekkers[i], i);
        }

        // generate all possible combinations of player trekkers, 1-3 people.
        var allCombo = Utils.GenerateCombinations(playerTrekkers, 1, 3);

        var validTeams = new Dictionary<Commission, List<ValidTeam>>();

        foreach (var commission in dataProvider.GetCommissionsData())
        {
            validTeams.Add(commission, []);
            foreach (var combo in allCombo)
            {
                // from each combo, check which commissions they can participate in
                if (Utils.SatisfiesRequirements(combo.Select(x => x.Trekker.Role), commission.RequiredRoles))
                {
                    // also find out whether they can give bonus
                    bool hasBonus = Utils.SatisfiesRequirements(combo.Select(x => x.Trekker.Personality), commission.PersonalityBonus);
                    //Console.WriteLine($"Possible Team: {commission.Name} => {string.Join(" | ", combo.Select(x => x.Trekker.Name))} {(hasBonus ? "(BONUS POSSIBLE)" : "")}");

                    // now generate a trekker mask for each team, then store them in valid team
                    int mask = Utils.GenerateIntMask(combo.Select(x => trekkerToIntMask[x]));

                    // calculate the rewards if this team took the commission
                    var mainReward = commission.GetMainReward();
                    var minReward = mainReward?.MinReward ?? 0;
                    var maxReward = mainReward?.MaxReward ?? minReward;

                    if (hasBonus)
                    {
                        var bonusMainReward = commission.GetBonusMainReward();
                        minReward += bonusMainReward?.MinReward ?? 0;
                        maxReward += bonusMainReward?.MaxReward ?? bonusMainReward?.MinReward ?? 0;
                    }

                    RewardType rewardType = mainReward?.RewardType ?? RewardType.Unknown;
                    float vigorEfficiency = commission.GetTotalVigorEfficiency(hasBonus);

                    // finally create a valid team, then add it to the dictionary
                    var validTeam = new ValidTeam(commission, combo, mask, rewardType, vigorEfficiency);
                    validTeams[commission].Add(validTeam);
                }
            }
        }

        var unorderedSuggestions = new UnorderedSuggestions(options);

        // sort the "dictionary" to get the least valid teams for commissions first
        var sortedCommissions = validTeams
            .OrderBy(x => x.Value.Count)
            .Select(x => x.Key)
            .ToList();

        var commissionCombos = Utils.GenerateCombinations(sortedCommissions, 4, 4);
        ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxCompletionPortThreads);

        Console.WriteLine($"Wallahi you're finished, you need to check {Utils.Combination(sortedCommissions.Count, 4)} combinations");
        Console.WriteLine($"But don't worry! You've got {maxWorkerThreads} threads to help you with this.");

        var consoleLock = new object();

        var parallelOptions = new ParallelOptions()
        {
            CancellationToken = cancellationToken,
            //MaxDegreeOfParallelism = Math.Max(1, Environment.ProcessorCount / 2)
        };
#if RELEASE
        Console.WriteLine("Calculating... This will take roughly 20 minutes.");
        Console.WriteLine($"Time when calculation started: {DateTime.Now:HH:mm:ss}");
#endif
        try
        {
            Parallel.ForEach(commissionCombos, parallelOptions, combo =>
            {
                var c1Teams = validTeams[combo[0]];
                var c2Teams = validTeams[combo[1]];
                var c3Teams = validTeams[combo[2]];
                var c4Teams = validTeams[combo[3]];

                foreach (var t1 in c1Teams)
                {
                    int mask1 = t1.TrekkerMask;

                    foreach (var t2 in c2Teams)
                    {
                        if ((mask1 & t2.TrekkerMask) != 0) continue;
                        int mask12 = mask1 | t2.TrekkerMask;

                        foreach (var t3 in c3Teams)
                        {
                            if ((mask12 & t3.TrekkerMask) != 0) continue;
                            int mask123 = mask12 | t3.TrekkerMask;

                            foreach (var t4 in c4Teams)
                            {
                                if ((mask123 & t4.TrekkerMask) != 0) continue;

                                Interlocked.Increment(ref checksDone);
#if DEBUG
                                if (checksDone % 10000 == 0)
                                {
                                    lock (consoleLock)
                                    {
                                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                        Console.Write($"Valid setups found: {checksDone}");
                                    }
                                }
#endif
                                // get data regarding this team combo, then arrange them into the top 3s.

                                var suggestion = new PossibleSuggestion(t1, t2, t3, t4);
                                unorderedSuggestions.Suggest(suggestion);
                            }
                        }
                    }
                }
            });
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }

        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
        Console.WriteLine($"Valid setups found: {checksDone}");

        return new OptimizationResults(
            Options: options,
            Top3Results: [.. unorderedSuggestions.CompileResults()]
            );
    }
}
