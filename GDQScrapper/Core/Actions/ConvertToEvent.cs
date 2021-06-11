using GDQScrapper.Core.Domain;
using GDQScrapper.Core.Infrastructure;
using GDQScrapper.HtmlDataExtractor.Domain;

namespace GDQScrapper.Core.Actions
{
    public class ConvertToEvent
    {
        private readonly IEventConverterService eventConverterService;

        public ConvertToEvent(IEventConverterService eventConverterService)
        {
            this.eventConverterService = eventConverterService;
        }

        public Event Excecute(RawEvent rawEvent)
        {
            return eventConverterService.Convert(rawEvent);
        }
    }
}
