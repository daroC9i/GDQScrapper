using System;
using System.Collections.Generic;
using GDQScrapper.Calendar.Domain;
using GDQScrapper.Core.Domain;

namespace GDQScrapper.Export.Actions
{
    public class GetAllEvents
    {
        private readonly IEventsRepositoryService eventsRepositoryService;

        public GetAllEvents(IEventsRepositoryService eventsRepositoryService)
        {
            this.eventsRepositoryService = eventsRepositoryService;
        }

        public List<Event> Execute()
        {
            return eventsRepositoryService.GetAll();
        }
    }
}
