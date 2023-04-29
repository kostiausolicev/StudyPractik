using game;
using StudyPractic.Templates.Abstract;
using StudyPractic.Templates.Interfaces;
using StudyPractic.Templates.Levels;
using static game.Game;

namespace StudyPractic.Templates
{
    class MainMenu
    {
        // Пункты меню
        private static string[] menuPointers = new string[] { "- Новая игра", "- Выбрать скин", "- Выбор уровня", "- Продолжить" };
        // Приветственная надпись
        private static string[] welcome = new string[]
        {
            @"=======  ====\  ||   /||  ||==\   ||===  ======  ||",
            @"||   ||  ||  |  ||  / ||  ||==/   ||__     ||    ||",
            @"||   ||  ||==/  || /  ||  ||===\  ||       ||    ||",
            @"||   ||  ||     ||/   ||  ||===/  ||===    ||    <>",
        };
        // Указатель на пунт меню
        private static int pointerMenu = -1;
        public static void show()
        {
            // Вспомогательная информаця
            string[] techText = new string[] { "Чтобы управлять меню, используейте клавишы:", "W и S или стрелки вверх и вниз", "Для выбора нужного пункта нажмите Enter"};
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < Pattern.LevelHigth - 1; i++)
            {
                if (i < 1) Console.WriteLine(Pattern.SpriteTop);
                else if (i > 47) Console.WriteLine(Pattern.SpriteBottom);
                else { Console.WriteLine(Pattern.SpritePlate); }
            }
            // Отрисовка пунктов меню
            int yPosition = 0;
            for (int i = 0; i < menuPointers.Length; i++)
            {
                Console.SetCursorPosition(10, 10 + 2 * yPosition++);
                Console.Write(menuPointers[i]);
            }
            // Отрисовка приветствия
            for (int i = 0; i < welcome.Length; i++)
            {
                Console.SetCursorPosition(10, 3 + i);
                Console.Write(welcome[i]);
            }
            // Отрисовка вспомогательной информации
            yPosition = 0;
            for (int i = 0; i < techText.Length; i++)
            {
                Console.SetCursorPosition(2,34 +  8 + 2 * yPosition++);
                Console.Write(techText[i]);
            }
        }
        // Метод описывающий логику выбора пункта меню с помощью стрелок
        public static void chooseMenu(ConsoleKey key, ref int code)
        {
            switch(key)
            {
                case ConsoleKey.Enter:
                    {
                        code = pointerMenu + 1;
                        pointerMenu = -1;
                        return;
                    }
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    {
                        if (pointerMenu != -1)
                        {
                            Console.SetCursorPosition(10, 10 + pointerMenu * 2);
                            Console.Write(menuPointers[pointerMenu]);
                        }

                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;

                        pointerMenu = (pointerMenu + 1) % menuPointers.Length;
                        Console.SetCursorPosition(10, 10 + (pointerMenu) * 2);
                        Console.Write(menuPointers[pointerMenu]);

                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    }
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    {
                        if (pointerMenu != -1)
                        {
                            Console.SetCursorPosition(10, 10 + pointerMenu * 2);
                            Console.Write(menuPointers[pointerMenu]);
                        }

                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;

                        if (pointerMenu != -1)
                        {
                            pointerMenu = (pointerMenu - 1) % menuPointers.Length;
                            pointerMenu = pointerMenu < 0 ? menuPointers.Length - 1 : pointerMenu;
                        }
                        else pointerMenu = 0;
                        Console.SetCursorPosition(10, 10 + (pointerMenu) * 2);
                        Console.Write(menuPointers[pointerMenu]);

                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    }
            }
        }

