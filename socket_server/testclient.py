import gamesocket

client = gamesocket.TestClient()
client.normal_send("0000#C[255]")
server = gamesocket.ManageServer()