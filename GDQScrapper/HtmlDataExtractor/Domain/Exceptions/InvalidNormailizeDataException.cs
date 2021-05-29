using System;
namespace GDQScrapper.HtmlDataExtractor.Domain.Exceptions
{
    public class InvalidNormailizeDataException : Exception
    {
        public InvalidNormailizeDataException() : base() { }

        public InvalidNormailizeDataException(string message) : base(message) { }
    }
}
