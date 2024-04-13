namespace Bank
{
    class Program
    {
        public static void Main()
        {
            string userInput = Console.ReadLine();
            Console.WriteLine(Calculate(userInput));
        }

        public static double Calculate(string userInput)
        {
            string[] text = userInput.Split(' ');
            var deposit = double.Parse(text[0], System.Globalization.CultureInfo.InvariantCulture);
            var percent = double.Parse(text[1]);
            var months = int.Parse(text[2]);
            return deposit * Math.Pow((1 + (percent / 12) / 100), months);
        }
    }
}