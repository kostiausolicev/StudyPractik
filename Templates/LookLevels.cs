using game;
using StudyPractic.Templates.Abstract;

namespace StudyPractic.Templates
{
    class LookLevels
    {
        private static int choosenLvl = -1;
        public static void show()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < Pattern.LevelHigth - 1; i++)
            {
                if (i < 1) Console.WriteLine(Pattern.SpriteTop);
                else if (i > 47) Console.WriteLine(Pattern.SpriteBottom);
                else Console.WriteLine(Pattern.SpritePlate);
            }
            Console.SetCursorPosition(10, 8);
            Console.Write("Это меню выбора уровня, чтобы выбрать уровень нажммайте W или S или стрелки вверх вниз, затем нажмите Enter");
            Console.SetCursorPosition(10, 9);
            Console.Write("Здесь открыты только уровни которые вы посещали в игре, обучение открыто всегда");
            for (int i = 0; i < Game.FinishLevels.Count; i++)
            {
                Console.SetCursorPosition(10, 11 + i * 2);
                Console.Write("Уровень № " + i + (i == 0 ? " (обучение)" : ""));
            }
        }

        public static void chooseLevelInteract(ConsoleKey key, ref int setLevel)
        {
            setLevel = -2;
            switch(key)
            {
                case ConsoleKey.Enter:
                    {
                        setLevel = choosenLvl;
                        choosenLvl = -1;
                        break;
                    }
                case ConsoleKey.Escape:
                    {
                        setLevel = -1;
                        break;
                    }
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    {
                        if (choosenLvl != -1)
                        {
                            Console.SetCursorPosition(10, 11 + choosenLvl * 2);
                            Console.Write("Уровень № " + (choosenLvl) + (choosenLvl == 0 ? " (обучение)" : ""));
                        }

                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;

                        choosenLvl = (choosenLvl + 1) % Game.FinishLevels.Count;
                        Console.SetCursorPosition(10, 11 + (choosenLvl) * 2);
                        Console.Write("Уровень № " + (choosenLvl) + (choosenLvl == 0 ? " (обучение)" : ""));

                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    }
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    {

                        if (choosenLvl != -1)
                        {
                            Console.SetCursorPosition(10, 11 + choosenLvl * 2);
                            Console.Write("Уровень № " + (choosenLvl) + (choosenLvl == 0 ? " (обучение)" : ""));
                        }
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;

                        if (choosenLvl != -1)
                        {
                            choosenLvl = (choosenLvl - 1) % Game.FinishLevels.Count;
                            choosenLvl = choosenLvl < 0 ? Game.FinishLevels.Count - 1 : choosenLvl;
                        }
                        else choosenLvl = 0;
                        Console.SetCursorPosition(10, 11 + (choosenLvl) * 2);
                        Console.Write("Уровень № " + (choosenLvl) + (choosenLvl == 0 ? " (обучение)" : ""));

                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    }
            }
        }

        public static void clear()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Pattern.LevelHigth; i++)
            {
                Console.WriteLine(string.Join("", Enumerable.Repeat(" ", Pattern.LevelWight)));
            }
            MainMenu.show();
        }
    }
}
