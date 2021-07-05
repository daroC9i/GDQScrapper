using System.Collections.Generic;
using GDQScrapper.Core.Domain;

namespace GDQScrapper.Calendar.Domain
{
    public interface IEventsRepositoryService
    {
        List<Event> Get();
        void Insert(List<Event> events);
        void Update(List<Event> events);
        void Delete(List<Event> events);
        void DeleteAll();
    }
}
