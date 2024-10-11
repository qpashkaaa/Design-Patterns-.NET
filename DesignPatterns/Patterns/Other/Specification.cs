using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Other;

/// <summary>
/// Спецификация.
/// </summary>
/// <remarks>
/// Сочетание логических операций для проверки соответствия объекта определенным критериям.
/// 
/// Шаблон проектирования, посредством которого представление правил бизнес логики может быть преобразовано в виде цепочки объектов, связанных операциями булевой логики.
/// 
/// <see cref="https://bool.dev/blog/detail/spetsifikatsiya-pattern-proektirovaniya"/>
/// </remarks>
[Description("Спецификация: Сочетание логических операций для проверки соответствия объекта определенным критериям.\r\n\r\nШаблон проектирования, посредством которого представление правил бизнес логики может быть преобразовано в виде цепочки объектов, связанных операциями булевой логики.")]
internal class Specification : IPattern
{
    /// <summary>
    /// Интерфейс спецификации.
    /// </summary>
    /// <typeparam name="T">Тип модели.</typeparam>
    interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);
        ISpecification<T> And(ISpecification<T> other);
        ISpecification<T> Or(ISpecification<T> other);
        ISpecification<T> Not();
    }

    /// <summary>
    /// Базовый класс спецификации.
    /// </summary>
    /// <typeparam name="T">Тип модели.</typeparam>
    class SpecificationBase<T> : ISpecification<T>
    {
        private readonly Func<T, bool> _expression;

        public SpecificationBase(Func<T, bool> expression)
        {
            _expression = expression;
        }

        public bool IsSatisfiedBy(T entity) => _expression(entity);

        public ISpecification<T> And(ISpecification<T> other)
        {
            return new SpecificationBase<T>(entity => IsSatisfiedBy(entity) && other.IsSatisfiedBy(entity));
        }

        public ISpecification<T> Or(ISpecification<T> other)
        {
            return new SpecificationBase<T>(entity => IsSatisfiedBy(entity) || other.IsSatisfiedBy(entity));
        }

        public ISpecification<T> Not()
        {
            return new SpecificationBase<T>(entity => !IsSatisfiedBy(entity));
        }
    }

    /// <summary>
    /// Класс продукта.
    /// </summary>
    class Product
    {
        public decimal Price { get; set; }
        public required string Category { get; set; }
    }

    /// <summary>
    /// Спецификация для цены продукта.
    /// </summary>
    class ProductPriceSpecification : SpecificationBase<Product>
    {
        public ProductPriceSpecification(decimal minPrice, decimal maxPrice)
        : base(p => p.Price >= minPrice && p.Price <= maxPrice) { }
    }

    /// <summary>
    /// Спецификация для категории продукта.
    /// </summary>
    class ProductCategorySpecification : SpecificationBase<Product>
    {
        public ProductCategorySpecification(string category)
            : base(p => p.Category == category) { }
    }

    /// <summary>
    /// Фильтр для продуктов.
    /// </summary>
    class ProductFilter
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> products, ISpecification<Product> specification)
        {
            return products.Where(specification.IsSatisfiedBy);
        }
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        Console.WriteLine("Список продуктов: ");

        var products = new List<Product>
        {
            new Product { Price = 100, Category = "Electronics" },
            new Product { Price = 200, Category = "Clothing" },
            new Product { Price = 150, Category = "Electronics" },
        };

        foreach (var product in products)
        {
            Console.WriteLine($"\tPrice: {product.Price}, Category: {product.Category}");
        }

        var priceSpecification = new ProductPriceSpecification(100, 200);
        var categorySpecification = new ProductCategorySpecification("Electronics");

        var specification = priceSpecification.And(categorySpecification);

        Console.WriteLine();
        Console.WriteLine($"Добавлена спецификация: цена должна находиться в диапазоне от 100 до 200, категория: \"Electronics\".");
        Console.WriteLine($"Производится фильтрация списка согласно спецификации.");
        Console.WriteLine();

        var productFilter = new ProductFilter();
        var filteredProducts = productFilter.Filter(products, specification);

        Console.WriteLine("Список продуктов после фильтрации по заданной спецификации: ");

        foreach (var product in filteredProducts)
        {
            Console.WriteLine($"\tPrice: {product.Price}, Category: {product.Category}");
        }
    }
}
