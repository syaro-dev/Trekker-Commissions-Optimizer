using CommissionsOptimizerLib.Core.Enums;

namespace CommissionsOptimizerLib.Core.Models;

public sealed class Reward
{
    public required RewardType RewardType { get; set; }
    public required int MinReward { get; set; }
    public int? MaxReward { get; set; }
}
