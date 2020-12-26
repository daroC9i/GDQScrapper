using GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor;
using NUnit.Framework;

namespace Tests.HtmlExtractor
{
    public class HtmlRowExtractorShould
    {
        private HtmlRowExtractorService htmlRowExtractor;

        [SetUp]
        public void Setup()
        {
            htmlRowExtractor = new HtmlRowExtractorService();
        }

        [Test]
        public void Extract_Row()
        {
            // Given
            string tableRow = "<tr><td>test_1</td></tr><tr><td>test_2</td></tr>";

            // When
            var result = htmlRowExtractor.ExtractTableRows(tableRow);

            // Then
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual("<td>test_1</td><td>test_2</td>", result[0]);
        }

        [Test]
        public void Extract_Row_With_Extend_Table_Row_Tag()
        {
            // Given
            string tableRow = "<tr><td>test_1</td></tr><tr class='second-row'><td>test_2</td></tr>";

            // When
            var result = htmlRowExtractor.ExtractTableRows(tableRow);

            // Then
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual("<td>test_1</td><td>test_2</td>", result[0]);
        }

        [Test]
        public void Extract_Row_With_Garbage_In_Front()
        {
            // Given
            string tableRow = "bfkjwfbjbwef<tr><td>test_1</td></tr><tr><td>test_2</td></tr>";

            // When
            var result = htmlRowExtractor.ExtractTableRows(tableRow);

            // Then
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual("<td>test_1</td><td>test_2</td>", result[0]);
        }

        [Test]
        public void Extract_Row_With_Garbage_On_Back()
        {
            // Given
            string tableRow = "<tr><td>test_1</td></tr><tr><td>test_2</td></tr>dsgdgdgdgdg";

            // When
            var result = htmlRowExtractor.ExtractTableRows(tableRow);

            // Then
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual("<td>test_1</td><td>test_2</td>", result[0]);
        }

        [Test]
        public void Extract_Row_With_Garbage_In_Middel()
        {
            // Given
            string tableRow = "<tr><td>test_1</td></tr>cdcdcc<tr><td>test_2</td></tr>";

            // When
            var result = htmlRowExtractor.ExtractTableRows(tableRow);

            // Then
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual("<td>test_1</td><td>test_2</td>", result[0]);
        }

        [Test]
        public void Extract_Two_Rows()
        {
            // Given
            string tableRow = @"<tr><td>testA_1</td></tr><tr><td>testA_2</td></tr>
                           <tr><td>testB_1</td></tr><tr><td>testB_2</td></tr>";

            // When
            var result = htmlRowExtractor.ExtractTableRows(tableRow);

            // Then
            Assert.IsTrue(result.Count == 2);
            Assert.AreEqual("<td>testA_1</td><td>testA_2</td>", result[0]);
            Assert.AreEqual("<td>testB_1</td><td>testB_2</td>", result[1]);
        }

        [Test]
        public void Extract_Two_Rows_With_Garbage_In_Middle_Of_Two_Rows()
        {
            // Given
            string tableRow = @"<tr><td>testA_1</td></tr><tr><td>testA_2</td></tr>
                 GARBAGE          <tr><td>testB_1</td></tr><tr><td>testB_2</td></tr>";

            // When
            var result = htmlRowExtractor.ExtractTableRows(tableRow);

            // Then
            Assert.IsTrue(result.Count == 2);
            Assert.AreEqual("<td>testA_1</td><td>testA_2</td>", result[0]);
            Assert.AreEqual("<td>testB_1</td><td>testB_2</td>", result[1]);
        }
    }
}
