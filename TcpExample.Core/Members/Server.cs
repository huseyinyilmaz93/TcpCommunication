using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TcpExample.Core.Base;
using TcpExample.Core.Contants;
using TcpExample.Core.Interfaces;

namespace TcpExample.Core.Members
{
    public class Server : ATcpEndPoint, IServer
    {
        public IList<ATcpEndPoint> Clients { get; set; }

        private CancellationTokenSource AcceptConnectionCancellationTokenSource = new CancellationTokenSource();
        private CancellationToken AcceptConnectionCancellationToken { get; set; }

        public Server()
        {
            Clients = new List<ATcpEndPoint>();
        }

        public void AcceptConnection()
        {
            IPAddress host = IPAddress.Parse(TcpConstants.TCP_SERVER_ADDRESS);
            IPEndPoint hostEndPoint = new IPEndPoint(host, TcpConstants.TCP_SERVER_PORT);
            TcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void StartListening()
        {

            while (AcceptConnectionCancellationToken.IsCancellationRequested == false)
            {
                Socket clientSocket = TcpSocket.Accept();
                Client client = new Client(clientSocket);
            }
        }
    }
}
 