using System;
using System.Collections.Generic;
using GDQScrapper.Core.Domain;
using GDQScrapper.Export.Infrastructure;
using GDQScrapper.Export.Infrastructure.Repositories;
using NUnit.Framework;
using Tests.Builders;

namespace GDQScrapperIntegrationTests
{
    public class CsvEventsRepositoryShould
    {
        private CsvEventsRepository repository;

        [SetUp]
        public void Setup()
        {
            var fileWriteService = new DotNetFileWriteService();
            repository = new CsvEventsRepository(fileWriteService);

            repository.DeleteAll();
        }


        [Test]
        public void Save_And_Get_Events_From_Repository()
        {
            var testEventOne = EventBuilder.SetBasicEvent().Build();
            var eventList = new List<Event>() { testEventOne };

            // Where
            repository.Insert(eventList);
            var result = repository.Get();

            // Then
            Assert.IsNotEmpty(result);
            Assert.IsTrue(result.Count == 1);
        }

        [Test]
        public void Get_Empty_List_of_Events_Where_Repository_Does_Not_Exist()
        {
            var result = repository.Get();

            // Then
            Assert.IsEmpty(result);
        }
    }
}
