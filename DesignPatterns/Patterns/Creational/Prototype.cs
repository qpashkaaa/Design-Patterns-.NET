using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Creational;

/// <summary>
/// Прототип.
/// </summary>
/// <remarks>
/// Создание новых объектов путем копирования существующих экземпляров (клонирование).
/// 
/// Паттерн Прототип (Prototype) позволяет создавать объекты на основе уже ранее созданных объектов-прототипов. 
/// То есть по сути данный паттерн предлагает технику клонирования объектов.
/// 
/// <see cref="https://metanit.com/sharp/patterns/2.4.php"/>
/// </remarks>
[Description("Прототип: Создание новых объектов путем копирования существующих экземпляров (клонирование).\r\n\r\nПаттерн Прототип (Prototype) позволяет создавать объекты на основе уже ранее созданных объектов-прототипов. \r\nТо есть по сути данный паттерн предлагает технику клонирования объектов.")]
internal class Prototype : IPattern
{
    /// <summary>
    /// Интерфейс прототипа.
    /// </summary>
    interface IAnimal
    {
        void SetName(string name);
        string GetName();
        IAnimal Clone();
    }

    /// <summary>
    /// Класс прототипа с методом Clone, позволяющий создать копию объекта.
    /// </summary>
    class Sheep : IAnimal
    {
        private string _name;
        public Sheep() { }
        public Sheep(Sheep prototype) => _name = prototype._name;
        public void SetName(string name) => _name = name;
        public string GetName() => _name;
        public IAnimal Clone() => new Sheep(this);
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        IAnimal sheep = new Sheep();
        sheep.SetName("Dolly");

        IAnimal sheepClone = sheep.Clone();

        Console.WriteLine($"Имя основного экземпляра (прототипа): {sheep.GetName()}");
        Console.WriteLine($"Имя клона: {sheepClone.GetName()}");
    }
}
