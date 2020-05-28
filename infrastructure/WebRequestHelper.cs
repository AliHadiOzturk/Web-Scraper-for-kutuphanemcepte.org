using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebScraping.infrastructure
{
    public static class WebRequestHelper
    {

        public static string GetSource(string url)
        {

            WebRequest request = WebRequest.Create(url);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            Stream dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            File.WriteAllText("response.html", responseFromServer);
            response.Close();
            return responseFromServer.RemoveHtmlJunks();

            // Close the response.
        }

        public static void GetFile(string url, string fileName)
        {

            while (true)
            {
                try
                {
                    // WebRequest request = WebRequest.Create(url);
                    // request.Credentials = CredentialCache.DefaultCredentials;
                    // request.Headers.Add(HttpRequestHeader.Accept, "video/webm,video/ogg,video/*;q=0.9,application/ogg;q=0.7,audio/*;q=0.6,*/*;q=0.5");
                    // request.Headers.Add(HttpRequestHeader.Cookie, "ASP.NET_SessionId=pogks3act4gzfr5tvrgafcdz");
                    // request.Headers.Add(HttpRequestHeader.Host, "kutuphanemcepte.org");
                    // request.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:77.0) Gecko/20100101 Firefox/77.0");
                    // WebResponse response = request.GetResponse();

                    // // Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                    // Stream dataStream = response.GetResponseStream();
                    var client = new WebClient();
                    client.DownloadFile(new Uri(url), fileName);
                    break;
                }
                catch (System.Exception e)
                {
                    System.Console.WriteLine("Something happaned :) -> " + e.Message);
                }
            }

        }
    }
}