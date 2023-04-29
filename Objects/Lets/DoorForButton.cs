using StudyPractic.Entites;
using StudyPractic.Objects.Abstract;

namespace StudyPractic.Objects.Lets
{
    public class DoorForButton : AbstractInteractLet
    {
        public int id = 0;
        public bool isOpen = false;
        public DoorForButton(string[] sprite, int startOfsetX, int startOfsetY) : base(sprite, startOfsetX, startOfsetY)
        {
        }
        public DoorForButton(string[] sprite, int startOfsetX, int startOfsetY, int id) : base(sprite, startOfsetX, startOfsetY)
        {
            this.id = id;
        }

        public override Direction collisium(Person person, Direction direction, Let let, List<AbstractInteractLet> interactLets, List<Item> items)
        {
            foreach (AbstractInteractLet button in interactLets) if ((button.GetType() == typeof(Button) && button.collisiumPerson && ((Button)button).id == this.id) ||
                    (button.GetType() == typeof(ButtonDel) && !button.collisiumPerson && ((ButtonDel)button).id.IndexOf(this.id) != -1))
                {
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
