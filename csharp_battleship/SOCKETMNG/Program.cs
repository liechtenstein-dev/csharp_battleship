using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPractico.TCPConnections;

namespace SOCKETMNG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SocketServer server = new SocketServer();
            server.init_server();
        }
    }
}
