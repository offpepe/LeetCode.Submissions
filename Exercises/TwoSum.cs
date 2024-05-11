using System.ComponentModel;
using System.Diagnostics;
using LeetCode.Attributes;
using LeetCode.Interfaces;

namespace LeetCode.Exercises;

[Completed(true)]
[DisplayName("Two Sum")]
[Code(2)]
[Description("Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target.")]
public sealed class TwoSum : IExercise
{

    public object[][] Cases => new[]
    {
        new object[]{new[] {1, 2}, new[] {3, 2, 4}, 6},
        new object[]{new[] {0, 1}, new[] {2, 7, 11, 15}, 9},
        new object[]{new[] {0, 1}, new[] {3, 3}, 6},
        new object[]{new[] {3, 4}, new[] {1, 2, 3, 4, 5}, 9},
        new object[]{new[] {0, 3}, new[] {10, 20, 30, 40}, 50},
        new object[]{new[] {0, 1}, new[] {1, 2, 3, 4, 5}, 3},
        new object[]{new[] {0, 3}, new[] {2, 4, 6, 8}, 10},
        new object[]{new[] {0, 2}, new[] {-1, 0, 1}, 0},
        new object[]{new[] {0, 1}, new[] {-3, -2, 1, 4, 5}, -5},
    };

    
    [DisplayName("Nested loops")]
    [Description("Beats 12.32% of users with C# in runtime and beats 51.80% of users with C# in memory")]
    public static int[]? Implementation1(int[] nums, int target)
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

    
    [DisplayName("Hash table index x value")]
    [Description("Beats 12.69% of users with C# in runtime and Beats 94.61% of users with C# in memory")]
    public static int[]? Implementation2(int[] nums, int target)
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
        return default;
    }

}