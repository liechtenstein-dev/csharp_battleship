using System;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Text;

namespace TrabajoPractico
{
    internal class SocketCliente
    {
        public static string response;

        public static async Task StartClientAsync(string msjjugada = @"0000#E[00]")
        {
            string serverIp = "127.0.0.1";
            int serverPort = 5050;
            try
            {
                TcpClient tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(serverIp, serverPort);
                await SocketCliente.SendDataAsync(tcpClient, msjjugada);
                string response = await SocketCliente.ReceiveDataAsync(tcpClient);
                SocketCliente.response = response;
                MessageBox.Show("Respuesta del servidor: " + response, "Respuesta", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static async Task SendDataAsync(TcpClient client, string data)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }

        public static async Task<string> ReceiveDataAsync(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }
    }
}
