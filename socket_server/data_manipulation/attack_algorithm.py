import random
class AttackAlgorithm:
    def __init__(self):
        self.played_moves = []
        self.hits = []  # store successful hits
        self.result = ""
        self.attack_row = 0
        self.attack_col = 0
    
    def return_setter(self, new_result):
        self.result = new_result
        self.hits_setter()
        return self.result

    def hits_setter(self): 
        if self.result == 'tocado':
            self.hits.append((self.attack_col, self.attack_row))            

    def hunting_attacks(self, board):
        if len(self.hits) > 0:
            # if theres a hit, we should look in the nearest positions
            self.attack_col , self.attack_row = self.hits[-1]
            directions = [(0, 1), (1, 0), (-1, 0), (0, -1)]
            valid_directions = []
            for dir_row, dir_col in directions:
                if (0 <= self.attack_col + dir_col < 15) and (0 <= self.attack_row + dir_row < 15):
                    valid_directions.append(((self.attack_col + dir_col), (self.attack_row+dir_row)))
            selected = random.choice(valid_directions)
            if selected not in self.played_moves:
                self.played_moves.append(selected)
            return selected
        else:
            # it looks cool the one liners but they are kinda awkward to debug xd
            available_moves = [(i, j) 
                               for i in range(len(board))
                               for j in range(len(board[0]))
                               if (i, j) not in self.played_moves]
            if available_moves:
                self.attack_col, self.attack_row = random.choice(available_moves)
                self.played_moves.append((self.attack_col, self.attack_row))
                print(f"Ataque a la coordenada ({self.attack_col}, {self.attack_row}): {self.result}")
                return (self.attack_col, self.attack_row)
    

""" import board as brd  # noqa: E402
a = AttackAlgorithm()
b = brd.Board()
x,y = a.hunting_attacks(b.board)
print(x,y)
rt = a.return_setter("tocado")
print(rt)
x,y = a.hunting_attacks(b.board)
print(x,y)
 """