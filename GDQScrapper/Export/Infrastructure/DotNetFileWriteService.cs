using System;
using System.IO;
using System.Text;
using GDQScrapper.Calendar.Domain;

namespace GDQScrapper.Export.Infrastructure
{
    public class DotNetFileWriteService : IFileWriteService
    {
        public void ExportToFile(string file, string fileName, string extencion)
        {
            fileName = fileName.Replace(" ", "_") + "." + extencion.ToLower();

            try
            {  
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                    Console.WriteLine("Old file was removed");
                }

                using (FileStream fs = File.Create(fileName))
                {
                    byte[] author = new UTF8Encoding(true).GetBytes(file);
                    fs.Write(author, 0, author.Length);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
