using StudyPractic.Entites;

namespace StudyPractic.Objects.Lets
{
    // Класс описывает сами препятствия на карте и способы взаимодействия с ними
    public class Let
    {
        // Словарь координат КЛЮЧ: координата по у ЗНАЧЕНИЕ: координаты по х
        public Dictionary<int, List<int>> kords { get; set; } = new Dictionary<int, List<int>>();
        public int LengthX { get; set; } = 0;
        public int LengthY { get; set; } = 0;
        public string[] sprite { get; set; }

        // Конструктор, в нем мы получает массив строк. Если элемент этого массива не пробел, то сохраняем его координаты
        public Let(string[] sprite)
        {
            this.sprite = sprite;
            for (int i = 0; i < sprite.Length; i++)
            {
                kords[i] = new List<int>();
                for (int j = 0; j < sprite[i].Length; j++)
                {
                    if (sprite[i][j] != ' ') kords[i].Add(j + 1);
                }
            }
            LengthX = sprite[0].Length;
            LengthY = sprite.Length;
        }

        public void show()
        {
            for (int i = 0; i < LengthY; i++)
            {
                Console.SetCursorPosition(0, 1 + i);
                for (int j = 0; j < LengthX; j++)
                {
                    Console.Write(sprite[i][j]);
                }
            }
        }
        // Если персонаж может коснуться препятствия. то мы его останавливаем
        public Direction collisium(Person person, Direction direction)
        {
            if ((
                     kords.ContainsKey(person.Y - 1) && kords[person.Y - 1].IndexOf(person.X + 3) != -1 ||
                     kords.ContainsKey(person.Y) && kords[person.Y].IndexOf(person.X + 3) != -1 ||
                     kords.ContainsKey(person.Y - 2) && kords[person.Y - 2].IndexOf(person.X + 3) != -1
                 ) && direction == Direction.Right ||
                 (
                     kords.ContainsKey(person.Y + 1) && kords[person.Y + 1].IndexOf(person.X + 1) != -1 ||
                     kords.ContainsKey(person.Y + 1) && kords[person.Y + 1].IndexOf(person.X + 2) != -1 ||
                     kords.ContainsKey(person.Y + 1) && kords[person.Y + 1].IndexOf(person.X) != -1
                 ) && direction == Direction.Down ||
                 (
                     kords.ContainsKey(person.Y - 2) && kords[person.Y - 2].IndexOf(person.X - 1) != -1 ||
                     kords.ContainsKey(person.Y) && kords[person.Y].IndexOf(person.X - 1) != -1 ||
                     kords.ContainsKey(person.Y - 1) && kords[person.Y - 1].IndexOf(person.X - 1) != -1
                 ) && direction == Direction.Left ||
                 (
                     kords.ContainsKey(person.Y - 3) && kords[person.Y - 3].IndexOf(person.X + 2) != -1 ||
                     kords.ContainsKey(person.Y - 3) && kords[person.Y - 3].IndexOf(person.X) != -1 ||
                     kords.ContainsKey(person.Y - 3) && kords[person.Y - 3].IndexOf(person.X + 1) != -1
                 ) && direction == Direction.Up)
            {
                return Direction.Stop;
            }
            return direction;
        }
    }
}
