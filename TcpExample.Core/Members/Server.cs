using System.Net;
using System.Threading;
using System.Net.Sockets;
using TcpExample.Core.Base;
using System.Threading.Tasks;
using TcpExample.Core.Contants;
using TcpExample.Core.Interfaces;
using System.Collections.Generic;

namespace TcpExample.Core.Members
{
    public class Server : ATcpEndPoint, IServer
    {
        public static object locker = new object();

        public IList<ATcpEndPoint> Clients { get; set; }

        private CancellationTokenSource AcceptConnectionCancellationTokenSource { get; set; }

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
            TcpSocket.Bind(hostEndPoint);
            TcpSocket.Listen(TcpConstants.MAX_CONNECTION);
        }

        public async void StartListening()
        {
            AcceptConnectionCancellationTokenSource = new CancellationTokenSource();
            AcceptConnectionCancellationToken = AcceptConnectionCancellationTokenSource.Token;
            while (AcceptConnectionCancellationToken.IsCancellationRequested == false)
            {
                Socket clientSocket = TcpSocket.Accept();
                Client client = await CreateNewInstance(clientSocket);
                Task.Factory.StartNew(() => base.ReceiveMessage()).ContinueWith(client.CommandReceived);
            }
        }

        private async Task<Client> CreateNewInstance(Socket clientSocket)
        {
            Client client = new Client(clientSocket);
            lock (locker)
            {
                Clients.Add(client);
            }
            return await Task.FromResult(client);
        }

        public void Disconnect(Client client)
        {
            lock (locker)
            {
                Clients.Remove(client);
            }
        }

        public void StopListening()
        {
            if (AcceptConnectionCancellationTokenSource != null)
                AcceptConnectionCancellationTokenSource.Cancel();
        }
    }
}
 