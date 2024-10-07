using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Behavioral;

/// <summary>
/// Шаблонный метод.
/// </summary>
/// <remarks>
/// Определение основного алгоритма в методе, оставляя детали на реализацию подклассов.
/// 
/// Шаблонный метод (Template Method) определяет общий алгоритм поведения подклассов, 
/// позволяя им переопределить отдельные шаги этого алгоритма без изменения его структуры.
/// 
/// Преимуществом паттерна является то, что он облегчает повторное использование кода.
/// 
/// <see cref="https://metanit.com/sharp/patterns/3.4.php"/>
/// </remarks>
[Description("Шаблонный метод: Определение основного алгоритма в методе, оставляя детали на реализацию подклассов.\r\n\r\nШаблонный метод (Template Method) определяет общий алгоритм поведения подклассов, \r\nпозволяя им переопределить отдельные шаги этого алгоритма без изменения его структуры.\r\n\r\nПреимуществом паттерна является то, что он облегчает повторное использование кода.")]
internal class TemplateMethod : IPattern
{
    /// <summary>
    /// Интерфейс передатчика.
    /// </summary>
    interface ITransmitter
    {
        void ProcessStart();
    }

    /// <summary>
    /// Базовый класс передатчика.
    /// </summary>
    abstract class Transmitter : ITransmitter
    {
        protected virtual void VoiceRecord() => Console.WriteLine($"[{nameof(Transmitter)}]: Запись фрагмента речи");

        /// <summary>
        /// При необходимости методы могут быть абстрактными. Все зависит от цели и способов реализации.
        /// </summary>
        protected virtual void Simpling() { }

        /// <summary>
        /// При необходимости методы могут быть абстрактными. Все зависит от цели и способов реализации.
        /// </summary>
        protected virtual void Digitization() { }

        /// <summary>
        /// При необходимости методы могут быть абстрактными. Все зависит от цели и способов реализации.
        /// </summary>
        protected virtual void Modulation() { }


        protected virtual void Transmission() => Console.WriteLine($"[{nameof(Transmitter)}]: Передача сигнала по радиоканалу.");

        public void ProcessStart()
        {
            Console.WriteLine($"[{nameof(Transmitter)}]: Запущен процесс формирования сигнала.");

            VoiceRecord();
            Simpling();
            Digitization();
            Modulation();
            Transmission();

            Console.WriteLine($"[{nameof(Transmitter)}]: Процесс формирования сигнала завершен.");
        }
    }

    /// <summary>
    /// Аналоговый передатчик.
    /// </summary>
    class AnalogTransmitter : Transmitter
    {
        protected override void Modulation() => Console.WriteLine($"[{nameof(AnalogTransmitter)}]: Модуляция аналогового сигнала.");
    }

    /// <summary>
    /// Цифровой передатчик.
    /// </summary>
    class DigitalTransmitter : Transmitter
    {
        protected override void Simpling() => Console.WriteLine($"[{nameof(DigitalTransmitter)}]: Дискредитация записанного фрагмента.");
        protected override void Digitization() => Console.WriteLine($"[{nameof(DigitalTransmitter)}]: Оцифровка записанного фрагмента.");
        protected override void Modulation() => Console.WriteLine($"[{nameof(DigitalTransmitter)}]: Модуляция цифрового сигнала.");
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        ITransmitter analogTransmitter = new AnalogTransmitter();
        Console.WriteLine($"Создан {nameof(AnalogTransmitter)}:");
        analogTransmitter.ProcessStart();

        Console.WriteLine();

        ITransmitter digitalTransmitter = new DigitalTransmitter();
        Console.WriteLine($"Создан {nameof(DigitalTransmitter)}:");
        digitalTransmitter.ProcessStart();
    }
}
