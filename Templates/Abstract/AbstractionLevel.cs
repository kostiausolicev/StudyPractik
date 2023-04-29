using game;
using StudyPractic.Entites;
using StudyPractic.Objects.Abstract;
using StudyPractic.Objects.Lets;
using StudyPractic.Templates.Abstract;

namespace StudyPractic.Templates.Interfaces
{
    public abstract class AbstractionLevel
    {
        public string[] techText { get; } = new string[]
        {
            "Приветствую, это корокий рассказ о предметах в этой игре:",
            "### - это препятствия через которые нельзя пройти",
            "!blue", "###", "!green", " - это препятствия которые можно двигать",
            "!red", "###", "!green", " - это препятствия которые сами двигаются",
            "!white", "???", "!green", " - это ваш персонаж (? - ваш скин)",
            "!yellow", @"\-/", "!green", "",
            "!yellow", @" |_", "!green", " - это финальный ключ",
            "!yellow", @" |-", "!green", "",
            "___ - это зона к которой надо подойти с ключом, чтобы ", "\t   закончить уровень",
             "!redBG", "!white",  "|+|", "!blackBG", "!green", " - это кнопка, которая открывает некоторые двери",
            "",
            "Немного об управлении:",
            "W - вверх A - влево S - вниз D - вправо, R - рестарт уровня, ",
            "ESC - выход в главное меню"
        };
        // Константа отвечающая за время, через которое сменяются кадры
        public const int FrameMs = 8;
        public abstract int timeOnLevel { get; }
        public Person person {get; set;}
        public Direction direction { get; set; }
        public abstract Let lets { get; set; }
        public abstract List<Objects.Abstract.AbstractInteractLet> interactLets { get; set; }
        public abstract List<Item> items { get; set; }
        public abstract void show(string[] sprite, out AbstractionLevel level, ref int code);
        public abstract bool finalLevel(ref int code);
        public abstract void StartLevel(Person person, ref int codeLvl);
        public abstract Direction ReadMovementAndServiceButton(Direction direction, ref int codeLvl);
        public abstract int ReadServiceButtons(ConsoleKey key);
        public void clear()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Pattern.LevelHigth + 1; i++)
            {
                Console.WriteLine(String.Join("", Enumerable.Repeat(" ", 210)));
            }
        }
    }
}
