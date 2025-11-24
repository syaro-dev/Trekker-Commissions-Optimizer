using CommissionsOptimizerLib.Core.Enums;

namespace CommissionsOptimizerLib.ConsoleApp.Bruteforcer.Helpers;

internal readonly struct Utils
{
    public static IEnumerable<List<T>> GenerateCombinations<T>(IList<T> items, int minDepth, int maxDepth)
    {
        // Local iterator method
        IEnumerable<List<T>> Build(int index, List<T> current, int depth)
        {
            // Yield a copy of the current combination
            if (depth >= minDepth)
                yield return new List<T>(current);

            if (depth == maxDepth)
                yield break;

            for (int next = index + 1; next < items.Count; next++)
            {
                current.Add(items[next]);

                foreach (var combo in Build(next, current, depth + 1))
                    yield return combo;

                current.RemoveAt(current.Count - 1); // backtrack
            }
        }

        for (int i = 0; i < items.Count; i++)
        {
            var start = new List<T> { items[i] };

            foreach (var combo in Build(i, start, 1))
                yield return combo;
        }
    }

    // generate permutation
    public static IEnumerable<List<T>> GeneratePermutations<T>(IList<T> items, int minDepth, int maxDepth)
    {
        if (minDepth < 0 || maxDepth < minDepth)
            throw new ArgumentException("Invalid minDepth/maxDepth.");

        var used = new bool[items.Count];
        var current = new List<T>();

        IEnumerable<List<T>> Backtrack()
        {
            // If we reached within depth range, yield a copy
            if (current.Count >= minDepth && current.Count <= maxDepth)
                yield return new List<T>(current);

            // Stop if max depth reached
            if (current.Count == maxDepth)
                yield break;

            for (int i = 0; i < items.Count; i++)
            {
                if (used[i]) continue;

                used[i] = true;
                current.Add(items[i]);

                foreach (var p in Backtrack())
                    yield return p;

                current.RemoveAt(current.Count - 1);
                used[i] = false;
            }
        }

        foreach (var perm in Backtrack())
            yield return perm;
    }

    public static bool SatisfiesRequirements<T>(IEnumerable<T> has, IEnumerable<T> requires)
    {
        var copyOfHas = new List<T>(has);

        foreach (var required in requires)
        {
            if (!copyOfHas.Contains(required))
                return false;

            copyOfHas.Remove(required);
        }

        return true;
    }

    public static int GenerateIntMask(params int[] bitPositions)
    {
        int mask = 0;
        foreach (int pos in bitPositions)
        {
            mask |= 1 << pos; // set the bit at position 'pos'
        }
        return mask;
    }

    public static int GenerateIntMask(IEnumerable<int> bitPositions)
        => GenerateIntMask([.. bitPositions]);

    // Function to calculate combinations C(n, k)
    public static long Combination(int n, int k)
    {
        if (k > n) return 0;
        if (k == 0 || k == n) return 1;

        // Take advantage of symmetry: C(n, k) == C(n, n-k)
        if (k > n - k)
            k = n - k;

        long result = 1;
        for (int i = 1; i <= k; i++)
        {
            result *= n - (i - 1); // Multiply descending numbers
            result /= i;           // Divide by i (factorial part)
        }

        return result;
    }
}
