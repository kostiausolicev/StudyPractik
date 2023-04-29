
using StudyPractic.Entites;

namespace StudyPractic.Objects.Abstract
{
    public abstract class Item
    {
        public abstract string Type { get; }
        public abstract string[] Sprite { get; }
        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public abstract void show();
        public abstract void clear();
        public abstract void collisium(Person person);
    }
}
