using System;

namespace KrTrade.Nt.Core.Helpers
{
    public static class ConsoleHelpers
    {
        public static void Title(string title)
        {
            string line = string.Empty;
            foreach (char c in title)
                line += "-";
            Console.WriteLine(line);
            Console.WriteLine(title.ToUpper());
            Console.WriteLine(line);
        }
        
        public static void Write(string msg = "")
        {
            Console.WriteLine(msg);
        }

        public static void NewLine()
        {
            Console.WriteLine();
        }
        
        public static void Wait()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        public static void Clear()
        {
            Console.Clear();
        }

        public static void WaitAndClear()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

    }
}
