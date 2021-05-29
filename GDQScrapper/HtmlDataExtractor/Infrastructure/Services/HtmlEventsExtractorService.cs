using GDQScrapper.Core.Domain;
using GDQScrapper.Core.Domain.EventData;
using GDQScrapper.HtmlDataExtractor.Domain.Exceptions;
using HTMLExtensionTools;

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

            var setupLenghtDuration = new SetupLenghtDuration(NormalizeDuration(ExtractFirstRow()));

            var eventDuration = new EventDuration(NormalizeDuration(ExtractFirstRow()));

            var conditionAndPlatform = SplitConditionFromPlatform(ExtractFirstRow());

            var condition = new Condition(conditionAndPlatform[0].Trim());
            var gamePlatform = new GamePlatform(conditionAndPlatform[1].Trim());


            var host = new Host(ExtractFirstRow());

            var endTime = new EndEventDateTime(startEventDateTime.DateTime.Add(eventDuration.TimeSpan));

            return new Event(startEventDateTime, game, runner, setupLenghtDuration, eventDuration, endTime, condition, gamePlatform, host);
        }

        private string [] SplitConditionFromPlatform(string raw)
        {
            string[] result = raw.Split('—');

            if(result.Length != 2)
                throw new InvalidNormailizeDataException("Invalid split with '—' with: " + raw);

            return result;
        }

        private string NormalizeDuration(string rawDuration)
        {
            rawDuration = rawDuration.Trim();

            if (string.IsNullOrEmpty(rawDuration))
                rawDuration = "00:00:00";

            return rawDuration;
        }

        private string ExtractFirstRow()
        {
            var row = dataRaw.ExtractFirstSingleWithTag("td"); // ExtractFirstSingleWithTag
            var normalizedRow = Normalize(row);
            dataRaw = dataRaw.RemoveFirstSingleTag("td"); // RemoveFirstSingleTag

            return normalizedRow;
        }

        private string Normalize(string dataRow)
        {
            // RemoveSingleOnlyTag + TryRemoveSinleTag + RemoveSpacesInFronAndBack
            return dataRow.RemoveSingleOnlyTag("td").TryRemoveSinleTag("i").RemoveSpacesInFronAndBack();
        }
    }
}
