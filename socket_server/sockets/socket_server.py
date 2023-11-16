import socket
import data_manipulation.logic as dm
import threading as threads

class SocketServer:
    def __init__(self, port=5050, connections_list=2, host="127.0.0.1"):
        self.port = port
        self.host = host
        self.state = False
        self.clients_connection_list = []
        self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.sock.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        self.sock.bind((host, int(self.port)))
        self.sock.listen(connections_list)
        self.blogic = dm.BattleShipLogic()
        
    def start_server(self):
        print(f"Server listening on {self.host}:{self.port}")
        try: 
            while True:
                connection, address = self.sock.accept()
                self.clients_connection_list.append(connection)
                thread = threads.Thread(target=self.handle_connection, args=(connection, address))
                thread.start()

                if self.state:
                    thread.shutdown(wait=False)
                    print("Server handler destroyed successfully")
                    break
        except KeyboardInterrupt:
            print("Stopped by Ctrl+C")
        finally:
            if self.sock:
                self.sock.close()
            for t in self.clients_connection_list:
                t.join()
    # close one socket connection
    def remove(self, connection):
        if connection in self.clients_connection_list:
            self.clients_connection_list.remove(connection)
            connection.close()
    # moved because handle_connection was tooo big
    def verify_data(self, data):
        if len(data) != 11:
            print("Error en el mensaje que se envio al servidor")
            for char in data:
                print(ord(char))
            print(f"data: {data} len: {len(data)}")
            return False
        return True
    # close all sockets connections
    def close_all(self):
        for x in self.clients_connection_list:
            self.remove(x)
    def destroy_socket_server(self, connection, winner):
        self.state = True
        connection.sendall(
            (winner).encode("utf-8")
        )
        self.remove(connection)
        self.close_all()
        print(
            "Sockets remaining into server socket: ", len(self.clients_connection_list)
        )
    # being constantly handle foreach thread in connection
    def handle_connection(self, client_socket, address):
        try:
            while True:
                data = ((client_socket.recv(1024)).decode("utf-8")).strip()
                print(data)
                
                if (not self.verify_data(data)):
                    break

                print(f"Received data from {address}: {data}\nDecoded: {repr(data)}")
                reply_change_play = (self.blogic.add_data(data).encode("utf-8"))

                for connection in self.clients_connection_list:
                    if connection != client_socket:
                        connection.send(reply_change_play)
                
                # if a winner is determined game is ended
                if self.blogic.state_game:
                    # send to all players (don't know if this works)
                    # destroy server
                    self.destroy_socket_server(client_socket, self.blogic.determine_winner())
                    break
                if(len(reply_change_play)==0):
                    return

        except Exception as e:
            if "10054" in str(e):
                print("Conexión cerrada por el host remoto.")

