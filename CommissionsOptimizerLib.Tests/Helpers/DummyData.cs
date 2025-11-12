using CommissionsOptimizerLib.Core.Enums;
using CommissionsOptimizerLib.Core.Models;

namespace CommissionsOptimizerLib.Tests.Helpers;

internal readonly struct DummyData
{
    public static List<Commission> Commissions =
    [
        new Commission
        {
            ID = "1",
            Name = "Trekker Material A: III",
            RequiredRoles =
            [
                Role.Vanguard,
                Role.Support,
                Role.Vanguard
            ],
            PersonalityBonus =
            [
                Personality.Steady,
                Personality.Inquisitive
            ],
            UnlocksAtTyrantLevel = 1,
            TrekkerLevelRequirement = 1,
            Rewards =
            [
                new Reward
                {
                    RewardType = RewardType.TrekkerMats_Grotesque,
                    MinReward = 85,
                },
                new Reward
                {
                    RewardType = RewardType.Gifts,
                    MinReward = 8,
                    MaxReward = 16
                }
            ],
            BonusRewards =
            [
                new Reward
                {
                    RewardType = RewardType.TrekkerMats_Grotesque,
                    MinReward = 17,
                },
            ]
        },
        new Commission
        {
            ID = "2",
            Name = "Trekker Material B: III",
            RequiredRoles =
            [
                Role.Vanguard,
                Role.Support,
                Role.Vanguard
            ],
            PersonalityBonus =
            [
                Personality.Collector,
                Personality.Creative
            ],
            UnlocksAtTyrantLevel = 1,
            TrekkerLevelRequirement = 1,
            Rewards =
            [
                new Reward
                {
                    RewardType = RewardType.TrekkerMats_Duloos,
                    MinReward = 85,
                },
                new Reward
                {
                    RewardType = RewardType.Gifts,
                    MinReward = 8,
                    MaxReward = 16
                }
            ],
            BonusRewards =
            [
                new Reward
                {
                    RewardType = RewardType.TrekkerMats_Duloos,
                    MinReward = 17,
                },
            ]
        },
        new Commission
        {
            ID = "3",
            Name = "Trekker Material C: III",
            RequiredRoles =
            [
                Role.Versatile,
                Role.Support,
                Role.Versatile
            ],
            PersonalityBonus =
            [
                Personality.Steady,
                Personality.Creative
            ],
            UnlocksAtTyrantLevel = 1,
            TrekkerLevelRequirement = 1,
            Rewards =
            [
                new Reward
                {
                    RewardType = RewardType.TrekkerMats_Lampflower,
                    MinReward = 85,
                },
                new Reward
                {
                    RewardType = RewardType.Gifts,
                    MinReward = 8,
                    MaxReward = 16
                }
            ],
            BonusRewards =
            [
                new Reward
                {
                    RewardType = RewardType.TrekkerMats_Lampflower,
                    MinReward = 17,
                },
            ]
        },
        new Commission
        {
            ID = "4",
            Name = "Disc Material A: III",
            RequiredRoles =
            [
                Role.Versatile,
                Role.Support,
                Role.Versatile
            ],
            PersonalityBonus =
            [
                Personality.Steady,
                Personality.Creative
            ],
            UnlocksAtTyrantLevel = 1,
            TrekkerLevelRequirement = 1,
            Rewards =
            [
                new Reward
                {
                    RewardType = RewardType.DiscMats_Grotesque,
                    MinReward = 85,
                },
                new Reward
                {
                    RewardType = RewardType.Gifts,
                    MinReward = 8,
                    MaxReward = 16
                }
            ],
            BonusRewards =
            [
                new Reward
                {
                    RewardType = RewardType.DiscMats_Grotesque,
                    MinReward = 17,
                },
            ]
        },
        new Commission
        {
            ID = "5",
            Name = "Disc Material B: III",
            RequiredRoles =
            [
                Role.Versatile,
                Role.Support,
                Role.Versatile
            ],
            PersonalityBonus =
            [
                Personality.Steady,
                Personality.Creative
            ],
            UnlocksAtTyrantLevel = 1,
            TrekkerLevelRequirement = 1,
            Rewards =
            [
                new Reward
                {
                    RewardType = RewardType.DiscMats_Duloos,
                    MinReward = 85,
                },
                new Reward
                {
                    RewardType = RewardType.Gifts,
                    MinReward = 8,
                    MaxReward = 16
                }
            ],
            BonusRewards =
            [
                new Reward
                {
                    RewardType = RewardType.DiscMats_Duloos,
                    MinReward = 17,
                },
            ]
        },
        new Commission
        {
            ID = "6",
            Name = "Disc Material C: III",
            RequiredRoles =
            [
                Role.Versatile,
                Role.Support,
                Role.Versatile
            ],
            PersonalityBonus =
            [
                Personality.Steady,
                Personality.Creative
            ],
            UnlocksAtTyrantLevel = 1,
            TrekkerLevelRequirement = 1,
            Rewards =
            [
                new Reward
                {
                    RewardType = RewardType.DiscMats_Lampflower,
                    MinReward = 85,
                },
                new Reward
                {
                    RewardType = RewardType.Gifts,
                    MinReward = 8,
                    MaxReward = 16
                }
            ],
            BonusRewards =
            [
                new Reward
                {
                    RewardType = RewardType.DiscMats_Lampflower,
                    MinReward = 17,
                },
            ]
        },
    ];
    public const string TEST_COMMISSIONS_FILE_PATH = "test_commissions_data.json";

    public static List<TrekkerData> Trekkers =
    [
        new TrekkerData
        {
            ID = "1",
            Name = "Donna :)",
            Role = Role.Vanguard,
            Personality = Personality.Adventurous,
        },
        new TrekkerData
        {
            ID = "2",
            Name = "Noya",
            Role = Role.Vanguard,
            Personality = Personality.Creative,
        },
        new TrekkerData
        {
            ID = "3",
            Name = "Iris",
            Role = Role.Versatile,
            Personality = Personality.Creative,
        },
        new TrekkerData
        {
            ID = "4",
            Name = "Amber",
            Role = Role.Vanguard,
            Personality = Personality.Collector,
        },
        new TrekkerData
        {
            ID = "5",
            Name = "Chitose",
            Role = Role.Vanguard,
            Personality = Personality.Inquisitive,
        },
        new TrekkerData
        {
            ID = "6",
            Name = "Shia",
            Role = Role.Vanguard,
            Personality = Personality.Adventurous,
        },
        new TrekkerData
        {
            ID = "7",
            Name = "Ann",
            Role = Role.Support,
            Personality = Personality.Adventurous,
        },
        new TrekkerData
        {
            ID = "8",
            Name = "Cosette",
            Role = Role.Support,
            Personality = Personality.Inquisitive,
        },
        new TrekkerData
        {
            ID = "9",
            Name = "Teresa",
            Role = Role.Support,
            Personality = Personality.Steady,
        },
        new TrekkerData
        {
            ID = "10",
            Name = "Minova",
            Role = Role.Versatile,
            Personality = Personality.Steady,
        },
        new TrekkerData
        {
            ID = "11",
            Name = "Jinglin",
            Role = Role.Versatile,
            Personality = Personality.Inquisitive,
        },
        new TrekkerData
        {
            ID = "12",
            Name = "Chixia",
            Role = Role.Versatile,
            Personality = Personality.Collector,
        },
    ];
    public const string TEST_TREKKERS_FILE_PATH = "test_trekkers_data.json";
}
