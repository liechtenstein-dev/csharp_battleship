import numpy as np

class SimulatePlayer:
  def __init__(self, id = 0):
    # all ids that are up to 9XXX are bots Only 999 bots are possible
    self.player_id = 9000 + id
    # random hits for 15*15 (maximum moves in a game), si ya lo se porque pongo comentarios en ingles
    # no lo se ajsdasjdjas suena bien cuando lo leo spngo xd
    self.arr_random_positions = np.random.choice(np.arange(15*15), size=(15*15), replace=False) 
    # these are the positions that where used
    self.used_positions = []
    self.effective_hit_positions = []
    # where the ships are
    self.create_positions = []
  
  def expose(self):
    return self.arr_random_positions
  
  def create_island(self) :
    for i in range(5):
      # selects a random pos from the array [arr_random_positions] 
      random_start_position = np.random.choice(self.arr_random_positions)
      # select a random orientation between -1 and 1
      # left of right
      rlr = np.random.randint(low=-1, high=1, dtype=int) 
      # up or down
      rud = np.random.uniform(low=-16, high=16)
      # rlr or rud
      orientation = np.random.uniform(low=rlr, high=rud)
      ship_position = np.arange(start=random_start_position, stop=random_start_position+orientation*i, step=orientation)
      self.create_positions.append(ship_position)      
    return self.create_positions

  def hit_phase(self):
    # select a position
    random_hit_position = np.random.choice(self.arr_random_positions)
    # build string for server
    string_builder = f"{self.player_id}#M[{random_hit_position}]"
    # remove the hit position from the array
    # numpy delete specific elements is kindof similar to linq/sql querys delete stuff where this cond is right xdxd 
    self.arr_random_positions = np.delete(self.arr_random_positions, np.where(self.arr_random_positions == random_hit_position))
    return string_builder

  def create_phase(self) -> list:
    if (len(self.create_positions) == 0):
      print("You must use the library CORRECTLY DOOD")
  
    # wow i need oficially to read a book about how to write good names for variables kekw
    command_sock_position = []
    for pos in self.create_positions:
      command_sock_position.append(f"{self.player_id}#C[{pos}]")
    
    return command_sock_position