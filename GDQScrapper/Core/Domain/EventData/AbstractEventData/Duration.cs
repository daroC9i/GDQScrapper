using System;
using GDQScrapper.Core.Domain.Errors;

namespace GDQScrapper.Core.Domain.EventData
{
    public abstract class Duration
    {
        public TimeSpan TimeSpan { get; private set; }
        public bool IsZero { get { return TimeSpan.Seconds == 0; } }

        public Duration(string duration)
        {
            if (string.IsNullOrEmpty(duration))
                throw new InvalidDurationException();

            TimeSpan = TimeSpan.Parse(duration);
        }

        public override string ToString()
        {
            return TimeSpan.ToString(@"hh\:mm\:ss");
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
