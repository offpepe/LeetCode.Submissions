using System.ComponentModel;
using System.Reflection;
using LeetCode.Attributes;
using LeetCode.Enums;
using LeetCode.Interfaces;

namespace LeetCode;

class Program
{
    private const string EXERCISE_NAMESPACE = "LeetCode.Exercises";
    private const string TITLE = "LeetCode submissions";
    private static readonly Type[] Exercises = Assembly.GetExecutingAssembly().GetTypes()
        .Where(t => t is { Namespace: EXERCISE_NAMESPACE, IsClass: true, IsVisible: true })
        .OrderBy(t => t.GetCustomAttribute<CodeAttribute>()?.Code ?? -1)
        .ToArray(); 
    private static void Main(string[] args)
    {
        Console.Title = TITLE;
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\n\n");
        Console.Write(TITLE.ToUpper().Centralize());
        Console.Write("\n\n");
        Console.ResetColor();
        Console.WriteLine(string.Empty);
        if (args.ElementAtOrDefault(0) == "-l")
        {
            ListExercises();
            return;
        }
        if (args.Length == 0)
        {
            Console.WriteLine("Exercise code is required");
            return;
        } 
        ExecuteExercises(args);
    }
    
    private static void ListExercises()
    {
        foreach (var exercise in Exercises)
        {
            var name = exercise.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? exercise.Name;
            var code = exercise.GetCustomAttribute<CodeAttribute>()?.Code ?? -1;
            Console.WriteLine("{0}: {1}", code.AddPaddings(3), name.AddPaddings(4, PaddingDirection.Right));
        }
    }

    private static void ExecuteExercises(string[] args)
    {
        if (args.ElementAtOrDefault(0) == "--all")
        {
            RunExerciseCollection(Exercises.Where(e => e.GetCustomAttribute<CompletedAttribute>()?.Completed ?? false));
            return;
        }
        var toBeExecuted = Exercises.Where(e => args.Any(a => e.GetCustomAttribute<CodeAttribute>()?.Code == int.Parse(a))).ToArray();
        if (toBeExecuted.Length == 0)
        {
            Console.WriteLine("None exercise was found with this code");
            return;
        }
        RunExerciseCollection(toBeExecuted);
        return;

        void RunExerciseCollection(IEnumerable<Type> methods)
        {
            foreach (var method in methods)
            {
                if (method.Assembly.CreateInstance(method.FullName ?? string.Empty) is not IExercise obj) throw new Exception("PROGRAM WAS NOT CAPABLE TO READ PROGRAM");
                EvaluateExercise(obj);
            }
        }
    }
    private static void EvaluateExercise<TExercise>(TExercise target) where TExercise: IExercise
    {
        var evaluator = new Evaluator<TExercise>(target);
        evaluator.Evaluate();
    }
}