using System.Collections.Generic;

namespace GDQScrapper.Core.Domain.EventData
{
    public class Hosts
    {
        private List<Host> hosts = new List<Host>();
        public int Count { get { return hosts.Count; } }

        public Hosts(Host [] hosts)
        {
            foreach (var host in hosts)
                this.hosts.Add(host);
        }

        public Host [] GetHosts()
        {
            return hosts.ToArray();
        }

        public override string ToString()
        {
            return string.Join(',', hosts);
        }
    }
}
