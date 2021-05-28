using System;
using System.Collections.Generic;
using GDQScrapper.Core.Domain;
using GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor;
using GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor.Extensions;

namespace GDQScrapper.GDQProcessor.Domain
{
    public class HtmlExtractorService : IHtmlExtractorService
    {
        private readonly IHtmlRowExtractorService htmlRowExtractor;
        private readonly IHtmlEventExtractorService htmlEventExtractorService;

        public HtmlExtractorService(IHtmlRowExtractorService htmlRowExtractor, IHtmlEventExtractorService htmlEventExtractorService)
        {
            this.htmlRowExtractor = htmlRowExtractor;
            this.htmlEventExtractorService = htmlEventExtractorService;
        }

        public List<Event> CreateEventsOf(string raw)
        {
            List<Event> events = new List<Event>();

            var filterTable = Normalize(raw);
            var table = ExtractTable(filterTable);
            var tableBody = ExtractTableBody(table);
            var tableRowa = htmlRowExtractor.ExtractTableRows(tableBody);

            foreach (var row in tableRowa)
            {
                events.Add(htmlEventExtractorService.CreateEvent(row));
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
