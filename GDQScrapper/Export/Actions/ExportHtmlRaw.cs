using System;
using GDQScrapper.Export.Infrastructure;

namespace GDQScrapper.Export.Actions
{
    public class ExportHtmlRaw
    {
        private DotNetFileWriteService dotNetFileWriteService = new DotNetFileWriteService();

        public void Execute(string raw)
        {
            dotNetFileWriteService.ExportToFile(raw, "htmlraw", "html");
        }

    }
}
