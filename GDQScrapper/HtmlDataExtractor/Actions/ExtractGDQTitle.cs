using System;
using GDQScrapper.HtmlDataExtractor.Domain;

namespace GDQScrapper.HtmlDataExtractor.Actions
{
    public class ExtractGDQTitle
    {
        private readonly IHtmlTitleExtractorService htmlExtractorService;

        public ExtractGDQTitle(IHtmlTitleExtractorService htmlExtractorService)
        {
            this.htmlExtractorService = htmlExtractorService;
        }

        public string Excecute(string raw)
        {
            return htmlExtractorService.GetTitleFrom(raw);
        }
    }
}
