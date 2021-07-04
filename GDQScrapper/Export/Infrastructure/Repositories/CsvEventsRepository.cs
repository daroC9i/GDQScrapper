using System;
using System.Collections.Generic;
using System.Text;
using GDQScrapper.Calendar.Domain;
using GDQScrapper.Core.Domain;
using GDQScrapper.Export.Domain;

namespace GDQScrapper.Export.Infrastructure.Repositories
{
    public class CsvEventsRepository :IEventsRepositoryService
    {
        private readonly IFileWriteService fileWriteService;

        private StringBuilder stringBuilder = new StringBuilder();
        private StringBuilder stringEventsBuilder = new StringBuilder();

        private const string SEPARATOR = ";";
        private const string NEWLINE = "\n";

        public CsvEventsRepository(IFileWriteService fileWriteService)
        {
            this.fileWriteService = fileWriteService;
        }

        public List<Event> Get()
        {
            throw new NotImplementedException();
        }

        public void Insert(List<Event> events)
        {
            stringEventsBuilder.Clear();

            foreach (var item in events)
            {
                stringEventsBuilder.Append(ConvertEventToStringLine(item));
                stringEventsBuilder.Append(NEWLINE);
            }

            var file = stringEventsBuilder.ToString();

            fileWriteService.ExportToFile(file, ExportConfiguration.DefaultEventsRepositoryFileName, "csv");
        }

        public void Update(List<Event> events)
        {
            throw new NotImplementedException();
        }

        public void Delete(List<Event> events)
        {
            throw new NotImplementedException();
        }

        private string ConvertEventToStringLine(Event eventToConvert)
        {
            stringBuilder.Clear();

            stringBuilder.Append(eventToConvert.EventId);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.StartDateTime);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.EndDateTime);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.Game);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.Runners);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.SetupLenght);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.EventDuration);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.Condition);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.GamePlatform);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.Hosts);

            return stringBuilder.ToString();
        }
    }
}
