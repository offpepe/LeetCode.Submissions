using System.ComponentModel;
using LeetCode.Attributes;
using LeetCode.Interfaces;

namespace LeetCode.Exercises;
[Completed(false)]
[DisplayName("Remove K Digits")]
[Code(457)]
[Description("Given string num representing a non-negative integer num, and an integer k, return the smallest possible integer after removing k digits from num.")]
public sealed class RemoveKDigits : IExercise
{
    public object[][] Cases => new[]
    {
        new object[]{"1219", "1432219", 3},
        new object[]{"200", "10200", 1},
        new object[]{"0", "10", 2},
        new object[]{"11", "112", 1},
        new object[]{"0", "10001", 4},
        new object[]{"1321", "43214321", 4},
    };
    
    public static string Implementation1(string num, int k)
    {
        var result = int.Parse(num);
        if (k == num.Length) return "0";
        var numSize = num.Length - k;
        var numArr = num.ToCharArray();
        for (var i = 0; i < num.Length; i++)
        {
            var subNumArr = num.ToCharArray();
            var range = k;
            var pos = i;
            while (range > 0)
            {
                if (pos == num.Length) pos = 0;
                subNumArr[pos] = char.MinValue;
                pos++;
                range--;
            }
            var subNum = int.Parse(subNumArr.Where(c => c != char.MinValue).ToArray());
            if (result > subNum) result = subNum;
        }
        return result.ToString();
    }
    
    public static string Implementation2(string num, int k)
    {
        var result = int.Parse(num);
        if (k == num.Length) return "0";
        for (var i = 0; i < num.Length; i++)
        {
            var workingNum = num;
            if (i + k > workingNum.Length)
            {
                var distanceToEnd = num.Length - i;
                var rangeBegin = Math.Abs(k - distanceToEnd); 
                workingNum = workingNum.Remove(i, distanceToEnd);
                workingNum = workingNum[rangeBegin..];
            }
            else
            {
                workingNum = workingNum.Remove(i, k);
            }
            var subNum = int.Parse(workingNum);
            if (result > subNum) result = subNum;
        }
        return result.ToString();
    }

    public static string Implementation3(string num, int k)
    {
        if (k == num.Length) return "0";
        var result = int.Parse(num);
        var skipIdx = new int[k];
        for (var i = 0; i < k; i++) skipIdx[i] = i;
        var workIdx = k - 1;
        while (workIdx != 0)
        {
            var nList = num.ToList();
            foreach (var idx in skipIdx)
            {
                nList.RemoveAt(idx);
            }

            var n = int.Parse(new string(nList.ToArray()));
            if (result > n) result = n;
            skipIdx[workIdx] += 1;
            if (skipIdx[workIdx] == k) workIdx--;
        }

        return result.ToString();
    }
    
}