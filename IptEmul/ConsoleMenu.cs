using System;
using System.Collections.Generic;

namespace IptEmul
{
    /// <summary>Меню в консоли.</summary>
    internal class ConsoleMenu
    {
        #region Свойства

        private readonly ConsoleColor _itemColor;
        private readonly List<ConsoleMenuItem> _items;
        private readonly ConsoleColor _selectionColor;

        private int _top; //Положение первой строки меню

        private int SelectedIndex { get; set; }

        #endregion

        public ConsoleMenu(params ConsoleMenuItem[] items)
        {
            _items = new List<ConsoleMenuItem>();
            _items.AddRange(items);
            _itemColor = ConsoleColor.White;
            _selectionColor = ConsoleColor.Blue;
        }

        private void MoveDown()
        {
            SelectedIndex = SelectedIndex == _items.Count - 1 ? 0 : SelectedIndex + 1;
            Console.SetCursorPosition(0, _top);
            Show(false);
        }

        private void MoveUp()
        {
            SelectedIndex = SelectedIndex == 0 ? _items.Count - 1 : SelectedIndex - 1;
            Console.SetCursorPosition(0, _top);
            Show(false);
        }

        /// <summary>Показать меню.</summary>
        /// <param name="addEmptyLineBefore">Добавлять ли пустую строку перед меню.</param>
        public void Show(bool addEmptyLineBefore = true)
        {
            _top = Console.CursorTop;
            if (addEmptyLineBefore)
            {
                Console.WriteLine();
                _top++;
            }
            Console.ForegroundColor = _itemColor;
            for (int i = 0; i < _items.Count; i++)
            {
                if (i == SelectedIndex)
                {
                    Console.BackgroundColor = _selectionColor;
                }
                else
                {
                    Console.ResetColor();
                    Console.ForegroundColor = _itemColor;
                }
                Console.WriteLine("{0}. {1}", i + 1, _items[i].Title);
            }
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ResetColor();
            WaitForInput();
        }

        private void WaitForInput()
        {
            ConsoleKeyInfo cki = Console.ReadKey();
            switch (cki.Key)
            {
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;
                case ConsoleKey.Enter:
                    _items[SelectedIndex].Execute();
                    Show();
                    break;
            }
        }
    }
}