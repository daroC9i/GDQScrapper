using System;
using System.Collections.Generic;
using GDQScrapper.Core.Domain;

namespace GDQScrapper.GDQProcessor.Domain.Displayer
{
    public class DisplayerService : IDisplayerService
    {
        public void DisplayEvents(List<Event> events)
        {
            Console.WriteLine("Update:: " + DateTime.Now + "\n\n\n");

            foreach (var @event in events)
            {
                DisplayEvent(@event);
            }
        }

        private void DisplayEvent(Event eventData)
        {
            Console.WriteLine("--- " + eventData.Game.ToString() + " ( " + eventData.Condition.ToString() + " )" + " ---");
            Console.WriteLine("StartDateTime: " + eventData.StartDateTime.ToString());
            Console.WriteLine("Setup: " + eventData.SetupLenght.ToString());
            Console.WriteLine("Duration: " + eventData.EventDuration.ToString());
            Console.WriteLine("Duration: " + eventData.Condition.ToString());

            Console.WriteLine("");
            Console.WriteLine("Runner: " + eventData.Runners.ToString());
            Console.WriteLine("Host: " + eventData.Host.ToString());
            Console.WriteLine("\n\n\n");
        }

    }
}
