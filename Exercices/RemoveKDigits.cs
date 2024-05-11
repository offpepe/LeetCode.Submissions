using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace LeetCode.Exercices;

public static class RemoveKDigits
{
    private static readonly Stopwatch sw = new();
    private static readonly (string, int, string)[] Scenarios = new (string, int, string)[]
    {
        ("1432219", 3, "1219"),
        ("10200", 1, "200"),
        ("10",  2, "0"),
        ("112", 1, "11"),
        ("10001", 4, "0"),
        ("43214321", 4, "1321")
    };
    public static void Run()
    {
        Console.WriteLine("FIRST");
        foreach (var scenario in Scenarios)
        {
            sw.Start();
            var (num, k, expected) = scenario;
            var result = Third(num, k);
            sw.Stop();
            if (result != expected)
            {
                Console.WriteLine("({0}) [FAILED] EXPECTED {1} - {2} = {3} || RECEIVED: {1} - {2} = {4}", sw.Elapsed, num, k, expected, result);
                sw.Reset();
                continue;
            }
            Console.WriteLine("({0}) {1} - {2} = {3}", sw.Elapsed, num, k, result);
            sw.Reset();
        }
    }
    
    private static string First(string num, int k)
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
    
    private static string Second(string num, int k)
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

    private static string Third(string num, int k)
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