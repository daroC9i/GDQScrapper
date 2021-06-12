using System;
using System.Globalization;
using GDQScrapper.Core.Domain.EventData;
using GDQScrapper.Core.Domain.Errors;
using GDQScrapper.Core.Infrastructure;
using GDQScrapper.HtmlDataExtractor.Domain;
using NUnit.Framework;

namespace GDQScrapperTests.Core
{
    public class EventConverterServiceShould
    {
        private EventConverterService eventConverterService;

        private readonly string SomeStartDate = "2021-01-03T10:30:00Z";
        private readonly string SomeGameName = "Game";
        private readonly string SomeRunnerName = "Runner";
        private readonly string SomeSetupDuration = "0:10:00";
        private readonly string SomeEventDuration = "0:20:00";
        private readonly string SomeCondition = "Condition";
        private readonly string SomePlatform = "Platform";
        private readonly string SomeHostName = "host";

        [SetUp]
        public void Setup()
        {
            eventConverterService = new EventConverterService();
        }

        [Test]
        public void Create_Simple_Event()
        {
            // Given
            var expectedStartDateTime = CreateExpectedDateTime("2021-01-03 10:30:00Z");
            var expectedEndDateTime = CreateExpectedDateTime("2021-01-03 10:50:00Z");
            var expectedGameName = "Game";
            var expectedRunnerName = "Runner";
            var expectedSetupLenghtDuration = new SetupLenghtDuration("0:10:00");
            var expectedEventDuration = new EventDuration("0:20:00");
            var expectedCondition = "Condition";
            var expectedGamePlatform = "Platform";
            var expectedHostName = "host";

            var rawEvent = new RawEvent(SomeStartDate, SomeGameName, SomeRunnerName,
                SomeSetupDuration, SomeEventDuration,null, SomeCondition, SomePlatform, SomeHostName);

            // When
            var result = eventConverterService.Convert(rawEvent);

            // Then
            Assert.IsNotEmpty(result.EventId.Value);
            Assert.AreEqual(expectedStartDateTime, result.StartDateTime.DateTime);
            Assert.AreEqual(expectedEndDateTime, result.EndDateTime.DateTime);
            Assert.AreEqual(expectedGameName, result.Game.ToString());
            Assert.AreEqual(expectedRunnerName, result.Runners.ToString());
            Assert.AreEqual(expectedSetupLenghtDuration, result.SetupLenght);
            Assert.AreEqual(expectedEventDuration, result.EventDuration);
            Assert.AreEqual(expectedCondition, result.Condition.ToString());
            Assert.AreEqual(expectedGamePlatform, result.GamePlatform.ToString());
            Assert.AreEqual(expectedHostName, result.Hosts.ToString());
        }

        [Test]
        public void Create_Event_With_Two_Or_More_Runners_Name()
        {
            // Given
            var expectedRunnersNames = "Shockwve, MunchaKoopas, Tokeegee, Traderkirk, Scoobyfoo, BystanderTim";
            var SomeRunnersNamesWithSpaces = "Shockwve, MunchaKoopas, Tokeegee, Traderkirk, Scoobyfoo, BystanderTim";

            var rawEvent = new RawEvent(SomeStartDate, SomeGameName, SomeRunnersNamesWithSpaces,
                SomeSetupDuration, SomeEventDuration, null, SomeCondition, SomePlatform, SomeHostName);

            // When
            var result = eventConverterService.Convert(rawEvent);

            // Then
            Assert.IsTrue(result.Runners.Count > 1);
            Assert.AreEqual(expectedRunnersNames, result.Runners.ToString());
        }

        [Test]
        public void Create_Event_With_Two_Or_More_Hosts_Name()
        {
            // Given
            var expectedHostsNames = "SomeHost1, SomeHost2, SomeHost3, SomeHost4, SomeHost5";
            var SomeHostsNamesWithSpaces = "SomeHost1, SomeHost2, SomeHost3, SomeHost4, SomeHost5";

            var rawEvent = new RawEvent(SomeStartDate, SomeGameName, SomeRunnerName,
                SomeSetupDuration, SomeEventDuration, null, SomeCondition, SomePlatform, SomeHostsNamesWithSpaces);

            // When
            var result = eventConverterService.Convert(rawEvent);

            // Then
            Assert.IsTrue(result.Hosts.Count > 1);
            Assert.AreEqual(expectedHostsNames, result.Hosts.ToString());
        }

        [Test]
        public void Create_Event_Without_Setup_Leght_Duration()
        {
            // Given
            var emptySetupLeghtDuration = string.Empty;

            var rawEvent = new RawEvent(SomeStartDate, SomeGameName, SomeRunnerName,
                emptySetupLeghtDuration, SomeEventDuration,null, SomeCondition, SomePlatform, SomeHostName);

            // When - Then
            Assert.Throws<InvalidDurationException>(() => eventConverterService.Convert(rawEvent));
        }

        [Test]
        public void Create_Event_Without_Event_Leght_Duration()
        {
            // Given
            var emptyEventLeghtDuration = string.Empty;

            var rawEvent = new RawEvent(SomeStartDate, SomeGameName, SomeRunnerName,
                SomeSetupDuration, emptyEventLeghtDuration, null, SomeCondition, SomePlatform, SomeHostName);

            // When - Then
            Assert.Throws<InvalidDurationException>(() => eventConverterService.Convert(rawEvent));
        }

        private DateTime CreateExpectedDateTime(string data)
        {
            return DateTime.ParseExact(data, "yyyy-MM-dd HH:mm:ssZ", CultureInfo.InvariantCulture);
        }
    }
}
