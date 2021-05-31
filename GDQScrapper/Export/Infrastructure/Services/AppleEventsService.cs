using System;
using System.Collections.Generic;
using GDQScrapper.Export.Domain;
using GDQScrapper.Core.Domain;
using AppleEventsCreator;

namespace GDQScrapper.Calendar.Domain
{
    public class AppleEventsService : IEventsService
    {
        private readonly IFileWriteService fileWriteService;
        private AppleEventsCreatorService eventsCreator = new AppleEventsCreatorService();

        private readonly List<AppleEvent> appleEvents = new List<AppleEvent>();
        private string eventsName;

        public AppleEventsService(IFileWriteService fileWriteService)
        {
            this.fileWriteService = fileWriteService;
        }

        public void Export(List<Event> events, string eventsName)
        {
            this.eventsName = eventsName;
            PrepareListEventsForExport(events);
            var export = eventsCreator.CreateFile(appleEvents);
            fileWriteService.ExportToFile(export.Data, ExportConfiguration.DefaultAppleEventsFileName, export.FileExtension);
        }

        private void PrepareListEventsForExport(List<Event> events)
        {
            appleEvents.Clear();

            foreach (var item in events)
                appleEvents.Add(ConvertEventToAppleEvent(item));
        }

        private AppleEvent ConvertEventToAppleEvent(Event @event)
        {
            var eventSummay = @event.Game.ToString();

            if (!string.IsNullOrEmpty(eventsName))
                eventSummay += " - " + eventsName;

            return new AppleEvent(
                DateTime.Now,
                @event.StartDateTime.DateTime,
                @event.EndDateTime.DateTime,
                eventSummay,
                @event.Condition.ToString()
            );
        }
    }
}
