using NUnit.Framework;
using GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor.Extensions;

namespace Tests.HtmlExtractor
{
    public class HtmlExtensionsShould
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test()
        {
            // Given
            string test = " testString ";

            // When
            var result = test.RemoveSpacesInFronAndBack();

            // Then
            Assert.AreEqual("testString", result);
        }

    }
}
