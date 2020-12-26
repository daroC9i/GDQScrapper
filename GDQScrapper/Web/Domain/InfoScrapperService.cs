using System;

namespace GDQScrapper.WebScrapper.Domain
{
    public interface IInfoScrapperService
    {
        string GetInfo(string url);
    }

    public class InfoScrapperService : IInfoScrapperService
    {
        private readonly IWebClient webClient;

        public InfoScrapperService(IWebClient webClient)
        {
            this.webClient = webClient;
        }

        public string GetInfo(string url)
        {
            return webClient.DownloadStringInfoFrom(url);
        }
    }
}
