import sockets as sc
import numpy as np
from _thread import start_new_thread
import simulate_matches.simulate_player as sm_player

class SimulateMatches:
  
  def __init__(self, amount=0) -> None:
    if (amount >= 999):
      raise Exception("The maximum amount of matches is 999")
    for i in range(len(amount)):
      self.matches = []
      self.player_one = sm_player.SimulatePlayer(1)
      self.player_two = sm_player.SimulatePlayer(2)
      self.matches.append(self.player_one, self.player_two)
    self.thread_matches.append(self.matches) 
    self.positions_winn = []

  def remove(self, match):
    for index, item in enumerate(self.thread_matches):
      if (match == self.thread_matches[index]):
        self.thread_matches.remove(match)
        return
      
  def start_simulation(self):
    print("Starting simulation....\n")
    for index, item in enumerate(self.matches):
      print(f"Playing {index} game")
      start_new_thread(self.matches[index].simulate_game, (self.matches[index].player_one, self.player_two))
    return

  def simulate_game(self, player_one: sm_player.SimulatePlayer, player_two: sm_player.SimulatePlayer):
    # create phase for both players 
    islands = []
    players = []
    for i in players.append(player_one, player_two):
      islands.append(i.create_island())
    
    for i in range(len(players)):
      # creating 2 threads into each match for each player
      start_new_thread(self.simulate_player_moves, players[i], islands[i])


  def simulate_player_moves(self, player: sm_player.SimulatePlayer, position_island: list):
    sock = sc.TestClient()
    # yeah i think i need to improve naming
    ships_generated_pos = player.create_phase()    
    for command in ships_generated_pos:
     _ = sock.normal_send(command)
    # end of the creation phase
    
    # hit phase
    for x in range(len(player.arr_random_positions)):
      response:str = sock.normal_send(player.hit_phase())  
      # if i win i stop sending
      if(response[4:6] == "#W"):
        self.positions_winn.append(response[7:-1])
        break
    