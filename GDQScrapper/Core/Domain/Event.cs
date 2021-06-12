using GDQScrapper.Core.Domain.EventData;

namespace GDQScrapper.Core.Domain
{
    public class Event
    {
        public EventId EventId { get; }
        public StartEventDateTime StartDateTime { get; }
        public EndEventDateTime EndDateTime { get; }
        public Game Game { get; }
        public Runner Runners { get; }
        public SetupLenghtDuration SetupLenght { get; }
        public EventDuration EventDuration { get; }
        public Condition Condition { get; }
        public GamePlatform GamePlatform { get; }
        public Host Host { get; }

        public Event(
            EventId eventId,
            StartEventDateTime startDateTime,
            Game game,
            Runner runners,
            SetupLenghtDuration setupLenght,
            EventDuration eventDuration,
            EndEventDateTime endDateTime,
            Condition condition,
            GamePlatform gamePlatform,
            Host host)
        {
            EventId = eventId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Condition = condition;
            GamePlatform = gamePlatform;
            Runners = runners;
            Host = host;
            SetupLenght = setupLenght;
            EventDuration = eventDuration;
            Game = game;
        }
    }
}
