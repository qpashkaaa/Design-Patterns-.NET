using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Behavioral;

/// <summary>
/// Команда.
/// </summary>
/// <remarks>
/// Превращение запросов в объекты, позволяя передавать их как аргументы.
/// 
/// Паттерн "Команда" (Command) позволяет инкапсулировать запрос на выполнение определенного действия в виде отдельного объекта. Этот объект запроса на действие и называется командой. При этом объекты, инициирующие запросы на выполнение действия, отделяются от объектов, которые выполняют это действие.
/// 
/// Команды могут использовать параметры, которые передают ассоциированную с командой информацию.Кроме того, команды могут ставиться в очередь и также могут быть отменены.
/// 
/// <see cref="https://metanit.com/sharp/patterns/3.3.php"/>
/// </remarks>
[Description("Команда: Превращение запросов в объекты, позволяя передавать их как аргументы.\r\n\r\nПаттерн \"Команда\" (Command) позволяет инкапсулировать запрос на выполнение определенного действия в виде отдельного объекта. Этот объект запроса на действие и называется командой. При этом объекты, инициирующие запросы на выполнение действия, отделяются от объектов, которые выполняют это действие.\r\n\r\nКоманды могут использовать параметры, которые передают ассоциированную с командой информацию.Кроме того, команды могут ставиться в очередь и также могут быть отменены.")]
internal class Command : IPattern
{
    /// <summary>
    /// Базовый интерфейс команды.ы
    /// </summary>
    interface ICommand
    {
        void Positive();
        void Negative();
    }

    /// <summary>
    /// Класс конвеера.
    /// </summary>
    class Conveyor
    {
        public void On() => Console.WriteLine("Конвеер запущен.");
        public void Off() => Console.WriteLine("Конвеер остановлен.");
        public void SpeedIncrease() => Console.WriteLine("Скорость конвеера увеличена.");
        public void SpeedDecrease() => Console.WriteLine("Скорость конвеера уменьшена.");
    }

    /// <summary>
    /// Команды, отвечающие за работу конвеера.
    /// </summary>
    class ConveyorWorkCommand : ICommand
    {
        private Conveyor _conveyor;
        public ConveyorWorkCommand(Conveyor conveyor) => _conveyor = conveyor;
        public void Positive() => _conveyor.On();
        public void Negative() => _conveyor.Off();
    }

    /// <summary>
    /// Команды, отвечающие за регулировку конвеера.
    /// </summary>
    class ConveyorAdjustCommand : ICommand
    {
        private Conveyor _conveyor;
        public ConveyorAdjustCommand(Conveyor conveyor) => _conveyor = conveyor;
        public void Positive() => _conveyor.SpeedIncrease();
        public void Negative() => _conveyor.SpeedDecrease();
    }

    /// <summary>
    /// Исполнитель команд.
    /// </summary>
    class CommandHandler
    {
        private Dictionary<int, ICommand> _commands;
        private Stack<ICommand> _historyCommands;

        public CommandHandler()
        {
            _commands = new Dictionary<int, ICommand>();
            _historyCommands = new Stack<ICommand>();
        }

        public void SetCommand(int button, ICommand command) => _commands[button] = command;
        public void PressOn(int button)
        {
            _commands[button].Positive();
            _historyCommands.Push(_commands[button]);
        }
        public void PressOff()
        {
            if (_historyCommands.Count != 0)
            {
                _historyCommands.Pop().Negative();
            }
        }
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        Conveyor conveyor = new Conveyor();
        CommandHandler handler = new CommandHandler();

        handler.SetCommand(0, new ConveyorWorkCommand(conveyor));
        handler.SetCommand(1, new ConveyorAdjustCommand(conveyor));

        handler.PressOn(0);
        handler.PressOn(1);

        handler.PressOff();
        handler.PressOff();
    }
}
