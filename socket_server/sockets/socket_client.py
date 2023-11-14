import socket

class SocketClientTest:
    def __init__(self, ip, port):
        self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.sock.connect((ip, port))
        
    def send_data(self, data) -> str:
        self.sock.sendall(data.encode("utf-8"))
        data = (self.sock.recv(1024)).decode("utf-8")
        return data
