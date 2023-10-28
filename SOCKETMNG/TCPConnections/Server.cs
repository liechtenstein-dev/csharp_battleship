using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPractico.TCPConnections
{
    internal class SocketServer
    {
        Queue<string> lastHitPlayed; Socket serverSocket; 
        bool played;  string clientResponse;

        public static void StartServer()
        {
            string data;
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            try
            {
                using (Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(10);
                    Console.WriteLine("Waiting for a connection...");

                    while (true)
                    {
                        using (Socket handler = listener.Accept())
                        {
                            data = null;
                            byte[] bytes = new byte[1024];

                            while (true)
                            {
                                int bytesRec = handler.Receive(bytes);
                                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                                if (data.IndexOf("<EOF>") > -1)
                                {
                                    break;
                                }
                            }

                            Console.WriteLine("Text received : {0}", data);
                        }

                        if( data.Contains("<EOG>"))
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // private string FirstPlayed()
        // {
        //     return ListeningConnections();
        // }

        private void AdvertiseStateSet()
        {
            // TODO Should say if it is firsplayed, if indeed is first played
            // the player should send the hitposition
            //serverSocket.Send();
        }
    }
}
