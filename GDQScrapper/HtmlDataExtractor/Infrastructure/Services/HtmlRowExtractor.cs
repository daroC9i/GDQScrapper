using System.Collections.Generic;
using HTMLExtensionTools;

namespace GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor
{
    public class HtmlRowExtractorService : IHtmlRowExtractorService
    {
        private const string TABLE_ROW_START_TAG_BEGIN = "<tr";
        private const string TABLE_ROW_END_TAG = "</tr>";

        public List<string> ExtractTableRows(string tableBody)
        {
            List<string> tableRows = new List<string>();

            while (true)
            {
                if (string.IsNullOrEmpty(tableBody))
                    break;

                var firstRowStartIndex = tableBody.IndexOf(TABLE_ROW_START_TAG_BEGIN);
                var firstRowEndIndex = tableBody.IndexOf(TABLE_ROW_END_TAG);
                var firstRowlenght = firstRowEndIndex - firstRowStartIndex + TABLE_ROW_END_TAG.Length;

                if (firstRowStartIndex < 0 || firstRowEndIndex < 0)
                    break;

                int secondRowStartPoint = firstRowEndIndex + TABLE_ROW_END_TAG.Length;
                var secondRowStartIndex = tableBody.IndexOf(TABLE_ROW_START_TAG_BEGIN, secondRowStartPoint);
                var secondRowEndIndex = tableBody.IndexOf(TABLE_ROW_END_TAG, secondRowStartPoint);
                var secondRowlenght = secondRowEndIndex - secondRowStartIndex + TABLE_ROW_END_TAG.Length;

                if (secondRowStartIndex < 0 || secondRowEndIndex < 0)
                    break;

                var firtsRow = tableBody.Substring(firstRowStartIndex, firstRowlenght);
                var secondRow = tableBody.Substring(secondRowStartIndex, secondRowlenght);

                firtsRow = firtsRow.RemoveSingleOnlyTag("tr");
                secondRow = secondRow.RemoveSingleOnlyTag("tr");

                tableRows.Add(string.Concat(firtsRow, secondRow));

                int removeRowLenght = secondRowEndIndex - firstRowStartIndex + TABLE_ROW_END_TAG.Length;
                tableBody = tableBody.Remove(firstRowStartIndex, removeRowLenght);
            }

            return tableRows;
        }
    }
}
