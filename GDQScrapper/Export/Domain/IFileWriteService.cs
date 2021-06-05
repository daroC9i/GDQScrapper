namespace GDQScrapper.Calendar.Domain
{
    public interface IFileWriteService
    {
        void ExportToFile(string file, string fileName, string extension);
    }
}
