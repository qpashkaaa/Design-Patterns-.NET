using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Behavioral;

/// <summary>
/// Итератор.
/// </summary>
/// <remarks>
/// Последовательный доступ к элементам агрегированного объекта без раскрытия его внутренней структуры.
/// 
/// Паттерн Итератор (Iterator) предоставляет абстрактный интерфейс для последовательного доступа ко всем элементам составного объекта без раскрытия его внутренней структуры.
/// 
/// <see cref="https://metanit.com/sharp/patterns/3.5.php"/>
/// </remarks>
[Description("Итератор: Последовательный доступ к элементам агрегированного объекта без раскрытия его внутренней структуры.\r\n\r\nПаттерн Итератор (Iterator) предоставляет абстрактный интерфейс для последовательного доступа ко всем элементам составного объекта без раскрытия его внутренней структуры.")]
internal class Iterator : IPattern
{
    #region 1-й пример.

    /// <summary>
    /// Класс стека данных.
    /// </summary>
    class DataStack
    {
        private int[] _items;
        private int _length;
        public DataStack()
        {
            _items = new int[10];
            _length = -1;
        }
        public DataStack(DataStack stack)
        {
            _items = stack._items;
            _length = stack._length;
        }

        public int[] Items { get => _items; }
        public int Length { get => _length; }

        public void Push(int value) => _items[++_length] = value;
        public int Pop() => _items[_length--];

        public static bool operator ==(DataStack left, DataStack right)
        {
            StackIterator iterator1 = new StackIterator(left);
            StackIterator iterator2 = new StackIterator(right);

            while(iterator1.IsEnd() || iterator2.IsEnd())
            {
                if (iterator1.Get() != iterator2.Get())
                {
                    break;
                }

                iterator1++;
                iterator2++;
            }

            return !iterator1.IsEnd() && !iterator2.IsEnd();
        }

        public static bool operator !=(DataStack left, DataStack right) 
        {
            StackIterator iterator1 = new StackIterator(left);
            StackIterator iterator2 = new StackIterator(right);

            while (iterator1.IsEnd() || iterator2.IsEnd())
            {
                if (iterator1.Get() != iterator2.Get())
                {
                    break;
                }

                iterator1++;
                iterator2++;
            }

            return !(iterator1.IsEnd() || iterator2.IsEnd());
        }
    }

    /// <summary>
    /// Итератр <see cref="DataStack"/>.
    /// </summary>
    class StackIterator
    {
        private DataStack _stack;
        private int _index;

        public StackIterator(DataStack stack)
        {
            _stack = stack;
            _index = 0;
        }

        public static StackIterator operator ++(StackIterator stack)
        {
            stack._index++;
            return stack;
        }

        public int Get()
        {
            if (_index < _stack.Length)
            {
                return _stack.Items[_index];
            }

            return 0;
        }

        public bool IsEnd() => _index != _stack.Length;
    }

    #endregion

    #region 2-й пример.

    /// <summary>
    /// Базовый интерфейс оператора.
    /// </summary>
    /// <typeparam name="T">Тип модели.</typeparam>
    interface IIterator<T>
    {
        bool HasNext();
        T Next();
    }

    /// <summary>
    /// Итератор для <see cref="CustomCollection{T}"/>.
    /// </summary>
    /// <typeparam name="T">Тип модели.</typeparam>
    class CustomCollectionIterator<T> : IIterator<T>
    {
        private CustomCollection<T> _collection;
        private int _position = -1;

        public CustomCollectionIterator(CustomCollection<T> collection)
        {
            _collection = collection;
        }

        public bool HasNext()
        {
            return _position < _collection.Items.Count - 1;
        }

        public T Next()
        {
            return _collection.Items[++_position];
        }
    }

    /// <summary>
    /// Кастомная коллекция.
    /// </summary>
    /// <typeparam name="T">Тип модели.</typeparam>
    class CustomCollection<T>
    {
        private List<T> _items;

        public CustomCollection(List<T> items)
        {
            _items = items;
        }

        public List<T> Items { get => _items; }

        public void SetItems(List<T> items)
        {
            _items = items;
        }

        public IIterator<T> CreateIterator()
        {
            return new CustomCollectionIterator<T>(this);
        }
    }

    #endregion

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        #region 1-й пример.

        Console.WriteLine("1-й пример: ");

        DataStack stack = new DataStack();

        for (int i = 1; i < 5; i++)
        {
            stack.Push(i);
        }

        DataStack stack2 = new DataStack(stack);

        Console.WriteLine($"Сравнение стеков после клонирования: {stack == stack2}");

        stack2.Push(10);

        Console.WriteLine($"Сравнение стеков после добавления значения: {stack == stack2}");

        #endregion

        Console.WriteLine("\n");

        #region 2-й пример.

        Console.WriteLine("2-й пример: ");

        List<int> items = new List<int>() { 1, 2, 3, 4, 5 };
        CustomCollection<int> customCollection = new CustomCollection<int>(items);

        IIterator<int> iterator = customCollection.CreateIterator();

        Console.WriteLine("Осуществляется проход по элементам коллекции с помощью кастомного итератора:");

        while (iterator.HasNext())
        {
            Console.WriteLine(iterator.Next());
        }

        #endregion
    }
}
