using GDQScrapper.Core.Domain.EventData;

namespace GDQScrapper.Core.Domain
{
    public class Event
    {
        public StartEventDateTime StartDateTime { get; }
        public EndEventDateTime EndDateTime { get; }
        public Game Game { get; }
        public Runner Runners { get; }
        public SetupLenghtDuration SetupLenght { get; }
        public EventDuration EventDuration { get; }
        public Condition Condition { get; }
        public Host Host { get; }

        public Event(
            StartEventDateTime startDateTime,
            Game game,
            Runner runners,
            SetupLenghtDuration setupLenght,
            EventDuration eventDuration,
            EndEventDateTime endDateTime,
            Condition condition,
            Host host)
        {
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Condition = condition;
            Runners = runners;
            Host = host;
            SetupLenght = setupLenght;
            EventDuration = eventDuration;
            Game = game;
        }
    }
}
