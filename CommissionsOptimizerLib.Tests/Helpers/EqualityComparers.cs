using CommissionsOptimizerLib.Core.Models;
using System.Diagnostics.CodeAnalysis;

namespace CommissionsOptimizerLib.Tests.Helpers;

internal readonly struct EqualityComparers
{
    public static CommissionsComparer Commissions => new();
    public static TrekkersComparer Trekkers => new();
    public static RewardsComparer Rewards => new();

    public sealed class CommissionsComparer : IEqualityComparer<Commission>
    {
        public bool Equals(Commission? x, Commission? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;

            return x.ID == y.ID
                && x.Name == y.Name
                && Utils.SequenceEqualSafe(x.RequiredRoles, y.RequiredRoles)
                && Utils.SequenceEqualSafe(x.PersonalityBonus, y.PersonalityBonus)
                && x.UnlocksAtTyrantLevel == y.UnlocksAtTyrantLevel
                && x.TrekkerLevelRequirement == y.TrekkerLevelRequirement
                && Utils.SequenceEqualSafe(x.Rewards, y.Rewards, EqualityComparers.Rewards)
                && Utils.SequenceEqualSafe(x.BonusRewards, y.BonusRewards, EqualityComparers.Rewards);
        }

        public int GetHashCode([DisallowNull] Commission obj)
        {
            return HashCode.Combine(obj.ID, obj.Name, Utils.GetListHashCode(obj.RequiredRoles), Utils.GetListHashCode(obj.PersonalityBonus), obj.UnlocksAtTyrantLevel, obj.TrekkerLevelRequirement, Utils.GetListHashCode(obj.Rewards), Utils.GetListHashCode(obj.BonusRewards));
        }
    }

    public sealed class TrekkersComparer : IEqualityComparer<TrekkerData>
    {
        public bool Equals(TrekkerData? x, TrekkerData? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;
            return x.ID == y.ID
                && x.Name == y.Name
                && x.Personality == y.Personality
                && x.Role == y.Role;
        }

        public int GetHashCode([DisallowNull] TrekkerData obj)
        {
            return HashCode.Combine(obj.ID, obj.Name, obj.Personality, obj.Role);
        }
    }

    public sealed class RewardsComparer : IEqualityComparer<Reward>
    {
        public bool Equals(Reward? x, Reward? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;
            return x.RewardType == y.RewardType
                && x.MinReward == y.MinReward
                && x.MaxReward == y.MaxReward;
        }

        public int GetHashCode([DisallowNull] Reward obj)
        {
            return HashCode.Combine(obj.RewardType, obj.MinReward, obj.MaxReward);
        }
    }
}
