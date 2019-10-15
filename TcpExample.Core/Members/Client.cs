using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using TcpExample.Core.Base;
using TcpExample.Core.Commands;
using TcpExample.Core.Interfaces;

namespace TcpExample.Core.Members
{
    public class Client : ATcpEndPoint, IClient
    {
        public Client(Socket clientSocket)
        {
            base.TcpSocket = clientSocket;
        }

        public async void CommandReceived(Task<Command> commandTask)
        {
            Command command = await commandTask;
            //DO something with command...
        }
    }
}
