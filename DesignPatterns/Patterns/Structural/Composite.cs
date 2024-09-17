using DesignPatterns.Interfaces;

namespace DesignPatterns.Patterns.Structural;

/// <summary>
/// Компоновщик.
/// </summary>
/// <remarks>
/// Организация объектов в древовидную структуру для представления иерархий "часть-целое".
/// 
/// Паттерн Компоновщик (Composite) объединяет группы объектов в древовидную структуру 
/// по принципу "часть-целое" и позволяет клиенту одинаково работать как с отдельными объектами, так и с группой объектов.
/// 
/// <see cref="https://metanit.com/sharp/patterns/4.4.php"/>
/// </remarks>
internal class Composite : IPattern
{
    /// <summary>
    /// Базовый класс элемента меню.
    /// </summary>
    abstract class Item
    {
        protected string? _itemName;
        protected string? _ownerName;
        public void SetOwner(string ownerName) => _ownerName = ownerName;
        public Item(string itemName) => _itemName = itemName;
        public virtual void Add(Item subItem) { }
        public virtual void Remove(Item subItem) { }
        public abstract void Display();
    }

    /// <summary>
    /// Кликабельный элемент.
    /// </summary>
    class ClickableItem : Item
    {
        public ClickableItem(string itemName) : base(itemName) { }
        public override void Add(Item subItem)
        {
            throw new Exception("Кликабельное меню не может содержать подпункты.");
        }
        public override void Remove(Item subItem)
        {
            throw new Exception("Кликабельное меню не может содержать подпункты.");
        }
        public override void Display()
        {
            Console.WriteLine(_itemName);
        }
    }

    /// <summary>
    /// Элемент выпадающего списка.
    /// </summary>
    class DropdownItem : Item
    {
        private List<Item> _childrens;
        public DropdownItem(string itemName) : base(itemName) 
        {
            _childrens = new List<Item>();
        }
        public override void Add(Item subItem)
        {
            subItem.SetOwner(this._itemName);
            _childrens.Add(subItem);
        }
        public override void Remove(Item subItem) => _childrens.Remove(subItem);
        public override void Display()
        {
            foreach (var children in _childrens)
            {
                if (_ownerName != null)
                {
                    Console.Write(_ownerName + _itemName);
                }

                children.Display();
            }
        }
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        Item file = new DropdownItem("Файл -> ");
        Item create = new DropdownItem("Создать -> ");
        Item open = new DropdownItem("Открыть -> ");
        Item exit = new ClickableItem("Выход");

        file.Add(create);
        file.Add(open);
        file.Add(exit);

        Item project = new ClickableItem("Проект...");
        Item repository = new ClickableItem("Репозиторий...");

        create.Add(project);
        create.Add(repository);

        Item solution = new ClickableItem("Решение...");
        Item folder = new ClickableItem("Папка...");

        open.Add(solution);
        open.Add(folder);

        file.Display();

        Console.WriteLine("\n\n");

        file.Remove(create);

        file.Display();
    }
}
