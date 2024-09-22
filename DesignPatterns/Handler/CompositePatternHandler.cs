using DesignPatterns.Handler;
using DesignPatterns.Interfaces;
using System.ComponentModel;
using System.Reflection;

namespace DesignPatterns.Models;
internal class CompositePatternHandler : ICompositePatternHandler
{
    private readonly IEnumerable<IPattern> _patterns;

    public CompositePatternHandler(IEnumerable<IPattern> patterns)
    {
        _patterns = patterns;
    }

    public void Execute()
    {
        foreach (var pattern in _patterns) 
        {
            var description = pattern.GetType().GetCustomAttribute<DescriptionAttribute>()?.Description;

            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{pattern.GetType().Name}:\n");

            if (description != null)
            {
                Console.WriteLine(new string('+', 50));
                Console.WriteLine($"Description: {description}");
                Console.WriteLine(new string('+', 50));
                Console.WriteLine("\n");
            }

            Console.WriteLine("Example:\n");

            pattern.ShowExample();

            Console.WriteLine(new string('-', 50));
            Console.WriteLine("\n\n");
        }
    }
}
