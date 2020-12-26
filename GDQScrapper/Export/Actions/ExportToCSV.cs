using System.Collections.Generic;
using GDQScrapper.Core.Domain;
using GDQScrapper.Export.Domain;

namespace GDQScrapper.Export.Actions
{
    public class ExportToCSV
    {
        private readonly CsvService csvService;

        public ExportToCSV(CsvService csvService)
        {
            this.csvService = csvService;
        }

        public void Excecute(List<Event> events)
        {
            csvService.Export(events);
        }
    }
}
