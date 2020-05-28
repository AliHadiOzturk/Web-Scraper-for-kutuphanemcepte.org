using System.IO;
using System.Net;
using System.Web;
using WebScraping.infrastructure;

namespace WebScraping.kutuphanemcepte
{
    public class KutuphanemMain
    {
        string ROOTPATH = Directory.GetCurrentDirectory() + "\\files\\";
        string sourceURL = "http://kutuphanemcepte.org";
        string str;
        public void run(string str)
        {
            this.str = str;
            GetList();
        }
        // Utils.ExtraxtDataWithHtml(responseFromServer, "<div class=\"col-xl-3 col-lg-4 col-6\">", "<div class=\"col-xl-3 col-lg-4 col-6\">");

        public void GetList()
        {
            // var index = 0;
            while (true)
            {
                var data = str.ExtractData("<div class=\"col-xl-3 col-lg-4 col-6\">", "</a>");
                var result = data.ExtractedString;
                result = result.RemoveHtmlJunks();
                str = str.Substring(data.LastIndex);
                if (!string.IsNullOrEmpty(result))
                    GetBook(Utils.FindAndExtractHref(result));
                else
                    break;
                // index = data.LastIndex;
            }

        }

        public void GetBook(string path)
        {
            var page = WebRequestHelper.GetSource(sourceURL + "/" + path);

            var header = page.ExtractData("<div class=\"header-details\"", "</div>");
            var name = header.ExtractedString.ExtractData("<h3>", "</h3>").ExtractedString.HtmlToString();
            System.Console.WriteLine($"Downloading ->{name}");
            var root = ROOTPATH + name;
            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);
            CreateInfoFile(header.ExtractedString, name);

            //int idx = 0;//page.IndexOf("<div id=\"playlist\"");
            var playlist = page.ExtractData("<div id=\"playlist\"", "<div class=\"section padding"); ;
            int endIndex = playlist.LastIndex;
            while (true)
            {
                var chapter = playlist.ExtractedString.ExtractData("<a ", "</a>");
                if (string.IsNullOrEmpty(chapter.ExtractedString))
                    break;
                playlist.ExtractedString = playlist.ExtractedString.Substring(chapter.LastIndex);
                var chapterName = chapter.ExtractedString.ExtractData("<h3 class=\"play-listing-title\">", "</h3>").ExtractedString.HtmlToString();
                var mp3Path = Utils.FindAndExtractHref(chapter.ExtractedString);
                var filePath = $"{root}\\{chapterName.Trim()}.mp3";
                System.Console.WriteLine($"\t\tChapter ->{chapterName.Trim()}");
                // File.Create(filePath);
                WebRequestHelper.GetFile(sourceURL + (mp3Path.StartsWith("/") ? mp3Path : "/" + mp3Path), filePath);
                // idx = section.LastIndex;
            }
        }

        public void CreateInfoFile(string header, string name)
        {

            var writer = header.ExtractData("<li><strong>Yazan</strong>", "</li>").ExtractedString.HtmlToString();
            var seslendiren = header.ExtractData("<li><strong>Seslendiren</strong>", "</li>").ExtractedString.HtmlToString();
            var time = header.ExtractData("<li><strong>Süre</strong>", "</li>").ExtractedString.HtmlToString();
            var fileStream = new FileStream($"{ROOTPATH}{name}\\{name}-Bilgiler.txt", FileMode.OpenOrCreate);
            var sw = new StreamWriter(fileStream);

            sw.WriteLine($"İsim : {name}");
            sw.WriteLine($"Yazar : {writer}");
            sw.WriteLine($"Seslendiren : {seslendiren}");
            sw.WriteLine($"Zaman : {time}");
            sw.Flush();
            sw.Close();
            // sw.Write(args)
        }
    }
}