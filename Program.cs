using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using WebScraping.infrastructure;
using WebScraping.kutuphanemcepte;

namespace WebScraping
{
    class Program
    {
        static void Main(string[] args)
        {
            run();
        }


        public static void run()
        {
            //WebRequestHelper.GetFile("http://kutuphanemcepte.org/Media/Books/Beyaz_Geceler/9789750727658/9789750727658_001.mp3", "1.mp3");
            var source = WebRequestHelper.GetSource("http://kutuphanemcepte.org/yetiskin-kitaplari");
            var c = new KutuphanemMain();
            c.run(source);
        }
    }
}
