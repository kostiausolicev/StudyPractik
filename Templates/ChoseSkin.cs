using StudyPractic.Entites;
using StudyPractic.Templates.Abstract;

namespace StudyPractic.Templates
{
    class ChoseSkin
    {
        private static string[][] skins = new string[][] {PersonSprites.sprite1, PersonSprites.sprite2, PersonSprites.sprite3, PersonSprites.sprite4, PersonSprites.sprite5, PersonSprites.sprite6 };
        private static int pointerSkins = -1;
        public static void show(ref string[] skin)
        {
            string[] techText = new string[] { "Чтобы выбрать скин, нужно нажимать клавишы:", "A и D или стрелки влево и вправо", "Для выбора нужного скина нажмите Enter" };
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Pattern.LevelHigth - 1; i++)
            {
                if (i == 0) Console.WriteLine(Pattern.SpriteTop);
                else if (i > 47) Console.WriteLine(Pattern.SpriteBottom);
                else Console.WriteLine(Pattern.SpritePlate);
            }

            for (int i = 0; i < skins.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                for (int j = 0; j < 3; j++)
                {
                    Console.SetCursorPosition(10 * i + 10, 10 + j);
                    Console.Write(skins[i][j]);
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(10 * i + 11, 14);
                Console.Write(i + 1);
            }
            int yPosition = 0;
            for (int i = 0; i < techText.Length; i++)
            {
                Console.SetCursorPosition(10, 5 + yPosition++);
                Console.Write(techText[i]);
            }
        }

        public static void scinInteract(ConsoleKey key, ref string[] skin, ref bool setSkin)
        {
            Console.Beep();
            switch (key)
            {
                case ConsoleKey.Enter:
                    {
                        skin = skins[pointerSkins];
                        pointerSkins = -1;
                        setSkin = true;
                        return;
                    }
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    {
                        if (pointerSkins != -1)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            for (int j = 0; j < 3; j++)
                            {
                                Console.SetCursorPosition(10 * pointerSkins + 10, 10 + j);
                                Console.Write(skins[pointerSkins][j]);
                            }
                        }

                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        pointerSkins = (pointerSkins + 1) % skins.Length;

                        for (int j = 0; j < 3; j++)
                        {
                            Console.SetCursorPosition(10 * pointerSkins + 10, 10 + j);
                            Console.Write(skins[pointerSkins][j]);
                        }
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;

                        break;
                    }
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    {
                        if (pointerSkins != -1)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            for (int j = 0; j < 3; j++)
                            {
                                Console.SetCursorPosition(10 * pointerSkins + 10, 10 + j);
                                Console.Write(skins[pointerSkins][j]);
                            }
                        }
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        if (pointerSkins != -1)
                        {
                            pointerSkins = (pointerSkins - 1) % skins.Length;
                            pointerSkins = pointerSkins < 0 ? skins.Length - 1 : pointerSkins;
                        }
                        else pointerSkins = 0;

                        for (int j = 0; j < 3; j++)
                        {
                            Console.SetCursorPosition(10 * pointerSkins + 10, 10 + j);
                            Console.Write(skins[pointerSkins][j]);
                        }

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
            Console.SetCursorPosition(0, 0);
            MainMenu.show();
        }
    }
}
