using System.ComponentModel;
using System.Runtime.CompilerServices;
using LeetCode.Attributes;
using LeetCode.Interfaces;

namespace LeetCode.Exercises;

[Code(4)]
[Completed]
[DisplayName("Median of Two Sorted Arrays")]
[Description("Given two sorted arrays nums1 and nums2 of size m and n respectively, return the median of the two sorted arrays.")]
public class MedianOfTwoSortedArrays : IExercise
{
    public object[][] Cases => new[]
    {
        new object[] { 2.0, new[] {1, 3}, new[] {2}},
        new object[] { 2.5, new[] {1, 2}, new[] {3,4}},
        new object[] { 2.5, new[] {1, 3}, new[] {2,7}},
    };
    [DisplayName("Full linq")]
    [Description("Beats 31.15% of users with C# and beats 15.49% of users with C#")]
    public static double Implementation1(int[] nums1, int[] nums2)
    {
        var merged = nums1.Concat(nums2).Order().ToArray();
        var center = (merged.Length - 1) / 2.0;
        Unsafe.SkipInit<double>(out var res);
        if (merged.Length % 2 != 0) 
        {
            res = merged[(int) Math.Ceiling(center)];
            return res;
        }

        var pos = (int) center;
        res = (merged[pos] + merged[pos + 1]) / 2.0;
        return res;
    }
}