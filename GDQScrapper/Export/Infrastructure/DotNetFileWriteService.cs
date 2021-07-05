using GDQScrapper.Calendar.Domain;
using DotNetFileIO;
using System;
using GDQScrapper.Export.Domain.Exceptions;

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

        public string[] TryReadFile(string fileName, string extension)
        {
            try
            {
                return FileService.ReadFile(path, fileName, extension);
            }
            catch (Exception)
            {
                return new string[0];
            }
        }

        public string[] ReadFile(string fileName, string extension)
        {
            try
            {
                return FileService.ReadFile(path, fileName, extension);
            }
            catch(Exception ex)
            {
                if (ex is DotNetFileIO.Errors.FailToReadFileException)
                    throw new FailToReadFileException();
                else
                    throw;
            }
        }

        public void DeleteFile(string fileName, string extension)
        {
            FileService.DeleteFile(path, fileName, extension);
        }

        
    }
}
