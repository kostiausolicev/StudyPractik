using StudyPractic.Entites;
using StudyPractic.Objects.Abstract;

namespace StudyPractic.Objects.Lets
{
    internal class RetryBox : Abstract.AbstractInteractLet
    {
        public RetryBox(string[] sprite, int startOfsetX, int startOfsetY) : base(sprite, startOfsetX, startOfsetY)
        {
        }

        // Проверка касания персонажем
        public override Direction collisium(Person person, Direction direction, Let let, List<AbstractInteractLet> interactLets, List<Item> items)
        {
            if (((
                     Kords.ContainsKey(person.Y) && Kords[person.Y].IndexOf(person.X + 2) != -1 ||
                     Kords.ContainsKey(person.Y - 1) && Kords[person.Y - 1].IndexOf(person.X + 2) != -1 ||
                     Kords.ContainsKey(person.Y + 1) && Kords[person.Y + 1].IndexOf(person.X + 2) != -1
                 ) && direction == Direction.Right) ||
                 (
                     Kords.ContainsKey(person.Y) && Kords[person.Y].IndexOf(person.X - 2) != -1 ||
                     Kords.ContainsKey(person.Y - 1) && Kords[person.Y - 1].IndexOf(person.X - 2) != -1 ||
                     Kords.ContainsKey(person.Y + 1) && Kords[person.Y + 1].IndexOf(person.X - 2) != -1
                 ) && direction == Direction.Left ||
                 (
                    Kords.ContainsKey(person.Y - 2) && Kords[person.Y - 2].IndexOf(person.X + 1) != -1 ||
                    Kords.ContainsKey(person.Y - 2) && Kords[person.Y - 2].IndexOf(person.X - 1) != -1 ||
                    Kords.ContainsKey(person.Y - 2) && Kords[person.Y - 2].IndexOf(person.X) != -1
                 ) && direction == Direction.Up ||
                 (
                    Kords.ContainsKey(person.Y + 2) && Kords[person.Y + 2].IndexOf(person.X + 1) != -1 ||
                    Kords.ContainsKey(person.Y + 2) && Kords[person.Y + 2].IndexOf(person.X - 1) != -1 ||
                    Kords.ContainsKey(person.Y + 2) && Kords[person.Y + 2].IndexOf(person.X) != -1
                 ) && direction == Direction.Down)
            {
                collisiumPerson = true;
                return Direction.Stop;
            }
           return direction;
        }
        // RetryBox не двигается
        public override void move(Direction personDirection)
        {
        }

        public override void show()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            int y = 0, x = 0;
            foreach (KeyValuePair<int, List<int>> kords in Kords)
            {
                for (int j = 0; j < Kords[kords.Key].Count; j++)
                {
                    Console.SetCursorPosition(kords.Value[j], kords.Key);
                    Console.Write(sprite[y][x++]);
                }
                y++;
                x = 0;
            }
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
