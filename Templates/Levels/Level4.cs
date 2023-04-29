using StudyPractic.Entites;
using StudyPractic.Objects.Abstract;
using StudyPractic.Objects.Keys;
using StudyPractic.Objects.Lets;
using StudyPractic.Templates.Abstract;
using StudyPractic.Templates.Interfaces;
using System.Diagnostics;

namespace StudyPractic.Templates.Levels
{
    internal class Level4 : AbstractionLevel
    {
        // Константы отвечающие за координаты спавна игрока
        public const int SpavnPersoneX = 68;
        public const int SpavnPersoneY = 41;

        public override Let lets { get; set; } = new Let(
                new string[]
                {
                     "#################################################_____________________######################################################################",
                     "##                                                                                                                                        ##",
                     "##     #####################                                                                                    #####################     ##",
                     "##     ##                 ##                                                                                    ##                 ##     ##",
                     "##     ##                 ##                                                                                    ##                 ##     ##",
                     "##     ##                 ##                                                                                    ##                 ##     ##",
                     "##     ##                 ##                                                                                    ##                 ##     ##",
                     "##     ##                 ##              ##                                                    ##              ##                 ##     ##",
                     "##     ######        #######            ##                                                        ##            #######        ######     ##",
                     "##     ##            ##               ##             #############       ##############             ##               ##            ##     ##",
                     "##     ##            ##             ##                #       _ #         #        _ #                ##             ##            ##     ##",
                     "##     ##            ##           ##                   #########           ##########                   ##           ##            ##     ##",
                     "##     ##            ##         ##                                                                        ##         ##            ##     ##",
                     "##     ####      #################                                                                        #################      ####     ##",
                     "##     ##                       ##                                                                        ##                       ##     ##",
                     "##     ##                       ##                                                                        ##                       ##     ##",
                     "##     ##                       ##                                                                        ##                       ##     ##",
                     "##     ##                       ##                                                                        ##                       ##     ##",
                     "##     ##                       ##                                                                        ##                       ##     ##",
                     "##     ##                       ##                    #                              #                    ##                       ##     ##",
                     "##     ########     ##############                     ##############################                     ##############     ########     ##",
                     "##     ##                         ##                                                                    ##                         ##     ##",
                     "##     ##                           ##                                                                ##                           ##     ##",
                     "##     ##                             ##                                                            ##                             ##     ##",
                     "##     #############      ################                                                        ################      #############     ##",
                     "##     ##                             ######                                                    ######                             ##     ##",
                     "##     ##                             ##                                                            ##                             ##     ##",
                     "##     ##                             ##                                                            ##                             ##     ##",
                     "##     ##                             ##                                                            ##                             ##     ##",
                     "##     ##                             ##                                                            ##                             ##     ##",
                     "##     ##                             ##                                                            ##                             ##     ##",
                     "##     ################      ###########                                                            ###########      ################     ##",
                     "####   ##                                                                                                                          ##   ####",
                     "##  ##   ##                                                                                                                      ##   ##  ##",
                     "###   ##   ##                                                                                                                  ##   ##   ###",
                     "## ##   ##   ##                                                                                                              ##   ##   ## ##",
                     "##   ##   ##   ##                                                                                                          ##   ##   ##   ##",
                     "##     ##   ##   ##                                                                                                      ##   ##   ##     ##",
                     "##       ##   ##   ##                                                                                                  ##   ##   ##       ##",
                     "##         ##   ##   ##                                                                                              ##   ##   ##         ##",
                     "##           ##   ##   ##                                                                                          ##   ##   ##           ##",
                     "##             ##   ##   ##                                                                                      ##   ##   ##             ##",
                     "############################################################################################################################################",
                }
            );

