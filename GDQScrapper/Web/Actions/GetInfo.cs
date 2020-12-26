using System;
using GDQScrapper.WebScrapper.Domain;

namespace GDQScrapper.WebScrapper.Actions
{
    public class GetInfo
    {
        private readonly IInfoScrapperService infoScrapper;

        public GetInfo(IInfoScrapperService infoScrapper)
        {
            this.infoScrapper = infoScrapper;
        }

        public string Excecute(string url)
        {
            return infoScrapper.GetInfo(url);
        }
    }
}
