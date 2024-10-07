using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Behavioral;

/// <summary>
/// Стратегия.
/// </summary>
/// <remarks>
/// Определение семейства алгоритмов, инкапсуляция каждого из них и обеспечение взаимозаменяемости.
/// 
/// Паттерн Стратегия (Strategy) представляет шаблон проектирования, который определяет набор алгоритмов, инкапсулирует каждый из них и обеспечивает их взаимозаменяемость. 
/// В зависимости от ситуации мы можем легко заменить один используемый алгоритм другим. 
/// При этом замена алгоритма происходит независимо от объекта, который использует данный алгоритм.
/// 
/// <see cref="https://metanit.com/sharp/patterns/3.1.php"/>
/// </remarks>
[Description("Стратегия: Определение семейства алгоритмов, инкапсуляция каждого из них и обеспечение взаимозаменяемости.\r\n\r\nПаттерн Стратегия (Strategy) представляет шаблон проектирования, который определяет набор алгоритмов, инкапсулирует каждый из них и обеспечивает их взаимозаменяемость. \r\nВ зависимости от ситуации мы можем легко заменить один используемый алгоритм другим. \r\nПри этом замена алгоритма происходит независимо от объекта, который использует данный алгоритм.")]
internal class Strategy : IPattern
{
    /// <summary>
    /// Интерфейс читателя.
    /// </summary>
    interface IReader
    {
        void Parse(string url);
    }

    /// <summary>
    /// Читатель ресурсов (исполнитель стратегий).
    /// </summary>
    class ResourceReader
    {
        private IReader _reader;
        public ResourceReader(IReader reader) => _reader = reader;
        public void SetStrategy(IReader reader) => _reader = reader;
        public void Read(string url) => _reader.Parse(url);
    }

    /// <summary>
    /// Читатель новостных сайтов.
    /// </summary>
    class NewsSiteReader : IReader
    {
        public void Parse(string url) => Console.WriteLine($"Чтение данных с новостного сайта: {url}");
    }

    /// <summary>
    /// Читатель социальных сетей.
    /// </summary>
    class SocialNetworkReader : IReader
    {
        public void Parse(string url) => Console.WriteLine($"Чтение ленты новостей из социальной сети: {url}");
    }

    /// <summary>
    /// Читатель Telegram каналов.
    /// </summary>
    class TelegramChannelReader : IReader
    {
        public void Parse(string url) => Console.WriteLine($"Чтение информации из Telegram-канала: {url}");
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        ResourceReader resourceReader = new ResourceReader(new NewsSiteReader());
        string url = "https://news.com";
        resourceReader.Read(url);

        url = "https://vk.com";
        resourceReader.SetStrategy(new SocialNetworkReader());
        resourceReader.Read(url);

        url = "@telegram-channel";
        resourceReader.SetStrategy(new TelegramChannelReader());
        resourceReader.Read(url);
    }
}
