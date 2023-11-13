import sockets as socket

class ManageServer:
  def __init__(self):
    self.server = socket.SocketServer(5050)
    self.server.start_server()

class TestClient:
  def __init__(self):
    self.socket = socket.SocketClientTest("127.0.0.1", 5050)
  
  def kill_server(self):
    self.socket.send_data("0000#E[00]")
  
  def normal_send(self, data):
    self.socket.send_data(data)