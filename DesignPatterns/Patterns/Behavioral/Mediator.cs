using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Behavioral;

/// <summary>
/// Посредник.
/// </summary>
/// <remarks>
/// Определение объекта, который инкапсулирует взаимодействие между множеством объектов.
/// 
/// Паттерн Посредник (Mediator) представляет такой шаблон проектирования, который обеспечивает взаимодействие множества объектов без необходимости ссылаться друг на друга. 
/// Тем самым достигается слабосвязанность взаимодействующих объектов.
/// 
/// <see cref="https://metanit.com/sharp/patterns/3.9.php"/>
/// </remarks>
[Description("Посредник: Определение объекта, который инкапсулирует взаимодействие между множеством объектов.\r\n\r\nПаттерн Посредник (Mediator) представляет такой шаблон проектирования, который обеспечивает взаимодействие множества объектов без необходимости ссылаться друг на друга. \r\nТем самым достигается слабосвязанность взаимодействующих объектов.")]
internal class Mediator : IPattern
{
    /// <summary>
    /// Интерфейс посредника.
    /// </summary>
    interface IMediator
    {
        void Notify(Employee employee, string? message);
    }

    /// <summary>
    /// Базовый класс работника.
    /// </summary>
    abstract class Employee
    {
        protected IMediator? _mediator;
        public Employee(IMediator? mediator) => _mediator = mediator;
        public void SetMediator(IMediator mediator) => _mediator = mediator;
    }

    /// <summary>
    /// Класс дизайнера.
    /// </summary>
    class Designer : Employee
    {
        private bool _isWorking;
        public Designer(IMediator? mediator = null) : base(mediator) { }
        public void ExecuteWork()
        {
            Console.WriteLine($"{nameof(Designer)} начал работу.");

            _mediator?.Notify(this, $"{nameof(Designer)} проектирует.");
        }
        public void SetWork(bool isWorking)
        {
            _isWorking = isWorking;

            if (!isWorking)
            {
                Console.WriteLine($"{nameof(Designer)} освобожден от работы.");
            }
            else
            {
                Console.WriteLine($"{nameof(Designer)} занят работой.");
            }
        }
    }

    /// <summary>
    /// Класс директора.
    /// </summary>
    class Director : Employee
    {
        private string? _command;
        public Director(IMediator? mediator = null) : base(mediator) { }
        public void GiveCommand(string? command = null)
        {
            _command = command;

            if (_command == null)
            {
                Console.WriteLine($"{nameof(Director)} знает, что {nameof(Designer)} уже работает.");
            }
            else
            {
                Console.WriteLine($"{nameof(Director)} дал комманду {nameof(Designer)}: {command}");
            }

            _mediator?.Notify(this, _command);
        }
    }

    /// <summary>
    /// Класс посредника, который находится между директором и дизайнером.
    /// </summary>
    class Controller : IMediator
    {
        private Designer _designer;
        private Director _director;

        public Controller(Designer designer, Director director)
        {
            _designer = designer;
            _director = director;

            _designer.SetMediator(this);
            _director.SetMediator(this);
        }

        public void Notify(Employee employee, string? message)
        {
            if (employee is Director)
            {
                _designer.SetWork(message == null ? false : true);
            }
        }
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        Designer designer = new Designer();
        Director director = new Director();
        Controller mediator = new Controller(designer, director);

        director.GiveCommand("Проектировать.");

        Console.WriteLine();

        designer.ExecuteWork();

        director.GiveCommand();
    }
}
