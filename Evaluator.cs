using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using LeetCode.Interfaces;

namespace LeetCode;

public class Evaluator<TClass>(TClass target)
    where TClass : IExercise 
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
        Console.Write((title?.DisplayName ?? _targetType.Name).Centralize());
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(string.Empty);
        if (description != null)
        {
            Console.WriteLine(description.Description);
        }
        Console.WriteLine("\n");
        foreach (var method in _methods) Validate(method);
    }

    private void Validate(MethodBase submission)
    {
        var description = submission.GetCustomAttribute<DescriptionAttribute>();
        var displayName = submission.GetCustomAttribute<DisplayNameAttribute>();
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("NAME: ");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("{0}", displayName?.DisplayName ?? submission.Name);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("RESULT:");
        Console.ForegroundColor = description == null ? ConsoleColor.Gray : ConsoleColor.Yellow;
        Console.WriteLine(" {0} \n", description?.Description ??"NOT SUBMITTED");
        foreach (var subCase in target.Cases)
        {
            var expected = subCase[0];
            var parameters = subCase[1..];
            object? res;
            try
            {
                _stopwatch.Start();
                res = submission.Invoke(parameters, BindingFlags.Default, null, parameters,
                    CultureInfo.InvariantCulture);
                _stopwatch.Stop();
                var success = res?.ToString() == expected.ToString();
                var avaliation = success ? "[SUCCESS]" : "[FAILED] ";
                var paramStr = string.Join(", ", parameters.Select(ConvertParam));
                var expectStr = expected is int[] expObjArr
                    ? string.Join(", ", expObjArr)
                    : expected.ToString()!;
                var resultStr = res is int[] resObjArr
                    ? string.Join(", ", resObjArr)
                    : res?.ToString() ?? string.Empty;
                Console.ForegroundColor = success ? ConsoleColor.DarkGreen : ConsoleColor.Red;
                Console.WriteLine("{0} Input: {1} | Expected: {2} | Received: {3} | Execution time: {4}", avaliation,
                    paramStr.AddPaddings(12), expectStr.AddPaddings(4), resultStr.AddPaddings(4), _stopwatch.Elapsed);
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[ERROR] UNHANDLED ERROR WHILE EXECUTING SUBMISSION. MESSAGE: {0}", ex.InnerException?.Message);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(string.Empty);
                break;
            }
            finally
            {
                _stopwatch.Reset();
            }
            
        }
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("================\n\n");
    }
    
    private static string ConvertParam(object param) => param switch
    {
        Array arr => string.Join(", ", ConvertArrType(arr)),
        _ => param.ToString() ?? throw new InvalidCastException($"Error while converting type {param.GetType().Name} into string")
    };

    private static string[] ConvertArrType(Array objArr)
    {
        var result = new string[objArr.Length];
        short position = 0;
        foreach (var obj in objArr)
        {
            result[position] = obj switch
            {
                Array arr => $"[{string.Join(", ", arr)}]",
                _ => obj.ToString()
            } ?? throw new InvalidCastException($"Error while converting type {obj.GetType().Name} into string");
            position++;
        }

        return result;
    }
}