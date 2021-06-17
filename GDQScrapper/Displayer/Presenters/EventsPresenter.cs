using System;
using System.Collections.Generic;
using GDQScrapper.Core.Domain;
using GDQScrapper.Displayer.Domain.Exceptions;
using GDQScrapper.Displayer.Views;

namespace GDQScrapper.Displayer.Presenters
{
    public class EventsPresenter
    {
        private readonly ITableView tableView;

        private const string START_DATE_HEADER = "Date";
        private const string START_TIME_HEADER = "Hour";
        private const string EVENT_DURATION_HEADER = "Lenght";
        private const string END_DATIME_HEADER = "End";
        private const string GAME_HEADER = "Game";
        private const string PLATFORM_HEADER = "Platform";
        private const string CONDITION_HEADER = "Condition";
        private const string RUNNER_HEADER = "Runners";
        private const string HOST_HEADER = "Hosts";
        private const string SETUP_DURATION_HEADER = "Set";

        private const int DURATION_LENGHT = 6;
        private const int BIG_LENGHT = 16;
        private const int MEDIUM_LENGHT = 9;
        private const int SHORT_LENGHT = 3;

        private string Title;
        private List<Event> Events;

        public EventsPresenter(ITableView tableView)
        {
            this.tableView = tableView;
        }

        public void LoadTitle(string title)
        {
            Title = title;
        }

        public void LoadEvents(List<Event> events)
        {
            Events = events;
        }

        public void Show()
        {
            if (string.IsNullOrEmpty(Title))
                throw new HasNotSetTitleException();

            if (Events == null || Events.Count == 0)
                throw new HasNotSetEventsException();

            tableView.SetTitle(Title);

            tableView.AddColumn(START_DATE_HEADER, MEDIUM_LENGHT);
            tableView.AddColumn(START_TIME_HEADER, DURATION_LENGHT);
            tableView.AddColumn(EVENT_DURATION_HEADER, DURATION_LENGHT);
            tableView.AddColumn(END_DATIME_HEADER, DURATION_LENGHT);
            tableView.AddColumn(GAME_HEADER, BIG_LENGHT);
            tableView.AddColumn(PLATFORM_HEADER, MEDIUM_LENGHT);
            tableView.AddColumn(CONDITION_HEADER, BIG_LENGHT);
            tableView.AddColumn(RUNNER_HEADER, MEDIUM_LENGHT);
            tableView.AddColumn(HOST_HEADER, MEDIUM_LENGHT);
            tableView.AddColumn(SETUP_DURATION_HEADER, SHORT_LENGHT);

            Events.ForEach(currentEvent => SetEvent(currentEvent));

            tableView.ShowTable();
        }

        private void SetEvent(Event currentEvent)
        {
            tableView.AddValueToColumn(START_DATE_HEADER, OnlyDayFormat(currentEvent.StartDateTime.DateTime));
            tableView.AddValueToColumn(START_TIME_HEADER, OnlyMinutesFormat(currentEvent.StartDateTime.DateTime));
            tableView.AddValueToColumn(EVENT_DURATION_HEADER, OnlyHoursFormat(currentEvent.EventDuration.TimeSpan));
            tableView.AddValueToColumn(END_DATIME_HEADER, OnlyMinutesFormat(currentEvent.EndDateTime.DateTime));
            tableView.AddValueToColumn(GAME_HEADER, currentEvent.Game.ToString());
            tableView.AddValueToColumn(PLATFORM_HEADER, currentEvent.GamePlatform.ToString());
            tableView.AddValueToColumn(CONDITION_HEADER, currentEvent.Condition.ToString());
            tableView.AddValueToColumn(RUNNER_HEADER, currentEvent.Runners.ToString());
            tableView.AddValueToColumn(HOST_HEADER, currentEvent.Hosts.ToString());
            tableView.AddValueToColumn(SETUP_DURATION_HEADER, OnlyHoursFormat(currentEvent.SetupLenght.TimeSpan));
        }

        private string OnlyMinutesFormat(DateTime dateTime)
        {
            return dateTime.ToString("HH:mm");
        }

        private string OnlyDayFormat(DateTime dateTime)
        {
            return dateTime.ToString("dddd");
        }

        private string OnlyHoursFormat(TimeSpan timeSpan)
        {
            var hours = timeSpan.Hours;
            var minutes = timeSpan.Minutes;

            if (hours > 0)
                return hours + "h " + minutes + "'";
            else
                return minutes + "'";
        }

    }
}
