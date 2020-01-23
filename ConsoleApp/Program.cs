using System;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Simple harness of api!");
            Console.WriteLine("Curious known bug if you're using Visual Studio 2017 - multiple startup projects don't seem to work very well the first time you run the project. If this happens, can you just try running the project again");

            string url = "https://localhost:44344/api/trade";
            string details = CallRestMethod(url);
            Console.WriteLine(details);
            Console.ReadKey();
        }

        public static string CallRestMethod(string url)
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            string result = string.Empty;
            result = responseStream.ReadToEnd();
            webresponse.Close();
            return result;
        }

    }
}
