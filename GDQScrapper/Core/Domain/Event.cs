using GDQScrapper.Core.Domain.EventData;

namespace GDQScrapper.Core.Domain
{
    public class Event
    {
        public EventId EventId { get; }
        public StartEventDateTime StartDateTime { get; }
        public EndEventDateTime EndDateTime { get; }
        public Game Game { get; }
        public Runners Runners { get; }
        public SetupLenghtDuration SetupLenght { get; }
        public EventDuration EventDuration { get; }
        public Condition Condition { get; }
        public GamePlatform GamePlatform { get; }
        public Hosts Hosts { get; }
        public FavoriteState FavoriteState { get; private set; }

        public Event(
            EventId eventId,
            StartEventDateTime startDateTime,
            Game game,
            Runners runners,
            SetupLenghtDuration setupLenght,
            EventDuration eventDuration,
            EndEventDateTime endDateTime,
            Condition condition,
            GamePlatform gamePlatform,
            Hosts host,
            FavoriteState favoriteState)
        {
            EventId = eventId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Condition = condition;
            GamePlatform = gamePlatform;
            Runners = runners;
            Hosts = host;
            SetupLenght = setupLenght;
            EventDuration = eventDuration;
            Game = game;
            FavoriteState = favoriteState;
        }
    }
}
