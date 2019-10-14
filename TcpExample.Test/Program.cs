using System;
using System.Threading.Tasks;
using TcpExample.Core.Members;

namespace TcpExample.Test
{
    class Program
    {
        public static Server SampleServer { get; set; }

        static void Main(string[] args)
        {
            SampleServer = new Server();
            SampleServer.AcceptConnection();
            Task.Factory.StartNew(() => SampleServer.StartListening());
            if (Console.ReadKey().Key == ConsoleKey.S)
                SampleServer.StopListening();
        }
    }
}
