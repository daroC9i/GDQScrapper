
namespace GDQScrapper.HtmlDataExtractor.Domain
{
    public class RawEvent
    {
        public string StartDateTime { get; }
        public string EndDateTime { get; }
        public string Game { get; }
        public string Runners { get; }
        public string SetupLenght { get; }
        public string EventDuration { get; }
        public string Condition { get; }
        public string GamePlatform { get; }
        public string Host { get; }

        public RawEvent(
            string startDateTime,
            string game,
            string runners,
            string setupLenght,
            string eventDuration,
            string endDateTime,
            string condition,
            string gamePlatform,
            string host)
        {
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
