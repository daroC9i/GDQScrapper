using GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor;
using NUnit.Framework;

namespace Tests.HtmlExtractor
{
    public class HtmlEventsExtractorServiceShould
    {
        private HtmlEventExtractorService htmlEventsExtractorService;

        private static string SimpleRAW =
                "<td>2021-01-03T16:30:00Z</td>" +
                "<td>Game</td>" +
                "<td>Runner</td>" +
                "<td>0:10:00</td>" +
                "<td>0:20:00</td>" +
                "<td>Condition</td>" +
                "<td>host</td>";

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
            // When
            var result = htmlEventsExtractorService.CreateEvent(SimpleRAW);

            // Then
            Assert.AreEqual("2021-01-03 13:30:00", result.StartDateTime.ToString());
            Assert.AreEqual("2021-01-03 13:50:00", result.EndDateTime.ToString());
            Assert.AreEqual("Game", result.Game.ToString());
            Assert.AreEqual("Runner", result.Runners.ToString());
            Assert.AreEqual("0:10:00", result.SetupLenght.ToString());
            Assert.AreEqual("0:20:00", result.Duration.ToString());
            Assert.AreEqual("Condition", result.Condition.ToString());
            Assert.AreEqual("host", result.Host.ToString());
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
            Assert.AreEqual("0:52:00", result.Duration.ToString());
            Assert.AreEqual("Sky Fortress DLC &mdash; PC", result.Condition.ToString());
            Assert.AreEqual("Asuka424", result.Host.ToString());
        }
    }
}
