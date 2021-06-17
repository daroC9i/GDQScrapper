using System.Collections.Generic;
using GDQScrapper.Core.Domain;
using GDQScrapper.Displayer.Presenters;
using GDQScrapper.Displayer.Views;

namespace GDQScrapper.Displayer.Actions
{
    public class DisplayTableOfEvents
    {
        private readonly ITableView tableView;
        private EventsPresenter presenter;

        public DisplayTableOfEvents(ITableView tableView)
        {
            this.tableView = tableView;
            presenter = new EventsPresenter(this.tableView);
        }

        public void Excecute(string title, List<Event> events)
        {
            presenter.LoadTitle(title);
            presenter.LoadEvents(events);
            presenter.Show();
        }
    }
}
