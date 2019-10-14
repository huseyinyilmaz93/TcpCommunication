using System.Net.Sockets;
using TcpExample.Core.Base;
using TcpExample.Core.Interfaces;

namespace TcpExample.Core.Members
{
    public class Client : ATcpEndPoint, IClient
    {
        public Client(Socket clientSocket)
        {
            base.TcpSocket = clientSocket;
        }
    }
}
