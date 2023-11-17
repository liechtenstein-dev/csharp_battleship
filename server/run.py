import sockets as sock

socks = sock.TestClient()
socks.normal_send("[10,11]")
print("info sended")
checkhit = socks.recieve_data()
print(checkhit)
hitserver = socks.recieve_data()
print(hitserver)
respond = "gg".encode("utf-8")
socks.normal_send(respond)
print("gg")