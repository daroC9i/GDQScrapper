using System.Net;
using GDQScrapper.WebScrapper.Domain;
using System.Text;

namespace GDQScrapper.WebScrapper.Infrastructure
{
    public class WebClientDotNet : IWebClient
    {
        private WebClient webClient;

        public WebClientDotNet()
        {
            webClient = new WebClient();
        }

        public string DownloadStringInfoFrom(string url)
        {
            return Encoding.UTF8.GetString(webClient.DownloadData(url));
        }
    }
}
