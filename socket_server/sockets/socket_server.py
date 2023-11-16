import socket
import threading as threads
import data_manipulation.logic as dm

class SocketServer:
    def __init__(self, port=5050, connections_list=1, host="127.0.0.1"):
        self.port = port
        self.host = host
        self.server_board = dm.Board()
        self.attack_algorithm = dm.AttackAlgorithm()
        self.clients_connection_list = []
        self.state = False

    def start_server(self):
        try: 
            self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            self.sock.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
            self.sock.bind((self.host, self.port))
            self.sock.listen()
            print(f"Server listening on {self.host}:{self.port}")

            while True:
                connection, address = self.sock.accept()
                self.clients_connection_list.append(connection)
                thread = threads.Thread(target=self.handle_connection, args=(connection, address))
                thread.start()

                if self.state:
                    self.close_all()
                    break
        except KeyboardInterrupt:
            print("Server stopped by Ctrl+C")
            self.close_all()
        finally:
            if self.sock:
                self.sock.close()

    def handle_connection(self, client_socket, address):
        try:
            while True:
                data = client_socket.recv(1024).decode("utf-8").strip()
                print(f"Received data from {address}: {data}")

                if not self.verify_data(data):
                    break
                print(f"row:{data[1:3]}, col:{data[4:6]}")
                check_hit_str = self.server_board.check_coordinate(int(data[1:3]),int(data[4:6]) )
                check_hit_encoded = check_hit_str.encode("utf-8")
                print(check_hit_str)
                client_socket.sendall(check_hit_encoded)
                self.server_board.show_board()
                if check_hit_str == "gg":
                    self.destroy_socket_server(client_socket, "gg")

                #reply_play_col, reply_play_row = self.attack_algorithm.hunting_attacks()
                #reply_play_encoded = f"[{reply_play_col},{reply_play_row}]".encode("utf-8")
                #client_socket.sendall(reply_play_encoded)
                
                #result_data = client_socket.recv(1024).decode("utf-8").strip()
                #self.attack_algorithm.return_setter(result_data)

               # if result_data == "gg":
               #     self.destroy_socket_server(client_socket, "Winner")
               #     break
        except Exception as e:
            if "10054" in str(e):
                print("Connection closed by remote host.")

    def remove(self, connection):
        if connection in self.clients_connection_list:
            self.clients_connection_list.remove(connection)
            connection.close()

    def close_all(self):
        for x in self.clients_connection_list:
            self.remove(x)

    def destroy_socket_server(self, connection, winner):
        self.state = True
        connection.sendall(winner.encode("utf-8"))
        self.remove(connection)
        self.close_all()
        print("Remaining sockets in server:", len(self.clients_connection_list))

    def verify_data(self, data):
        if len(data) != 7:
            print("Error in the message sent to the server")
            for char in data:
                print(ord(char))
            print(f"data: {data} len: {len(data)}")
            return False
        return True
