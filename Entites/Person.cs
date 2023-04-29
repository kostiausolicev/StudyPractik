using StudyPractic.Objects.Abstract;

namespace StudyPractic.Entites
{
    public class Person
    {
        // Координаты персонажа относительно левого верхнего угла
        public int X { get; set; }
        public int Y { get; set; }
        public string[] Sprite { get; set; }
        public List<Item> Inventory { get; set; }

        public Person(int x, int y, string[] sprite)
        {
            X = x;
            Y = y;
            Inventory = new List<Item> { };
            Sprite = sprite;
        }
        public void show()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(X - 1, Y - 1);
            for (int i = 0; i < 3; i++)
            {
                Console.Write(Sprite[i]);
                if (i != 2) Console.SetCursorPosition(X - 1, Y + i);
            }
            Console.ForegroundColor = ConsoleColor.Green;
        }
        public void clear()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(X - 1, Y - 1);
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(X - 1, Y + j);
            }
        }
        // Метод описывающий управление персонажем.
        public void move(Direction direction)
        {
            if (direction == Direction.Stop) return;
            clear();
            switch (direction)
            {
                case Direction.Left:
                    {
                         --X;
                        break;
                    }
                case Direction.Right:
                    {
                        ++X;
                        break;
                    }
                case Direction.Up:
                    {
                         if (Y >= 3) --Y;
                        break;
                    }
                case Direction.Down:
                    {
                        ++Y; 
                        break;
                    }
            }
            show();
        }
    }
}
