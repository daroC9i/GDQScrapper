using System;
namespace GDQScrapper.Export.Domain.Exceptions
{
    public class InvalidEventImportException : Exception
    {
        public InvalidEventImportException(){ }
        public InvalidEventImportException(string message) : base(message) { }
    }
}
