using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPractico.Forms.BattleGames.CLSocket
{
    internal class SocketCliente
    {
        private Socket clientSocket;
        private byte[] buffer = new byte[1024]; 
        public static event EventHandler<string> MessageReceived;
        public void StartSocket()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(IPAddress.Parse("127.0.0.1"), 5050);
            clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int received = clientSocket.EndReceive(ar);
                if (received > 0)
                {
                    byte[] data = new byte[received];
                    Array.Copy(buffer, data, received);

                    string message = Encoding.UTF8.GetString(data);
                    MessageReceived?.Invoke(this, message);

                    clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, null);
                }
            }
            catch (SocketException)
            {
            }
        }

        public void SendMessageToServer(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            clientSocket.Send(data);
        }

        public void StopSocket()
        {
            // Cerrar el socket
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }
}