        public override List<Objects.Abstract.AbstractInteractLet> interactLets { get; set; } = new List<Objects.Abstract.AbstractInteractLet> 
        {
            new MovingLet(new string[]{"#"}, 14, 22, Direction.Left),
            new MovingLet(new string[]{"#"}, 14, 23, Direction.Left),
            new MovingLet(new string[]{"#"}, 14, 24, Direction.Left),
            new MovingLet(new string[]{"#"}, 123, 22, Direction.Right),
            new MovingLet(new string[]{"#"}, 123, 23, Direction.Right),
            new MovingLet(new string[]{"#"}, 123, 24, Direction.Right),

            new RetryBox(new string[] {">", ">", ">"}, 9, 22),
            new RetryBox(new string[] {"<", "<", "<"}, 130, 22),

            new DoorForButton(new string[] {"_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ "}, 44, 8, 1),
            new DoorForButton(new string[] {" _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _"}, 44, 8, 2),
            new DoorForButton(new string[] {"____________________________________________________"}, 44, 26, 3),
            new DoorForButton(new string[] {"________"}, 119, 9),
            new DoorForKey(new string[] {"|", "|", "|", "|", "|", "|", "|", "|","|",}, 53, 11),
            new DoorForKey(new string[] {"|", "|", "|", "|", "|", "|", "|", "|","|",}, 86, 11),
            new DoorForKey(new string[] {"_______"}, 66, 10),

            new ButtonDel(new string[] { "|-+-|", "|-+-|"}, 17, 16, new List<int>(){1}),
            new Button(new string[] { "|-+-|", "|-+-|"}, 17, 16, 3),
            new Button(new string[] { "|-+-|", "|-+-|"}, 118, 16, 2),
            new Button(new string[] {"|+|"}, 96, 20, 3),
            new Button(new string[] { "|+|"}, 14, 7),
            new Button(new string[] {"|+|"}, 40, 20),

            new Box(new string[] {"##", "##", "##"}, 64, 33),
            new Box(new string[] {"##", "##", "##"}, 72, 33),
            new Box(new string[] {"##"}, 14, 7),
            new Box(new string[] {"##"}, 123, 8),
            new Box(new string[] {"###", "###"}, 40, 15),
            new Box(new string[] {"###", "###"}, 96, 15),
        };
        public override List<Item> items { get; set; } = new List<Item>()
        {
            new FinalKey(69, 16),
            new KeyForDoor(118, 6),
        };

        public static int TimeOnLevel { get; set; } = 180;
        public override int timeOnLevel { get => 180; }
        public static TimerCallback tm { get; set; } = new TimerCallback(CheckTimer);
        public static Timer timer { get; set; }

        public override void StartLevel(Person person, ref int codeLvl)
        {
            Stopwatch sw = new Stopwatch();
            direction = Direction.Stop;
            while (true)
            {
                sw.Restart();
                // Считывание нажатой кнопки в перерывах между кадрами
                while (sw.ElapsedMilliseconds <= FrameMs)
                {
                    direction = ReadMovementAndServiceButton(direction, ref codeLvl);
                    Thread.Sleep(16);
                }
                // Отрисовка координат игрока
                Console.SetCursorPosition(Pattern.LevelWight + 1, 0);
                Console.Write("                      ");
                Console.SetCursorPosition(Pattern.LevelWight + 1, 0);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write("X: " + person.X + " Y: " + person.Y);
                // Отрисовка оставшегося времени на прохождение уровня
                Console.SetCursorPosition(Pattern.LevelWight + 1, 1);
                Console.Write("                       ");
                Console.SetCursorPosition(Pattern.LevelWight + 1, 1);
                Console.Write(TimeOnLevel + " <- оставшееся время");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Black;

                for (int interLet = 0; interLet < this.interactLets.Count; interLet++)
                {
                    AbstractInteractLet curr = interactLets[interLet];
                    interactLets.RemoveAt(interLet);
                    direction = curr.collisium(person, direction, lets, interactLets, items);
                    interactLets.Insert(interLet, curr);
                }
                // Проверка колиизий с препятствиями 
                direction = lets.collisium(person, direction);
                // Проверка коллизий с каждым интерактивными предметом на карте с его особенностями
                foreach (Item item in this.items) item.collisium(person);
                person.move(direction);
                for (int interLet = 0; interLet < this.interactLets.Count; interLet++) this.interactLets[interLet].move(direction);
                if (finalLevel(ref codeLvl)) break;
            }
            sw.Stop();
            this.clear();
        }

