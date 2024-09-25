using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Behavioral;

/// <summary>
/// Цепочка обязанностей.
/// </summary>
/// <remarks>
/// Передача запроса по цепочке обработчиков.
/// 
/// Цепочка Обязанностей (Chain of responsibility) - поведенческий шаблон проектирования, который позволяет избежать жесткой привязки отправителя запроса к получателю. 
/// Все возможные обработчики запроса образуют цепочку, а сам запрос перемещается по этой цепочке. Каждый объект в этой цепочке при получении запроса выбирает, либо закончить обработку запроса, 
/// либо передать запрос на обработку следующему по цепочке объекту.
/// 
/// <see cref="https://metanit.com/sharp/patterns/3.7.php"/>
/// </remarks>
[Description("Цепочка обязанностей: Передача запроса по цепочке обработчиков.\r\n\r\nЦепочка Обязанностей (Chain of responsibility) - поведенческий шаблон проектирования, который позволяет избежать жесткой привязки отправителя запроса к получателю. \r\nВсе возможные обработчики запроса образуют цепочку, а сам запрос перемещается по этой цепочке. Каждый объект в этой цепочке при получении запроса выбирает, либо закончить обработку запроса, \r\nлибо передать запрос на обработку следующему по цепочке объекту.")]
internal class ChainOfResponsibility : IPattern
{
    /// <summary>
    /// Базовый интерфейс работника.
    /// </summary>
    interface IWorker
    {
        IWorker SetNextWorker(IWorker worker);
        string Execute(string command);
    }

    /// <summary>
    /// Базовый класс работника.
    /// </summary>
    abstract class Worker : IWorker
    {
        private IWorker? _nextWorker;
        public Worker() => _nextWorker = null;
        public IWorker SetNextWorker(IWorker worker)
        {
            _nextWorker = worker;
            return worker;
        }
        public virtual string Execute(string command) 
        { 
            if (_nextWorker != null)
            {
                return _nextWorker.Execute(command);
            }

            return string.Empty;
        }

        public virtual string GetNextWorkerName()
        {
            return _nextWorker?.GetType().Name ?? "Не определено";
        }
    }

    /// <summary>
    /// Работник - дизайнер.
    /// </summary>
    class Designer : Worker
    {
        public override string Execute(string command)
        {
            if (command == "Создать проект дома")
            {
                return $"{nameof(Designer)} выполнил команду: {command}";
            }

            Console.WriteLine($"{nameof(Designer)} не может выполнить команду: {command}. Команда передана следующему исполнителю от {nameof(Designer)} к {base.GetNextWorkerName()}");

            return base.Execute(command);
        }
    }

    /// <summary>
    /// Работник - каменщик.
    /// </summary>
    class Bricklayer : Worker
    {
        public override string Execute(string command)
        {
            if (command == "Уложить кирпичи")
            {
                return $"{nameof(Bricklayer)} выполнил команду: {command}";
            }

            Console.WriteLine($"{nameof(Bricklayer)} не может выполнить команду: {command}. Команда передана следующему исполнителю от {nameof(Bricklayer)} к {base.GetNextWorkerName()}");

            return base.Execute(command);
        }
    }

    /// <summary>
    /// Работник, занимающийся внутренней отделкой дома.
    /// </summary>
    class Finisher : Worker
    {
        public override string Execute(string command)
        {
            if (command == "Поклеить обои")
            {
                return $"{nameof(Finisher)} выполнил команду: {command}.";
            }

            Console.WriteLine($"{nameof(Finisher)} не может выполнить команду: {command}. Команда передана следующему исполнителю от {nameof(Finisher)} к {base.GetNextWorkerName()}");

            return base.Execute(command);
        }
    }

    /// <summary>
    /// Метод передачи команды работнику.
    /// </summary>
    /// <param name="worker">Работник.</param>
    /// <param name="command">Команда.</param>
    void GiveCommand(IWorker worker, string command)
    {
        var executeResult = worker.Execute(command);

        if (string.IsNullOrEmpty(executeResult))
        {
            Console.WriteLine($"Никто не может выполнить команду: {command}");
        }
        else
        {
            Console.WriteLine(executeResult);
        }
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        Designer designer = new Designer();
        Bricklayer bricklayer = new Bricklayer();
        Finisher finisher = new Finisher();

        designer
            .SetNextWorker(bricklayer)
            .SetNextWorker(finisher);

        GiveCommand(designer, "Создать проект дома");
        Console.WriteLine("\n");
        GiveCommand(designer, "Уложить кирпичи");
        Console.WriteLine("\n");
        GiveCommand(bricklayer, "Поклеить обои");
        Console.WriteLine("\n");
        GiveCommand(designer, "Провести проводку");
    }
}
