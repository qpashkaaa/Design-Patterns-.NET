using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Behavioral;

/// <summary>
/// Наблюдатель.
/// </summary>
/// <remarks>
/// Автоматическое уведомление зависимых объектов об изменениях состояния.
/// 
/// Паттерн "Наблюдатель" (Observer) представляет поведенческий шаблон проектирования, который использует отношение "один ко многим". 
/// В этом отношении есть один наблюдаемый объект и множество наблюдателей. 
/// И при изменении наблюдаемого объекта автоматически происходит оповещение всех наблюдателей.
/// 
/// <see cref="https://metanit.com/sharp/patterns/3.2.php"/>
/// </remarks>
[Description("Наблюдатель: Автоматическое уведомление зависимых объектов об изменениях состояния.\r\n\r\nПаттерн \"Наблюдатель\" (Observer) представляет поведенческий шаблон проектирования, который использует отношение \"один ко многим\". \r\nВ этом отношении есть один наблюдаемый объект и множество наблюдателей. \r\nИ при изменении наблюдаемого объекта автоматически происходит оповещение всех наблюдателей.")]
internal class Observer : IPattern
{
    /// <summary>
    /// Интерфейс наблюдателя.
    /// </summary>
    interface IObserver
    {
        void Update(double price);
    }

    /// <summary>
    /// Интерфейс наблюдаемого объекта.
    /// </summary>
    interface IObservable
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void Notify();
    }

    /// <summary>
    /// Класс продукта (наблюдаемый объект).
    /// </summary>
    class Product : IObservable
    {
        private List<IObserver> _observers;
        private double _price;
        public Product(double price)
        {
            Console.WriteLine($"Стартовая цена продукта {price}");
            _price = price;
            _observers = new List<IObserver>();
        }
        public void ChangePrice(double price)
        {
            Console.WriteLine($"Цена на продукт изменилась. Новая цена: {price}");
            _price = price;
            Notify();
        }
        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }
        public void RemoveObserver(IObserver observer) 
        { 
            _observers.Remove(observer);
        }
        public void Notify()
        {
            foreach (var observer in _observers.ToList())
            {
                observer.Update(_price);
            }
        }
    }

    /// <summary>
    /// Класс оптового покупателя (наблюдатель).
    /// </summary>
    class Wholesale : IObserver
    {
        private IObservable _observable;
        public Wholesale(IObservable observable)
        {
            _observable = observable;
            _observable.AddObserver(this);
        }
        public void Update(double price)
        {
            if (price < 300)
            {
                Console.WriteLine($"{nameof(Wholesale)} закупил товар по цене: {price}.");
                _observable.RemoveObserver(this);
            }
        }
    }

    /// <summary>
    /// Класс обычного покупателя (наблюдатель).
    /// </summary>
    class Buyer : IObserver
    {
        private IObservable _observable;
        public Buyer(IObservable observable)
        {
            _observable = observable;
            _observable.AddObserver(this);
        }
        public void Update(double price)
        {
            if (price < 350)
            {
                Console.WriteLine($"{nameof(Buyer)} закупил товар по цене: {price}.");
                _observable.RemoveObserver(this);
            }
        }
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        Product product = new Product(400);
        Wholesale wholesale = new Wholesale(product);
        Buyer buyer = new Buyer(product);

        Console.WriteLine();

        product.ChangePrice(550);

        Console.WriteLine();

        product.ChangePrice(530);

        Console.WriteLine();

        product.ChangePrice(320);

        Console.WriteLine();

        product.ChangePrice(280);

        Console.WriteLine();

        product.ChangePrice(120);
    }
}
