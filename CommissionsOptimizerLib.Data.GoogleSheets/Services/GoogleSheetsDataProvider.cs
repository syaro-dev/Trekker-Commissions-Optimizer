using CommissionsOptimizerLib.Core.Enums;
using CommissionsOptimizerLib.Core.Interfaces;
using CommissionsOptimizerLib.Core.Models;
using CommissionsOptimizerLib.Data.GoogleSheets.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace CommissionsOptimizerLib.Data.GoogleSheets.Services;

public class GoogleSheetsDataProvider : IDataProvider
{
    private readonly IReadOnlyList<Commission> commissions;
    private readonly IReadOnlyList<TrekkerData> trekkers;

    public IReadOnlyList<Commission> GetCommissionsData() => commissions;

    public IReadOnlyList<TrekkerData> GetTrekkersData() => trekkers;

    public static async Task<GoogleSheetsDataProvider> CreateAsync(string commissionsDataUrl, string trekkersDataUrl)
    {
        try
        {
            var getCommissionsTask = GetListOfDataAsync<GoogleSheetsCommissionsData>(commissionsDataUrl);
            var getTrekkersTask = GetListOfDataAsync<GoogleSheetsTrekkerData>(trekkersDataUrl);

            await Task.WhenAll(getCommissionsTask, getTrekkersTask);

            var gSheetsCommissionsData = getCommissionsTask.Result;
            var gSheetsTrekkersData = getTrekkersTask.Result;

            var commissions = gSheetsCommissionsData.Where(x => !x.Hidden).Select(ParseCommissionData).ToList();
            var trekkers = gSheetsTrekkersData.Where(x => !x.Hidden).Select(ParseTrekkerData).ToList();

            return new GoogleSheetsDataProvider(commissions, trekkers);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new GoogleSheetsDataProvider([], []);
        }
    }

    private GoogleSheetsDataProvider(IReadOnlyList<Commission> commissions, IReadOnlyList<TrekkerData> trekkers)
    {
        this.commissions = commissions;
        this.trekkers = trekkers;
    }

    private static async Task<List<T>> GetListOfDataAsync<T>(string url)
    {
        using HttpClient client = new();

        var csvData = await client.GetStringAsync(url);

        Console.WriteLine(csvData);

        using var reader = new StringReader(csvData);
        using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch =
                args => args.Header?.Trim().Replace(" ", "").Replace("_", "").ToLower() ?? "",
            HasHeaderRecord = true,
        });

        var records = csvReader.GetRecords<T>();
        return [.. records];
    }

    // TODO: Custom parser support
    private static Commission ParseCommissionData(GoogleSheetsCommissionsData gSheetsData)
    {
        List<Role> roles = [];
        if (Enum.TryParse(gSheetsData.Role1?.Trim(), true, out Role role1))
            roles.Add(role1);
        if (Enum.TryParse(gSheetsData.Role2?.Trim(), true, out Role role2))
            roles.Add(role2);
        if (Enum.TryParse(gSheetsData.Role3?.Trim(), true, out Role role3))
            roles.Add(role3);

        List<Personality> personalities = [];
        if (Enum.TryParse(gSheetsData.Personality1?.Trim(), true, out Personality personality1))
            personalities.Add(personality1);
        if (Enum.TryParse(gSheetsData.Personality2?.Trim(), true, out Personality personality2))
            personalities.Add(personality2);
        if (Enum.TryParse(gSheetsData.Personality3?.Trim(), true, out Personality personality3))
            personalities.Add(personality3);

        List<Reward> mainRewards = [];
        List<Reward> bonusRewards = [];

        // parse the main reward
        if (TryParseRewardType(gSheetsData.MainReward, out var rewardType))
        {
            Reward mainReward = new()
            {
                RewardType = rewardType,
                MinReward = gSheetsData.MinAmt,
                MaxReward = gSheetsData.MaxAmt ?? 0,
            };
            mainRewards.Add(mainReward);
            Reward bonusReward = new()
            {
                RewardType = rewardType,
                MinReward = gSheetsData.MinBonusAmt,
                MaxReward = gSheetsData.MaxBonusAmt ?? 0,
            };
            bonusRewards.Add(bonusReward);
        }

        // parse the gifts reward
        if (gSheetsData.GiftsMinAmt != null)
        {
            Reward giftsReward = new()
            {
                RewardType = RewardType.Gifts,
                MinReward = gSheetsData.GiftsMinAmt ?? 0,
                MaxReward = gSheetsData.GiftsMaxAmt ?? 0,
            };
            mainRewards.Add(giftsReward);
        }

        // parse the chess pieces reward
        return new Commission()
        {
            ID = gSheetsData.ID,
            Name = gSheetsData.Name,
            RequiredRoles = roles,
            PersonalityBonus = personalities,
            UnlocksAtTyrantLevel = gSheetsData.TyrantLvlReq,
            TrekkerLevelRequirement = gSheetsData.TrekkerLvlReq,
            Rewards = mainRewards,
            BonusRewards = bonusRewards,
        };
    }

    private static TrekkerData ParseTrekkerData(GoogleSheetsTrekkerData gSheetsData)
    {
        return new TrekkerData()
        {
            ID = gSheetsData.ID,
            Name = gSheetsData.Name,
            Role = Enum.Parse<Role>(gSheetsData.Role),
            Personality = Enum.Parse<Personality>(gSheetsData.Personality)
        };
    }

    private static bool TryParseRewardType(string value, out RewardType rewardType)
    {
        rewardType = value switch
        {
            "Money" => RewardType.Money,
            "Trekker XP" => RewardType.TrekkerXP,
            "Disc XP" => RewardType.DiscXP,
            "Trekker Mat - Grotesque" => RewardType.TrekkerMats_Grotesque,
            "Trekker Mat - Duloos" => RewardType.TrekkerMats_Duloos,
            "Trekker Mat - Lampflower" => RewardType.TrekkerMats_Lampflower,
            "Disc Mat - Grotesque" => RewardType.DiscMats_Grotesque,
            "Disc Mat - Duloos" => RewardType.DiscMats_Duloos,
            "Disc Mat - Lampflower" => RewardType.DiscMats_Lampflower,
            "Skill Mat - Rhythm" => RewardType.SkillMats_Rhythm,
            "Skill Mat - Shooter" => RewardType.SkillMats_Shooter,
            "Skill Mat - Kungfu" => RewardType.SkillMats_Kungfu,
            _ => throw new KeyNotFoundException()
        };

        return true;
    }
}
