using System.Collections.Generic;
using GDQScrapper.Core.Domain;
using GDQScrapper.GDQProcessor.Domain;

namespace GDQScrapper.GDQProcessor.Infrastructure
{
    public class InMemoryEvents : IEvents
    {
        private List<Event> events = new List<Event>();

        public void Add(Event @event)
        {
            events.Add(@event);
        }

        public List<Event> GetAllEvents()
        {
            return events;
        }
    }
}
