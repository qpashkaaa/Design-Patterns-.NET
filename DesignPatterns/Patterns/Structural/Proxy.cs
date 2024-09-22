using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Structural;

/// <summary>
/// Заместитель.
/// </summary>
/// <remarks>
/// Предоставление суррогата или заместителя для другого объекта.
/// 
/// Паттерн Заместитель (Proxy) предоставляет объект-заместитель, который управляет доступом к другому объекту. 
/// То есть создается объект-суррогат, который может выступать в роли другого объекта и замещать его.
/// 
/// Положительным моментом использования паттерна в клиент-серверном приложении является то, что в нем применяется кэширование ранее полученных данных и тем самым снижается количество запросов к серверу.
/// </remarks>
[Description("Заместитель: Предоставление суррогата или заместителя для другого объекта.\r\n\r\nПаттерн Заместитель (Proxy) предоставляет объект-заместитель, который управляет доступом к другому объекту. \r\nТо есть создается объект-суррогат, который может выступать в роли другого объекта и замещать его.\r\n\r\nПоложительным моментом использования паттерна в клиент-серверном приложении является то, что в нем применяется кэширование ранее полученных данных и тем самым снижается количество запросов к серверу.")]
internal class Proxy : IPattern
{
    /// <summary>
    /// Интерфейс сайта.
    /// </summary>
    interface ISite
    {
        string GetPage(int num);
    }

    /// <summary>
    /// Базовый класс сайта.
    /// </summary>
    class Site : ISite
    {
        public string GetPage(int num) => $"Страница: {num}";
    }

    /// <summary>
    /// Класс сайта-прокси, который внутри себя содержит кеш страниц, к которым уже происходило обращение.
    /// </summary>
    class SiteProxy : ISite
    {
        private ISite _site;
        private Dictionary<int, string> _cache;

        public SiteProxy(ISite site)
        {
            _site = site;
            _cache = new Dictionary<int, string>();
        }

        public string GetPage(int num)
        {
            string page;

            if (_cache.ContainsKey(num))
            {
                page = $"Закешированная страница: {_cache[num]}";
            }
            else
            {
                page = _site.GetPage(num);
                _cache.Add(num, page);
            }

            return page;
        }
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        ISite site = new SiteProxy(new Site());

        Console.WriteLine(site.GetPage(1));
        Console.WriteLine(site.GetPage(2));
        Console.WriteLine(site.GetPage(3));

        Console.WriteLine(site.GetPage(1));
        Console.WriteLine(site.GetPage(2));
        Console.WriteLine(site.GetPage(3));
        Console.WriteLine(site.GetPage(4));
    }
}
