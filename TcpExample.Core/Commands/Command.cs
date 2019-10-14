using TcpExample.Core.Enumerations;
using TcpExample.Core.Interfaces;

namespace TcpExample.Core.Commands
{
    public class Command : ICommand
    {
        public TcpCommandType CommandType { get; set; }
    }
}
