using System;
using System.Threading.Tasks;

namespace QueryConsoleUI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Process.ProcessRequest();
            var carrier = new Receiver();
            var authorObj = await carrier.MethodStore.getDataTask("https://jsonmock.hackerrank.com/api/article_users/search?page=1");
            var authorList = authorObj.Data;
            Console.WriteLine(authorList[0].Username);
            Console.WriteLine("Hello World!");
        }
    }
}
