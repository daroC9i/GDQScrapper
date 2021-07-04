using GDQScrapper.HtmlDataExtractor.Domain;
using GDQScrapper.HtmlDataExtractor.Domain.Exceptions;
using HTMLExtensionTools;

namespace GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor
{
    public class HtmlEventExtractorService : IHtmlEventExtractorService
    {
        string dataRaw;

        public RawEvent CreateRawEvent(string eventDataRaw)
        {
            dataRaw = eventDataRaw;

            var startEventDateTime = ExtractFirstRow();

            var game = ExtractFirstRow();

            var runner = ExtractFirstRow();

            var setupLenghtDuration = NormalizeDuration(ExtractFirstRow());

            var eventDuration = NormalizeDuration(ExtractFirstRow());

            var conditionAndPlatform = SplitConditionFromPlatform(ExtractFirstRow());
            var condition = conditionAndPlatform[0].Trim();
            var gamePlatform = conditionAndPlatform[1].Trim();

            var host = ExtractFirstRow();

            return new RawEvent(startEventDateTime, game, runner, setupLenghtDuration, eventDuration, null, condition, gamePlatform, host);
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
            var row = dataRaw.ExtractFirstSingleWithTag("td");
            var normalizedRow = Normalize(row);
            dataRaw = dataRaw.RemoveFirstSingleTag("td");

            return normalizedRow;
        }

        private string Normalize(string dataRow)
        {
            return dataRow.RemoveSingleOnlyTag("td").TryRemoveSinleTag("i").RemoveSpacesInFronAndBack();
        }
    }
}
