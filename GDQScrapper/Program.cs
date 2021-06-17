using System;
using System.Collections.Generic;
using GDQScrapper.Calendar.Actions;
using GDQScrapper.Calendar.Domain;
using GDQScrapper.Core.Actions;
using GDQScrapper.Core.Domain;
using GDQScrapper.Core.Infrastructure;
using GDQScrapper.Displayer.Actions;
using GDQScrapper.Displayer.Views;
using GDQScrapper.Export.Infrastructure;
using GDQScrapper.GDQProcessor.Actions;
using GDQScrapper.GDQProcessor.Domain;
using GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor;

namespace GDQScrapper
{
    class Program
    {
        private static string GDQEventName;

        static void Main(string[] args)
        {
            GDQEventName = "GDQ - " + DateTime.Now.ToString("yyyy");
            
            Console.WriteLine("Event Name: " + GDQEventName);

            var webcClient = new WebScrapper.Infrastructure.WebClientDotNet();
            var infoScrapper = new WebScrapper.Domain.InfoScrapperService(webcClient);
            var scrapper = new WebScrapper.Actions.GetInfo(infoScrapper);


            IHtmlEventExtractorService htmlEventExtractorService = new HtmlEventExtractorService();
            IHtmlRowExtractorService htmlRowExtractorService = new HtmlRowExtractorService();
            IHtmlExtractorService htmlExtractorService = new HtmlExtractorService(htmlRowExtractorService, htmlEventExtractorService);
            ExtractEvents processHtmlInfo = new ExtractEvents(htmlExtractorService);

            IConsoleView consoleView = new DotNetConsole();
            ITableView tableView = new TableView(consoleView);
            DisplayTableOfEvents displayEvents = new DisplayTableOfEvents(tableView);

            IFileWriteService fileWriteService = new DotNetFileWriteService();
            AppleEventsService eventsService = new AppleEventsService(fileWriteService);
            ExportToAppleEvents exportToAppleEvents = new ExportToAppleEvents(eventsService);

            IEventConverterService eventConverterService = new EventConverterService();
            ConvertToEvent convertToEvent = new ConvertToEvent(eventConverterService);

            var info = scrapper.Excecute("https://gamesdonequick.com/schedule");
            var raeEvents = processHtmlInfo.Excecute(info);

            List<Event> events = new List<Event>();
            raeEvents.ForEach(item => events.Add(convertToEvent.Excecute(item)));

            exportToAppleEvents.Excecute(events, GDQEventName);
            displayEvents.Excecute("-- " + GDQEventName + " ---", events);

            Console.WriteLine("Finished");
        }
    }
}
