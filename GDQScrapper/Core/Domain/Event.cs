using GDQScrapper.Core.Domain.EventData;

namespace GDQScrapper.Core.Domain
{
    public class Event
    {
        public StartDateTime StartDateTime { get; }
        public EndDateTime EndDateTime { get; }
        public Game Game { get; }
        public Runner Runners { get; }
        public SetupLenght SetupLenght { get; }
        public Duration Duration { get; }
        public Condition Condition { get; }
        public Host Host { get; }

        public Event(
            StartDateTime startDateTime,
            Game game,
            Runner runners,
            SetupLenght setupLenght,
            Duration duration,
            EndDateTime endDateTime,
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
