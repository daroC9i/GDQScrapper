using System.Collections.Generic;
using GDQScrapper.Core.Domain;
using GDQScrapper.GDQProcessor.Domain.Displayer;

namespace GDQScrapper.GDQProcessor.Actions
{
    public class DisplayEvents
    {
        private readonly IDisplayerService displayerService;

        public DisplayEvents(IDisplayerService displayerService)
        {
            this.displayerService = displayerService;
        }

        public void Excecute(List<Event> events)
        {
            displayerService.DisplayEvents(events);
        }
    }
}
