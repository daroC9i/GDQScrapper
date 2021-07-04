using System.Collections.Generic;
using GDQScrapper.Core.Domain;

namespace GDQScrapper.Calendar.Domain
{
    public interface IEventsExporterService
    {
        void Export(List<Event> events, string eventsName);
    }
}