        public override bool finalLevel(ref int code)
        {
            if (code == 0 || code == 1)
            {
                timer.Dispose();
                TimeOnLevel = 180;
                return true;
            }
            foreach (Item key in person.Inventory)
            {
                if (key.Type == "FinalKey" && person.Y < 5 && person.X >= 50 && person.X <= 70 && (direction == Direction.Up || direction == Direction.Stop))
                {
                    timer.Dispose();
                    TimeOnLevel = this.timeOnLevel;
                    return true;
                }
            }
            foreach (AbstractInteractLet interLet in interactLets)
            {
                if (interLet.GetType() != typeof(RetryBox)) continue;
                if (interLet.collisiumPerson)
                {
                    timer.Dispose();
                    TimeOnLevel = 180;
                    code = 0;
                    return true;
                }
            }
            return false;
        }

        public override Direction ReadMovementAndServiceButton(Direction direction, ref int codeLvl)
        {
            codeLvl = TimeOnLevel <= 0 ? 1 : 2;
            if (!Console.KeyAvailable) return Direction.Stop;
            ConsoleKey key = Console.ReadKey(true).Key;
            codeLvl = ReadServiceButtons(key);
            codeLvl = TimeOnLevel <= 0 ? 1 : ReadServiceButtons(key);
            direction = key switch
            {
                ConsoleKey.W => Direction.Up,
                ConsoleKey.S => Direction.Down,
                ConsoleKey.A => Direction.Left,
                ConsoleKey.D => Direction.Right,
                ConsoleKey.UpArrow => Direction.Up,
                ConsoleKey.DownArrow => Direction.Down,
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right,
                _ => Direction.Stop
            };
            return direction;
        }

        public override void show(string[] sprite, out AbstractionLevel level, ref int code)
        {
            // отрисовка шаблона кровня
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < Pattern.LevelHigth - 7; i++)
            {
                if (i == 0) Console.WriteLine(Pattern.SpriteTop);
                else if (i == 42)
                {
                    Console.SetCursorPosition(0, 42);
                    Console.WriteLine(Pattern.SpriteInventory);
                }
            }
            // Отрисовка технической информации
            int ofsetTechText = 0, ofsetTechTextX = 0;
            for (int i = 0; i < techText.Length; i++)
            {
                switch (techText[i])
                {
                    case "!blue": Console.ForegroundColor = ConsoleColor.Blue; break;
                    case "!red": Console.ForegroundColor = ConsoleColor.Red; break;
                    case "!white": Console.ForegroundColor = ConsoleColor.White; break;
                    case "!yellow": Console.ForegroundColor = ConsoleColor.Yellow; break;
                    case "!green": Console.ForegroundColor = ConsoleColor.Green; break;
                    case "!redBG": Console.BackgroundColor = ConsoleColor.Red; break;
                    case "!blackBG": Console.BackgroundColor = ConsoleColor.Black; break;
                    default:
                        {
                            if (Console.ForegroundColor == ConsoleColor.Green)
                            {
                                Console.SetCursorPosition(Pattern.LevelWight + 1 + ofsetTechTextX, 2 + ofsetTechText++);
                                Console.Write(techText[i]);
                                ofsetTechText++;
                                if (ofsetTechTextX != 0) ofsetTechTextX = 0;
                            }
                            else
                            {
                                Console.SetCursorPosition(Pattern.LevelWight + 1 + ofsetTechTextX, 2 + ofsetTechText);
                                Console.Write(techText[i]);
                                ofsetTechTextX = techText[i].Length;
                            }
                            break;
                        }
                }
            }
            // Отрисовка препятствий на карте
            this.lets.show();
            // Отрисовка всех объектов с которыми можно взаимодействовать
            for (int k = 0; k < this.interactLets.Count; k++)
            {
                this.interactLets[k].show();
            }
            // Создание, отрисовка и начала контроля управления персонажем
            this.person = new Person(SpavnPersoneX, SpavnPersoneY, sprite);
            this.person.show();
            foreach (Item item in this.items) item.show();
            timer = new Timer(tm, 0, 0, 1000);
            Console.CursorVisible = false;
            StartLevel(this.person, ref code);
            level = new Level4();
        }
        public override int ReadServiceButtons(ConsoleKey key)
        {
            int isSB = key switch
            {
                ConsoleKey.R => 0,
                ConsoleKey.Escape => 1,
                _ => 2
            };
            return isSB;
        }
        public static void CheckTimer(object obj)
        {
            TimeOnLevel--;
        }
    }
}
