using System;
using System.Threading.Tasks;

namespace QueryConsoleUI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Process.ProcessRequest();
        }
    }
}
