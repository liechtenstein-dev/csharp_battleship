import gamesockets as socket

s = socket.SocketClientTest("127.0.0.1", 5050)
s.senddata("0000#C[255]")