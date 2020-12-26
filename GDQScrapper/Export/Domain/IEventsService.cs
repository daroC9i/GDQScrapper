using System.Collections.Generic;
using GDQScrapper.Core.Domain;

namespace GDQScrapper.Calendar.Domain
{
    public interface IEventsService
    {
        void Export(List<Event> events);
    }
}
