using NUnit.Framework;
using GDQScrapper.GDQProcessor.Domain;
using GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor;

namespace Tests.HtmlExtractor
{
    public class HtmlExtractorServiceShould
    {
        private IHtmlRowExtractorService htmlRowExtractor;
        private IHtmlEventExtractorService htmlEventExtractorService;
        private HtmlExtractorService tableRowConverterService;

        [SetUp]
        public void Setup()
        {
            htmlRowExtractor = new HtmlRowExtractorService();
            htmlEventExtractorService = new HtmlEventExtractorService();

            tableRowConverterService = new HtmlExtractorService(htmlRowExtractor, htmlEventExtractorService);
        }

        [Test]
        public void Extract_Event()
        {
            // Given
            string tableRow = @"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf - 8'>
                    <meta name = 'viewport'>
                </head>
                <body>
                    <table id = 'runTable' class='table table-condensed'>
                    <thead>
                        <tr class='day-split'>
                            <td>Time &amp; Length</td>
                            <td>Run</td>
                            <td>Runners &amp; <i class='fa fa-microphone'></i> Host</td>
                            <td class='visible-lg'>Setup&nbsp;Length</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class='start - time text - right'>2021-01-03T19:05:00Z</td>
                            <td> Just Cause 3 </ td >
                            <td> pmcTRILOGY </ td >
                            < td rowspan = '2' class='visible-lg text-center'> <i class='fa fa-clock-o text-gdq-red' aria-hidden='true'></i> 0:12:00 </td>
                        </tr>

                        <tr class='second-row'> 
                            <td class='text-right'> <i class='fa fa-clock-o' aria-hidden='true'></i> 0:52:00 </td>
                            <td>Sky Fortress DLC &mdash; PC</td>
                            <td><i class='fa fa-microphone'></i> Asuka424</td>
                        </tr>
                    </tbody>
                    </table>
                </body>
                </html>";

            // When
            var result = tableRowConverterService.CreateEventsOf(tableRow);

            // Then
            Assert.AreEqual("2021-01-03 16:05:00", result[0].StartDateTime.ToString());
            Assert.AreEqual("2021-01-03 16:57:00", result[0].EndDateTime.ToString());
            Assert.AreEqual("Just Cause 3", result[0].Game.ToString());
            Assert.AreEqual("pmcTRILOGY", result[0].Runners.ToString());
            Assert.AreEqual("0:12:00", result[0].SetupLenght.ToString());
            Assert.AreEqual("0:52:00", result[0].Duration.ToString());
            Assert.AreEqual("Sky Fortress DLC - PC", result[0].Condition.ToString());
            Assert.AreEqual("Asuka424", result[0].Host.ToString());
        }

        [Test]
        public void Extract_Events()
        {
            // Given
            string tableRow = @"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf - 8'>
                    <meta name = 'viewport'>
                </head>
                <body>
                    <table id = 'runTable' class='table table-condensed'>
                    <thead>
                        <tr class='day-split'>
                            <td>Time &amp; Length</td>
                            <td>Run</td>
                            <td>Runners &amp; <i class='fa fa-microphone'></i> Host</td>
                            <td class='visible-lg'>Setup&nbsp;Length</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class='start - time text - right'>2021-01-03T19:05:00Z</td>
                            <td> Just Cause 3 </ td >
                            <td> pmcTRILOGY </ td >
                            < td rowspan = '2' class='visible-lg text-center'> <i class='fa fa-clock-o text-gdq-red' aria-hidden='true'></i> 0:12:00 </td>
                        </tr>

                        <tr class='second-row'> 
                            <td class='text-right'> <i class='fa fa-clock-o' aria-hidden='true'></i> 0:52:00 </td>
                            <td>Sky Fortress DLC &mdash; PC</td>
                            <td><i class='fa fa-microphone'></i> Asuka424</td>
                        </tr>

                        <tr>
                        <td class='start - time text - right'>2021-01-04T18:03:00Z</td>
                            < td > Sonic 3: Angel Island Revisited </ td >
                            < td > Zaxon96 </ td >
                            < td rowspan = '2' class='visible-lg text-center'> <i class='fa fa-clock-o text-gdq-red' aria-hidden='true'></i> 0:10:00 </td>
                        </tr>

                        <tr class='second-row'>
                            <td class='text-right '> <i class='fa fa-clock-o' aria-hidden='true'></i> 0:42:00 </td>
                            <td>Any% Sonic + Tails &mdash; PC</td>
                            <td><i class='fa fa-microphone'></i> iggyzig</td>
                        </tr>
                    </tbody>
                    </table>
                </body>
                </html>";

            // When
            var result = tableRowConverterService.CreateEventsOf(tableRow);

            // Then
            Assert.IsTrue(result.Count == 2);

            Assert.AreEqual("2021-01-03 16:05:00", result[0].StartDateTime.ToString());
            Assert.AreEqual("2021-01-03 16:57:00", result[0].EndDateTime.ToString());
            Assert.AreEqual("Just Cause 3", result[0].Game.ToString());
            Assert.AreEqual("pmcTRILOGY", result[0].Runners.ToString());
            Assert.AreEqual("0:12:00", result[0].SetupLenght.ToString());
            Assert.AreEqual("0:52:00", result[0].Duration.ToString());
            Assert.AreEqual("Sky Fortress DLC - PC", result[0].Condition.ToString());
            Assert.AreEqual("Asuka424", result[0].Host.ToString());

            Assert.AreEqual("2021-01-04 15:03:00", result[1].StartDateTime.ToString());
            Assert.AreEqual("2021-01-04 15:45:00", result[1].EndDateTime.ToString());
            Assert.AreEqual("Sonic 3: Angel Island Revisited", result[1].Game.ToString());
            Assert.AreEqual("Zaxon96", result[1].Runners.ToString());
            Assert.AreEqual("0:10:00", result[1].SetupLenght.ToString());
            Assert.AreEqual("0:42:00", result[1].Duration.ToString());
            Assert.AreEqual("Any% Sonic + Tails - PC", result[1].Condition.ToString());
            Assert.AreEqual("iggyzig", result[1].Host.ToString());
        }
    }
}