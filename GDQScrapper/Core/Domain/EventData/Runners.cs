using System;
using System.Collections.Generic;
using System.Linq;

namespace GDQScrapper.Core.Domain.EventData
{
    public class Runners
    {
        public List<Runner> runners = new List<Runner>();

        public int Count { get { return runners.Count; } }

        public Runners(Runner [] runners)
        {
            foreach (var runner in runners)
                this.runners.Add(runner);
        }

        public Runner [] GetRunners()
        {
            return runners.ToArray();
        }

        public override string ToString()
        {
            return string.Join(',', runners);
        }
    }
}
