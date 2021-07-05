using System;
using System.Collections.Generic;
using GDQScrapper.Core.Domain;
using GDQScrapper.Core.Domain.EventData;

namespace Tests.Builders
{
    public class EventBuilder
    {
        private EventId eventId;
        private StartEventDateTime startDateTime = new StartEventDateTime("2021-01-01T10:00:00Z");
        private EventDuration duration = new EventDuration("1:10:00");
        private EndEventDateTime endDateTime = new EndEventDateTime("2021-01-01T11:10:00Z");
        private SetupLenghtDuration setupDateTime = new SetupLenghtDuration("0:10:00");

        private Game game = new Game("SomeGameName");
        private Condition condition = new Condition("SomeCondition");
        private GamePlatform gamePlatform = new GamePlatform("SomeGamePlatform");

        private FavoriteState favoriteState = new FavoriteState();

        private List<Runner> runnersList;
        private List<Host> hostsList;

        private Runners runners;
        private Hosts hosts;

        private EventBuilder() {}

        public static EventBuilder SetBasicEvent()
        {
            return new EventBuilder();
        }

        public EventBuilder SetEventId(string eventId)
        {
            this.eventId = new EventId(eventId);
            return this;
        }

        public EventBuilder SetGame(string game)
        {
            this.game = new Game(game);
            return this;
        }

        public EventBuilder SetGamePlatform(string gamePlatform)
        {
            this.gamePlatform = new GamePlatform(gamePlatform);
            return this;
        }

        public EventBuilder SetCondition(string condition)
        {
            this.condition = new Condition(condition);
            return this;
        }


        public Event Build()
        {
            if (eventId == null)
                eventId = new EventId(Guid.NewGuid().ToString());

            if (runnersList == null)
                runnersList = new List<Runner>() { new Runner("SomeRunner") };

            if (hostsList == null)
                hostsList = new List<Host>(){ new Host("SomeHost") };

            runners = new Runners(runnersList.ToArray());
            hosts = new Hosts(hostsList.ToArray());

            return new Event(eventId, startDateTime, game, runners, setupDateTime,
                duration, endDateTime, condition, gamePlatform, hosts, favoriteState);
        }
    }
}
