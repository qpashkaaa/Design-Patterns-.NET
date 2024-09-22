using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Structural;

/// <summary>
/// Адаптер.
/// </summary>
/// <remarks>
/// Преобразование интерфейса одного класса в интерфейс другого, который ожидают клиенты.
/// 
/// Паттерн Адаптер (Adapter) предназначен для преобразования интерфейса одного класса в интерфейс другого. 
/// Благодаря реализации данного паттерна можноы использовать вместе классы с несовместимыми интерфейсами.
/// «Адаптер», предназначен для организации использования функций объекта, недоступного для модификации, через специально созданный интерфейс. 
/// Другими словами, он позволяет объектам с несовместимыми интерфейсами работать вместе.
/// 
/// <see cref="https://metanit.com/sharp/patterns/4.2.php"/>
/// </remarks>
[Description("Адаптер: Преобразование интерфейса одного класса в интерфейс другого, который ожидают клиенты.\r\n\r\nПаттерн Адаптер (Adapter) предназначен для преобразования интерфейса одного класса в интерфейс другого. \r\nБлагодаря реализации данного паттерна можноы использовать вместе классы с несовместимыми интерфейсами.\r\n«Адаптер», предназначен для организации использования функций объекта, недоступного для модификации, через специально созданный интерфейс. \r\nДругими словами, он позволяет объектам с несовместимыми интерфейсами работать вместе.")]
internal class Adapter : IPattern
{
    #region 1 вариант

    /// <summary>
    /// Интерфейс весов.
    /// </summary>
    interface IScales
    {
        float GetWeight();
    }

    /// <summary>
    /// Российские весы, которые измеряют вес в килограммах.
    /// </summary>
    class RussianScales : IScales
    {
        private float _currentWeight;
        public RussianScales(float currentWeight) => _currentWeight = currentWeight;
        public float GetWeight() => _currentWeight;
    }

    /// <summary>
    /// Британские весы, которые измеряют вес в фунтах.
    /// </summary>
    class BritishScales : IScales
    {
        private float _currentWeight;
        public BritishScales(float currentWeight) => _currentWeight = currentWeight;
        public float GetWeight() => _currentWeight;
    }

    /// <summary>
    /// Адаптер для британских весов, который будет адаптировать вес под фунты.
    /// </summary>
    class BritishScalesAdapter : IScales
    {
        BritishScales _scales;
        public BritishScalesAdapter(BritishScales scales) => _scales = scales;
        public float GetWeight() => _scales.GetWeight() * 0.453f;
    }

    #endregion

    #region 2 вариант

    /// <summary>
    /// Интерфейс отправителя запросов.
    /// </summary>
    interface IRequestSender
    {
        void Send();
    }

    /// <summary>
    /// Предположим, что класс лежит где-то в dll и его невозможно изменить, но нужен класс, наследующий интерфейс <see cref="IRequestSender"/>.
    /// </summary>
    class RequestSender
    {
        public void SendRequest()
        {
            Console.WriteLine("Отправлен запрос.");
        }
    }

    /// <summary>
    /// Адаптер для недоступного для модификации класса <see cref="RequestSender"/>.
    /// </summary>
    class RequestSenderAdapter : RequestSender, IRequestSender
    {
        public void Send()
        {
            this.SendRequest();
        }
    }

    #endregion

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        Console.WriteLine("_____Вариант 1_____\n");
        float kg = 55.0f; // кг
        float lb = 55.0f; // фунты

        IScales russianScales = new RussianScales(kg);
        IScales britishScales = new BritishScalesAdapter(new BritishScales(lb));

        Console.WriteLine($"Российские весы: {russianScales.GetWeight()} кг.");
        Console.WriteLine($"Британские весы: {britishScales.GetWeight()} фунтов.");
        Console.WriteLine("___________________\n");

        Console.WriteLine("_____Вариант 2_____\n");
        IRequestSender requestSender = new RequestSenderAdapter();
        requestSender.Send();
        Console.WriteLine("___________________\n");
    }
}
