
using System;

namespace GDQScrapper.Core.Domain.EventData
{
    public class EndDateTime : EventDateTime
    {
        public EndDateTime(string datetime) : base(datetime) { }

        public EndDateTime(DateTime datetime) : base(datetime) { }
    }
}
