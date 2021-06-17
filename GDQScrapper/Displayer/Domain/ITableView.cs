namespace GDQScrapper.Displayer.Views
{
    public interface ITableView
    {
        void AddColumn(string title, int lenght = 0);
        void AddValueToColumn(string columnTitle, string value);
        void SetTitle(string title);
        void ShowTable();
    }
}