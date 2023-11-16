import sockets as sock

cliente = sock.TestClient()
a= cliente.normal_send("0000#C[255]")
print(a)