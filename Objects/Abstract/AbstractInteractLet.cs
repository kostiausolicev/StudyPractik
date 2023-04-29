using StudyPractic.Entites;
using StudyPractic.Objects.Lets;

namespace StudyPractic.Objects.Abstract
{
    public abstract class AbstractInteractLet
    {
        protected AbstractInteractLet(string[] sprite, int startOfsetX, int startOfsetY)
        {
            this.startOfsetX = startOfsetX;
            this.startOfsetY = startOfsetY;
            this.sprite = sprite;
            for (int i = 0; i < sprite.Length; i++)
            {
                Kords.Add(i + startOfsetY, new List<int>());
                for (int j = 0; j < sprite[i].Length; j++)
                {
                    if (sprite[i][j] != ' ') Kords[i + startOfsetY].Add(j + startOfsetX);
                }
            }
        }
        public bool collisiumPerson = false;
        // Словарь координат КЛЮЧ: координата по у ЗНАЧЕНИЕ: координаты по х
        public Dictionary<int, List<int>> Kords { get; set; } = new Dictionary<int, List<int>>();
        // Изначаельное смещение координат препятствия относительно левого верхнего угла
        public int startOfsetX { get; set; }
        public int startOfsetY { get; set; }
        // Внешний вид интерактивного объекта
        public string[] sprite { get; set; }
        public abstract Direction collisium(Person person, Direction direction, Let let, List<AbstractInteractLet> interactLets, List<Item> items);
        // Отрисовка препятствия
        public abstract void show();
        // Стирание препятствия
        public void clear()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            foreach (KeyValuePair<int, List<int>> kords in this.Kords)
            {
                for (int j = 0; j < this.Kords[kords.Key].Count; j++)
                {
                    Console.SetCursorPosition(kords.Value[j], kords.Key);
                    Console.Write(" ");
                }
            }
        }
        public abstract void move(Direction personDirection);
    }
}
