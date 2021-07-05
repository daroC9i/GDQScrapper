using System;
using GDQScrapper.Calendar.Domain;

namespace GDQScrapper.Export.Actions
{
    public class DeleteAllEvents
    {
        private readonly IEventsRepositoryService eventsRepositoryService;

        public DeleteAllEvents(IEventsRepositoryService eventsRepositoryService)
        {
            this.eventsRepositoryService = eventsRepositoryService;
        }

        public void Execute()
        {
            eventsRepositoryService.DeleteAll();
        }

    }
}
