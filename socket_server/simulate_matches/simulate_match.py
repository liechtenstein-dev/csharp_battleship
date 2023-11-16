import threading
import sockets as sc
import simulate_matches.simulate_player as sm_player

class SimulateMatches:
  
  def __init__(self, amount=1):
    if amount >= 999:
      raise Exception("La cantidad máxima de partidas es 999")
    self.thread_matches = []  
    self.matches = []
    self.positions_winn = []

    for i in range(amount):
      player_one = sm_player.SimulatePlayer(i)
      player_two = sm_player.SimulatePlayer(i+1)
      self.matches.append([player_one, player_two])

  def simulate_game(self, player_one, player_two):
    islands = [player_one.create_island(), player_two.create_island()]

    for i, player in enumerate([player_one, player_two]):
      threading.Thread(target=self.simulate_player_moves, args=(player, islands[i])).start()

  def start_simulation(self):
    print("Iniciando simulación...\n")
    for index, match in enumerate(self.matches):
      print(f"Jugando el juego {index}")
      self.simulate_game(match[0], match[1])

  def simulate_player_moves(self, player, position_island):
    sock = sc.TestClient()
    ships_generated_pos = player.create_phase()    
    for command in ships_generated_pos:
      _ = sock.normal_send(command)

    for x in range(len(player.arr_random_positions)):
      response = sock.normal_send(player.hit_phase())  
      if response[4:6] == "#W":
        self.positions_winn.append(response[7:-1])
        break
