using GDQScrapper.Calendar.Domain;
using GDQScrapper.Export.Domain;

namespace GDQScrapper.Export.Infrastructure.Exporters
{
    public class HtmlRawExporterService : IHtmlRawExporterService
    {
        private readonly IFileWriteService fileWriteService;

        public HtmlRawExporterService(IFileWriteService fileWriteService)
        {
            this.fileWriteService = fileWriteService;
        }

        public void Export(string rawData)
        {
            fileWriteService.ExportToFile(rawData, ExportConfiguration.DefaultHtmlRawExportedFileName, "html");
        }
    }
}
