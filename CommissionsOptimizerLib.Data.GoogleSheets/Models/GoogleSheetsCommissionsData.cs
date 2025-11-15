namespace CommissionsOptimizerLib.Data.GoogleSheets.Models;

internal record GoogleSheetsCommissionsData
{
    public required string ID { get; set; }
    public required string Name { get; set; }
    public string? Role1 { get; set; }
    public string? Role2 { get; set; }
    public string? Role3 { get; set; }
    public string? Personality1 { get; set; }
    public string? Personality2 { get; set; }
    public string? Personality3 { get; set; }
    public int TyrantLvlReq { get; set; }
    public int TrekkerLvlReq { get; set; }
    public required string MainReward { get; set; }
    public int MinAmt { get; set; }
    public int? MaxAmt { get; set; }
    public int MinBonusAmt { get; set; }
    public int? MaxBonusAmt { get; set; }
    public int? GiftsMinAmt { get; set; }
    public int? GiftsMaxAmt { get; set; }
    public bool Hidden { get; set; }
}
