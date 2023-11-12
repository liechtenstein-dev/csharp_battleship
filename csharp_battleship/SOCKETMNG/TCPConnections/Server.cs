using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrabajoPractico.TCPConnections
{
	internal class ParserXMLRegister {
			
			// you are not supposed to use this DataSet out of class base imp
			// in which you inherit ParserXML and that base class uses this 
			private protected DataSet ds;
            string xml_location;

			public ParserXMLRegister(string xml_location){
				// todo: check if this location is the correct one should be
				// SOLUTION DIR: /UTILS/GameReg/ <-
				this.xml_location = xml_location;
			}

			public void Read(){
			}

			public void Write(){}

	}

	internal class GameRecord{}

	
	internal class MapperMoves {
	
		string Move;



		public MapperMoves(string movesended){
			
			if(movesended.Length == 0)
				throw new Exception("CLIENT MESSAGE EMPTY!");
			
			// MAX LENGTH IS 235
			if(movesended.Length > 235)
				throw new Exception(@"Island is not correctly created, there are less than expected '(229)' ${movesended.Lenght()}.");
			
			this.Move = movesended;
		}

	}

    internal class SocketServer
    {
        private Queue<string> lastHitPlayed; 
        private bool played;
        private int puerto = 3080;
        private Socket socketEscucha;
        private string clientResponse;

        public void init_server()
        {
            Console.WriteLine("Server init");
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, puerto);
            
            
            socketEscucha = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp); 
            socketEscucha.Bind(ie); 
            socketEscucha.Listen(5);
            Console.WriteLine("waiting for connections in port:{0}", puerto);
            
            try
            {
                while (true)
                {
                    Socket socketCliente = socketEscucha.Accept();
                    Thread hiloCliente = new Thread(atend_client_connection);
                    hiloCliente.IsBackground = true;
                    Console.WriteLine("Socket is here and attend connection");
                    hiloCliente.Start(socketCliente);
                }
            }
            catch (SocketException)
            {
                Console.WriteLine("Connections finalized");
            }

        }
        
        public void atend_client_connection(object o)
        {
            string mensaje;
            Boolean clienteActivo = true;
            Socket cliente = (Socket)o;
            IPEndPoint ieCliente = (IPEndPoint)cliente.RemoteEndPoint;

            Console.WriteLine("Connect socket address: {0} in the port:{1}",
                ieCliente.Address, ieCliente.Port);

            NetworkStream ns = new NetworkStream(cliente);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            sw.WriteLine("Socket into server");
            sw.Flush();

            while (clienteActivo)
            {
                // Recibe mensaje del cliente
                mensaje = sr.ReadLine();

                //Se recibe null si se cierra el cliente de golpe
                if (mensaje == null)
                {
                    clienteActivo = false;
                    break;
                }

                // Gestion del protocolo
                switch (mensaje)
                {
                    case "#salir":
                        clienteActivo = false;
                        break;
                    case "#apagar":
                        clienteActivo = false;
                        break;
                    default:
                        sw.WriteLine(mensaje);
                        sw.Flush();
                        break;
                }

                Console.WriteLine("{0} says: {1}",
                    ieCliente.Address, mensaje);
            }

            Console.WriteLine("Connection finalized with {0}:{1}",
                ieCliente.Address, ieCliente.Port);

            sw.Close();
            sr.Close();
            ns.Close();
            cliente.Close();
        }
        
    }
}
