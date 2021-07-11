using GDQScrapper.Calendar.Domain;

namespace GDQScrapper.Export.Actions
{
    public class ExportHtmlRaw
    {
        private readonly IHtmlRawExporterService htmlRawExporterService;

        public ExportHtmlRaw(IHtmlRawExporterService htmlRawExporterService)
        {
            this.htmlRawExporterService = htmlRawExporterService;
        }

        public void Execute(string raw)
        {
            htmlRawExporterService.Export(raw);
        }
    }
}
