using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Behavioral;

/// <summary>
/// Посетитель.
/// </summary>
/// <remarks>
/// Добавление операций к объектам, не изменяя их классы.
/// 
/// Паттерн Посетитель (Visitor) позволяет определить операцию для объектов других классов без изменения этих классов.
/// 
/// Преимуществом паттерна является то, что он объединяет родственный операции в одном классе, упрощает добавление операций, работающих со сложными структурами объектов. Отрицательным моментом является возможное нарушение инкапсуляции элементов.
/// 
/// <see cref="https://metanit.com/sharp/patterns/3.11.php"/>
/// </remarks>
[Description("Посетитель: Добавление операций к объектам, не изменяя их классы.\r\n\r\nПаттерн Посетитель (Visitor) позволяет определить операцию для объектов других классов без изменения этих классов.\r\n\r\nПреимуществом паттерна является то, что он объединяет родственный операции в одном классе, упрощает добавление операций, работающих со сложными структурами объектов. Отрицательным моментом является возможное нарушение инкапсуляции элементов.")]
internal class Visitor : IPattern
{
    /// <summary>
    /// Интерфейс посетителя.
    /// </summary>
    interface IVisitor
    {
        void Visit(Zoo zoo);
        void Visit(Cinema cinema);
        void Visit(Circus circus);
    }

    /// <summary>
    /// Интерфейс места для посещения.
    /// </summary>
    interface IPlace
    {
        void Accept(IVisitor visitor);
    }

    /// <summary>
    /// Место для посещения - зоопарк.
    /// </summary>
    class Zoo : IPlace
    {
        public void Accept(IVisitor visitor) => visitor.Visit(this);
    }

    /// <summary>
    /// Место для посещения - кинотеатр.
    /// </summary>
    class Cinema : IPlace
    {
        public void Accept(IVisitor visitor) => visitor.Visit(this);
    }

    /// <summary>
    /// Место для посещения - цирк.
    /// </summary>
    class Circus : IPlace
    {
        public void Accept(IVisitor visitor) => visitor.Visit(this);
    }

    /// <summary>
    /// Класс посетителя - отдыхающий.
    /// </summary>
    class HolidayMaker : IVisitor
    {
        public string? Watched { get; set; }
        public void Visit(Zoo zoo) => Watched = $"{nameof(HolidayMaker)} увидел слона в зоопарке.";
        public void Visit(Cinema cinema) => Watched = $"{nameof(HolidayMaker)} посмотрел Гарри Поттера в кино.";
        public void Visit(Circus circus) => Watched = $"{nameof(HolidayMaker)} увидел клоуна в цирке.";
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        List<IPlace> places = new List<IPlace>
        {
            new Zoo(),
            new Cinema(),
            new Circus()
        };

        foreach (var place in places)
        {
            HolidayMaker visitor = new HolidayMaker();
            place.Accept(visitor);
            Console.WriteLine(visitor.Watched);
        }
    }
}
