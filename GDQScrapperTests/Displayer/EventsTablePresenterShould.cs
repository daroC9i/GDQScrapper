using NUnit.Framework;
using GDQScrapper.Displayer.Presenters;
using GDQScrapper.Displayer.Views;
using GDQScrapper.Core.Domain;
using System.Collections.Generic;
using Tests.Builders;

namespace Tests.Displayer
{
    public class EventsTablePresenterShould
    {

        private EventsPresenter presenter;

        [SetUp]
        public void Setup()
        {
            IConsoleView consoleView = new DotNetConsole();
            ITableView tableView = new TableView(consoleView);
            presenter = new EventsPresenter(tableView);
        }

        [Test]
        public void ShowEvents()
        {
            // Given

            string someTitle = "--- SomeTitle ---";
            var someEvents = new List<Event>();

            var someEventOne = EventBuilder.SetBasicEvent().Build();
            someEvents.Add(someEventOne);

            var someEventTwo = EventBuilder.SetBasicEvent().SetGame("OtherGameName").Build();
            someEvents.Add(someEventTwo);

            // Where
            presenter.LoadTitle(someTitle);
            presenter.LoadEvents(someEvents);
            presenter.Show();
        }
    }
}
