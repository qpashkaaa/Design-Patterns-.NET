using DesignPatterns.Interfaces;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlTypes;

namespace DesignPatterns.Patterns.Structural;

/// <summary>
/// Легковес.
/// </summary>
/// <remarks>
/// Уменьшение количества создаваемых объектов для снижения потребления памяти.
/// 
/// Паттерн Легковес. (Flyweight) - структурный шаблон проектирования, который позволяет использовать разделяемые объекты сразу в нескольких контекстах. 
/// Данный паттерн используется преимущественно для оптимизации работы с памятью.
/// 
/// Легковес экономит память, выделяя и сохраняя общие параметры объектов. Однако при использовании данного паттерна расходуется процессорное время на поиск, а также из-за введения дополнительных классов усложняется код программы.
/// 
/// <see cref="https://metanit.com/sharp/patterns/4.7.php"/>
/// </remarks>
[Description("Легковес: Уменьшение количества создаваемых объектов для снижения потребления памяти.\r\n\r\nПаттерн Легковес. (Flyweight) - структурный шаблон проектирования, который позволяет использовать разделяемые объекты сразу в нескольких контекстах. \r\nДанный паттерн используется преимущественно для оптимизации работы с памятью.\r\n\r\nЛегковес экономит память, выделяя и сохраняя общие параметры объектов. Однако при использовании данного паттерна расходуется процессорное время на поиск, а также из-за введения дополнительных классов усложняется код программы.")]
internal class Flyweight : IPattern
{
    /// <summary>
    /// Структура с общими данными.
    /// </summary>
    struct Shared
    {
        private string _company;
        private string _position;
        public Shared(string company, string position)
        {
            _company = company;
            _position = position;
        }
        public string Company { get => _company; }
        public string Position { get => _position; }
    }

    /// <summary>
    /// Структура с уникальными данными.
    /// </summary>
    struct Unique
    {
        private string _name;
        private string _passport;
        public Unique(string name, string passport)
        {
            _name = name;
            _passport = passport;
        }
        public string Name { get => _name; }
        public string Passport { get => _passport; }
    }

    /// <summary>
    /// Модель легковес конференции.
    /// </summary>
    class ConferenceFlyweidth
    {
        private Shared _shared;
        public ConferenceFlyweidth(Shared shared) => _shared = shared;
        public void Process(Unique unique)
        {
            Console.WriteLine($"\tОбщие данные: {_shared.Company}, {_shared.Position}. Уникальные данные: {unique.Name}, {unique.Passport}.");
        }
        public string GetData() => $"{_shared.Company}_{_shared.Position}";
    }

    /// <summary>
    /// Фабрика легковесов.
    /// </summary>
    class FlyweightFactory
    {
        private Hashtable _flyweights;
        private string GetKey(Shared shared) => $"{shared.Company}_{shared.Position}";
        public FlyweightFactory(IEnumerable<Shared> shareds)
        {
            _flyweights = new Hashtable();

            foreach (var shared in shareds) 
            {
                _flyweights.Add(GetKey(shared), new ConferenceFlyweidth(shared));
            }
        }
        public ConferenceFlyweidth GetFlyweidth(Shared shared)
        {
            string key = GetKey(shared);
            
            if (!_flyweights.Contains(key))
            {
                Console.WriteLine($"Фабрика легковесов: Общий элемент по ключу {key} не найден. Создаем новый.");
                _flyweights.Add(key, new ConferenceFlyweidth(shared));
            }
            else
            {
                Console.WriteLine($"Фабрика легковесов: Извлекаем данные из имеющихся записей по ключу {key}");
            }

            return (ConferenceFlyweidth)_flyweights[key]!;
        }

        public void ListFlyweights()
        {
            Console.WriteLine($"Фабрика легковесов: всего записей - {_flyweights.Count}. Список:");
            
            foreach (ConferenceFlyweidth value in _flyweights.Values)
            {
                Console.WriteLine($"\t{value.GetData()}");
            }
        }
    }

    /// <summary>
    /// Метод добавления специалиста в базу данных.
    /// </summary>
    /// <param name="flyweightFactory">Фабрика легковесов.</param>
    /// <param name="company">Компания.</param>
    /// <param name="position">Должность.</param>
    /// <param name="name">Имя.</param>
    /// <param name="passport">Паспорт.</param>
    void AddSpecialistInDatabase(
        FlyweightFactory flyweightFactory,
        string company,
        string position,
        string name,
        string passport)
    {
        var flyweight = flyweightFactory.GetFlyweidth(new Shared(company, position));
        flyweight.Process(new Unique(name, passport));
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        FlyweightFactory factory = new FlyweightFactory(new List<Shared>
        {
            new Shared("Microsoft", "Управляющий"),
            new Shared("Google", "Android-разработчик"),
            new Shared("Google", "Web-разработчик"),
            new Shared("Apple", "Data Scientist"),
        });

        factory.ListFlyweights();

        Console.WriteLine();

        AddSpecialistInDatabase(factory, "Google", "Web-разработчик", "Иван", "AA-12345");
        AddSpecialistInDatabase(factory, "Microsoft", "Управляющий", "Петр", "AA-12346");
        AddSpecialistInDatabase(factory, "Apple", "Web-разработчик", "Степан", "AA-12347");
        AddSpecialistInDatabase(factory, "Yandex", "Web-разработчик", "Евгений", "AA-12348");

        Console.WriteLine();

        factory.ListFlyweights();
    }
}
