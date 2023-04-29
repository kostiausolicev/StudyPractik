using StudyPractic.Entites;
using StudyPractic.Objects.Abstract;
using StudyPractic.Templates.Interfaces;
using StudyPractic.Templates.Abstract;
using System.Diagnostics;
using static System.Console;
using game;
using StudyPractic.Objects.Lets;
using StudyPractic.Objects.Keys;

namespace StudyPractic.Templates.Levels
{
    public class Level1 : AbstractionLevel
    {
        // Константы отвечающие за координаты спавна игрока
        public const int SpavnPersoneX = 100;
        public const int SpavnPersoneY = 40;
        public override Let lets { get; set; } = new Let(
                new string[]
                {
                    "#################################################_____________________######################################################################",
                    "##                                                                                                                                        ##",
                    "##                ####                                                                                                                    ##",
                    "##                ##                                                                                                                      ##",
                    "##                                                                                                                                        ##",
                    "##           #####                                                                                                                        ##",
                    "##                           ##   ##                                                                                                      ##",
                    "##                           ##   ##                                                                                                      ##",
                    "##                           ##   ##                                                                                                      ##",
                    "##                           ##   ##############################################                                                          ##",
                    "##                           ##                                               ##                                                          ##",
                    "##                           ##                                               ##                                                          ##",
                    "##                           ##                                               ##                                                          ##",
                    "##                           #############                                    ##                                                          ##",
                    "##                                      ####     ##############           ######                                                          ##",
                    "##                                      ##         ##        ##           ##                                                              ##",
                    "##                                      ##         ##        ##           ##                                                              ##",
                    "##                                      ##         ##        ##           ##                                                              ##",
                    "##                               #########         ##        ##           ##                                                              ##",
                    "##                               ##                ##        ##           ##                                                              ##",
                    "##                               ##                ##        ##           ##                                                              ##",
                    "##                               ##                ##        ##           ##                                                              ##",
                    "##                               ##                ##        ##           ##                                                              ##",
                    "##                               ##                ##        ##           ##                                                              ##",
                    "##                               ##                ##        ##           ##                                                              ##",
                    "##                               ####################        ##           ##                                                              ##",
                    "##                                                           ##           ##################                                              ##",
                    "##                                                           ##                                                                           ##",
                    "##                                                           ##                                                                           ##",
                    "##                                                           ##                                                                           ##",
                    "##                                                           ##                                                                           ##",
                    "##                                                           ##                                                                           ##",
                    "##                                                           ##                                                                           ##",
                    "##                                                           ##                                                                           ##",
                    "##                                                           ###############################                                              ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "############################################################################################################################################",
                }
            );
/*
        Список элементов, которые реализуют интерфейс AbstractInteractLet
        которому соответсвуют все интерактивные объекты на карте и методы работы с ними (проверка коллизий, отрисовка и тп)
*/
        public override List<Objects.Abstract.AbstractInteractLet> interactLets { get; set; } = new List<Objects.Abstract.AbstractInteractLet>{
            new MovingLet(
                new string[]
                {
                    "##",
                }, 58, 11, Direction.Left
            ),
            new MovingLet(
                new string[]
                {
                    "##",
                }, 56, 12, Direction.Left
            ),
            new MovingLet(
                new string[]
                {
                    "##",
                }, 54, 13, Direction.Left
            ),
            new MovingLet(
                new string[]
                {
                    "##",
                }, 52, 14, Direction.Left
            ),
            new MovingLet(
                new string[]
                {
                    "###",
                }, 60, 5, Direction.Right
            ),
            new MovingLet(
                new string[]
                {
                    "###",
                }, 60, 6, Direction.Right
            ),
            new MovingLet(
                new string[]
                {
                    "###",
                }, 60, 8, Direction.Right
            ),
            new MovingLet(
                new string[]
                {
                    "###",
                }, 60, 7, Direction.Right
            ),
            new Box(new string[] { "######", "######", "######"}, 80, 37),
            new Box(new string[] {"####", "   #", "   #", "   #" }, 30, 35),
            new DoorForKey(new string[] {"|","|","|"}, 40, 11),
            new DoorForKey(new string[] {"---"}, 31, 7),
            new DoorForButton(new string[] {"-----------"}, 63, 21),
            new Button(new string[] {"|+|"}, 100, 35),
        };
        public override List<Item> items { get; set; } = new List<Item>()
        {
            new FinalKey(34, 12),
            new KeyForDoor(40, 24)
        };
        public static int TimeOnLevel { get; set; } = 180;
        public override int timeOnLevel { get => 180; }
        public static TimerCallback tm { get; set; } = new TimerCallback(CheckTimer);
        public static Timer timer { get; set; }
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
            level = new Level1();
        }
        // Метод который отслеживает нажатие клавиш
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
                if (interLet.GetType() != typeof(RetryBox)) return false;
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
            if (!KeyAvailable) return Direction.Stop;
            ConsoleKey key = ReadKey(true).Key;
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
