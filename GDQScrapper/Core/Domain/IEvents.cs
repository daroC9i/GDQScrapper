using System.Collections.Generic;
using GDQScrapper.Core.Domain;

namespace GDQScrapper.GDQProcessor.Domain
{
    public interface IEvents
    {
        void Add(Event @event);
        List<Event> GetAllEvents();
    }
}
