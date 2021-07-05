using System.Collections.Generic;
using GDQScrapper.Core.Domain;

namespace GDQScrapper.Calendar.Domain
{
    public interface IEventsRepositoryService
    {
        List<Event> GetAll();
        void Insert(List<Event> events);
        void Update(List<Event> events);
        void Delete(List<Event> events);
        void DeleteAll();
    }
}
