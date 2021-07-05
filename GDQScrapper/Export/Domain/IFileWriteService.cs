namespace GDQScrapper.Calendar.Domain
{
    public interface IFileWriteService
    {
        void ExportToFile(string file, string fileName, string extension);
        string[] ReadFile(string fileName, string extension);
        string[] TryReadFile(string fileName, string extension);
        void DeleteFile(string fileName, string extension);
    }
}
