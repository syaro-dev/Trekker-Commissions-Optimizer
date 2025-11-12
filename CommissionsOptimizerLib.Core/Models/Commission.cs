using CommissionsOptimizerLib.Core.Enums;

namespace CommissionsOptimizerLib.Core.Models;

public sealed class Commission
{
    /// <summary>
    /// Custom identifier of Commission
    /// </summary>
    public required string ID { get; set; }
    public required string Name { get; set; }
    /// <summary>
    /// Assigned Trekkers must fulfill all the required roles before the commission
    /// can start.
    /// </summary>
    public required List<Role> RequiredRoles { get; set; }
    /// <summary>
    /// When Trekkers assigned to this Commission fulfill these personalities,
    /// BonusRewards will be obtained. Fulfilling this is optional.
    /// </summary>
    public required List<Personality> PersonalityBonus { get; set; }
    /// <summary>
    /// What level should the Tyrant (your player acc) be to unlock this commission?
    /// </summary>
    public required int UnlocksAtTyrantLevel { get; set; }
    /// <summary>
    /// Level Requirement for all Trekkers assigned to this commission.
    /// </summary>
    public required int TrekkerLevelRequirement { get; set; }
    /// <summary>
    /// The rewards you get after completing the commission.
    /// </summary>
    public required List<Reward> Rewards { get; set; }
    /// <summary>
    /// Bonus rewards you get if you fulfill the personality bonus.
    /// </summary>
    public required List<Reward> BonusRewards { get; set; }
}
