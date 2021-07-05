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
            var eventId = new EventId(CreateEventIdFromRaw(rawEvent));

            var startEventDateTime = new StartEventDateTime(rawEvent.StartDateTime);

            var game = new Game(rawEvent.Game);

            var runners = ConvertToRunners(rawEvent.Runners);

            var setupLenghtDuration = new SetupLenghtDuration(rawEvent.SetupLenght);

            var eventDuration = new EventDuration(rawEvent.EventDuration);

            var condition = new Condition(rawEvent.Condition);

            var gamePlatform = new GamePlatform(rawEvent.GamePlatform);

            var hosts = ConvertToHosts(rawEvent.Host);

            var endTime = new EndEventDateTime(startEventDateTime.DateTime.Add(eventDuration.TimeSpan));

            var favortiteState = new FavoriteState();

            return new Event(eventId, startEventDateTime, game, runners, setupLenghtDuration,
                eventDuration, endTime, condition, gamePlatform, hosts, favortiteState);
        }

        private string CreateEventIdFromRaw(RawEvent rawEvent)
        {
            return rawEvent.Game + rawEvent.GamePlatform + rawEvent.Condition + rawEvent.Runners.ToString();
        }

        private Runners ConvertToRunners(string runnersRaw)
        {
            var runners = runnersRaw.Split(',').Select(item => new Runner(item)).ToArray();
            return new Runners(runners);
        }

        private Hosts ConvertToHosts(string hostsRaw)
        {
            var runners = hostsRaw.Split(',').Select(item => new Host(item)).ToArray();
            return new Hosts(runners);
        }
    }
}
