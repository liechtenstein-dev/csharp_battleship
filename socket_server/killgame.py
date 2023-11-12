import gamesockets as socket
cl = socket.SocketClientTest("127.0.0.1", 5050)
print(cl.senddata("0000#E[230]"))
