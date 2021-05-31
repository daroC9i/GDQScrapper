using System.Collections.Generic;
using GDQScrapper.Calendar.Domain;
using GDQScrapper.Core.Domain;

namespace GDQScrapper.Calendar.Actions
{
    public class ExportToAppleEvents
    {
        private readonly AppleEventsService service;

        public ExportToAppleEvents(AppleEventsService service)
        {
            this.service = service;
        }

        public void Excecute(List<Event> events, string eventsName)
        {
            service.Export(events, eventsName);
        }
    }
}