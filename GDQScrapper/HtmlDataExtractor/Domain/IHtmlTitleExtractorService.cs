using System;

namespace GDQScrapper.HtmlDataExtractor.Domain
{
    public interface IHtmlTitleExtractorService
    {
        string GetTitleFrom(string raw);
    }
}
