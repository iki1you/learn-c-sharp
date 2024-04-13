namespace Pluralize
{
    public static class PluralizeTask
    {
        public static string PluralizeRubles(int count)
        {
            if (count % 100 >= 5 && count % 100 <= 19 || count % 10 >= 5 || count % 10 == 0)
                return "рублей";
            if (count % 10 == 1)
                return "рубль";
            return "рубля";
        }
    }
}