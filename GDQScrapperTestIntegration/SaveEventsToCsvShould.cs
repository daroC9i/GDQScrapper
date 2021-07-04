using System;
using System.Collections.Generic;
using GDQScrapper.Core.Domain;
using GDQScrapper.Export.Infrastructure;
using GDQScrapper.Export.Infrastructure.Repositories;
using NUnit.Framework;
using Tests.Builders;

namespace GDQScrapperIntegrationTests
{
    public class SaveEventsToCsvShould
    {
        [Test]
        public void SaveToRepository()
        {
            var fileWriteService = new DotNetFileWriteService();
            CsvEventsRepository repository = new CsvEventsRepository(fileWriteService);
            var testEventOne = EventBuilder.SetBasicEvent().Build();
            //var testEventTwo = new Event();
            //var testEventThree = new Event();
            var eventList = new List<Event>() { testEventOne };


            // Where
            repository.Insert(eventList);
        }
    }
}
