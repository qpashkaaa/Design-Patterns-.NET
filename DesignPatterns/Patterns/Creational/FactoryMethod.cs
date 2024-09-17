using DesignPatterns.Interfaces;

namespace DesignPatterns.Patterns.Creational;

/// <summary>
/// Фабричный метод.
/// </summary>
/// <remarks>
/// Определение интерфейса для создания объекта, но позволение подклассам изменять тип создаваемого объекта.
/// 
/// Это порождающий паттерн, который определяет интерфейс для создания объектов некоторого класса, 
/// но непосредственное решение о том, объект какого класса создавать происходит в подклассах. 
/// То есть паттерн предполагает, что базовый класс делегирует создание объектов классам-наследникам.
/// 
/// <see cref="https://metanit.com/sharp/patterns/2.1.php"/>
/// </remarks>
internal class FactoryMethod : IPattern
{
    /// <summary>
    /// Базовый интерфейс производства.
    /// </summary>
    interface IProduction
    {
        void Release();
    }

    /// <summary>
    /// Производство легковых автомобилей.
    /// </summary>
    class CarProduction : IProduction
    {
        public void Release() => Console.WriteLine($"Выпущен легковой автомобиль.");
    }

    /// <summary>
    /// Производство грузовых автомобилей.
    /// </summary>
    class TruckProduction : IProduction
    {
        public void Release() => Console.WriteLine("Выпущен грузовой автомобиль.");
    }

    /// <summary>
    /// Базовый интерфейс мастерской.
    /// </summary>
    interface IWorkshop
    {
        IProduction Create();
    }

    /// <summary>
    /// Мастерская по производству легковых автомобилей.
    /// </summary>
    class CarWorkshop : IWorkshop
    {
        public IProduction Create() => new CarProduction();
    }

    /// <summary>
    /// Мастерская по производству грузовых автомобилей.
    /// </summary>
    class TruckWorkshop : IWorkshop
    {
        public IProduction Create() => new TruckProduction();
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        IWorkshop? workshop = null;
        IProduction? production = null;

        workshop = new CarWorkshop();
        production = workshop.Create();
        Console.WriteLine($"IWorkshopType: {workshop.GetType().Name}, IProductionType: {production.GetType().Name}");
        production.Release();

        workshop = new TruckWorkshop();
        production = workshop.Create();
        Console.WriteLine($"IWorkshopType: {workshop.GetType().Name}, IProductionType: {production.GetType().Name}");
        production.Release();
    }
}
