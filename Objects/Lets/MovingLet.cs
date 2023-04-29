using StudyPractic.Entites;
using StudyPractic.Objects.Abstract;

namespace StudyPractic.Objects.Lets
{
    internal class MovingLet : AbstractInteractLet
    {
        public Direction direction { get; set; }
        private Direction previosDirection { get; set; }
        public MovingLet(string[] sprite, int startOfsetX, int startOfsetY, Direction direction) : base(sprite, startOfsetX, startOfsetY)
        {
            this.direction = direction;
            this.previosDirection = direction;
        }
        public override Direction collisium(Person person, Direction direction, Let let, List<AbstractInteractLet> interactLets, List<Item> items)
        {
            if (this.direction == Direction.Left || this.direction == Direction.Right)
            {
                if (
                     Kords.ContainsKey(person.Y) && Kords[person.Y].IndexOf(person.X - 2 * (int)this.direction / 2) != -1 ||
                     Kords.ContainsKey(person.Y - 1) && Kords[person.Y - 1].IndexOf(person.X - 2 * (int)this.direction / 2) != -1 ||
                     Kords.ContainsKey(person.Y + 1) && Kords[person.Y + 1].IndexOf(person.X - 2 * (int)this.direction / 2) != -1
                 )
                {
                    collisiumPerson = true;
                }

                foreach (KeyValuePair<int, List<int>> kordsLet in let.kords)
                {
                    if (Kords.ContainsKey(kordsLet.Key + 1))
                    {
                        for (int j = 0; j < kordsLet.Value.Count; j++)
                        {
                            if (Kords[kordsLet.Key + 1].IndexOf(kordsLet.Value[j] + ((int)this.direction > 0 ? -2 : 0)) != -1)
                            {
                                this.direction = (Direction)(-1 * (int)this.direction);
                                collisiumPerson = false;
                                return direction;
                            }
                        }
                    }
                }
                foreach (AbstractInteractLet interLet in interactLets) 
                {
                    foreach (KeyValuePair<int, List<int>> kordsInterLet in interLet.Kords)
                    {
                        if (Kords.ContainsKey(kordsInterLet.Key))
                        {
                            for (int j = 0; j < kordsInterLet.Value.Count; j++)
                            {
                                if (Kords[kordsInterLet.Key].IndexOf(kordsInterLet.Value[j] - 1 * (int)this.direction / 2) != -1)
                                {
                                    if (interLet.GetType() == typeof(Button))
                                    {
                                        interLet.collisiumPerson = true;
                                        collisiumPerson = false;
                                        return direction;
                                    }
                                    this.direction = (Direction)(-1 * (int)this.direction);
                                    collisiumPerson = false;
                                    return direction;
                                }
                            }
                        }
                    }
                }
                if (collisiumPerson) return this.direction;
                else return direction;
            }
            else if (this.direction == Direction.Down || this.direction == Direction.Up)
            {
                if (
                     Kords.ContainsKey(person.Y - 2 * (int)this.direction) && Kords[person.Y - 2 * (int)this.direction].IndexOf(person.X) != -1 ||
                     Kords.ContainsKey(person.Y - 2 * (int)this.direction) && Kords[person.Y - 2 * (int)this.direction].IndexOf(person.X - 1) != -1 ||
                     Kords.ContainsKey(person.Y - 2 * (int)this.direction) && Kords[person.Y - 2 * (int)this.direction].IndexOf(person.X  + 1) != -1
                 )
                {
                    collisiumPerson = true;
                }
                foreach (KeyValuePair<int, List<int>> kordsLet in let.kords)
                {
                    if (Kords.ContainsKey(kordsLet.Key + ((int)this.direction > 0 ? -2 : 0) + 2))
                    {
                        for (int j = 0; j < kordsLet.Value.Count; j++)
                        {
                            if (Kords[kordsLet.Key + ((int)this.direction > 0 ? -2 : 0) + 2].IndexOf(kordsLet.Value[j] - 1) != -1)
                            {
                                this.direction = (Direction)(-1 * (int)this.direction);
                                collisiumPerson = false;
                                return direction;
                            }
                        }
                    }
                }

                foreach (AbstractInteractLet interLet in interactLets)
                {
                    foreach (KeyValuePair<int, List<int>> kordsInterLet in interLet.Kords)
                    {
                        if (Kords.ContainsKey(kordsInterLet.Key - 1 * (int)this.direction))
                        {
                            for (int j = 0; j < kordsInterLet.Value.Count; j++)
                            {
                                if (Kords[kordsInterLet.Key - 1 * (int)this.direction].IndexOf(kordsInterLet.Value[j]) != -1)
                                {
                                    if (interLet.GetType() == typeof(Button))
                                    {
                                        interLet.collisiumPerson = true;
                                        collisiumPerson = false;
                                        return direction;
                                    }
                                    this.direction = (Direction)(-1 * (int)this.direction);
                                    collisiumPerson = false;
                                    return direction;
                                }
                            }
                        }
                    }
                }
                foreach (KeyValuePair<int, List<int>> kords in Kords)
                {
                    if (kords.Value.IndexOf(2) != -1 || kords.Value.IndexOf(137) != -1)
                    {
                        this.direction = (Direction)(-1 * (int)this.direction);
                        collisiumPerson = false;
                        return Direction.Stop;
                    }
                }
                if (collisiumPerson) return this.direction;
                else return direction;
            }
            return direction;
        }
        public override void move(Direction personDirection)
        {
            if (previosDirection != direction)
            {
                int buffer = (int)direction;
                previosDirection = (Direction)buffer;
                return;
            }
            clear();
            if (personDirection == Direction.Stop && collisiumPerson)
            {
                this.direction = (Direction)(-1 * (int)this.direction);
                collisiumPerson = false;
            }
            if (direction == Direction.Left || direction == Direction.Right)
            {
                foreach (KeyValuePair<int, List<int>> kords in Kords)
                {
                    for (int j = 0; j < Kords[kords.Key].Count; j++)
                    {
                        Kords[kords.Key][j] += (int)direction / 2;
                    }
                }
            }
            else if (direction == Direction.Up || direction == Direction.Down)
            {
                Dictionary<int, List<int>> kords2 = new Dictionary<int, List<int>>();
                foreach (KeyValuePair<int, List<int>> kords in Kords)
                {
                    kords2.Add(kords.Key + (int)direction, kords.Value);
                }
                Kords = kords2;
            }
            show();
        }

        public override void show()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (KeyValuePair<int, List<int>> kords in Kords)
            {
                for (int j = 0; j < Kords[kords.Key].Count; j++)
                {
                    Console.SetCursorPosition(kords.Value[j], kords.Key);
                    Console.Write("#");
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
