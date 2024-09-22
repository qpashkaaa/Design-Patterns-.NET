using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Creational;

/// <summary>
/// Строитель.
/// </summary>
/// <remarks>
/// Поэтапное конструирование сложного объекта.
/// 
/// Паттерн проектирования, который инкапсулирует создание объекта и позволяет разделить его на различные этапы.
/// Он отделяет конструирование сложного объекта от его представления так, что в результате одного и того же процесса конструирования могут получаться разные представления.
/// 
/// <see cref="https://metanit.com/sharp/patterns/2.5.php"/>
/// </remarks>
[Description("Строитель: Поэтапное конструирование сложного объекта.\r\n\r\nПаттерн проектирования, который инкапсулирует создание объекта и позволяет разделить его на различные этапы.\r\nОн отделяет конструирование сложного объекта от его представления так, что в результате одного и того же процесса конструирования могут получаться разные представления.")]
internal class Builder : IPattern
{
    /// <summary>
    /// Модель телефона.
    /// </summary>
    class Phone
    {
        string _data;
        public Phone() => _data = "";
        public string AboutPhone() => _data;
        public void AppendData(string str) => _data += str;
    }

    /// <summary>
    /// Интерфейс разработчика.
    /// </summary>
    interface IDeveloper
    {
        void CreateDisplay();
        void CreateBox();
        void InstallSystem();
        Phone GetPhone();
    }

    /// <summary>
    /// Модель Android-разработчика.
    /// </summary>
    class AndroidDeveloper : IDeveloper
    {
        private Phone _phone;
        public AndroidDeveloper() => _phone = new Phone();
        public void CreateDisplay() => _phone.AppendData("Произведен дисплей Android; ");
        public void CreateBox() => _phone.AppendData("Произведен корпус Android; ");
        public void InstallSystem() => _phone.AppendData("Установлена система Android; ");
        public Phone GetPhone() => _phone;
    }

    /// <summary>
    /// Модель Iphone-разработчика.
    /// </summary>
    class IphoneDeveloper : IDeveloper
    {
        private Phone _phone;
        public IphoneDeveloper() => _phone = new Phone();
        public void CreateDisplay() => _phone.AppendData("Произведен дисплей Iphone; ");
        public void CreateBox() => _phone.AppendData("Произведен корпус Iphone; ");
        public void InstallSystem() => _phone.AppendData("Установлена система Iphone; ");
        public Phone GetPhone() => _phone;
    }

    /// <summary>
    /// Модель заказа.
    /// 
    /// Сама модель-строитель, которая инкапсулирует создание телефона, предоставляя пользователю простой интерфейс.
    /// </summary>
    class Order
    {
        private IDeveloper _developer;
        public Order(IDeveloper developer) => _developer = developer;
        public void SetDeveloper(IDeveloper developer) => _developer = developer;

        public Phone CreatePhoneWithoutSistem()
        {
            _developer.CreateBox();
            _developer.CreateDisplay();
            return _developer.GetPhone();
        }

        public Phone CreateFullPhone()
        {
            _developer.CreateDisplay();
            _developer.CreateBox();
            _developer.InstallSystem();
            return _developer.GetPhone();
        }
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        IDeveloper? developer = null;
        Phone? phone = null;
        Order? order = null;

        developer = new AndroidDeveloper();
        order = new Order(developer);
        phone = order.CreateFullPhone();
        Console.WriteLine($"{phone.AboutPhone()} / Developer: {developer.GetType().Name}");


        developer = new IphoneDeveloper();
        order.SetDeveloper(developer);
        phone = order.CreatePhoneWithoutSistem();
        Console.WriteLine($"{phone.AboutPhone()} / Developer: {developer.GetType().Name}");
    }
}
