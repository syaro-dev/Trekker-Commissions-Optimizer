using CommissionsOptimizerLib.Core.Enums;

namespace CommissionsOptimizerLib.Core.Models;

/// <summary>
/// Base Trekker data. Will get updated as new Trekkers get implemented.<br/>
/// For player trekker data use <see cref="PlayerTrekkerData"/> instead.
/// </summary>
public sealed class TrekkerData
{
    /// <summary>
    /// Custom identifier of Trekker
    /// </summary>
    public required string ID { get; set; }
    public required string Name { get; set; }
    public required Role Role { get; set; }
    public required Personality Personality { get; set; }
}
