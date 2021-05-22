using System;

namespace GDQScrapper.Core.Domain.EventData
{
    public class GamePlatform
    {
        public string Platform { get; }

        public GamePlatform(string platform)
        {
            Platform = platform;
        }

    }
}
