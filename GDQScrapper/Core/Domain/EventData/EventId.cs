
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
    }
}
