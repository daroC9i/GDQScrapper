using System;

namespace GDQScrapper.Displayer.Views
{
    public class DotNetConsole : IConsoleView
    {
        public int WindowHeight => Console.WindowHeight;
        public int WindowWidth => Console.WindowWidth;
        public int BufferHeight => Console.BufferHeight;
        public int BufferWidth => Console.BufferWidth;

        public void SetTitle(string title)
        {
            Console.Title = title;
        }

        public void AddLine(string line)
        {
            Console.WriteLine(line);
        }

        public void AddString(string value)
        {
            Console.Write(value);
        }
    }
}
