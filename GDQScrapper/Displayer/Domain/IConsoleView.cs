namespace GDQScrapper.Displayer.Views
{
    public interface IConsoleView
    {
        public int WindowHeight { get; }
        public int WindowWidth { get; }
        public int BufferHeight { get; }
        public int BufferWidth { get; }

        public void SetTitle(string title);
        public void AddLine(string line);
        public void AddString(string line);
    }
}
