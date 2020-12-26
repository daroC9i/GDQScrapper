
namespace GDQScrapper.WebScrapper.Domain
{
    public interface IWebClient
    {
        string DownloadStringInfoFrom(string url);
    }
}
