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
        public Duration Duration { get; }
        public Condition Condition { get; }
        public Host Host { get; }

        public Event(
            StartEventDateTime startDateTime,
            Game game,
            Runner runners,
            SetupLenghtDuration setupLenght,
            Duration duration,
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
            Duration = duration;
            Game = game;
        }
    }
}
