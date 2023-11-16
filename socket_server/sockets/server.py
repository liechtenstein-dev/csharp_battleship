import sockets.socket_client as socket_c
import sockets.socket_server as socket_s

class ManageServer:
  def __init__(self, port:int = 5050):
    self.server = socket_s.SocketServer(port)
    self.server.start_server()

class TestClient:
  def __init__(self):
    self.socket = socket_c.SocketClientTest("127.0.0.1", 5050)
  
 # is for testing purposes xd
 # def kill_server(self):
 #  self.socket.send_data("0000#E[00]")
  
  def normal_send(self, data):
    return self.socket.send_data(data)