using StudyPractic.Entites;
using StudyPractic.Objects.Abstract;
using StudyPractic.Templates.Abstract;

namespace StudyPractic.Objects.Lets
{
    public class DoorForKey : AbstractInteractLet
    {
        private bool open = false;
        public DoorForKey(string[] sprite, int startOfsetX, int startOfsetY) : base(sprite, startOfsetX, startOfsetY)
        {
        }
        public override Direction collisium(Person person, Direction direction, Let let, List<AbstractInteractLet> interactLets, List<Item> items)
        {
            foreach (Item key in person.Inventory) if (key.Type == "KeyForDoor")
                {
                    if (!open)
                    {
                        this.clear();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        this.show();
                        Console.ForegroundColor = ConsoleColor.Green;
                        open = true;
                    }
                    return direction;
                }
            // Проверка коллизии при толкании персонажем объект вправо
            if (direction == Direction.Right)
            {
                if (
                     Kords.ContainsKey(person.Y) && Kords[person.Y].IndexOf(person.X + 2) != -1 ||
                     Kords.ContainsKey(person.Y - 1) && Kords[person.Y - 1].IndexOf(person.X + 2) != -1 ||
                     Kords.ContainsKey(person.Y + 1) && Kords[person.Y + 1].IndexOf(person.X + 2) != -1
                 )
                {
                    return Direction.Stop;
                }
                else
                {
                    foreach (AbstractInteractLet box in interactLets)
                    {
                        if (box.GetType() == typeof(Box))
                        {
                            foreach (KeyValuePair<int, List<int>> boxKords in box.Kords)
                            {
                                if (Kords.ContainsKey(boxKords.Key))
                                {
                                    for (int j = 0; j < boxKords.Value.Count; j++)
                                    {
                                        if (Kords[boxKords.Key].IndexOf(boxKords.Value[j] + 1) != -1 && box.collisiumPerson) return Direction.Stop;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // Проверка коллизии при толкании персонажем объект влево
            else if (direction == Direction.Left)
            {
                if (
                     Kords.ContainsKey(person.Y) && Kords[person.Y].IndexOf(person.X - 2) != -1 ||
                     Kords.ContainsKey(person.Y - 1) && Kords[person.Y - 1].IndexOf(person.X - 2) != -1 ||
                     Kords.ContainsKey(person.Y + 1) && Kords[person.Y + 1].IndexOf(person.X - 2) != -1
                 )
                {
                    return Direction.Stop;
                }
                else
                {
                    foreach (AbstractInteractLet box in interactLets)
                    {
                        if (box.GetType() == typeof(Box))
                        {
                            foreach (KeyValuePair<int, List<int>> boxKords in box.Kords)
                            {
                                if (Kords.ContainsKey(boxKords.Key))
                                {
                                    for (int j = 0; j < boxKords.Value.Count; j++)
                                    {
                                        if (Kords[boxKords.Key].IndexOf(boxKords.Value[j] - 1) != -1 && box.collisiumPerson) return Direction.Stop;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // Проверка коллизии при толкании персонажем объект вверх
            else if (direction == Direction.Up)
            {
                if (
                    Kords.ContainsKey(person.Y - 2) && Kords[person.Y - 2].IndexOf(person.X + 1) != -1 ||
                    Kords.ContainsKey(person.Y - 2) && Kords[person.Y - 2].IndexOf(person.X - 1) != -1 ||
                    Kords.ContainsKey(person.Y - 2) && Kords[person.Y - 2].IndexOf(person.X) != -1
                 )
                {
                    return Direction.Stop;
                }
                else
                {
                    foreach (AbstractInteractLet box in interactLets)
                    {
                        if (box.GetType() == typeof(Box))
                        {
                            foreach (KeyValuePair<int, List<int>> boxKords in box.Kords)
                            {
                                if (Kords.ContainsKey(boxKords.Key - 1))
                                {
                                    for (int j = 0; j < boxKords.Value.Count; j++)
                                    {
                                        if (Kords[boxKords.Key - 1].IndexOf(boxKords.Value[j] - 1) != -1 && box.collisiumPerson) return Direction.Stop;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // Проверка коллизии при толкании персонажем объект вниз
            else if (direction == Direction.Down)
            {
                if (
                    Kords.ContainsKey(person.Y + 2) && Kords[person.Y + 2].IndexOf(person.X + 1) != -1 ||
                    Kords.ContainsKey(person.Y + 2) && Kords[person.Y + 2].IndexOf(person.X - 1) != -1 ||
                    Kords.ContainsKey(person.Y + 2) && Kords[person.Y + 2].IndexOf(person.X) != -1
                 )
                {
                    return Direction.Stop;
                }
                else
                {
                    foreach (AbstractInteractLet box in interactLets)
                    {
                        if (box.GetType() == typeof(Box))
                        {
                            foreach (KeyValuePair<int, List<int>> boxKords in box.Kords)
                            {
                                if (Kords.ContainsKey(boxKords.Key + 1))
                                {
                                    for (int j = 0; j < boxKords.Value.Count; j++)
                                    {
                                        if (Kords[boxKords.Key + 1].IndexOf(boxKords.Value[j]) != -1 && box.collisiumPerson) return Direction.Stop;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return direction;
        }

        public override void move(Direction personDirection)
        {
            
        }

        public override void show()
        {
            int i = 0;
            foreach (KeyValuePair<int, List<int>> kords in Kords)
            {
                for (int j = 0; j < Kords[kords.Key].Count; j++)
                {
                    Console.SetCursorPosition(kords.Value[j], kords.Key);
                    Console.Write(sprite[i][j]);
                }
                i++;
            }
        }
    }
}
