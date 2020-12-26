using System.Collections.Generic;
using GDQScrapper.Core.Domain;

namespace GDQScrapper.GDQProcessor.Domain
{
    public interface IHtmlExtractorService
    {
        List<Event> CreateEventsOf(string raw);
    }
}
