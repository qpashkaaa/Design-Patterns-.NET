using DesignPatterns.Interfaces;

namespace DesignPatterns.Patterns.Creational;

/// <summary>
/// Абстрактная фабрика.
/// </summary>
/// <remarks>
/// Создание семейств связанных или зависимых объектов без указания их конкретных классов.
/// 
/// Порождающий паттерн, который предоставляет интерфейс для создания семейств взаимосвязанных объектов 
/// с определенными интерфейсами без указания конкретных типов данных объектов.
/// Шаблон применяется в случаях, когда программа должна быть не зависимой от процессов и типов создаваемых новых объектов, 
/// а также, когда необходимо создавать группы взаимосвязанных объектов.
/// 
/// <see cref="https://metanit.com/sharp/patterns/2.2.php"/>
/// </remarks>
internal class AbstractFactory : IPattern
{
    /// <summary>
    /// Базовый интерфейс двигателя.
    /// </summary>
    interface IEngine
    {
        void Release();
    }

    /// <summary>
    /// Класс японского двигателя.
    /// </summary>
    class JapaneseEngine : IEngine
    {
        public void Release() => Console.WriteLine("Японский двигатель.");
    }

    /// <summary>
    /// Класс российского двигателя.
    /// </summary>
    class RussianEngine : IEngine
    {
        public void Release() => Console.WriteLine("Российский двигатель.");
    }

    /// <summary>
    /// Базовый интерфейс автомобиля.
    /// </summary>
    interface ICar
    {
        void Release(IEngine engine);
    }

    /// <summary>
    /// Класс японского автомобиля.
    /// </summary>
    class JapaneseCar : ICar
    {
        public void Release(IEngine engine)
        {
            Console.WriteLine("Собрали японский автомобиль: ");
            engine.Release();
        }
    }

    /// <summary>
    /// Класс российского автомобиля.
    /// </summary>
    class RussianCar : ICar
    {
        public void Release(IEngine engine)
        {
            Console.WriteLine("Собрали российский автомобиль: ");
            engine.Release();
        }
    }

    /// <summary>
    /// Базовый интерфейс абстрактной фабрики.
    /// </summary>
    interface IFactory
    {
        IEngine CreateEngine();
        ICar CreateCar();
    }

    /// <summary>
    /// Класс японской фабрики.
    /// </summary>
    class JapaneseFactory : IFactory
    {
        public IEngine CreateEngine() => new JapaneseEngine();
        public ICar CreateCar() => new JapaneseCar();
    }

    /// <summary>
    /// Класс российской фабрики.
    /// </summary>
    class RussianFactory : IFactory
    {
        public IEngine CreateEngine() => new RussianEngine();
        public ICar CreateCar() => new RussianCar();
    }

    /// <summary>
    /// Класс фабрики.
    /// </summary>
    class Factory
    {
        private IEngine _engine;
        private ICar _car;

        public Factory(IFactory factory)
        {
            _engine = factory.CreateEngine();
            _car = factory.CreateCar();
        }

        public void ReleaseCar()
        {
            _car.Release(_engine);
        }
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        Factory? factory = null;

        factory = new Factory(new JapaneseFactory());
        factory.ReleaseCar();

        factory = new Factory(new RussianFactory());
        factory.ReleaseCar();
    }
}
