import socket
import random
import threading

class ThreadServer(threading.Thread):
    def __init__(self):
        threading.Thread.__init__(self)
        self.host = "127.0.0.1"
        self.port = 5080
        self.server_sockset = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.connected_clients = []

    def start_server(self, server_port=5050):
        self.server_socket.bind((self.host, self.port))
        self.server_socket.listen(2)
        print("Esperando conexiones...")

        while len(self.connected_clients) < 2:
            client_socket, addr = self.server_socket.accept()
            self.connected_clients.append(client_socket)
            print(f"Conectado a {addr}")

        self.server_sockset.connect(("127.0.0.1", server_port))
        self.server_sockset.send("RP".encode('utf-8'))
        
        for i in range(len(self.connected_clients)):
            initial_status = self.connected_clients[i].recv(1024).decode('utf-8')
            self.server_sockset.send(initial_status.encode('utf-8'))
        
        c = 0
        while(True):        
            client = self.connected_clients[c]
            response = self.selected_client(client)
            server_response = self.server_sockset.send(response.encode('utf-8'))
            
            for i in range(len(self.connected_clients)):
                self.connected_clients[i].send(server_response)
            if (str(server_response.decode('utf-8')).find("#W") != -1):
                break
            c += 1
        for i in range(len(self.connected_clients)):
            self.connected_clients[i].close()
        self.server_sockset.close()        
        self.stop()
            
            
    def selected_client(self, socket):
        socket.send("TURN".encode('utf-8'))
        return socket.recv(1024).decode('utf-8')