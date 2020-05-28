namespace WebScraping
{
    public static class Utils
    {

        public static string ExtractHrefFromATag(string tag)
        {
            int Start, End;
            Start = tag.IndexOf("href=\"", 0) + tag.Length;
            End = tag.IndexOf("\"", Start);
            return tag.Substring(Start, End - Start);
        }

        public static string FindAndExtractHref(string html)
        {
            return html.ExtractData("href=\"", "\"").ExtractedString;
        }
    }
}