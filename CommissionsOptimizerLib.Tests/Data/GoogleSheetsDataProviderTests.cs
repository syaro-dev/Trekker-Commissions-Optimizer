using CommissionsOptimizerLib.Data.GoogleSheets.Services;

namespace CommissionsOptimizerLib.Tests.Data;

public class GoogleSheetsDataProviderTests
{
    private static GoogleSheetsDataProvider? provider;
    private const string commissionsDataUrl = "https://docs.google.com/spreadsheets/d/e/2PACX-1vQRBUi0tmdK0PmkVXIwcNowWxcz4oTijMc12YF5mdbeb9Sw2Hi8jfJURb2Pe9wjAvamK2I6eLS50yMb/pub?gid=573834810&single=true&output=csv";
    private const string trekkersDataUrl = "https://docs.google.com/spreadsheets/d/e/2PACX-1vQRBUi0tmdK0PmkVXIwcNowWxcz4oTijMc12YF5mdbeb9Sw2Hi8jfJURb2Pe9wjAvamK2I6eLS50yMb/pub?gid=369100612&single=true&output=csv";

    private static async Task<GoogleSheetsDataProvider> GetProvider()
    {
        if (provider == null)
        {
            provider = await GoogleSheetsDataProvider.CreateAsync(commissionsDataUrl, trekkersDataUrl);
            return provider;
        }
        else
        {
            return provider;
        }
    }

    [Fact]
    public async Task Should_Read_From_Google_Sheets()
    {
        try
        {
            // Arrange
            var provider = await GetProvider();

            // Act
            var commissions = provider.GetCommissionsData();
            var trekkers = provider.GetTrekkersData();

            // Test
            foreach (var commission in commissions)
            {
                Console.WriteLine($"{commission.Name} ---- Trekker Req Lv {commission.TrekkerLevelRequirement}");
                Console.WriteLine(string.Join(" | ", commission.RequiredRoles));
                Console.WriteLine(string.Join(" | ", commission.PersonalityBonus));
                var mainReward = commission.Rewards.FirstOrDefault(x => x.RewardType != Core.Enums.RewardType.Gifts);
                if (mainReward != null)
                {
                    string rewardAmt = $"{mainReward.MinReward}";
                    if (mainReward.MaxReward > 0)
                        rewardAmt += $"-{mainReward.MaxReward}";
                    Console.WriteLine($"Main reward: {rewardAmt} {mainReward.RewardType}");
                }
            }
            Console.WriteLine("----------------------------");
            foreach (var trekker in trekkers)
            {
                Console.WriteLine($"{trekker.Name} [{trekker.Role}] [{trekker.Personality}]");
            }
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    } 
}
