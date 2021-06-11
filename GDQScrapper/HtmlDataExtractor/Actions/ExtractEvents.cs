using System.Collections.Generic;
using GDQScrapper.GDQProcessor.Domain;
using GDQScrapper.HtmlDataExtractor.Domain;

namespace GDQScrapper.GDQProcessor.Actions
{
    public class ExtractEvents
    {
        private readonly IHtmlExtractorService htmlExtractorService;

        public ExtractEvents(IHtmlExtractorService htmlExtractorService)
        {
            this.htmlExtractorService = htmlExtractorService;
        }

        public List<RawEvent> Excecute(string raw)
        {
            return htmlExtractorService.CreateEventsOf(raw);
        }
    }
}