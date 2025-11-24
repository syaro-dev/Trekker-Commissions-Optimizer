using CommissionsOptimizerLib.Core.Enums;

namespace CommissionsOptimizerLib.Core.Models;

public sealed class MaterialPrioritySetting
{
    public required RewardType Material { get; init; }
    public required int Priority { get; set; }
}
