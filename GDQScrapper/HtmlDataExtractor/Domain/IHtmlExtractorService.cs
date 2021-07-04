using System.Collections.Generic;
using GDQScrapper.HtmlDataExtractor.Domain;

namespace GDQScrapper.GDQProcessor.Domain
{
    public interface IHtmlEventsExtractorService
    {
        List<RawEvent> CreateEventsOf(string raw);
    }
}
