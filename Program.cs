using static System.Console;
using StudyPractic.Entites;
using StudyPractic.Templates.Levels;
using StudyPractic.Templates.Interfaces;
using StudyPractic.Templates;

namespace game
{
    class Game
    {
        // Параментры консоли ширина и высота
        private const int MapWidth = 210;
        private const int MapHeigth = 52;
        // Список уровней
        public readonly static List<AbstractionLevel> Levels = new List<AbstractionLevel>() { /*new Level0(), new Level1(), new Level2(), new Level3(),*/ new Level4(), new FinalScreen() };
        // Список уже завершенных уровней
        public static List<AbstractionLevel> FinishLevels = new List<AbstractionLevel>() { new Level0() };
        // Уровень на который надо перенаправить пользователя в случае продолжения игры, а не начала новой
        public static int userLevel = 0;

        public static void Main(string[] args)
        {
            // Задание параметров экрана
            SetWindowSize(MapWidth, MapHeigth);
            SetBufferSize(MapWidth, MapHeigth);
            CursorVisible = false;

            string[] sprite = PersonSprites.sprite1;
            MainMenu.MainMenuInteract(sprite);

            ReadKey();
        }

        // Метод описывает логику хода игры
        public static void StartGame(string[] sprite, bool cont = false) // аргумент bool cont означает вызывается новая игра (false) или требуется продолжение (true)
        {
            // Код который означает что надо делать с игрой в дальнейшем
            // 0 - уровень должен быть переигран (рестарт)
            // 1 - выход в главное меню
            // 2 - уровень завершен успешно
            int codeLvl;
            for (int i = (cont ? userLevel : 0); i < Levels.Count; i++)
            {
                // Переменная абстрактного класса, которая передается с модификатором out в уровень, чтобы была возможность сделать рестарт
                AbstractionLevel level;
                // Коды: 0 - рестарт, 2 - прохождение, 1 - выход в главное меню
                do
                {
                    codeLvl = 2;
                    Levels[i].show(sprite, out level, ref codeLvl);
                    Levels[i] = level;
                    // Пока код игры равен 0 мы будем переигрывать и переигрывать уровень
                } while (codeLvl == 0);
                userLevel = i % (Levels.Count - 1);
                if (codeLvl == 1)
                {
                    break;
                }
                // Проверка на то был ли добавлен уже уровень в список завершенных
                bool flag = true;
                for (int r = 0; r < FinishLevels.Count; r++)
                {
                    if (FinishLevels[r].GetType() == level.GetType() || userLevel == 0) flag = false;
                }
                if (flag) FinishLevels.Add(level);
            }
        }
    }
}