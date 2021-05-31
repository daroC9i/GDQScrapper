using System;
using GDQScrapper.Calendar.Actions;
using GDQScrapper.Calendar.Domain;
using GDQScrapper.Export.Infrastructure;
using GDQScrapper.GDQProcessor.Actions;
using GDQScrapper.GDQProcessor.Domain;
using GDQScrapper.GDQProcessor.Domain.Displayer;
using GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor;

namespace GDQScrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var webcClient = new WebScrapper.Infrastructure.WebClientDotNet();
            var infoScrapper = new WebScrapper.Domain.InfoScrapperService(webcClient);
            var scrapper = new WebScrapper.Actions.GetInfo(infoScrapper);


            IHtmlEventExtractorService htmlEventExtractorService = new HtmlEventExtractorService();
            IHtmlRowExtractorService htmlRowExtractorService = new HtmlRowExtractorService();
            IHtmlExtractorService htmlExtractorService = new HtmlExtractorService(htmlRowExtractorService, htmlEventExtractorService);
            ExtractEvents processHtmlInfo = new ExtractEvents(htmlExtractorService);

            IDisplayerService displayerService = new DisplayerService();
            DisplayEvents displayEvents = new DisplayEvents(displayerService);

            IFileWriteService fileWriteService = new DotNetFileWriteService();
            AppleEventsService eventsService = new AppleEventsService(fileWriteService);
            ExportToAppleEvents exportToAppleEvents = new ExportToAppleEvents(eventsService);

            var info = scrapper.Excecute("https://gamesdonequick.com/schedule");
            var events = processHtmlInfo.Excecute(info);

            exportToAppleEvents.Excecute(events, "SGDQ 2021");
            displayEvents.Excecute(events);

            Console.WriteLine("Finished");
        }
    }
}
