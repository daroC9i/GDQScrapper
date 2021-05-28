using System;
using System.Globalization;
using GDQScrapper.Core.Domain.EventData;
using GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor;
using NUnit.Framework;

namespace Tests.HtmlExtractor
{
    public class HtmlEventsExtractorServiceShould
    {
        private HtmlEventExtractorService htmlEventsExtractorService;

        private readonly string SomeDate = "<td>2021-01-03T10:30:00Z</td>";
        private readonly string SomeGameName = "<td>Game</td>";
        private readonly string SomeRunnerName = "<td>Runner</td>";
        private readonly string SomeEventDuration = "<td>0:10:00</td>";
        private readonly string SomeSetupDuration = "<td>0:20:00</td>";
        private readonly string SomeCondition = "<td>Condition</td>";
        private readonly string SomeHostName = "<td>host</td>";

        private static string ComplexRAW =
               "<td class='start - time text - right'>2021-01-03T19:05:00Z</td>" +
               "<td> Just Cause 3 </td>" +
               "<td> pmcTRILOGY</td>" +
               "<td rowspan = '2' class='visible-lg text-center'> <i class='fa fa-clock-o text-gdq-red' aria-hidden='true'></i> 0:12:00 </td>" +
               "<td class='text-right'> <i class='fa fa-clock-o' aria-hidden='true'></i> 0:52:00 </td>" +
               "<td>Sky Fortress DLC &mdash; PC</td>" +
               "<td><i class='fa fa-microphone'></i> Asuka424</td>";

        [SetUp]
        public void Setup()
        {
            htmlEventsExtractorService = new HtmlEventExtractorService();
        }

        [Test]
        public void Create_Simple_Event()
        {
            var expectedStartDateTime = CreateExpectedDateTime("2021-01-03 10:30:00Z");
            var expectedEndDateTime = CreateExpectedDateTime("2021-01-03 10:50:00Z");
            var expectedGameName = "Game";
            var expectedRunnerName = "Runner";
            var expectedSetupLenghtDuration = new SetupLenghtDuration("0:10:00");
            var expectedEventDuration = new EventDuration("0:20:00");
            var expectedCondition = "Condition";
            var expectedHostName = "host";

            // Given
            string simpleEvent = string.Concat(new [] {SomeDate, SomeGameName, SomeRunnerName,
                SomeEventDuration, SomeSetupDuration, SomeCondition, SomeHostName});

            // When
            var result = htmlEventsExtractorService.CreateEvent(simpleEvent);

            // Then
            Assert.AreEqual(expectedStartDateTime, result.StartDateTime.DateTime);
            Assert.AreEqual(expectedEndDateTime, result.EndDateTime.DateTime);
            Assert.AreEqual(expectedGameName, result.Game.ToString());
            Assert.AreEqual(expectedRunnerName, result.Runners.ToString());
            Assert.AreEqual(expectedSetupLenghtDuration, result.SetupLenght);
            Assert.AreEqual(expectedEventDuration, result.EventDuration);
            Assert.AreEqual(expectedCondition, result.Condition.ToString());
            Assert.AreEqual(expectedHostName, result.Host.ToString());
        }

        [Test]
        public void Create_Event()
        {
            // When
            var result = htmlEventsExtractorService.CreateEvent(ComplexRAW);

            // Then
            Assert.AreEqual("2021-01-03 16:05:00", result.StartDateTime.ToString());
            Assert.AreEqual("2021-01-03 16:57:00", result.EndDateTime.ToString());
            Assert.AreEqual("Just Cause 3", result.Game.ToString());
            Assert.AreEqual("pmcTRILOGY", result.Runners.ToString());
            Assert.AreEqual("0:12:00", result.SetupLenght.ToString());
            Assert.AreEqual("0:52:00", result.EventDuration.ToString());
            Assert.AreEqual("Sky Fortress DLC &mdash; PC", result.Condition.ToString());
            Assert.AreEqual("Asuka424", result.Host.ToString());
        }

        private DateTime CreateExpectedDateTime(string data)
        {
            return DateTime.ParseExact(data, "yyyy-MM-dd HH:mm:ssZ", CultureInfo.InvariantCulture);
        }
    }
}
