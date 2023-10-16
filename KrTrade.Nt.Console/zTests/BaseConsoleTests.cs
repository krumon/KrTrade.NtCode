using System;

namespace KrTrade.Nt.Console.Tests
{
    public abstract class BaseConsoleTests
    {
        public abstract void Run();
        
        public void Title(string title)
        {
            string line = string.Empty;
            foreach (char c in title)
                line += "-";
            WriteLine(line);
            WriteLine(title.ToUpper());
            WriteLine(line);
        }
        
        public void Subtitle(string subtitle)
        {
            WriteLine($"*** {subtitle.ToUpper()} ***");
        }
        
        public void WriteLine(object o = null)
        {
            if (o == null)
            {
                System.Console.WriteLine();
                return;
            }
            System.Console.WriteLine(o.ToString());
        }

        public void Write(object o = null)
        {
            if (o != null)
                System.Console.Write(o);
        }

        public void NewLine()
        {
            System.Console.WriteLine();
        }
        
        public void Wait()
        {
            System.Console.WriteLine();
            System.Console.Write("Press any key to continue...");
            System.Console.ReadKey();
            System.Console.WriteLine();
        }

        public void Clear()
        {
            System.Console.Clear();
        }

        public void WaitAndClear()
        {
            System.Console.WriteLine();
            System.Console.Write("Press any key to continue...");
            System.Console.ReadKey();
            System.Console.Clear();
        }

    }
}
