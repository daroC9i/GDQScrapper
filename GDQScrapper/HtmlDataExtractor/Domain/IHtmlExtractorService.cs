using System.Collections.Generic;
using GDQScrapper.HtmlDataExtractor.Domain;

namespace GDQScrapper.GDQProcessor.Domain
{
    public interface IHtmlExtractorService
    {
        List<RawEvent> CreateEventsOf(string raw);
    }
}
