using System.ComponentModel;
using LeetCode.Attributes;
using LeetCode.Interfaces;

namespace LeetCode.Exercises;
[Code(3)]
[Completed(true)]
[DisplayName("Longest Substring Without Repeating Characters")]
[Description("Given a string s, find the length of the longest substring without repeating characters.")]
public sealed class LengthOfLongestSubstring : IExercise
{
    public object[][] Cases => new object[][] {
        new object[]{3, "abcabcbb"},
        new object[]{1, "bbbbb"},
        new object[]{3, "pwwkew"},
        new object[]{0, ""},
        new object[]{1, " "},
        new object[]{7, "abcdefg"},
        new object[]{2, "aab"},
        new object[]{3, "dvdf"},
        new object[]{5, "tmmzuxt"},
    };

    [DisplayName("Nested loops")]
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
    
    [DisplayName("Dictionary char x pos submission")]
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