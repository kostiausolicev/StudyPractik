using StudyPractic.Entites;
using StudyPractic.Objects.Abstract;

namespace StudyPractic.Objects.Lets
{
    public class Box : AbstractInteractLet
    {
        public Box(string[] sprite, int startOfsetX, int startOfsetY) : base(sprite, startOfsetX, startOfsetY)
        {
        }

        public override Direction collisium(Person person, Direction direction, Let let, List<AbstractInteractLet> interactLets, List<Item> items)
        {
            // Проверка коллизии при толкании персонажем объект вправо
            if ((
                     Kords.ContainsKey(person.Y) && Kords[person.Y].IndexOf(person.X + 2) != -1 ||
                     Kords.ContainsKey(person.Y - 1) && Kords[person.Y - 1].IndexOf(person.X + 2) != -1 ||
                     Kords.ContainsKey(person.Y + 1) && Kords[person.Y + 1].IndexOf(person.X + 2) != -1
                 ) && direction == Direction.Right)
            {
                collisiumPerson = true;
                return collisiumRight(person, direction, let, interactLets, items);
            }
            // Аналогично для остальных направлений
            // Проверка коллизии при толкании персонажем объект влево
            else if ((
                     Kords.ContainsKey(person.Y) && Kords[person.Y].IndexOf(person.X - 2) != -1 ||
                     Kords.ContainsKey(person.Y - 1) && Kords[person.Y - 1].IndexOf(person.X - 2) != -1 ||
                     Kords.ContainsKey(person.Y + 1) && Kords[person.Y + 1].IndexOf(person.X - 2) != -1
                 ) && direction == Direction.Left)
            {
                collisiumPerson = true;
                return collisiumLeft(person, direction, let, interactLets, items);
            }
            // Проверка коллизии при толкании персонажем объект вверх
            else if ((
                    Kords.ContainsKey(person.Y - 2) && Kords[person.Y - 2].IndexOf(person.X + 1) != -1 ||
                    Kords.ContainsKey(person.Y - 2) && Kords[person.Y - 2].IndexOf(person.X - 1) != -1 ||
                    Kords.ContainsKey(person.Y - 2) && Kords[person.Y - 2].IndexOf(person.X) != -1
                 ) && direction == Direction.Up)
            {
                collisiumPerson = true;
                return collisiumUp(person, direction, let, interactLets, items);
            }
            // Проверка коллизии при толкании персонажем объект вниз
            else if ((
                    Kords.ContainsKey(person.Y + 2) && Kords[person.Y + 2].IndexOf(person.X + 1) != -1 ||
                    Kords.ContainsKey(person.Y + 2) && Kords[person.Y + 2].IndexOf(person.X - 1) != -1 ||
                    Kords.ContainsKey(person.Y + 2) && Kords[person.Y + 2].IndexOf(person.X) != -1
                 ) && direction == Direction.Down)
            {
                collisiumPerson = true;
                return collisiumDown(person, direction, let, interactLets, items);
            }
            return direction;
        }
        // Блок методов, в которых описана логика для коллизий коробки в зависимости от направления
        private Direction collisiumRight(Person person, Direction direction, Let let, List<AbstractInteractLet> interactLets, List<Item> items)
        {
            foreach (KeyValuePair<int, List<int>> kords in Kords)
            {
                for (int j = 0; j < Kords[kords.Key].Count; j++)
                {
                    // Проверка на касание препятствий
                    if (let.kords.ContainsKey(kords.Key - 2) && let.kords[kords.Key - 1].IndexOf(kords.Value[j] + 2) != -1) return Direction.Stop;
                }
            }
            // Проверка на касание с другими коробками
            foreach (AbstractInteractLet interLet in interactLets)
            {
                // Проверяем только движущиеся короьки, retryBox и обычные коробки
                if (interLet.GetType() != typeof(Box) && interLet.GetType() != typeof(MovingLet) && interLet.GetType() != typeof(RetryBox)) continue;
                foreach (KeyValuePair<int, List<int>> boxKords in interLet.Kords)
                {
                    // Проверяем если наше абсткратное препятствие касается касается того для которого проверяем коллизии
                    if (Kords.ContainsKey(boxKords.Key))
                    {
                        for (int j = 0; j < boxKords.Value.Count; j++)
                        {
                            if (Kords[boxKords.Key].IndexOf(boxKords.Value[j] - 1) != -1)
                            {
                                // Если текущая коробка касается другой коробки -> другая коробка тоже начинает двигаться
                                if (interLet.GetType() == typeof(Box))
                                {
                                    interLet.collisiumPerson = true;
                                    break;
                                }
                                // Иначе, если мы касается не коробки, мы останавливаемся 
                                else return Direction.Stop;
                            }
                        }
                    }
                }
                // Проверяем на касание абстрактной коробки статических препятствий let
                foreach (KeyValuePair<int, List<int>> boxKords in interLet.Kords)
                {
                    // Проверяем на касание статического препятсвия
                    if (let.kords.ContainsKey(boxKords.Key - 1))
                    {
                        for (int j = 0; j < boxKords.Value.Count; j++)
                        {
                            // Если абстрактная коробка касается препятствия и при этом ее толкает персонаж через текущую коробку, то мы останавливаемся
                            if (let.kords[boxKords.Key - 1].IndexOf(boxKords.Value[j] + 2) != -1 && interLet.GetType() == typeof(Box) && interLet.collisiumPerson) return Direction.Stop;
                        }
                    }
                }
            }

            // Проверка на касание предметов типа Item (различных ключей)
            foreach (Item item in items)
            {
                if (
                    Kords.ContainsKey(item.Y) && Kords[item.Y].IndexOf(item.X - 2) != -1 ||
                    Kords.ContainsKey(item.Y + 1) && Kords[item.Y + 1].IndexOf(item.X - 2) != -1 ||
                    Kords.ContainsKey(item.Y - 1) && Kords[item.Y - 1].IndexOf(item.X - 2) != -1
                    ) return Direction.Stop;
            }
            return direction;
        }
        private Direction collisiumLeft(Person person, Direction direction, Let let, List<AbstractInteractLet> interactLets, List<Item> items)
        {
            foreach (KeyValuePair<int, List<int>> kords in Kords)
            {
                for (int j = 0; j < Kords[kords.Key].Count; j++)
                {
                    // Проверка на касание препятствий и стен
                    if (let.kords.ContainsKey(kords.Key - 1) && let.kords[kords.Key - 1].IndexOf(kords.Value[j]) != -1) // препятствия
                    {
                        return Direction.Stop;
                    }
                }
            }
            // Проверка на касание с другими коробками
            foreach (AbstractInteractLet interLet in interactLets)
            {
                if (interLet.GetType() != typeof(Box) && interLet.GetType() != typeof(MovingLet) && interLet.GetType() != typeof(RetryBox)) continue;
                foreach (KeyValuePair<int, List<int>> boxKords in interLet.Kords)
                {
                    if (Kords.ContainsKey(boxKords.Key))
                    {
                        for (int j = 0; j < boxKords.Value.Count; j++)
                        {
                            if (Kords[boxKords.Key].IndexOf(boxKords.Value[j] + 1) != -1)
                            {
                                if (interLet.GetType() == typeof(Box))
                                {
                                    interLet.collisiumPerson = true;
                                    break;
                                }
                                else return Direction.Stop;
                            }
                        }
                    }
                }
                foreach (KeyValuePair<int, List<int>> boxKords in interLet.Kords)
                {
                    if (let.kords.ContainsKey(boxKords.Key - 1))
                    {
                        for (int j = 0; j < boxKords.Value.Count; j++)
                        {
                            if (let.kords[boxKords.Key - 1].IndexOf(boxKords.Value[j]) != -1 && interLet.GetType() == typeof(Box) && interLet.collisiumPerson)
                            {
                                return Direction.Stop;
                            }
                        }
                    }
                }
            }

            // Проверка на касание предметов типа Item (различных ключей)
            foreach (Item item in items)
            {
                if (
                    Kords.ContainsKey(item.Y) && Kords[item.Y].IndexOf(item.X + 2) != -1 ||
                    Kords.ContainsKey(item.Y + 1) && Kords[item.Y + 1].IndexOf(item.X + 2) != -1 ||
                    Kords.ContainsKey(item.Y - 1) && Kords[item.Y - 1].IndexOf(item.X + 2) != -1
                    )
                {
                    return Direction.Stop;
                }
            }
            return direction;
        }
        private Direction collisiumUp(Person person, Direction direction, Let let, List<AbstractInteractLet> interactLets, List<Item> items)
        {
            foreach (KeyValuePair<int, List<int>> kords in Kords)
            {
                if (let.kords.ContainsKey(kords.Key - 2))
                {
                    // Проверка на касание препятствий
                    for (int j = 0; j < kords.Value.Count; j++)
                    {
                        if (let.kords[kords.Key - 2].IndexOf(kords.Value[j] + 1) != -1) return Direction.Stop;
                    }
                }
            }
            // Проверка на касание с другими коробками
            foreach (AbstractInteractLet interLet in interactLets)
            {
                if (interLet.GetType() != typeof(Box) && interLet.GetType() != typeof(MovingLet) && interLet.GetType() != typeof(RetryBox)) continue;
                foreach (KeyValuePair<int, List<int>> boxKords in interLet.Kords)
                {
                    if (Kords.ContainsKey(boxKords.Key + 1))
                    {
                        for (int j = 0; j < boxKords.Value.Count; j++)
                        {
                            if (Kords.ContainsKey(boxKords.Key + 1) && Kords[boxKords.Key + 1].IndexOf(boxKords.Value[j]) != -1)
                            {
                                if (interLet.GetType() == typeof(Box))
                                {
                                    interLet.collisiumPerson = true;
                                    break;
                                }
                                else return Direction.Stop;
                            }
                        }
                    }
                }
                foreach (KeyValuePair<int, List<int>> boxKords in interLet.Kords)
                {
                    if (let.kords.ContainsKey(boxKords.Key - 2))
                    {
                        for (int j = 0; j < boxKords.Value.Count; j++)
                        {
                            if (let.kords[boxKords.Key - 2].IndexOf(boxKords.Value[j] + 1) != -1 && interLet.GetType() == typeof(Box) && interLet.collisiumPerson)
                            {
                                return Direction.Stop;
                            }
                        }
                    }
                }
            }

            // Проверка на касание предметов типа Item (различных ключей)
            foreach (Item item in items)
            {
                if (
                    Kords.ContainsKey(item.Y + 2) && Kords[item.Y + 2].IndexOf(item.X + 1) != -1 ||
                    Kords.ContainsKey(item.Y + 2) && Kords[item.Y + 2].IndexOf(item.X - 1) != -1 ||
                    Kords.ContainsKey(item.Y + 2) && Kords[item.Y + 2].IndexOf(item.X) != -1
                    )
                {
                    return Direction.Stop;
                }
            }
            return direction;
        }
        private Direction collisiumDown(Person person, Direction direction, Let let, List<AbstractInteractLet> interactLets, List<Item> items)
        {
            foreach (KeyValuePair<int, List<int>> kords in Kords)
            {
                if (let.kords.ContainsKey(kords.Key))
                {
                    // Проверка на касание препятствий
                    for (int j = 0; j < kords.Value.Count; j++)
                    {
                        if (let.kords[kords.Key].IndexOf(kords.Value[j] + 1) != -1) return Direction.Stop;
                    }
                }
            }
            // Проверка на касание с другими коробками
            foreach (AbstractInteractLet interLet in interactLets)
            {
                if (interLet.GetType() != typeof(Box) && interLet.GetType() != typeof(MovingLet) && interLet.GetType() != typeof(RetryBox)) continue;
                foreach (KeyValuePair<int, List<int>> boxKords in interLet.Kords)
                {
                    if (Kords.ContainsKey(boxKords.Key - 1))
                    {
                        for (int j = 0; j < boxKords.Value.Count; j++)
                        {
                            if (Kords.ContainsKey(boxKords.Key - 1) && Kords[boxKords.Key - 1].IndexOf(boxKords.Value[j]) != -1)
                            {
                                if (interLet.GetType() == typeof(Box))
                                {
                                    interLet.collisiumPerson = true;
                                    break;
                                }
                                else return Direction.Stop;
                            }
                        }
                    }
                }
                foreach (KeyValuePair<int, List<int>> boxKords in interLet.Kords)
                {
                    if (let.kords.ContainsKey(boxKords.Key + 2))
                    {
                        for (int j = 0; j < boxKords.Value.Count; j++)
                        {
                            if (let.kords[boxKords.Key + 2].IndexOf(boxKords.Value[j] + 1) != -1 && interLet.GetType() == typeof(Box) && interLet.collisiumPerson)
                            {
                                return Direction.Stop;
                            }
                        }
                    }
                }
            }

            // Проверка на касание предметов типа Item (различных ключей)
            foreach (Item item in items)
            {
                if (
                    Kords.ContainsKey(item.Y - 2) && Kords[item.Y - 2].IndexOf(item.X + 1) != -1 ||
                    Kords.ContainsKey(item.Y - 2) && Kords[item.Y - 2].IndexOf(item.X - 1) != -1 ||
                    Kords.ContainsKey(item.Y - 2) && Kords[item.Y - 2].IndexOf(item.X) != -1
                    )
                {
                    return Direction.Stop;
                }
            }
            return direction;
        }
        // Метод который отвечает за отрисовку текущей коробки в зависимости от напраления персонажа
        public override void move(Direction personDirection)
        {
            // Двигаем коробку только если ее касается персонаж
            if (!collisiumPerson) return;
            clear();
            switch (personDirection)
            {
                case Direction.Up:
                    {
                        Dictionary<int, List<int>> kords2 = new Dictionary<int, List<int>>();
                        foreach (KeyValuePair<int, List<int>> kords in Kords)
                        {
                            kords2.Add(kords.Key - 1, kords.Value);
                        }
                        Kords = kords2;
                        break;
                    }
                case Direction.Down:
                    {
                        Dictionary<int, List<int>> kords2 = new Dictionary<int, List<int>>();
                        foreach (KeyValuePair<int, List<int>> kords in Kords)
                        {
                            kords2.Add(kords.Key + 1, kords.Value);
                        }
                        Kords = kords2;
                        show();
                        break;
                    }
                case Direction.Left:
                    {
                        foreach (KeyValuePair<int, List<int>> kords in Kords) for (int j = 0; j < Kords[kords.Key].Count; j++) Kords[kords.Key][j]--;
                        break;
                    }
                case Direction.Right:
                    {
                        foreach (KeyValuePair<int, List<int>> kords in Kords) for (int j = 0; j < Kords[kords.Key].Count; j++) Kords[kords.Key][j]++;
                        break;
                    }
            }
            show();
            collisiumPerson = false;
        }

        public override void show()

        {
            Console.ForegroundColor = ConsoleColor.Blue;
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
