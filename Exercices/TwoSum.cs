using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LeetCode.Exercices;

public static class TwoSum
{
    private static readonly Stopwatch sw = new();

    private static readonly (int[], int, int[])[] Scenarios = new[]
    {
        (new[] {3, 2, 4}, 6, new[] {1, 2}),
        (new[] {2, 7, 11, 15}, 9, new[] {0, 1}),
        (new[] {3, 3}, 6, new[] {0, 1}),
        (new[] {1, 2, 3, 4, 5}, 9, new[] {3, 4}),
        (new[] {10, 20, 30, 40}, 50, new[] {0, 3}),
        (new[] {1, 2, 3, 4, 5}, 3, new[] {0, 1}),
        (new[] {2, 4, 6, 8}, 10, new[] {0, 3}),
        (new[] {-1, 0, 1}, 0, new[] {0, 2}),
        (new[] {-3, -2, 1, 4, 5}, -5, new[] {0, 1}),
    };

    public static void Run()
    {
        Console.WriteLine("FIRST SUBMISSION");
        foreach (var scenario in Scenarios)
        {
            var (nums, target, expected) = scenario;
            sw.Start();
            var result = First(nums, target);
            sw.Stop();
            if (result != null && result[0] == expected[0] && result[1] == expected[1])
            {
                Console.WriteLine("[SUCCESS] ({3}) [{0}] -> {1} = [{2}]", string.Join(", ", nums), target, string.Join(", ", result), sw.Elapsed);
                sw.Reset();
                continue;
            }

            result ??= Array.Empty<int>();
            sw.Reset();
            Console.WriteLine("[FAILED] EXPECTED: [{2}] -> {3} = [{0}] | RECEIVED:[{2}] -> {3} = [{1}]", string.Join(", ", expected), string.Join(", ", result), string.Join(", ", nums), target);
        }
        Console.WriteLine("SECOND SUBMISSION");
        foreach (var scenario in Scenarios)
        {
            var (nums, target, expected) = scenario;
            sw.Start();
            var result = Second(nums, target);
            sw.Stop();
            if (result != null && result[0] == expected[0] && result[1] == expected[1])
            {
                Console.WriteLine("[SUCCESS] ({3}) [{0}] -> {1} = [{2}]", string.Join(", ", nums), target, string.Join(", ", result), sw.Elapsed);
                sw.Reset();
                continue;
            }
            result ??= Array.Empty<int>();
            sw.Reset();
            Console.WriteLine("[FAILED] EXPECTED: [{2}] -> {3} = [{0}] | RECEIVED:[{2}] -> {3} = [{1}]", string.Join(", ", expected), string.Join(", ", result), string.Join(", ", nums), target);
        }

    }

    private static int[]? First(int[] nums, int target)
    {
        int[]? result = null;
        for (var i = 0; i < nums.Length; i++)
        {
            var j =  i + 1;
            if (j == nums.Length) j = 0;
            while (j != i)
            {
                if (nums[i] + nums[j] == target)
                {
                    result = new[] {i, j};
                    break;
                }
                j++;
                if (j == nums.Length) j = 0;
            }
            if (result != null) break;
        }
        return result;
    }

    private static int[]? Second(int[] nums, int target)
    {
        var indexes = new Dictionary<int,int>();
        for (var i = 0; i < nums.Length; i++)
        {
            var compliment = target - nums[i];
            if(indexes.TryGetValue(compliment, out var value))
            {
                return new int[] {value, i};
            }
            indexes[nums[i]] = i;
        }
        return default  ;
    }
}