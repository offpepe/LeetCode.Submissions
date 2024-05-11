using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace LeetCode.Exercices;

public class Evaluator<TClass>(TClass target,(object[], object)[] cases)
    where TClass : class 
{
    private readonly IEnumerable<MethodInfo> _methods = target.GetType().GetMethods();
    private readonly Type _targetType = target.GetType();
    private readonly (object[], object)[] _cases = cases;
    private readonly Stopwatch _stopwatch = Stopwatch.StartNew();

    public void Evaluate()
    {
        var description = _targetType.GetCustomAttribute<DescriptionAttribute>();
        Console.Title = _targetType.Name;
        Console.WriteLine("EVALUATING EXERCISE: {0}", _targetType.Name);
        if (description != null)
        {
            Console.WriteLine(description);
        }
        Console.WriteLine("\n");
        foreach (var method in _methods) Validate(method);
    }

    private void Validate(MethodInfo submission)
    {
        var description = submission.GetCustomAttribute<DescriptionAttribute>();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(submission.Name);
        Console.ForegroundColor = description == null ? ConsoleColor.Gray : ConsoleColor.DarkYellow;
        Console.WriteLine((description == null ? "NOT SUBMITTED" : description.Description) + "\n");
        foreach (var subCase in cases)
        {
            var (parameters, expected) = subCase;
            _stopwatch.Restart();
            var res = submission.Invoke(parameters, BindingFlags.Default, null, parameters,
                CultureInfo.InvariantCulture);
            _stopwatch.Stop();
            var success = res == expected;
            var avaliation = success ? "[SUCCESS]" : "[FAILED] ";
            Console.ForegroundColor = success ? ConsoleColor.DarkGreen: ConsoleColor.Red;
            Console.WriteLine("{0} Input: {1} | Expected: {2} | Received: {3} | Execution time: {4}", avaliation, string.Join(", ",parameters), expected, res, _stopwatch.Elapsed);

        }

    }
}