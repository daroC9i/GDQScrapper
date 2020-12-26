using GDQScrapper.Core.Domain;
using GDQScrapper.Core.Domain.EventData;
using GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor.Extensions;

namespace GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor
{
    public class HtmlEventExtractorService : IHtmlEventExtractorService
    {
        public Event CreateEvent(string dataRow)
        {
            var row = dataRow;

            // StartTime
            var startTime = new StartDateTime(Normalize(row.ExtractFirstWithTag("td")));
            row = row.RemoveFirstTag("td");

            // Run
            var game = new Game(Normalize(row.ExtractFirstWithTag("td")));
            row = row.RemoveFirstTag("td");

            // Runner
            var runner = new Runner(Normalize(row.ExtractFirstWithTag("td")));
            row = row.RemoveFirstTag("td");

            // SetupTime
            var setupLenght = new SetupLenght(Normalize(row.ExtractFirstWithTag("td")));
            row = row.RemoveFirstTag("td");

            // Duration
            var duration = new Duration(Normalize(row.ExtractFirstWithTag("td")));
            row = row.RemoveFirstTag("td");

            // Condition
            var condition = new Condition(Normalize(row.ExtractFirstWithTag("td")));
            row = row.RemoveFirstTag("td");

            // Host
            var host = new Host(Normalize(row.ExtractFirstWithTag("td")));

            var endTime = new EndDateTime(startTime.DateTime.Add(duration.TimeSpan));

            return new Event(startTime, game, runner, setupLenght, duration, endTime, condition, host);
        }

        private string Normalize(string dataRow)
        {
            return dataRow.RemoveTag("td").TryRemoveTag("i").RemoveSpacesInFronAndBack();
        }
    }
}
