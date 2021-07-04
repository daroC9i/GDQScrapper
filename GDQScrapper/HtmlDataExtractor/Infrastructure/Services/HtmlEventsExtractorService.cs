using System;
using System.Collections.Generic;
using GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor;
using GDQScrapper.HtmlDataExtractor.Domain;
using HTMLExtensionTools;

namespace GDQScrapper.GDQProcessor.Domain
{
    public class HtmlEventsExtractorService : IHtmlEventsExtractorService
    {
        private readonly IHtmlRowExtractorService htmlRowExtractor;
        private readonly IHtmlEventExtractorService htmlEventExtractorService;

        public HtmlEventsExtractorService(IHtmlRowExtractorService htmlRowExtractor, IHtmlEventExtractorService htmlEventExtractorService)
        {
            this.htmlRowExtractor = htmlRowExtractor;
            this.htmlEventExtractorService = htmlEventExtractorService;
        }

        public List<RawEvent> CreateEventsOf(string raw)
        {
            List<RawEvent> events = new List<RawEvent>();

            var filterTable = Normalize(raw);
            var table = ExtractTable(filterTable);
            var tableBody = ExtractTableBody(table);
            var tableRowa = htmlRowExtractor.ExtractTableRows(tableBody);

            foreach (var row in tableRowa)
            {
                events.Add(htmlEventExtractorService.CreateRawEvent(row));
            }

            return events;
        }

        private string Normalize(string raw)
        {
            return raw
                .Replace("\n", "")
                .Replace("\r", "")
                .Replace("\t", "")
                .Replace("&mdash;","—")
                .Replace("&#039;", "'")
                .Replace("&amp;", "&")
                .RemoveSpacesBetweenBrackets();
        }

        private string ExtractTable(string raw)
        {
            var startIndex = raw.IndexOf("<table");
            var endIndex = raw.IndexOf("</table>");

            var table = raw.Substring(startIndex, endIndex - startIndex + 8);

            if (string.IsNullOrEmpty(table))
                throw new Exception();

            return table;
        }

        private string ExtractTableBody(string table)
        {
            var startIndex = table.IndexOf("<tbody");
            var endIndex = table.IndexOf("</tbody>");

            var tableBody = table.Substring(startIndex, endIndex - startIndex + 8); // leght of </tbody>

            if (string.IsNullOrEmpty(tableBody))
                throw new Exception();

            return tableBody;
        }
    }
}
