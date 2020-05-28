using System.Text;
using System.Web;
using WebScraping.models;

namespace WebScraping
{
    public static class StringExtentions
    {

        // "\">\r\n                            <a href=\"/devlet-ana\">\r\n                                "

        public static string RemoveHtmlJunks(this string html)
        {
            return html.Replace("\\\"", "\"").Replace("\r", "").Replace("\n", "").Replace("&nbsp;", " ").Trim();
        }

        public static string HtmlToString(this string source)
        {
            return HttpUtility.HtmlDecode(source);
        }

        public static Information ExtractData(this string strSource, string strStart, string strEnd, int startIndex = 0)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, startIndex) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);

                return new Information { ExtractedString = strSource.Substring(Start, End - Start), LastIndex = End };
            }

            return new Information();
        }
    }
}