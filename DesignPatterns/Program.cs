using DesignPatterns.Interfaces;
using DesignPatterns.Models;
using System.Reflection;

namespace DesignPatterns;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("-----------------------");
        Console.WriteLine("Hello, Design patterns!");
        Console.WriteLine("-----------------------");
        Console.WriteLine("\n\n");

        var handler = new CompositePatternHandler(
            Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IPattern).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(Activator.CreateInstance).Cast<IPattern>());

        handler.Execute();
    }
}
