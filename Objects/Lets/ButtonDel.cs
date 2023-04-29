using StudyPractic.Entites;
using StudyPractic.Objects.Abstract;

namespace StudyPractic.Objects.Lets
{
    public class ButtonDel : AbstractInteractLet
    {
        // Список всех дверей которые зависят от кнопки для более удобного использования 
        private List<DoorForButton> doors = new List<DoorForButton>();
        // Переменная которая отвечает за то открыты ли двери 
        private bool isClear = false;
        public List<int> id = new List<int>() {0};
        public ButtonDel(string[] sprite, int startOfsetX, int startOfsetY) : base(sprite, startOfsetX, startOfsetY)
        {
        }

        public ButtonDel(string[] sprite, int startOfsetX, int startOfsetY, List<int> id) : base(sprite, startOfsetX, startOfsetY)
        {
            this.id = id;
        }

        public override Direction collisium(Person person, Direction direction, Let let, List<AbstractInteractLet> interactLets, List<Item> items)
        {
            show();
            if ((Kords.ContainsKey(person.Y - 1) && (Kords[person.Y - 1].IndexOf(person.X - 1) != -1 || Kords[person.Y - 1].IndexOf(person.X) != -1 || Kords[person.Y - 1].IndexOf(person.X + 1) != -1)) || 
                (Kords.ContainsKey(person.Y + 1) && (Kords[person.Y + 1].IndexOf(person.X - 1) != -1 || Kords[person.Y + 1].IndexOf(person.X) != -1 || Kords[person.Y + 1].IndexOf(person.X + 1) != -1)) ||
                (Kords.ContainsKey(person.Y) && (Kords[person.Y].IndexOf(person.X - 1) != -1 || Kords[person.Y].IndexOf(person.X) != -1 || Kords[person.Y].IndexOf(person.X + 1) != -1))
                )
            {
                collisiumPerson = true;
                Console.BackgroundColor = ConsoleColor.Green;
                person.show();
            }
            else collisiumPerson = false;
            // Проверка интеграктивных препятствий на наличие дверей
            if (doors.Count == 0)
            {
                foreach (AbstractInteractLet interactLet in interactLets)
                {
                    if (interactLet.GetType() == typeof(DoorForButton) && this.id.IndexOf(((DoorForButton)interactLet).id) != -1)
                    {
                        doors.Add((DoorForButton)interactLet);
                        interactLet.clear();
                    }
                }
            }
            foreach (AbstractInteractLet interactLet in interactLets)
            {
                if (interactLet.GetType() != typeof(Box)) continue;
                foreach (KeyValuePair<int, List<int>> kords in interactLet.Kords)
                {
                    if (!Kords.ContainsKey(kords.Key)) continue;
                    foreach (int kordsX in kords.Value)
                    {
                        if (Kords[kords.Key].IndexOf(kordsX) != -1)
                        {
                            collisiumPerson = true;
                            Console.BackgroundColor = ConsoleColor.Green;
                            interactLet.show();
                            break;
                        }
                    }
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            return direction;
        }
        // Для кнопки этот метод отрисовывает или стирает двери которые связаны с кнопкой
        public override void move(Direction personDirection)
        {
            foreach (DoorForButton door in doors)
            {
                if (this.collisiumPerson)
                {
                    if (door.isOpen)
                    {
                        door.isOpen = false;
                        door.show();
                    }
                }
                else
                {
                    if (!door.isOpen)
                    {
                        door.isOpen = true;
                        door.clear();
                    }
                }
            }
        }

        public override void show()
        {
            Console.BackgroundColor = (collisiumPerson) ? ConsoleColor.Green : ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            foreach (KeyValuePair<int, List<int>> kords in Kords)
            {
                Console.SetCursorPosition(kords.Value[0], kords.Key);
                Console.Write(sprite[kords.Key - startOfsetY]); 
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
