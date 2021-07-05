using System;
using System.Collections.Generic;
using System.Text;
using GDQScrapper.Calendar.Domain;
using GDQScrapper.Core.Domain;
using GDQScrapper.Core.Domain.EventData;
using GDQScrapper.Export.Domain;
using GDQScrapper.Export.Domain.Exceptions;

namespace GDQScrapper.Export.Infrastructure.Repositories
{
    public class CsvEventsRepository : IEventsRepositoryService
    {
        private readonly IFileWriteService fileWriteService;

        private StringBuilder stringBuilder = new StringBuilder();
        private StringBuilder stringEventsBuilder = new StringBuilder();

        private const string SEPARATOR = ";";
        private const string NEWLINE = "\n";

        private const string FILE_EXTENSION = "csv";
        private readonly string FILE_NAME = ExportConfiguration.DefaultEventsRepositoryFileName;

        public CsvEventsRepository(IFileWriteService fileWriteService)
        {
            this.fileWriteService = fileWriteService;
        }

        public List<Event> Get()
        {
            var eventList = new List<Event>();

            var rawEventsStringLines = fileWriteService.TryReadFile(FILE_NAME, FILE_EXTENSION);

            foreach (var rawEventStringLine in rawEventsStringLines)
                eventList.Add(ConvertStringLineToEvent(rawEventStringLine));

            return eventList;
        }

        public void Insert(List<Event> events)
        {
            stringEventsBuilder.Clear();

            foreach (var item in events)
            {
                stringEventsBuilder.Append(ConvertEventToStringLine(item));
                stringEventsBuilder.Append(NEWLINE);
            }

            var file = stringEventsBuilder.ToString();

            fileWriteService.ExportToFile(file, FILE_NAME, FILE_EXTENSION);
        }

        public void Update(List<Event> events)
        {
            throw new NotImplementedException();
        }

        public void Delete(List<Event> events)
        {
            throw new NotImplementedException();
        }

        private string ConvertEventToStringLine(Event eventToConvert)
        {
            stringBuilder.Clear();

            stringBuilder.Append(eventToConvert.EventId);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.StartDateTime.ToStandarString());
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.EndDateTime.ToStandarString());
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.Game);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.Runners);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.SetupLenght);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.EventDuration);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.Condition);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.GamePlatform);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.Hosts);
            stringBuilder.Append(SEPARATOR);

            stringBuilder.Append(eventToConvert.FavoriteState);

            return stringBuilder.ToString();
        }

        private Event ConvertStringLineToEvent(string eventStringLine)
        {
            var eventStringLineSplited = eventStringLine.Split(SEPARATOR);

            if (eventStringLineSplited.Length != 11)
                throw new InvalidEventImportException();

            var id = new EventId(eventStringLineSplited[0]);
            var startDateTime = new StartEventDateTime(eventStringLineSplited[1]);
            var endDateTime = new EndEventDateTime(eventStringLineSplited[2]);
            var gameName = new Game(eventStringLineSplited[3]);
            var runnersName = new Runners(eventStringLineSplited[4]);
            var setupLenghtDuration = new SetupLenghtDuration(eventStringLineSplited[5]);
            var eventDuration = new EventDuration(eventStringLineSplited[6]);
            var condition = new Condition(eventStringLineSplited[7]);
            var gamePlatform = new GamePlatform(eventStringLineSplited[8]);
            var hostsName = new Hosts(eventStringLineSplited[9]);
            var favoriteState = new FavoriteState(bool.Parse(eventStringLineSplited[10]));

            return new Event(id, startDateTime, gameName, runnersName, setupLenghtDuration,
                eventDuration, endDateTime, condition, gamePlatform, hostsName, favoriteState);
        }

        public void DeleteAll()
        {
            fileWriteService.DeleteFile(FILE_NAME, FILE_EXTENSION);
        }
    }
}
