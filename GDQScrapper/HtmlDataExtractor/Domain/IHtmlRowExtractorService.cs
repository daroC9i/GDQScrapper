using System.Collections.Generic;

namespace GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor
{
    public interface IHtmlRowExtractorService
    {
        List<string> ExtractTableRows(string tableBody);
    }
}