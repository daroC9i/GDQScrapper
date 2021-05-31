using System.Collections.Generic;
using GDQScrapper.Calendar.Domain;
using System.Text;
using GDQScrapper.Core.Domain;

namespace GDQScrapper.Export.Domain
{
    public class CsvService : IEventsService
    {
        private StringBuilder stringBuilder = new StringBuilder();

        private const string SEPARATOR = ",";
        private const string SPACE = "\n";

        private const string FILE_EXTENCION = "csv";

        private readonly IFileWriteService fileWriteService;

        public CsvService(IFileWriteService fileWriteService)
        {
            this.fileWriteService = fileWriteService;
        }

        public void Export(List<Event> events, string eventsName)
        {
            stringBuilder.Clear();

            CreateHeader();
            
            foreach (var item in events)
            {
                SerializeEvent(item);
            }

            fileWriteService.ExportToFile(
                stringBuilder.ToString(),
                ExportConfiguration.DefaultCSVFileName,
                FILE_EXTENCION
            );
        }

        private void CreateHeader()
        {
            stringBuilder.Append("Game");
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append("Condition");
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append("SetupLenght");
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append("StartDateTime");
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append("Duration");
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append("EndDateTime");
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append("Runners");
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append("Host");

            stringBuilder.Append(SPACE);
        }

        private void SerializeEvent(Event @event)
        {
            stringBuilder.Append(@event.Game.ToString());
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append(@event.Condition.ToString());
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append(@event.SetupLenght.ToString());
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append(@event.StartDateTime.ToString());
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append(@event.EventDuration.ToString());
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append(@event.EndDateTime.ToString());
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append(@event.Runners.ToString());
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append(@event.Host.ToString());

            stringBuilder.Append(SPACE);
        }
    }
}
