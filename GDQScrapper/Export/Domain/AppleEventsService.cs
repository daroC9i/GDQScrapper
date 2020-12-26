using System;
using System.Collections.Generic;
using System.Text;
using GDQScrapper.Export.Domain;
using GDQScrapper.Core.Domain;

namespace GDQScrapper.Calendar.Domain
{
    public class AppleEventsService : IEventsService
    {
        private StringBuilder stringBuilder = new StringBuilder();

        private const string BEGIN_CALENDAR = "BEGIN:VCALENDAR";
        private const string CALENDAR_VERSION = "VERSION:2.0";
        private const string BEGIN_EVENT = "BEGIN:VEVENT";
        private const string CREATED_EVENT = "CREATED:";
        private const string URL_EVENT = "URL:";
        private const string START_DATETIME_EVENT = "DTSTART:";
        private const string END_DATETIME_EVENT = "DTEND:";
        private const string SUMMARY_EVENT = "SUMMARY:";
        private const string DESCRIPTION_EVENT = "DESCRIPTION:";
        private const string LOCATION_EVENT = "LOCATION:";
        private const string SEQUENCE_EVENT = "SEQUENCE:0"; 
        private const string UID_EVENT = "UID:";
        private const string END_EVENT = "END:VEVENT";
        private const string END_CALENDAR = "END:VCALENDAR";

        private const string FILE_EXTENCION = "ics";

        private const string SPACE = "\n";

        private readonly IFileWriteService fileWriteService;

        public AppleEventsService(IFileWriteService fileWriteService)
        {
            this.fileWriteService = fileWriteService;
        }

        public void Export(List<Event> events)
        {
            stringBuilder.Clear();
            stringBuilder.Append(BEGIN_CALENDAR);
            stringBuilder.Append(SPACE);

            stringBuilder.Append(CALENDAR_VERSION);
            stringBuilder.Append(SPACE);

            foreach (var item in events)
            {
                ExportEvent(item);
            }

            stringBuilder.Append(END_CALENDAR);

            Console.WriteLine(stringBuilder.ToString());

            fileWriteService.ExportToFile(
                stringBuilder.ToString(),
                ExportConfiguration.DefaultAppleEventsFileName,
                FILE_EXTENCION
            );
        }

        private void ExportEvent(Event @event)
        {
            stringBuilder.Append(BEGIN_EVENT);
            stringBuilder.Append(SPACE);

            stringBuilder.Append(CREATED_EVENT);
            stringBuilder.Append(FormatDateTime(DateTime.Now));
            stringBuilder.Append(SPACE);

            stringBuilder.Append(START_DATETIME_EVENT);
            stringBuilder.Append(FormatDateTime(@event.StartDateTime.DateTime));
            stringBuilder.Append(SPACE);

            stringBuilder.Append(END_DATETIME_EVENT);
            stringBuilder.Append(FormatDateTime(@event.EndDateTime.DateTime));
            stringBuilder.Append(SPACE);

            stringBuilder.Append(SUMMARY_EVENT);
            stringBuilder.Append(@event.Game);
            stringBuilder.Append(" - AGDQ 2021"); // HARDCODE
            stringBuilder.Append(SPACE);

            stringBuilder.Append(DESCRIPTION_EVENT);
            stringBuilder.Append(@event.Condition);
            stringBuilder.Append(SPACE);

            stringBuilder.Append(SEQUENCE_EVENT);
            stringBuilder.Append(SPACE);

            stringBuilder.Append(END_EVENT);
            stringBuilder.Append(SPACE);
        }

        private string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString("s").Replace("-", "").Replace(":", "") + "Z";
        }
    }
}
