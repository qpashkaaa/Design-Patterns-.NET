using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Structural;

/// <summary>
/// Фасад.
/// </summary>
/// <remarks>
/// Предоставление упрощенного интерфейса к сложной системе.
/// 
/// <see cref="https://metanit.com/sharp/patterns/4.3.php"/>.
/// </remarks>
[Description("Фасад: Предоставление упрощенного интерфейса к сложной системе.")]
internal class Facade : IPattern
{
    /// <summary>
    /// Класс поставщика товаров.
    /// </summary>
    class ProviderCommunication
    {
        public void Recieve() => Console.WriteLine("Получение продукции от производителя.");
        public void Payment() => Console.WriteLine("Оплата поставщику.");
    }

    /// <summary>
    /// Класс сайта с товарами.
    /// </summary>
    class Site
    {
        public void Placement() => Console.WriteLine("Размещение на сайте.");
        public void Delete() => Console.WriteLine("Удаление товара с сайта.");
    }

    /// <summary>
    /// Класс базы данных, содержащей информацию о товарах.
    /// </summary>
    class Database
    {
        public void Insert() => Console.WriteLine("Запись в базу данных.");
        public void Delete() => Console.WriteLine("Удаление товара из базы данных.");
    }

    /// <summary>
    /// Класс-фасад магазина.
    /// 
    /// Предоставляет упрощенный интерфейс для взаимодействия с классами <see cref="ProviderCommunication"/>, <see cref="Site"/>, <see cref="Database"/>,
    /// который позволяет отобразить действия "Получение товара" и "Продажа товара".
    /// </summary>
    class Marketplace
    {
        private ProviderCommunication _providerCommunication;
        private Site _site;
        private Database _database;

        public Marketplace()
        {
            _providerCommunication = new ProviderCommunication();
            _site = new Site();
            _database = new Database();
        }

        public void ProductReceipt()
        {
            _providerCommunication.Recieve();
            _site.Placement();
            _database.Insert();
        }

        public void ProductRelease()
        {
            _providerCommunication.Payment();
            _site.Delete();
            _database.Delete();
        }
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        var marketplace = new Marketplace();

        marketplace.ProductReceipt();

        Console.WriteLine();

        marketplace.ProductRelease();
    }
}
