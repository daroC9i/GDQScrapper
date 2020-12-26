namespace GDQScrapper.Core.Domain.EventData
{
    public class Runner
    {
        private string runners;

        public Runner(string runners)
        {
            this.runners = runners;
        }

        public override string ToString()
        {
            return runners;
        }
    }
}
