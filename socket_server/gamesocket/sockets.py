import socket
from logic import BattleShipLogic, Data
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

    # close all sockets connections
    def close_all(self):
        for x in self.clients_connection_list:
            self.remove(x)

    # being constantly handle foreach thread in connection
    def handle_connection(self, connection, address):
        try:
            while True:
                data = (connection.recv(1024)).decode("utf-8")
                data = data.strip()
                if len(data) != 11:
                    for char in data:
                        print(ord(char))
                    print(f"data: {data} len: {len(data)}")
                    raise Exception(
                        "Se envio un mensaje con un numero invalido de caracteres"
                    )
                    break

                print(f"Received data from {address}: {data}\nDecoded: {repr(data)}")
                # Decompressing data to actually usefull stuf
                usrid = data[0:4]
                actid = data[4:6]
                pos = data[7:-1]
                b = BattleShipLogic(data=Data(usrid, actid, pos), connection=connection)

                if actid == "#E":
                    b.test()
                else:
                    b.decode_position()
                    print(f"Move played: {pos}, Action {actid}")
                    connection.sendall(
                        (f"Move played: {pos}, Action {actid}").encode("utf-8")
                    )

                if b.state_game:
                    self.state = True
                    connection.sendall(
                        ("state changed to 'True' destroying all sockets").encode(
                            "utf-8"
                        )
                    )
                    self.remove(connection)
                    self.close_all()
                    print(
                        "Sockets remaining into server socket: ",
                        len(self.clients_connection_list),
                    )
                    break

        except Exception as e:
            print("Error: ", e)


class SocketClientTest:
    def __init__(self, ip, port):
        self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.sock.connect((ip, port))

    def send_data(self, data) -> str:
        self.sock.sendall(data.encode("utf-8"))
        data = (self.sock.recv(1024)).decode("utf-8")
        return data
