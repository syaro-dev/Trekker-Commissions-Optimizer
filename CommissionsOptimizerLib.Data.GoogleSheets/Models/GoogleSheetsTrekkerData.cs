namespace CommissionsOptimizerLib.Data.GoogleSheets.Models;

internal record GoogleSheetsTrekkerData
{
    public required string ID { get; set; }
    public required string Name { get; set; }
    public required string Role { get; set; }
    public required string Personality { get; set; }
    public bool Hidden { get; set; }
}