        /*
                Группа перегруженных методов очищает главный экран и вызывает отрисовку в зависимости от полученного кода
                1 - начало игры
                2 - смена скина
                3 - просмотр уровня
                4 - продолжить с уровня где закончил
                5 - начать с уровня, номер которого получен из меню выбора уровня
                
        */
        // очищение экрана для выбора скина
        public static void clear(int code, string[] sprite)
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Pattern.LevelHigth; i++)
            {
                Console.WriteLine(string.Join("", Enumerable.Repeat(" ", Pattern.LevelWight + 50)));
            }
            switch (code)
            {
                case 2:
                    {
                        ChoseSkin.show(ref sprite); 
                        break;
                    }
                default: break;
            }
        }
        // очищение экрана для выбора уровня
        public static void clear(int code)
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Pattern.LevelHigth; i++)
            {
                Console.WriteLine(string.Join("", Enumerable.Repeat(" ", Pattern.LevelWight + 50)));
            }
            switch (code)
            {
                case 3:
                    {
                        LookLevels.show(); 
                        break;
                    }
                default: break;
            }
        }
        // очищение экрана для старта или продолжения игры 
        public static void clear(int code, string[] sprite, List<AbstractionLevel> Levels, ref int userLevel)
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Pattern.LevelHigth; i++)
            {
                Console.WriteLine(string.Join("", Enumerable.Repeat(" ", Pattern.LevelWight + 50)));
            }
            switch (code)
            {
                case 1:
                    {
                        userLevel = 0;
                        Game.StartGame(sprite);
                        MainMenuInteract(sprite);
                        break;
                    }
                case 4:
                    {
                        if (userLevel == 0)
                        {
                            Console.SetCursorPosition(100, 25);
                            Console.Write("Вы еще не начинали игру!");
                            Thread.Sleep(3000);
                            show();
                            break;
                        }
                        Game.StartGame(sprite, true);
                        MainMenuInteract(sprite);
                        break;
                    }
                // код 5 означает, что уровень пользователя был изменен в меню выбор уровня
                case 5:
                    {
                        Game.StartGame(sprite, true);
                        MainMenuInteract(sprite);
                        break;
                    }
                default:
                    {
                        MainMenuInteract(sprite);
                        break;
                    }
            }
        }

        // Метод в котором описана логика взаимодействия с главным меню
        public static void MainMenuInteract(string[] sprite)
        {
            MainMenu.show();
            bool startGame = false;
            while (!startGame)
            {
                if (!Console.KeyAvailable) continue;
                ConsoleKey key = Console.ReadKey(true).Key;
                int code = 0;
                int chooseLvl = -2;
                Console.Beep();
                MainMenu.chooseMenu(key, ref code);
                switch (code)
                {
                    // При нажатии на 1 запускается игра
                    case 1:
                        {
                            startGame = true;
                            game.Game.FinishLevels = new List<AbstractionLevel>() { new Level0(), };
                            MainMenu.clear(1, sprite, game.Game.Levels, ref userLevel);
                            break;
                        }
                    // При нажатии на 2 запускается выбор скина
                    case 2:
                        {
                            SckinInteract(ref sprite);
                            break;
                        }
                    // При нажатии на 3 запускается менб просмотра уровней 
                    case 3:
                        {
                            LookLevelsInteract(ref chooseLvl);
                            if (chooseLvl != -1)
                            {
                                userLevel = chooseLvl;
                                MainMenu.clear(5, sprite, game.Game.Levels, ref userLevel);
                            } else
                            {
                                MainMenu.clear(-1, sprite, game.Game.Levels, ref userLevel);
                            }
                            break;
                        }
                    case 4:
                        {
                            MainMenu.clear(4, sprite, game.Game.Levels, ref userLevel);
                            break;
                        }
                }
            }
        }
        // Метод в котором описана логика меню просмотра уровня 
        public static void LookLevelsInteract(ref int lookLevel)
        {
            MainMenu.clear(3);
            do
            {
                if (!Console.KeyAvailable) continue;
                ConsoleKey key = Console.ReadKey(true).Key;
                Console.Beep();
                LookLevels.chooseLevelInteract(key, ref lookLevel);
            } while (lookLevel == -2);
        }
        public static void SckinInteract(ref string[] sprite)
        {
            MainMenu.clear(2, Array.Empty<string>());
            bool setSkin = false;
            while (!setSkin)
            {
                if (!Console.KeyAvailable) continue;
                ConsoleKey key = Console.ReadKey(true).Key;
                ChoseSkin.scinInteract(key, ref sprite, ref setSkin);
            }
            ChoseSkin.clear();
        }
    }
}
