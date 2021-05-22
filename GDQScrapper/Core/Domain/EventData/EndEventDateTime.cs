
using System;

namespace GDQScrapper.Core.Domain.EventData
{
    public class EndEventDateTime : EventDateTime
    {
        public EndEventDateTime(string datetime) : base(datetime) { }

        public EndEventDateTime(DateTime datetime) : base(datetime) { }
    }
}
