namespace GDQScrapper.Core.Domain.EventData
{
    public class Host
    {
        private string host;

        public Host(string host)
        {
            this.host = host;
        }

        public override string ToString()
        {
            return host;
        }
    }
}
