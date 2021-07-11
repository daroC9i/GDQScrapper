using System;
using System.Collections.Generic;
using System.Linq;
using GDQScrapper.Calendar.Actions;
using GDQScrapper.Calendar.Domain;
using GDQScrapper.Core.Actions;
using GDQScrapper.Core.Domain;
using GDQScrapper.Core.Infrastructure;
using GDQScrapper.Displayer.Actions;
using GDQScrapper.Displayer.Views;
using GDQScrapper.Export.Actions;
using GDQScrapper.Export.Infrastructure;
using GDQScrapper.Export.Infrastructure.Exporters;
using GDQScrapper.Export.Infrastructure.Repositories;
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
            GDQEventName = "SGDQ - " + DateTime.Now.ToString("yyyy"); // TODO automatically adjust

            Console.WriteLine("Event Name: " + GDQEventName);

            var webcClient = new WebScrapper.Infrastructure.WebClientDotNet();
            var infoScrapper = new WebScrapper.Domain.InfoScrapperService(webcClient);
            var scrapper = new WebScrapper.Actions.GetInfo(infoScrapper);


            IHtmlEventExtractorService htmlEventExtractorService = new HtmlEventExtractorService();
            IHtmlRowExtractorService htmlRowExtractorService = new HtmlRowExtractorService();
            IHtmlEventsExtractorService htmlExtractorService = new HtmlEventsExtractorService(htmlRowExtractorService, htmlEventExtractorService);
            ExtractEvents processHtmlInfo = new ExtractEvents(htmlExtractorService);

            IConsoleView consoleView = new DotNetConsole();
            ITableView tableView = new TableView(consoleView);
            DisplayTableOfEvents displayEvents = new DisplayTableOfEvents(tableView);

            IFileWriteService fileWriteService = new DotNetFileWriteService();
            IHtmlRawExporterService htmlRawExpoerterService = new HtmlRawExporterService(fileWriteService);
            IEventsRepositoryService eventsRepositoryService = new CsvEventsRepository(fileWriteService);
            AppleEventsService eventsService = new AppleEventsService(fileWriteService);
            ExportToAppleEvents exportToAppleEvents = new ExportToAppleEvents(eventsService);
            GetAllEvents getAllEvents = new GetAllEvents(eventsRepositoryService);
            SaveEvents saveEvents = new SaveEvents(eventsRepositoryService);
            ExportHtmlRaw exportHtmlRaw = new ExportHtmlRaw(htmlRawExpoerterService);

            IEventConverterService eventConverterService = new EventConverterService();
            ConvertToEvent convertToEvent = new ConvertToEvent(eventConverterService);

            // ------

            var info = scrapper.Excecute("https://gamesdonequick.com/schedule");
            exportHtmlRaw.Execute(info);
            var raeEvents = processHtmlInfo.Excecute(info);

            List<Event> eventsFromWeb = new List<Event>();
            raeEvents.ForEach(item => eventsFromWeb.Add(convertToEvent.Excecute(item)));


            var eventsFromRepository = getAllEvents.Execute();

            List<Event> eventsToExport = new List<Event>();
            if (eventsFromRepository.Count == 0)
            {
                Console.WriteLine("Create Repository");
                saveEvents.Execute(eventsFromWeb);
                eventsToExport = eventsFromWeb;
            }
            else
            {
                Console.WriteLine("Get Favorites Events");
                var favoritesEvents = eventsFromRepository.Where(repositoryEvent => repositoryEvent.FavoriteState.IsFavorite).ToList();

                foreach (var favoritesEvent in favoritesEvents)
                {
                    foreach (var eventFromWeb in eventsFromWeb)
                    {
                        if (favoritesEvent.Equals(eventFromWeb))
                            eventsToExport.Add(eventFromWeb);
                    }
                }

                // TODO Update Repository
            }

            exportToAppleEvents.Excecute(eventsToExport, GDQEventName);
            displayEvents.Excecute("-- " + GDQEventName + " ---", eventsToExport);

            Console.WriteLine("Finished");
        }
    }
}
