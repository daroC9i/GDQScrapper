
namespace GDQScrapper.GDQProcessor.Domain.HTMLTableExtractor.Extensions
{
    public static class HtmlExtensions
    {
        public static string RemoveTag(this string value, string tag)
        {
            tag = tag.ToLower();
            string startTag = "<" + tag;
            string endTag = "</" + tag + ">";

            var startTagBeginIndex = value.IndexOf(startTag);
            var startTagEndIndex = value.IndexOf(">");
            var startTagLenght = startTagEndIndex - startTagBeginIndex + ">".Length;
            value = value.Remove(startTagBeginIndex, startTagLenght);

            var endTagIndex = value.IndexOf(endTag);
            value = value.Remove(endTagIndex, endTag.Length);

            return value;
        }

        public static string TryRemoveTag(this string value, string tag)
        {
            try
            {
                return value.RemoveTag(tag);
            }
            catch
            {
                return value;
            }
        }

        public static string ExtractFirstRowWithTag(this string value, string tag)
        {
            tag = tag.ToLower();
            string startTag = "<" + tag;
            string endTag = "</" + tag + ">";

            var startIndex = value.IndexOf(startTag);
            var endIndex = value.IndexOf(endTag);
            var lenght = endIndex - startIndex + endTag.Length;

            return value.Substring(startIndex, lenght);
        }

        public static string RemoveFirstTag(this string value, string tag)
        {
            tag = tag.ToLower();
            string startTag = "<" + tag;
            string endTag = "</" + tag + ">";

            var startIndex = value.IndexOf(startTag);
            var endIndex = value.IndexOf(endTag);
            var lenght = endIndex - startIndex + endTag.Length;

            return value.Remove(startIndex, lenght);
        }

        public static string RemoveSpacesInFronAndBack(this string value)
        {
            int spaceIndexFront = 0;
            int spaceIndexBack = 0;

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] != ' ')
                {
                    spaceIndexFront = i;
                    break;
                }
            }

            for (int i = value.Length -1; i >= 0; i--)
            {
                if (value[i] != ' ')
                {
                    spaceIndexBack = i+1;
                    break;
                }   
            }

            return value.Substring(spaceIndexFront, spaceIndexBack - spaceIndexFront);
        }

        public static string RemoveSpacesBetweenBrackets(this string value)
        {
            int startPoint = 0;

            while (true)
            {
                if (startPoint > value.Length)
                    break;

                var startIndex = value.IndexOf("<", startPoint);
                var endIndex = value.IndexOf(">", startPoint);
                var lenght = endIndex - startIndex + 1;

                if (startIndex < 0 || endIndex < 0)
                    break;

                var newValue = value.Substring(startIndex, lenght).Replace(" ", "");

                value = value.Remove(startIndex, lenght);

                value = value.Insert(startIndex, newValue);
                endIndex = value.IndexOf(">", startPoint);

                startPoint = endIndex + 1;
            }

            return value;
        }
    }
}
