using NUnit.Framework;
using DotNetFileIO;
using NUnit.Framework.Internal;
using System;
using GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor;
using GDQScrapper.GDQProcessor.Domain;
using GDQScrapper.GDQProcessor.Actions;
using GDQScrapper.Calendar.Domain;
using GDQScrapper.Calendar.Actions;
using GDQScrapper.Core.Domain;
using System.Collections.Generic;
using GDQScrapper.Core.Infrastructure;
using GDQScrapper.Core.Actions;

namespace GDQScrapperTestIntegration
{
    public class IntegrationTest
    {
        private TanukiGu.ILogger logger;

        private FileService fileService;
        private readonly string FilePath = "../../../";
        private readonly string FileName = "SGDQ2021-Data";
        private readonly string FileExtension = "txt";

        private readonly string SomeGDQEventName = "SomeGDQEventName";

        IHtmlEventExtractorService htmlEventExtractorService;
        IHtmlRowExtractorService htmlRowExtractorService;
        IHtmlEventsExtractorService htmlExtractorService;
        ExtractEvents processHtmlInfo;

        DummyWriteService fileWriteService;
        AppleEventsService eventsService;
        ExportToAppleEvents exportToAppleEvents;

        IEventConverterService eventConverterService;
        ConvertToEvent convertToEvent;

        [SetUp]
        public void Setup()
        {
            logger = new Logger();
            fileService = new FileService();
            fileService.SetLogger(logger);

            htmlEventExtractorService = new HtmlEventExtractorService();
            htmlRowExtractorService = new HtmlRowExtractorService();
            htmlExtractorService = new HtmlEventsExtractorService(htmlRowExtractorService, htmlEventExtractorService);
            processHtmlInfo = new ExtractEvents(htmlExtractorService);

            fileWriteService = new DummyWriteService();
            eventsService = new AppleEventsService(fileWriteService);
            exportToAppleEvents = new ExportToAppleEvents(eventsService);

            eventConverterService = new EventConverterService();
            convertToEvent = new ConvertToEvent(eventConverterService);
        }


        [Test]
        public void GetEventsFromPlaceHolderData()
        {
            string [] fileLines = GetDummyFile();

            var rawEvents = processHtmlInfo.Excecute(string.Join('\n', fileLines));
            Assert.IsNotEmpty(rawEvents);


            List<Event> events = new List<Event>();
            rawEvents.ForEach(item => events.Add(convertToEvent.Excecute(item)));

            exportToAppleEvents.Excecute(events, SomeGDQEventName);
            Assert.IsNotEmpty(fileWriteService.File);
            Assert.IsNotEmpty(fileWriteService.FileName);
            Assert.IsNotEmpty(fileWriteService.Extension);
        }

        private class DummyWriteService : IFileWriteService
        {
            public string File { get; private set; }
            public string FileName { get; private set; }
            public string Extension { get; private set; }

            public void ExportToFile(string file, string fileName, string extension)
            {
                File = file;
                FileName = fileName;
                Extension = extension;
            }
        }

        private string [] GetDummyFile()
        {
            return fileService.ReadFile(FilePath, FileName, FileExtension);
        }

        private class Logger : TanukiGu.ILogger
        {
            public void Log(string message)
            {
                Console.WriteLine(message);
            }
        }
    }
}