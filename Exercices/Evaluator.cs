using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace LeetCode.Exercices;

public class Evaluator<TClass>(TClass target, IEnumerable<(object, object)> cases)
    where TClass : class 
{
    private readonly IEnumerable<MethodInfo> _methods = target.GetType().GetMethods().Where(p => p.Name.Contains("Implementation"));
    private readonly Type _targetType = target.GetType();
    private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
    
    public void Evaluate()
    {
        var description = _targetType.GetCustomAttribute<DescriptionAttribute>();
        var title = _targetType.GetCustomAttribute<DisplayNameAttribute>();
        Console.Title = _targetType.Name;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;
        Console.WriteLine(Centralize(title?.DisplayName ?? _targetType.Name));
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Yellow;
        if (description != null)
        {
            Console.WriteLine(description.Description);
        }
        Console.WriteLine("\n");
        foreach (var method in _methods) Validate(method);
    }

    private void Validate(MethodInfo submission)
    {
        var description = submission.GetCustomAttribute<DescriptionAttribute>();
        var displayName = submission.GetCustomAttribute<DisplayNameAttribute>();
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("NAME: ");
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("{0}", displayName?.DisplayName ?? submission.Name);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("DESCRIPTION:");
        Console.ForegroundColor = description == null ? ConsoleColor.Gray : ConsoleColor.DarkYellow;
        Console.WriteLine(" {0} \n", description?.Description ??"NOT SUBMITTED");
        foreach (var subCase in cases)
        {
            var (parameter, expected) = subCase;
            _stopwatch.Restart();
            var res = submission.Invoke(parameter, BindingFlags.Default, null, new[]{ parameter },
                CultureInfo.InvariantCulture);
            _stopwatch.Stop();
            var success = res?.ToString() == expected.ToString();
            var avaliation = success ? "[SUCCESS]" : "[FAILED] ";
            var paramStr = parameter is Array ? string.Join(",", parameter) : parameter.ToString(); 
            Console.ForegroundColor = success ? ConsoleColor.DarkGreen: ConsoleColor.Red;
            Console.WriteLine("{0} Input: {1} | Expected: {2} | Received: {3} | Execution time: {4}", avaliation, AddPaddings(paramStr, 8), AddPaddings(expected, 2), AddPaddings(res, 2), _stopwatch.Elapsed);
        }
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("================\n");
    }


    private static string AddPaddings(object? obj, short size)
    {
        var param = obj!.ToString() ?? string.Empty;
        if (param.Length >= size) return param;
        while (param.Length < size)
        {
            param += ' ';
        }
        return param;
    }

    private static string Centralize(string content)
    {
        for (int i = 0; i < 20; i++)
        {
            content = ' ' + content + ' ';
        }

        return content;
    }
}