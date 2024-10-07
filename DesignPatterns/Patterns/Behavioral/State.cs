using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Behavioral;

/// <summary>
/// Состояние.
/// </summary>
/// <remarks>
/// Изменение поведения объекта при изменении его состояния.
/// 
/// Состояние (State) - шаблон проектирования, который позволяет объекту изменять свое поведение в зависимости от внутреннего состояния.
/// 
/// <see cref="https://metanit.com/sharp/patterns/3.6.php"/>.
/// </remarks>
[Description("Состояние: Изменение поведения объекта при изменении его состояния.\r\n\r\nСостояние (State) - шаблон проектирования, который позволяет объекту изменять свое поведение в зависимости от внутреннего состояния.")]
internal class State : IPattern
{
    /// <summary>
    /// Базовый класс состояния.
    /// </summary>
    interface ITrafficLightState
    {
        void NextState(TrafficLight trafficLight);
        void PreviousState(TrafficLight trafficLight);
    }

    /// <summary>
    /// Класс светофора.
    /// </summary>
    class TrafficLight
    {
        public ITrafficLightState State { get; set; }
        public TrafficLight(ITrafficLightState state) => State = state;

        public void NextState()
        {
            State.NextState(this);
        }
        public void PreviousState() 
        {
            State.PreviousState(this);
        }
    }

    /// <summary>
    /// Класс зеленого состояния светофора (зеленый цвет).
    /// </summary>
    class GreenState : ITrafficLightState
    {
        public void NextState(TrafficLight trafficLight)
        {
            Console.WriteLine("Цвет светофора изменился с зеленого на желтый.");
            trafficLight.State = new YellowState();
        }
        public void PreviousState(TrafficLight trafficLight)
        {
            Console.WriteLine("Зеленый цвет светофора.");
        }
    }

    /// <summary>
    /// Класс желтого состояния светофора (желтый цвет).
    /// </summary>
    class YellowState : ITrafficLightState
    {
        public void NextState(TrafficLight trafficLight)
        {
            Console.WriteLine("Цвет светофора изменился с желтого на красный.");
            trafficLight.State = new RedState();
        }
        public void PreviousState(TrafficLight trafficLight)
        {
            Console.WriteLine("Цвет светофора изменился с желтого на зеленый.");
            trafficLight.State = new GreenState();
        }
    }

    /// <summary>
    /// Класс красного состояния светофора (красный цвет).
    /// </summary>
    class RedState : ITrafficLightState
    {
        public void NextState(TrafficLight trafficLight)
        {
            Console.WriteLine("Красный цвет светофора.");
        }
        public void PreviousState(TrafficLight trafficLight)
        {
            Console.WriteLine("Цвет светофора изменился с красного на желтый.");
            trafficLight.State = new YellowState();
        }
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        TrafficLight trafficLight = new TrafficLight(new YellowState());

        trafficLight.NextState();
        trafficLight.NextState();

        trafficLight.PreviousState();
        trafficLight.PreviousState();
        trafficLight.PreviousState();
    }
}
