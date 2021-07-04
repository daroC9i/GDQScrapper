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

        public Hosts(string hosts)
        {
            var hostSplited = hosts.Split(',');

            foreach (var host in hostSplited)
                this.hosts.Add(new Host(host));
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
