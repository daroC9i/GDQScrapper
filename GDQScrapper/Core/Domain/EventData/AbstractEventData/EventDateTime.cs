using System;

namespace GDQScrapper.Core.Domain.EventData
{
    public abstract class EventDateTime
    {
        public DateTime DateTime { get; internal set; }

        protected EventDateTime(string datetime)
        {
            Convert(datetime);
        }

        protected EventDateTime(DateTime datetime)
        {
            DateTime = datetime;
        }

        internal void Convert(string value)
        {
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            DateTime = DateTime.ParseExact(value, "yyyy-MM-ddTHH:mm:ssZ", culture);
        }

        public string ToStandarString()
        {
            return DateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        public override string ToString()
        {
            return DateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
