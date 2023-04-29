using game;
using StudyPractic.Entites;
using StudyPractic.Objects.Abstract;
using StudyPractic.Objects.Keys;
using StudyPractic.Objects.Lets;
using StudyPractic.Templates.Abstract;
using StudyPractic.Templates.Interfaces;
using System.Diagnostics;
using static System.Console;

namespace StudyPractic.Templates.Levels
{
    internal class Level0 : AbstractionLevel
    {
        // Константы отвечающие за координаты спавна игрока
        public const int SpavnPersoneX = 105;
        public const int SpavnPersoneY = 41;
        // Статические препятствия, они же представляют карту по которой ходит персонаж
        public override Let lets { get; set; } = new Let(
                new string[]
                {
                    @"#################################################_____________________######################################################################",
                    @"##                                                                      <- Это зона выхода, сюда нужно, когда ты получил ключ           / ##",
                    @"##          <- Это ключ без которого                                                                                                   /  ##",
                    @"##             нельзя закончить уровень                                                                   Тут в правом верхнем углу       ##",
                    @"##                                                                                                        время, выделенноена уровень     ##",
                    @"##                                                                                                        и координаты персонажа          ##",
                    @"##                                                                                                                                        ##",
                    @"##                                                                                                                                        ##",
                    @"##             <- Эта коробка при касании                                                                                                 ##",
                    @"##                перезапустит уровень, будте аккуратнее                                                                                  ##",
                    @"##                на некоторых уровнях движущиеся препятствия                                                                             ##",
                    @"##                будут двигать вас к таким коробкам                                                                                      ##",
                    @"##                                                                                                                                        ##",
                    @"##                                                                                                                                        ##",
                    @"##                                                                                                                                        ##",
                    @"##     #################                                                                                                                  ##",
                    @"##     #               #                                                                                                                  ##",
                    @"##     #               # <- препятствие в клетке само двигается                                                                           ##",
                    @"##     #               #    и сбивает персонажа, но не может                                                                              ##",
                    @"##     ########   ######    сдвигуть коробку                                                                                              ##",
                    @"##                                                                                                                                        ##",
                    @"##                                                                                                                                        ##",
                    @"##                                                                                                                                        ##",
                    @"##                       <- это коробка, ее можно двигать                                  Добро пожаловать!                              ##",
                    @"##                                                                                         Это обучение, тут будет краткая информация     ##",
                    @"##                                                                                         об управлении, цели игры и предметах в игре    ##",
                    @"##                                                                                                                                        ##",
                    @"##                          это кнопка, если на нее встать,                                Передвижение: WASD или стрелки                 ##",
                    @"##                       <- или поставить коробку                                          R - рестарт, ESC - выход в клавное меню        ##",
                    @"##                          то откроются или закроются                                     На остальных уровнях эта информация            ##",
                    @"##                          некоторые двери                                                будет с правой стороны карты                   ##",
                    @"##                                                                                                                                        ##",
                    @"##     ######                                                                                                                             ##",
                    @"##     #                                                                                                                                  ##",
                    @"##     #                 <- это ключ от некоторых дверей                                                                                  ##",
                    @"##     #                    чтобы их открыть к ним надо подойти                                                                           ##",
                    @"##     ######               с ключом (двери окрасятся серым)                                                                              ##",
                    @"##                                                                                                                                        ##",
                    @"##                                                                                                                                        ##",
                    @"##                                                                                                                                        ##",
                    @"##      это твой инвентарь, тут появятся ключи                                                               <- это твой персонаж         ##",
                    @"##            v                                                                                                                           ##",
                    @"############################################################################################################################################",
                }
            );
        /*
                Список элементов, которые реализуют абстрактный класс AbstractInteractLet
                которому соответсвуют все интерактивные объекты на карте и методы работы с ними (проверка коллизий, отрисовка и тп)
        */
        public override List<Objects.Abstract.AbstractInteractLet> interactLets { get; set; } = new List<Objects.Abstract.AbstractInteractLet>{
            new MovingLet(new string[] {"#", "#",}, 8, 17, Direction.Right),
            new MovingLet(new string[] {"#", }, 8, 19, Direction.Right),
            new Box(new string[] {"#",}, 12, 19),
            new Box(new string[] {"#####", "#####", "#####"}, 18, 24),
            new RetryBox(new string[] {"<###>", "<###>"}, 8, 9),
            new Button(new string[] {"|+|"}, 19, 29),
            new DoorForButton(new string[] {"|", "|", "|"}, 11, 28),
            new DoorForButton(new string[] {"|", "|", "|"}, 7, 28),
            new DoorForButton(new string[] {"---"}, 8, 28),
            new DoorForButton(new string[] {"---"}, 8, 30),
            new DoorForKey(new string[] {"|", "|", "|"}, 12, 34),
        };
        // Список ключей
        public override List<Item> items { get; set; } = new List<Item>()
        {
            new KeyForDoor(18, 35),
            new FinalKey(8, 4),
        };
        // Блок который нужен для работы таймера
        public static TimerCallback tm { get; set; } = new TimerCallback(CheckTimer);
        public static Timer timer { get; set; }
        public static int TimeOnLevel { get; set; } = 180;
        public override int timeOnLevel => 180;
        // Метод который запускает уровень
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
        // Метод который отслеживает выполнение условий победы или проигрыша
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
                    code = 0;
                    timer.Dispose();
                    TimeOnLevel = 180;
                    return true;
                }
            }
            return false;
        }
        // Метод который следит за нажатием клавиш
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
        // Метод который следит за нажатием клавиш R и ESC
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
            level = new Level0();
        }
        // Метод который каждую секунду уменьшает счетчик времени
        public static void CheckTimer(object obj)
        {
            TimeOnLevel--;
        }
    }
}
