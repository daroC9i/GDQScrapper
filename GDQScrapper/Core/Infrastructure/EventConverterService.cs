using System;
using System.Linq;
using GDQScrapper.Core.Domain;
using GDQScrapper.Core.Domain.EventData;
using GDQScrapper.HtmlDataExtractor.Domain;

namespace GDQScrapper.Core.Infrastructure
{
    public interface IEventConverterService
    {
        Event Convert(RawEvent rawEvent);
    }

    public class EventConverterService : IEventConverterService
    {
        public Event Convert(RawEvent rawEvent)
        {
            var eventId = new EventId(Guid.NewGuid().ToString());

            var startEventDateTime = new StartEventDateTime(rawEvent.StartDateTime);

            var game = new Game(rawEvent.Game);

            var runners = ConvertToRunners(rawEvent.Runners);

            var setupLenghtDuration = new SetupLenghtDuration(rawEvent.SetupLenght);

            var eventDuration = new EventDuration(rawEvent.EventDuration);

            var condition = new Condition(rawEvent.Condition);

            var gamePlatform = new GamePlatform(rawEvent.GamePlatform);

            var host = new Host(rawEvent.Host);

            var endTime = new EndEventDateTime(startEventDateTime.DateTime.Add(eventDuration.TimeSpan));

            return new Event(eventId, startEventDateTime, game, runners, setupLenghtDuration, eventDuration, endTime, condition, gamePlatform, host);
        }

        private Runners ConvertToRunners(string runnersRaw)
        {
            var runners = runnersRaw.Split(',').Select(item => new Runner(item)).ToArray();
            return new Runners(runners);
        }
    }
}
