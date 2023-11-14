import numpy as np

class Data:
    def __init__(self, usrid, actid: str, pos):
        self.usrid = usrid
        self.actid = actid
        self.pos = pos
    def __str__(self):
        return f"Data(usrid={self.usrid}, actid={self.actid}, pos={self.pos})"

class BattleShipLogic:
    def __init__(self):
        self.state_players: [bool] = [
            False,
            False,
        ]  # si uno de los jugadores se desconecta -> True
        self.players = []
        self.players_positions = []
        self.state_game: bool = False  # si el juego termino -> True

    def add_data(self, data: Data):
        if (self.players.count(data.usrid) == 0):
            self.players.append(data.usrid)
            
        if (self.players.count(data.usrid) > 2):
            return "wtf how the fkc"
        
        match(data.actid):
            case "#C":
                self.players_positions[data.usrid].append(data.pos)
                return
            case "#M":
                try: 
                    missil = self.get_opposite_player_positions(data.usrid).pop(data.pos)
                    # if hit causes the array of positions to get empty, its basically a win xd
                    if(len(self.get_opposite_player_positions(data.usrid)) == 0):
                        self.state_game = True
                        return f"{data.usrid}#W[{data.pos}]"
                    if (missil):
                        # hit must tell to the server so players do UI thing
                        return f"{data.usrid}#H[{data.pos}]"
                except IndexError:
                    # miss
                    return f"{data.usrid}#N[{data.pos}]"
    
    def get_opposite_player_positions(self, userid) -> list:
        for id in self.players_position:
            if(id != userid):
                return self.players_position[id]

