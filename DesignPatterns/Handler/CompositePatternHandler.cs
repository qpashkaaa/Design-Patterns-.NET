using DesignPatterns.Handler;
using DesignPatterns.Interfaces;

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
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"{pattern.GetType().Name} example:\n");

            pattern.ShowExample();

            Console.WriteLine("------------------------------------");
            Console.WriteLine("\n\n");
        }
    }
}
