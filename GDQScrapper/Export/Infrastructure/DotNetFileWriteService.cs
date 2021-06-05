using GDQScrapper.Calendar.Domain;
using DotNetFileIO;

namespace GDQScrapper.Export.Infrastructure
{
    public class DotNetFileWriteService : IFileWriteService
    {
        private FileService FileService = new FileService();

        private readonly string path = "";

        public void ExportToFile(string file, string fileName, string extencion)
        {
            extencion = extencion.ToLower();

            FileService.SaveFile(file, path, fileName, extencion);
        }
    }
}
