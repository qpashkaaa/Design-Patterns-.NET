using DesignPatterns.Interfaces;

namespace DesignPatterns.Patterns.Structural;

/// <summary>
/// Мост.
/// </summary>
/// <remarks>
/// Разделение абстракции и ее реализации так, чтобы они могли изменяться независимо.
/// 
/// Структурный шаблон проектирования, 
/// который позволяет отделить абстракцию от реализации таким образом, чтобы и абстракцию, и реализацию можно было изменять независимо друг от друга.
/// Шаблон «Мост» использует инкапсуляцию, агрегирование и может использовать наследование для того, чтобы разделить ответственность между классами.
/// 
/// <see cref="https://metanit.com/sharp/patterns/4.6.php"/>
/// </remarks>
internal class Bridge : IPattern
{
    /// <summary>
    /// Базовый интерфейс чтения данных.
    /// </summary>
    interface IDataReader
    {
        void Read();
    }

    /// <summary>
    /// Класс чтения данных из базы данных.
    /// </summary>
    class DatabaseReader : IDataReader
    {
        public void Read() => Console.WriteLine("Данные прочитаны из базы данных.");
    }

    /// <summary>
    /// Класс чтения данных из файла.
    /// </summary>
    class FileReader : IDataReader
    {
        public void Read() => Console.WriteLine("Данные прочитаны из файла.");
    }

    /// <summary>
    /// Абстрактный класс - отправитель.
    /// </summary>
    abstract class Sender
    {
        protected IDataReader _reader;
        public Sender(IDataReader reader) => _reader = reader;
        public void SetDataReader(IDataReader reader) => _reader = reader;
        public abstract void Send();
    }

    /// <summary>
    /// Класс-отправитель по Email.
    /// </summary>
    class EmailSender : Sender
    {
        public EmailSender(IDataReader reader) : base(reader) { }
        public override void Send()
        {
            _reader.Read();
            Console.WriteLine("Данные отправлены по электронной почте.");
        }
    }

    /// <summary>
    /// Класс-отправитель Telegram-bot.
    /// </summary>
    class TelegramBotSender : Sender
    {
        public TelegramBotSender(IDataReader reader) : base(reader) { }
        public override void Send()
        {
            _reader.Read();
            Console.WriteLine("Данные отправлены через телеграм-бота.");
        }
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        Sender sender = new EmailSender(new DatabaseReader());
        sender.Send();
        sender.SetDataReader(new FileReader());
        sender.Send();

        sender = new TelegramBotSender(new DatabaseReader());
        sender.Send();
    }
}
