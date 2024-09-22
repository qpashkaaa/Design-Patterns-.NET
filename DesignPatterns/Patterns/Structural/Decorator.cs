using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Structural;

/// <summary>
/// Декоратор.
/// </summary>
/// <remarks>
/// Динамическое добавление обязанностей объекту.
/// 
/// Декоратор (Decorator) представляет структурный шаблон проектирования, который позволяет динамически подключать к объекту дополнительную функциональность.
/// 
/// <see cref="https://metanit.com/sharp/patterns/4.1.php"/>
/// </remarks>
[Description("Декоратор: Динамическое добавление обязанностей объекту.\r\n\r\nДекоратор (Decorator) представляет структурный шаблон проектирования, который позволяет динамически подключать к объекту дополнительную функциональность.")]
internal class Decorator : IPattern
{
    /// <summary>
    /// Интерфейс передатчика.
    /// </summary>
    interface IProcessor
    {
        void Process();
    }

    /// <summary>
    /// Класс передатчика.
    /// </summary>
    class Transmitter : IProcessor
    {
        private string _data;
        public Transmitter(string data) => _data = data;
        public void Process() => Console.WriteLine($"Данные были переданы по каналу связи. Данные: {_data}");
    }

    /// <summary>
    /// Абстрактный класс декоратора над передатчиком.
    /// </summary>
    abstract class AbstractProcessorDecorator : IProcessor
    {
        protected IProcessor _processor;
        public AbstractProcessorDecorator(IProcessor processor) => _processor = processor;
        public virtual void Process() 
        {
            Console.Write("Использован декоратор -> ");
            _processor.Process();
        }
    }

    /// <summary>
    /// Декоратор над передатчиком, который накладывает на сообщение код Хамминга.
    /// </summary>
    class HammingCoder : AbstractProcessorDecorator
    {
        public HammingCoder(IProcessor processor) : base(processor) { }
        public override void Process()
        {
            Console.Write("Наложен помехоустойчивый код Хамминга (декоратор) -> ");
            base.Process();
        }
    }

    /// <summary>
    /// Декоратор на передатчиком, который шифрует данные.
    /// </summary>
    class Encryptor : AbstractProcessorDecorator
    {
        public Encryptor(IProcessor processor) : base(processor) { }
        public override void Process()
        {
            Console.Write("Проведено шифрование данных (декоратор) -> ");
            base.Process();
        }
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        IProcessor transmitter = new Transmitter("Данные для передатчика.");
        transmitter.Process();

        AbstractProcessorDecorator hammingCoder = new HammingCoder(transmitter);
        hammingCoder.Process();

        AbstractProcessorDecorator encryptor = new Encryptor(transmitter);
        encryptor.Process();
    }
}
