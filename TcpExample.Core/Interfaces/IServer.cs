using System.Collections;
using System.Collections.Generic;
using TcpExample.Core.Base;

namespace TcpExample.Core.Interfaces
{
    public interface IServer
    {
        IList<ATcpEndPoint> Clients { get; set; }

        void StartListening();

        void AcceptConnection();
    }
}
