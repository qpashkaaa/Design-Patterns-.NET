using DesignPatterns.Interfaces;

namespace DesignPatterns.Patterns.Creational;

/// <summary>
/// Одиночка.
/// </summary>
/// <remarks>
/// Обеспечение единственного экземпляра класса и глобальная точка доступа к нему.
/// 
/// Паттерн, который гарантирует, что для определенного класса будет создан только один объект, а также предоставит к этому объекту точку доступа.
/// 
/// <see cref="https://metanit.com/sharp/patterns/2.3.php"/>
/// </remarks>
internal class Singletone : IPattern
{
    /// <summary>
    /// Пример паттерна через статические поля и методы и приватный конструктор.
    /// </summary>
    class DatabaseHelper
    {
        private string _data;
        private static DatabaseHelper _databaseConnection;

        private DatabaseHelper() => Console.WriteLine("Инициализируется подключение к БД.");

        public static DatabaseHelper GetConnection()
        {
            if (_databaseConnection == null)
            {
                _databaseConnection = new DatabaseHelper();
            }

            return _databaseConnection;
        }

        public string GetData() => _data;
        public void InsertData(string data)
        {
            Console.WriteLine($"Новые данные внесены в БД: {data} ");
            _data = data;
        }
    }

    /// <summary>
    /// Пример паттерна через статический класс.
    /// </summary>
    static class DatabaseHelperStatic
    {
        private static string _data;
        public static string Data 
        { 
            get 
            {
                return _data;
            }
            set 
            {
                _data = value;
                Console.WriteLine($"Новые данные внесены в БД через свойство статического класса: {_data} ");
            }
        }

        static DatabaseHelperStatic()
        {
            Console.WriteLine("Инициализируется подключение к БД через статичкский класс.");
        }
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        DatabaseHelper.GetConnection().InsertData("Database DATA.");
        Console.WriteLine($"Получение данных из БД: {DatabaseHelper.GetConnection().GetData()}");

        Console.WriteLine("\n___Static class example___\n");
        DatabaseHelperStatic.Data = "Database static class DATA.";
        Console.WriteLine($"Получение данных из БД статического класса: {DatabaseHelperStatic.Data}");
    }
}
