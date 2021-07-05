using System;
namespace GDQScrapper.Export.Domain.Exceptions
{
    public class FailToReadFileException : Exception
    {
        public FailToReadFileException() { }
        public FailToReadFileException(string message) : base(message) { }
    }
}
