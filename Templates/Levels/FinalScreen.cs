using static System.Console;
using StudyPractic.Entites;
using StudyPractic.Templates.Abstract;
using StudyPractic.Templates.Interfaces;
using StudyPractic.Objects.Lets;
using StudyPractic.Objects.Abstract;

namespace StudyPractic.Templates.Levels
{
    public class FinalScreen : AbstractionLevel
    {
        public override int timeOnLevel => 0;

        public override Let lets { get; set; } = new Let(
            new string[]
                {
                    "#################################################_____________________######################################################################",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##            Вы прошли эту небольшую игру!                                                                                               ##",
                    "##                                                                                                                                        ##",
                    "##            Автор игры: Усольцев Констатин студент группы 4228                                                                          ##",
                    "##                                                                                                                                        ##",
                    "##            Игра написана на языке программирования С#                                                                                  ##",
                    "##                                                                                                                                        ##",
                    "##            Чтобы выйти в главное меню нажмите ESC                                                                                      ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
                    "##                                                                                                                                        ##",
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
        public override List<AbstractInteractLet> interactLets { get; set; }
        public override List<Item> items { get; set; }

        public override void show(string[] sprite, out AbstractionLevel level, ref int code)
        {

            // отрисовка шаблона кровня
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < Pattern.LevelHigth - 7; i++)
            {
                if (i == 0) Console.WriteLine(Pattern.SpriteTop);
                else if (i == 41) Console.WriteLine(Pattern.SpriteInventory);
                else if (i == 42) Console.WriteLine(Pattern.SpriteBottom);
                else Console.WriteLine(Pattern.SpritePlate);
            }
            lets.show();
            Thread.Sleep(1000);
            while (!finalLevel(ref code));
            level = new FinalScreen();
        }

        /*public override void showToLook()
        {
            throw new NotImplementedException();
        }*/
        public override bool finalLevel(ref int code)
        {
            if (!KeyAvailable) return false;
            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Escape) return true;
            return false;
        }
        public override void StartLevel(Person person, ref int code)
        {
            throw new NotImplementedException();
        }
        public override Direction ReadMovementAndServiceButton(Direction direction, ref int code)
        {
            throw new NotImplementedException();
        }

        public override int ReadServiceButtons(ConsoleKey key)
        {
            throw new NotImplementedException();
        }
    }
}
