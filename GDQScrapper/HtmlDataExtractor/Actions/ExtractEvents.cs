using System.Collections.Generic;
using GDQScrapper.Core.Domain;
using GDQScrapper.GDQProcessor.Domain;

namespace GDQScrapper.GDQProcessor.Actions
{
    public class ExtractEvents
    {
        private readonly IHtmlExtractorService htmlExtractorService;

        public ExtractEvents(IHtmlExtractorService htmlExtractorService)
        {
            this.htmlExtractorService = htmlExtractorService;
        }

        public List<Event> Excecute(string raw)
        {
            return htmlExtractorService.CreateEventsOf(raw);
        }
    }
}