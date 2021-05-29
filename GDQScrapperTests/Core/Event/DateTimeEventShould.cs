using System;
using GDQScrapper.Core.Domain.EventData;
using NUnit.Framework;

namespace GDQScrapperTests.Core.Event
{
    public class DateTimeEventShould
    {

        [Test]
        public void ConvertDateTime()
        {
            // Given
            string test = "2021-01-03T16:30:00Z";

            // When
            var resul = new TestEventDateTime(test);

            // Then
            Assert.AreEqual(new DateTime(2021,1,3,13,30,00), resul.DateTime);
        }

        private class TestEventDateTime : EventDateTime
        {
            public TestEventDateTime(string datetime) : base(datetime) { }
            public TestEventDateTime(DateTime datetime) : base(datetime) { }
        }
    }
}