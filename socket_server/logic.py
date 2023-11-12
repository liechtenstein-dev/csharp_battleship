class Data():
  def __init__(self, usrid, actid, pos):
    self.usrid = usrid
    self.actid = actid
    self.pos = pos
  
  def __repr__(self):
    return f"Data(usrid={self.usrid}, actid={self.actid}, pos={self.pos})"
  
  def __str__(self):
    return f"Data(usrid={self.usrid}, actid={self.actid}, pos={self.pos})"
  
  def __eq__(self, other):
    return self.usrid == other.usrid and self.actid == other.actid and self.pos == other.pos
  
  def __ne__(self, other):
    return not self.__eq__(other)
  
  def __hash__(self):
    return hash((self.usrid, self.actid, self.pos))
  
  def __len__(self):
    return len(self.usrid) + len(self.actid) + len(self.pos)
class BattleShipLogic:
  def __init__(self, data, connection):
    self.data = data
    self.conn_list_players = []
    self.data_list = []
    self.islands_players = []
    self.data_list.append(self.data)
    self.conn_list_players.append(connection)
    self.turn_players: int = 0 # 0 = player 1, 1 = player 2
    self.state_players: [bool] = [False, False] # si uno de los jugadores se desconecta -> True
    self.state_game: bool = False # si el juego termino -> True
    print(self.data_list)
  
  def decode_position(self):
    num = int(self.data.pos)
    row = 0
    col = 0
    while(num > 0):
      if(num % 16 == 0):
        num -= 16
        col+=1
      else:
        num -= 1
        row+=1
    print(f"Col: {col}\nRow: {row}")  
    
  
  def generate_island(self):
    island = []
    for i in range(15):
      for j in range(15):
        island[i][j] = '0'
    self.islands_players.append(island)
     
  def test(self):
    self.state_game = True