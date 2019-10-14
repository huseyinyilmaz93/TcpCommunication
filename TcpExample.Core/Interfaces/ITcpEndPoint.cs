using System.Net.Sockets;

namespace TcpExample.Core.Interfaces
{
    public interface ITcpEndPoint
    {
        Socket TcpSocket { get; set; }
    }
}
