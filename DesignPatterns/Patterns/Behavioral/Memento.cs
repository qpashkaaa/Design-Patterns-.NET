using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Behavioral;

/// <summary>
/// Снимок.
/// </summary>
/// <remarks>
/// Сохранение и восстановление состояния объекта.
/// 
/// Паттерн Снимокы (Memento) позволяет выносить внутреннее состояние объекта за его пределы для последующего возможного восстановления объекта без нарушения принципа инкапсуляции.
/// Положительным моментом использования паттерна является, то что он не нарушает инкапсуляции исходного объекта и упрощает его структуру. 
/// Недостатком может быть большое количество памяти, выделяемое при частом создании снимка состояния.
/// 
/// <see cref="https://metanit.com/sharp/patterns/3.10.php"/>
/// </remarks>
[Description("Снимок: Сохранение и восстановление состояния объекта.\r\n\r\nПаттерн Снимокы (Memento) позволяет выносить внутреннее состояние объекта за его пределы для последующего возможного восстановления объекта без нарушения принципа инкапсуляции.\r\nПоложительным моментом использования паттерна является, то что он не нарушает инкапсуляции исходного объекта и упрощает его структуру. \r\nНедостатком может быть большое количество памяти, выделяемое при частом создании снимка состояния.")]
internal class Memento : IPattern
{
    /// <summary>
    /// Базовый интерфейс снимка.
    /// </summary>
    interface IMemento
    {
        int GetDollars();
        int GetEuros();
    }

    /// <summary>
    /// Класс снимка биржи.
    /// </summary>
    class ExchangeMemento : IMemento
    {
        private int _dollars;
        private int _euros;
        public ExchangeMemento(int dollars, int euros)
        {
            _dollars = dollars;
            _euros = euros;
        }
        public int GetDollars() => _dollars;
        public int GetEuros() => _euros;
    }

    /// <summary>
    /// Класс биржи.
    /// </summary>
    class Exchange
    {
        private int _dollars;
        private int _euros;
        public Exchange(int dollars, int euros)
        {
            _dollars = dollars;
            _euros = euros;
        }
        public void GetDollars() => Console.WriteLine($"Доллары: {_dollars}");
        public void GetEuros() => Console.WriteLine($"Евро: {_euros}");
        public void Sell()
        {
            if (_dollars > 0)
            {
                --_dollars;
            }
        }
        public void Buy() => ++_euros;
        public ExchangeMemento Save() => new ExchangeMemento(_dollars, _euros);
        public void Restore(IMemento memento)
        {
            _dollars = memento.GetDollars();
            _euros = memento.GetEuros();
        }
    }

    /// <summary>
    /// Класс памяти.
    /// </summary>
    class Memory
    {
        private Stack<IMemento> _history;
        private Exchange _exchange;
        public Memory(Exchange exchange)
        {
            _exchange = exchange;
            _history = new Stack<IMemento>();
        }
        public void SetBackup() => _history.Push(_exchange.Save());
        public void GetLastBackup()
        {
            if (_history.Count == 0)
            {
                return;
            }

            _exchange.Restore(_history.Pop());
        }
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        Exchange exchange = new Exchange(10, 10);
        Memory memory = new Memory(exchange);

        exchange.GetDollars();
        exchange.GetEuros();

        Console.WriteLine($"\nВыполняется продажа 1 доллара, покупка 1 евро.\n");

        exchange.Sell();
        exchange.Buy();

        exchange.GetDollars();
        exchange.GetEuros();

        Console.WriteLine($"\nСохранение состояния биржи.");

        memory.SetBackup();

        Console.WriteLine($"\nВыполняется продажа 1 доллара, покупка 1 евро.\n");

        exchange.Sell();
        exchange.Buy();

        exchange.GetDollars();
        exchange.GetEuros();

        Console.WriteLine("\nВыполняется восстановление состояния из памяти.\n");

        memory.GetLastBackup();

        exchange.GetDollars();
        exchange.GetEuros();
    }
}
