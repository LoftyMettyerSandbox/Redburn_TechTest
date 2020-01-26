using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Timers;
using Flurl.Http;

namespace ConsoleApp
{
    class Program
    {

        private static string watchPath = AppDomain.CurrentDomain.BaseDirectory + "Mock\\";
        private static readonly FileSystemWatcher Watcher = new FileSystemWatcher();
        static Timer timer = new Timer(5000);
        private static string url = "https://localhost:44344/api/trade";

        static void Main(string[] args)
        {
            Console.Title = "Trade Data Client Streamer";

            Console.WriteLine("Simple harness of api!");
            Console.WriteLine("Curious known bug if you're using Visual Studio 2017 - multiple startup projects don't seem to work very well the first time you run the project. If this happens, can you just try running the project again");

            // Cache testing of data
            timer.Elapsed += timer_Elapsed;
            timer.Start();

            // Listen for file changes
            Console.WriteLine(string.Format("Listening to path {0}", Watcher.Path));
            Watcher.Path = watchPath;
            ProcessDirectory(watchPath);

            Console.WriteLine("Press 'q' to quit.");

            // Watch for changes in LastAccess and LastWrite times, and
            // the renaming of files or directories.
            //watcher.NotifyFilter = NotifyFilters.LastAccess
            //                     | NotifyFilters.LastWrite
            //                     | NotifyFilters.FileName
            //                     | NotifyFilters.DirectoryName;
            Watcher.NotifyFilter = NotifyFilters.LastWrite;
            Watcher.Filter = "*.txt";

            // Add event handlers.
            Watcher.Changed += OnFileChanged;

            // Begin watching.
            Watcher.EnableRaisingEvents = true;

            // Wait for the user to quit the program.
            while (Console.Read() != 'q') ;

        }


        public static void PostStream(string url, Stream streamContent) {

            streamContent.Position = 0;
            using (StreamReader reader = new StreamReader(streamContent, Encoding.UTF8))
            {
                var content = reader.ReadToEnd();
                var result = url
                    .WithHeader("Accept", "application/json")
                    .PostJsonAsync(content);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Posting...");
                Console.ResetColor();
            }
        }

        public static string CallRestMethod(string url)
        {
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Default);

            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            //webrequest.CachePolicy = policy;

            webrequest.Method = "GET";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            string result = string.Empty;
            result = responseStream.ReadToEnd();
            webresponse.Close();
            return result;
        }

        private static void OnFileChanged(object source, FileSystemEventArgs e)
        {

            try
            {
                Watcher.Changed -= OnFileChanged;
                Watcher.EnableRaisingEvents = false;
                ProcessFile(e.Name);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                Watcher.Changed += OnFileChanged;
                Watcher.EnableRaisingEvents = true;
            }

            ProcessFile(e.FullPath);
        }

        public static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory, "*.txt");
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

        }

        public static void ProcessFile(string path)
        {
            Console.WriteLine($"Processing File: {path}...");

            string jsonData = File.ReadAllText(path);
            var mockTrade = new MemoryStream(Encoding.UTF8.GetBytes(jsonData ?? ""));

            PostStream(url, mockTrade);

        }

        private static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string details = CallRestMethod(url + "/MKS");
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(details);
            Console.ResetColor();

        }

    }
}
