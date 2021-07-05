
using System;

namespace GDQScrapper.Core.Domain.EventData
{
    public class EventId
    {
        public string Value { get; }

        public EventId(int value)
        {
            Value = value.ToString();
        }

        public EventId(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            return obj is EventId id &&
                   Value == id.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}
