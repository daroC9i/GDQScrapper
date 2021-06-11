using GDQScrapper.HtmlDataExtractor.Domain;

namespace GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor
{
    public interface IHtmlEventExtractorService
    {
        RawEvent CreateRawEvent(string dataRow);
    }
}