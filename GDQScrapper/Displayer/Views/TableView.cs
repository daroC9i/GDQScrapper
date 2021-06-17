using System;
using System.Collections.Generic;
using System.Text;

namespace GDQScrapper.Displayer.Views
{
    public class TableView : ITableView
    {
        private const string VERTICAL_LINE = "—";
        private const string HORIZONTAL_LINE = "|";
        private const string NEW_LINE = "\n";
        private const string SPACE = " ";
        private const string THREE_POINTS = "...";

        private const int DEFAULT_CONSOLE_WIDTH = 100;

        private readonly IConsoleView consoleView;

        private StringBuilder stringBuilder = new StringBuilder();
        private Dictionary<string, TableColumn> columns = new Dictionary<string, TableColumn>();

        private int windowWidth;
        private int windowHeight;
        private int columnsCounter;

        private string Title;


        public TableView(IConsoleView consoleView)
        {
            this.consoleView = consoleView;
        }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void AddColumn(string title, int maxWidth = 0)
        {
            var newColumn = new TableColumn(title, columnsCounter);

            if (maxWidth != 0)
                newColumn.SetMaxWidth(maxWidth);

            columns.Add(title, newColumn);
            ++columnsCounter;
        }

        public void AddValueToColumn(string columnTitle, string value)
        {
            columns[columnTitle].AddValue(value);
        }

        public void ShowTable()
        {
            stringBuilder.Clear();
            windowWidth = consoleView.WindowWidth == 0 ? DEFAULT_CONSOLE_WIDTH : consoleView.WindowWidth;
            windowHeight = consoleView.WindowHeight;

            WriteTitle();
            WriteColumnsHeaders();
            WriteSeparator();
            WriteColumnsValues();

            consoleView.AddLine(stringBuilder.ToString());
        }

        private void WriteTitle()
        {
            int totalLenght = 0;

            foreach (var column in columns.Values)
                totalLenght += column.Width + 3;


            double windowsHalfWidth = totalLenght / 2;
            double titleHalfWidth = Title.Length / 2;
            var spacesForTitle = Math.Round(windowsHalfWidth, 0) - Math.Round(titleHalfWidth, 0);

            for (int i = 0; i < spacesForTitle; i++)
                stringBuilder.Append(SPACE);

            stringBuilder.Append(Title);
            stringBuilder.Append(NEW_LINE);
        }

        private void WriteColumnsHeaders()
        {
            foreach (var column in columns.Values)
            {
                stringBuilder.Append(SPACE);
                var columnTitle = column.Title;
                double columnHalfWidth = column.Width / 2;
                double titleHalfWidth = column.Title.Length / 2;
                var spacesFrontTitle = Math.Round(columnHalfWidth, 0) - Math.Round(titleHalfWidth, 0);

                for (int i = 0; i < spacesFrontTitle; i++)
                    stringBuilder.Append(SPACE);


                if (columnTitle.Length > column.Width)
                    columnTitle = columnTitle.Substring(0, column.Width - THREE_POINTS.Length) + THREE_POINTS;

                stringBuilder.Append(columnTitle);

                var spacesPostTitle = column.Width - spacesFrontTitle - column.Title.Length;

                for (int i = 0; i < spacesPostTitle; i++)
                    stringBuilder.Append(SPACE);

                stringBuilder.Append(SPACE);
                stringBuilder.Append(HORIZONTAL_LINE);
            }

            stringBuilder.Append(NEW_LINE);
        }

        private void WriteSeparator()
        {
            int totalLenght = 0;

            foreach (var column in columns.Values)
                totalLenght += column.Width + 3;

            for (int i = 0; i < totalLenght; i++)
                stringBuilder.Append(VERTICAL_LINE);

            stringBuilder.Append(NEW_LINE);
        }

        private void WriteColumnsValues()
        {
            int maxIndexValue = 0;

            foreach (var column in columns.Values)
            {
                if (column.Lenght > maxIndexValue)
                    maxIndexValue = column.Lenght;
            }

            for (int indexValue = 0; indexValue < maxIndexValue; indexValue++)
            {
                foreach (var column in columns.Values)
                {
                    stringBuilder.Append(SPACE);
                    var value = column.GetColumnValue(indexValue);
                    var spacesToEndOfColumn = column.Width - value.Length;

                    if (value.Length > column.Width)
                        value = value.Substring(0, column.Width - THREE_POINTS.Length) + THREE_POINTS;

                    stringBuilder.Append(value);

                    for (int j = 0; j < spacesToEndOfColumn; j++)
                        stringBuilder.Append(SPACE);

                    stringBuilder.Append(SPACE);
                    stringBuilder.Append(HORIZONTAL_LINE);
                }

                stringBuilder.Append(NEW_LINE);
            }
        }

        private class TableColumn
        {
            public string Title { get; }
            public int Index { get; }
            public int Width { get { return maxWidth == 0 ? dynamicWidth : maxWidth; } }
            public int Lenght { get { return values.Count; } }

            private int maxWidth = 0;
            private int dynamicWidth = 0;

            private List<string> values = new List<string>();


            public TableColumn(string title, int index)
            {
                Title = title;
                Index = index;
                dynamicWidth = title.Length;
            }

            internal void SetMaxWidth(int maxWidth)
            {
                this.maxWidth = maxWidth;
            }

            internal void AddValue(string value)
            {
                values.Add(value);

                if (value.Length > dynamicWidth)
                    dynamicWidth = value.Length;
            }

            internal string GetColumnValue(int index)
            {
                return values[index];
            }
        }
    }
}
