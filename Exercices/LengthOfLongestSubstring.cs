using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LeetCode.Exercices;

public static class LengthOfLongestSubstring
{
    private static readonly (string, int)[] _cases = new[]
    {
        ("abcabcbb", 3),
        ("bbbbb", 1),
        ("pwwkew", 3),
        ("", 0),
        (" ", 1),
        ("abcdefg", 7),
        ("aab", 2),
        ("dvdf", 3),
        ("tmmzuxt", 5),
    };
    public static void Run()
    {
        var methods = typeof(LengthOfLongestSubstring).GetMethods().Where(m => m.Name.Contains("Implementation"));
        foreach (var method in methods)
        {
            Eval(method);
        }
    }

    private static void Eval(MethodInfo submission)
    {
        var description = submission.GetCustomAttribute<DescriptionAttribute>();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(submission.Name);
        Console.ForegroundColor = description == null ? ConsoleColor.Gray : ConsoleColor.DarkYellow;
        Console.WriteLine((description == null ? "NOT SUBMITTED" : description.Description) + "\n");
        var stopWatch = new Stopwatch();
        foreach (var caset in _cases)
        {
            var (input, expected) = caset;
            stopWatch.Start();
            var res = ((int?) submission.Invoke(input, BindingFlags.Default, null, new object?[] { input }, CultureInfo.InvariantCulture)) ?? -1;
            stopWatch.Stop();
            var avaliation = res == expected ? "[SUCCESS]" : "[FAILED] ";
            Console.ForegroundColor = res == expected ? ConsoleColor.DarkGreen: ConsoleColor.Red;
            Console.WriteLine("{0} Data: {1} | Expected: {2} | Received: {3} | Execution time: {4}", avaliation, input, expected, res, stopWatch.Elapsed);
            Console.ResetColor();
            stopWatch.Reset();
        }

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("================================\n");
        Console.ResetColor();
    }
    
    [Description("Beats 5.96% c# users in runtime and beats 12.22% c# users in memory")]
    public static int Implementation1(string s)
    {
        var result = string.Empty;
        var size = 0;
        for (var i = 0; i < s.Length; i++)
        {
            for (var j = i; j < s.Length; j++)
            {
                var repeated = false;
                for (var z = 0; z < result.Length; z++)
                {
                    if (result[z] == s[j])
                    {
                        repeated = true;
                        break;
                    } 
                }
                if (repeated)
                {
                    if (result.Length > size)
                    {
                        size = result.Length;
                    }

                    result = string.Empty;
                    break;
                }
                result += s[j];
            }

            if (size == 0)
            {
                size = result.Length;
            }
        }
        return size;
    }
    
    [Description("Beats 72.72% c# users in runtime and beats 79.57% c# users in memory")]
    public static int Implementation2(string str)
    {
        if (str.Length <= 1) return str.Length;
        var readedCharPos = new Dictionary<char, short>();
        short left = 0;
        var max = 0;
        for (short right = 0; right < str.Length; right++)
        {
            var currentChar = str[right];
            if (readedCharPos.TryGetValue(currentChar, out var pos) && pos >= left) left = (short) (pos + 1);
            readedCharPos[currentChar] = right;
            var res = (short) (right - left + 1);
            if (res > max) max = res;
        }

        return max;
    }
}