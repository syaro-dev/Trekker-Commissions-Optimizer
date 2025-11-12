namespace CommissionsOptimizerLib.Tests.Helpers;

internal readonly struct Utils
{
    public static bool SequenceEqualSafe<T>(IEnumerable<T>? a, IEnumerable<T>? b)
    {
        if (a == null && b == null) return true;
        if (a == null || b == null) return false;
        return a.SequenceEqual(b);
    }

    public static bool SequenceEqualSafe<T>(IEnumerable<T>? a, IEnumerable<T>? b, IEqualityComparer<T> comparer)
    {
        if (a == null && b == null) return true;
        if (a == null || b == null) return false;
        return a.SequenceEqual(b, comparer);
    }

    public static int GetListHashCode<T>(IEnumerable<T>? list)
    {
        if (list == null) return 0;

        unchecked
        {
            int hash = 19;
            foreach (var item in list)
            {
                hash = hash * 31 + (item?.GetHashCode() ?? 0);
            }
            return hash;
        }
    }
}
