using System;

namespace GDQScrapper.Core.Domain.EventData
{
    public abstract class Duration
    {
        private string duration;

        public TimeSpan TimeSpan { get; private set; }

        public Duration(string duration)
        {
            this.duration = duration;

            TimeSpan = TimeSpan.Parse(duration);
        }

        public override string ToString()
        {
            return duration;
        }

        public override bool Equals(object obj)
        {
            return obj is Duration duration &&
                   TimeSpan.Equals(duration.TimeSpan);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TimeSpan);
        }
    }
}
