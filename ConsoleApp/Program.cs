using System;
using System.IO;
using System.Net;
using System.Text;
using Flurl.Http;

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

            PostRestDataAsync(url, Guid.NewGuid().ToString());

            Console.ReadKey();
        }

        public class Simples
        {
            public string Name { get; set; }
        }


        public static void PostRestDataAsync(string url, string content)
        {
            var result = url
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(content);

            //var client = new HttpClient();


            //var simps = new Simples() { Name = "bob" };

            //var json = JsonConvert.SerializeObject(simps);

            //var postData = new StringContent(json, Encoding.UTF8, "application/json");
            ////var formContent = new FormUrlEncodedContent(new[] {
            ////    new KeyValuePair<string, string>("", content)
            ////});

            ////  postData = "blah";

            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            //client.PostAsync(url, postData)
            //     //            client.PostAsync(url, postData)
            //     .ContinueWith(task =>
            //     {
            //         var responseNew = task.Result;
            //         //Console.WriteLine(responseNew.Content.ReadAsStringAsync().Result);
            //         Console.WriteLine(responseNew.ReasonPhrase);
            //     });

            ////client.PostAsync(url, formContent)
            ////     //            client.PostAsync(url, postData)
            ////     .ContinueWith(task =>
            ////     {
            ////         var responseNew = task.Result;
            ////         Console.WriteLine(responseNew.Content.ReadAsStringAsync().Result);
            ////     });


            ////  Console.WriteLine(resultContent);


            ////using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            ////{
            ////    var json = JsonConvert.SerializeObject(content);
            ////    using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
            ////    {
            ////        request.Content = stringContent;




            ////        //using (var response = await client
            ////        //    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            ////        //    .ConfigureAwait(false))
            ////        //{
            ////        //    response.EnsureSuccessStatusCode();
            ////        //}
            ////    }
            ////}

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
