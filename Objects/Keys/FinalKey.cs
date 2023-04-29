using StudyPractic.Entites;
using StudyPractic.Objects.Abstract;

namespace StudyPractic.Objects.Keys
{
    class FinalKey : Item
    {
        public override string Type => "FinalKey";
        public override string[] Sprite => new string[]
        {
            @"\-/",
            @" |_",
            @" |-"
        };
        public override int X { get; set; }
        public override int Y { get; set; }

        public FinalKey(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override void clear()
        {
            for (int i = 0; i < Sprite.Length; i++)
            {
                Console.SetCursorPosition(X - 1, Y - 1 + i);
                for (int j = 0; j < Sprite[i].Length; j++)
                {
                    Console.Write(" ");
                }
            }
        }

        public override void show()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < Sprite.Length; i++)
            {
                Console.SetCursorPosition(X - 1, Y - 1 + i);
                for (int j = 0; j < Sprite[i].Length; j++)
                {
                    Console.Write(Sprite[i][j]);
                }
            }
        }

        public override void collisium(Person person)
        {
            if (
                person.Y == Y && person.X == X
            )
            {
                person.Inventory.Add(this);
                X = 16 + 34 * (person.Inventory.Count - 1);
                Y = 45;
                person.show();
                show();

            }
            else if (!person.Inventory.Contains(this))
            {
                person.clear();
                show();
                person.show();
            }
        }
    }
}
