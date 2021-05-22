using GDQScrapper.Core.Domain;
using GDQScrapper.Core.Domain.EventData;
using GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor.Extensions;

namespace GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor
{
    public class HtmlEventExtractorService : IHtmlEventExtractorService
    {
        string dataRaw;

        public Event CreateEvent(string eventDataRaw)
        {
            dataRaw = eventDataRaw;

            var startEventDateTime = new StartEventDateTime(ExtractFirstRow());

            var game = new Game(ExtractFirstRow());

            var runner = new Runner(ExtractFirstRow());

            var setupLenghtDuration = new SetupLenghtDuration(ExtractFirstRow());

            var eventDuration = new EventDuration(ExtractFirstRow());

            var condition = new Condition(ExtractFirstRow());

            var host = new Host(ExtractFirstRow());

            var endTime = new EndEventDateTime(startEventDateTime.DateTime.Add(eventDuration.TimeSpan));

            return new Event(startEventDateTime, game, runner, setupLenghtDuration, eventDuration, endTime, condition, host);
        }

        private string ExtractFirstRow()
        {
            var row = dataRaw.ExtractFirstRowWithTag("td");
            var normalizedRow = Normalize(row);
            dataRaw = dataRaw.RemoveFirstTag("td");

            return normalizedRow;
        }

        private string Normalize(string dataRow)
        {
            return dataRow.RemoveTag("td").TryRemoveTag("i").RemoveSpacesInFronAndBack();
        }
    }
}
