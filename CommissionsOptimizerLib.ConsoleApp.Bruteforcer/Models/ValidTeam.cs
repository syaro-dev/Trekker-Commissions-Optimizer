using CommissionsOptimizerLib.Core.Enums;
using CommissionsOptimizerLib.Core.Models;

namespace CommissionsOptimizerLib.ConsoleApp.Bruteforcer.Models;

internal sealed record ValidTeam(Commission Commission, IReadOnlyList<PlayerTrekkerData> TeamComp, int TrekkerMask, RewardType RewardType, float VigorEfficiency)
{
    public CommissionGroup ToCommissionGroup()
    {
        return new CommissionGroup(
            Commission: Commission,
            TrekkersToSend: [.. TeamComp.Select(x => x.Trekker)],
            AverageRewards: VigorEfficiency
            );
    }
}
