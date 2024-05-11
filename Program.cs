using LeetCode.Exercices;

namespace LeetCode;

class Program
{
    private static void Main(string[] args)
    {
        var target = new LengthOfLongestSubstring();
        var evaluator = new Evaluator<LengthOfLongestSubstring>(target,
            target.cases.Select(tuple => ((object) tuple.Item1, (object) tuple.Item2)));
        evaluator.Evaluate();
    }
}