using System.IO;
using System.Text;

namespace TradeDataFeed.Streams
{
    public static class MockTradeDataStream
    {
        public static Stream GetTradeStream(string filename)
        {
            var path = string.Format("{0}\\Mock\\{1}.txt", Directory.GetCurrentDirectory(), filename);
            string jsonData = File.ReadAllText(path);

            return new MemoryStream(Encoding.UTF8.GetBytes(jsonData ?? ""));
        }
    }
}
