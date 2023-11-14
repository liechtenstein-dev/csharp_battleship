import socket
import data_manipulation as dm
from _thread import start_new_thread


class SocketServer:
    def __init__(self, port, connections_list=2, host="127.0.0.1"):
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

        while True:
            connection, address = self.sock.accept()
            self.clients_connection_list.append(connection)
            start_new_thread(self.handle_connection, (connection, address))
            if self.state:
                print("Server handler destroyed succesfully")
                break
        self.sock.close()

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

    # Data(userid, actionid, position)
    def decompress_data(self, data) -> dm.Data:
        return dm.Data(data[0:4], data[4:6], data[7:-1])
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
                if self.verify_data(data):
                    break

                print(f"Received data from {address}: {data}\nDecoded: {repr(data)}")

                # Decompressing data to actually usefull stuf
                self.decompress_data(data)
                reply_change_play = (self.blogic.add_data(data)).encode("utf-8")
                # if a winner is determined game is ended
                if self.blogic.state_game:
                    # send to all players (don't know if this works)
                    self.sock.sendall(reply_change_play)
                    # destroy server
                    self.destroy_socket_server(client_socket, self.blogic.determine_winner())
                    break
                if(len(reply_change_play)==0):
                    return

        except Exception as e:
            print("Error: ", e)
