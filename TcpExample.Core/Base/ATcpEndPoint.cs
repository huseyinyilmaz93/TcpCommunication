
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpExample.Core.Commands;
using TcpExample.Core.Contants;
using TcpExample.Core.Interfaces;

namespace TcpExample.Core.Base
{
    public abstract class ATcpEndPoint : ITcpEndPoint
    {
        public Socket TcpSocket { get; set; }
        
        public async Task<Command> ReceiveMessage()
        {
            byte[] buffer = new byte[TcpConstants.BUFFER_LENGTH];
            int recLength = TcpSocket.Receive(buffer, 0, buffer.Length, 0);
            string message = Encoding.Default.GetString(buffer, 0, recLength).Replace(TcpConstants.ESCAPE_CHARACTER, string.Empty);
            return await Task.FromResult(JsonConvert.DeserializeObject<Command>(message));
        }

        public async Task<bool> SendMessage(Command command)
        {
            string message = string.Concat(TcpConstants.ESCAPE_CHARACTER, JsonConvert.SerializeObject(command), TcpConstants.ESCAPE_CHARACTER);
            byte[] buffer = Encoding.Default.GetBytes(message);
            try
            {
                TcpSocket.Send(buffer, 0, buffer.Length, 0);
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }
    }
}
