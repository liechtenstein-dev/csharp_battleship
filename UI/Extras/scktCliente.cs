using System;
using System.Net;
using System.IO;
using System.Net.Sockets;

namespace TrabajoPractico
{
    internal class scktCliente
    {
        public static void StartClient(string msjjugada = @"hola server amigoooo, #salir"){
       
            string serverIP = "127.0.0.1";
            int serverPort = 3080;
            try
            {
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverEndPoint);
                NetworkStream networkStream = new NetworkStream(clientSocket);
                StreamReader reader = new StreamReader(networkStream);
                StreamWriter writer = new StreamWriter(networkStream);
                
                writer.WriteLine(msjjugada);
                writer.Flush();
                string serverResponse = reader.ReadLine();
                Console.WriteLine("Servidor dice: " + serverResponse);
                clientSocket.Close();
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Error de socket: " + ex.Message);
            }
        }
    }
}
