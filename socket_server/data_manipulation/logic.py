class Data:
    def __init__(self, usrid: str, actid: str, pos: str):
        self.usrid: str = usrid
        self.actid: str = actid
        self.pos: str = pos
    
    def __str__(self) -> str:
        return f"Data(usrid={self.usrid}, actid={self.actid}, pos={self.pos})"

class BattleShipLogic:
    def __init__(self):
        self.players_positions: dict[str, list[str]] = {}
        self.state_game: bool = False

    def decompress_data(self, data: str) -> Data:
        return Data(data[0:4], data[4:6], data[7:-1])
    
    def add_data(self, data: str) -> str:
        data = self.decompress_data(data)
        
        if data.usrid not in self.players_positions:
            self.players_positions[data.usrid] = []
        
        if data.actid == "#C" and data.usrid in self.players_positions:
            self.players_positions[data.usrid].append(data.pos)
            return "Creating ships"
        
        if data.actid == "#M" and data.usrid in self.players_positions:
            try: 
                missil = self.get_opposite_player_positions(data.usrid).pop(data.pos)
                if not self.get_opposite_player_positions(data.usrid):
                    self.state_game = True
                    return f"{data.usrid}#W[{data.pos}]"
                if missil:
                    return f"{data.usrid}#H[{data.pos}]"
            except IndexError:
                return f"{data.usrid}#N[{data.pos}]"
    
    def get_opposite_player_positions(self, userid: str) -> list:
        for id in self.players_positions:
            if id != userid:
                return self.players_positions[id]