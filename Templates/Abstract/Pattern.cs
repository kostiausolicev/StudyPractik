namespace StudyPractic.Templates.Abstract
{
    // Набор шаблонов для уровней
    class Pattern
    {
        // Высота и ширина уровня
        public static int LevelHigth { get; } = 50;
        public static int LevelWight { get; } = 140;
        public static string SpriteTop { get; set; } = new string("#################################################<                  >######################################################################");
        public static string SpriteBottom { get; set; } = string.Join("", Enumerable.Repeat("#", 140));
        // Спрайт для инвенторя (там планируется отмечать найденные ключи
        public static string SpriteInventory { get; set; } = '#' + string.Join("#", Enumerable.Repeat("##################################", 4)) + "\n#" +
                                                             string.Join("#", Enumerable.Repeat("##################################", 4)) + "\n#" +
                                                             string.Join("#", Enumerable.Repeat("##                              ##", 4)) + "\n#" +
                                                             string.Join("#", Enumerable.Repeat("##                              ##", 4)) + "\n#" +
                                                             string.Join("#", Enumerable.Repeat("##                              ##", 4)) + "\n#" +
                                                             string.Join("#", Enumerable.Repeat("##################################", 4)) + "\n#" +
                                                             string.Join("#", Enumerable.Repeat("##################################", 4));
        // Спрайт для обычной строки поля
        public static string SpritePlate { get; set; } = "#" + string.Join("", Enumerable.Repeat(" ", 138)) + "#";
    }
}
