namespace CommissionsOptimizerLib.Core.Models;

/// <summary>
/// Player's Trekker data. This will be used as player input.
/// </summary>
public sealed class PlayerTrekkerData
{
    public required TrekkerData Trekker { get; set; }
    public required int Level { get; set; }
    public required bool Exists { get; set; }
}
