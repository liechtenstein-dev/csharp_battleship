class AttackAlgorithm:
    def __init__(self):
        self.hits = []  # Almacena las coordenadas de los ataques exitosos
        self.result = ""
        self.attack_row = 0
        self.attack_col = 0
    
    def return_setter(self, new_result):
        self.result = new_result
        self.hits_setter()

    def hits_setter(self): 
        if self.result == 'gg':
            return (50, 50)
        if self.result != 'agua':
            self.hits.append((self.attack_row, self.attack_col))
            return self.hits
        
    def hunting_attacks(self, board):
            # Si hay coordenadas de golpes exitosos, obtén alrededor para el siguiente ataque
        if self.hits:
            self.attack_row, self.attack_col = self.next_attack(self.hits)
        else:
            # Si no, genera coordenadas aleatorias
            self.attack_row = random.randint(0, len(board) - 1)
            self.attack_col = random.randint(0, len(board[0]) - 1)

            # Realizar el ataque en las coordenadas generadas
        print(str(self.attack_row), str(self.attack_col))
        print(f"Ataque a la coordenada ({self.attack_col}, {self.attack_row}): {self.result}")
        return (self.attack_col, self.attack_row)
        


    # Función para determinar la siguiente posición de ataque alrededor de un golpe exitoso
    def next_attack(self, hits, board):
        # Obtener los golpes exitosos más recientes
        last_hit = hits[-1]
        row, col = last_hit
        # Definir direcciones alrededor del último golpe exitoso (arriba, abajo, izquierda, derecha)
        directions = [(-1, 0), (1, 0), (0, -1), (0, 1)]
        # Filtrar posiciones válidas para el siguiente ataque
        valid_attacks = [(row + dr, col + dc) for dr, dc in directions if 0 <= row + dr < len(board) and 0 <= col + dc < len(board[0])]
        # Devolver la siguiente posición para atacar
        return random.choice(valid_attacks)
