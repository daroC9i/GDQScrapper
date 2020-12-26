using GDQScrapper.Core.Domain;

namespace GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor
{
    public interface IHtmlEventExtractorService
    {
        Event CreateEvent(string dataRow);
    }
}