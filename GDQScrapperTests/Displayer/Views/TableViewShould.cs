using GDQScrapper.Displayer.Views;
using NUnit.Framework;

namespace Tests.Displayer.Views
{
    public class TableViewShould
    {
        private TableView tableView;

        private static string SomeTitle = "SomeTitle";
        private static string SomeColumnTitle = "SomeColumnTitle";
        private static string SomeOtherColumnTitle = "SomeOtherColumnTitle";

        [SetUp]
        public void Setup()
        {
            IConsoleView consoleView = new DotNetConsole();
            tableView = new TableView(consoleView);
        }

        [Test]
        public void ShowEmptyTable()
        {
            // Given
            tableView.SetTitle(SomeTitle);

            tableView.AddColumn(SomeColumnTitle);
            tableView.AddValueToColumn(SomeColumnTitle, "value 1");
            tableView.AddValueToColumn(SomeColumnTitle, "value 2");
            tableView.AddValueToColumn(SomeColumnTitle, "value 3");

            tableView.AddColumn(SomeOtherColumnTitle);
            tableView.AddValueToColumn(SomeOtherColumnTitle, "other value 1");
            tableView.AddValueToColumn(SomeOtherColumnTitle, "other value 2");
            tableView.AddValueToColumn(SomeOtherColumnTitle, "other value 3");

            // Where
            tableView.ShowTable();
        }
    }
}
