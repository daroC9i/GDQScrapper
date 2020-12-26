using System.Collections.Generic;
using GDQScrapper.Core.Domain;

namespace GDQScrapper.GDQProcessor.Domain.Displayer
{
    public interface IDisplayerService
    {
        void DisplayEvents(List<Event> events);
    }
}