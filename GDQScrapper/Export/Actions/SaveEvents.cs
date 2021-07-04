using System;
using System.Collections.Generic;
using GDQScrapper.Calendar.Domain;
using GDQScrapper.Core.Domain;

namespace GDQScrapper.Export.Actions
{
    public class SaveEvents
    {
        private readonly IEventsRepositoryService eventsRepositoryService;

        public SaveEvents(IEventsRepositoryService eventsRepositoryService)
        {
            this.eventsRepositoryService = eventsRepositoryService;
        }


        public void Execute(List<Event> events)
        {
            eventsRepositoryService.Insert(events);
        }
    }
}
