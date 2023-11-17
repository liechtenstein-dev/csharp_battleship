namespace TrabajoPractico.Extras
{
    public class GestorBarquito
    {
        private int[] availableMoves = new int[5] { 5, 4, 3, 2, 1 };
        private int[] possibleMoves = new int[5] { 5, 4, 3, 2, 1 };

        private int[] pos;
        private int selected_ship;
        
        private int selected_ship_arrays;
        private Dictionary<int, Color> type_ships_color = new Dictionary<int, Color>()
        {
            {1, Color.Crimson },
            {2, Color.Blue },
            {3, Color.Green },
            {4, Color.Red },
            {5, Color.Yellow },
        };
        
        private Dictionary<int, int> type_ships_moves = new Dictionary<int, int>();
        public event EventHandler<EventCellOutter> BarcoSeleccionado;

        public GestorBarquito(){}

        private bool LegalMove()
        {
            // no hay más movimientos posibles
            if (availableMoves[selected_ship_arrays] == 0)
                return false;
            // hay un barco en la pos que se quiere acceder
            if (type_ships_moves.ContainsKey(pos[0] * 16 + pos[1]))
                return false;
            // ya se hizo una jugada con ese barco
            if (posibleMoves[selected_ship_arrays] > availableMoves[selected_ship_arrays])
            {
                // busco para donde encaran los barcos que se movieron
                return CalculateNearPosition();
            }
            return true;
        }

        public bool CalculateNearPosition(){
            /*
             *  Filtro los movimientos registrados con el barcoID que esta seleccionado
             *  Para tener los movimientos/puntos del barco en cuestión y en base a eso
             *  Volver a filtrar con los posibles movimientos
             */
            var selectedShipKeys = type_ships_moves
                .Where(pair => pair.Value == selected_ship)
                .Select(pair => pair.Key)
                .ToList();
            var pos_ship = pos[0] + pos[1] * 16;
            var posibleMovesLR = new List<int>() { { pos_ship + 1 }, { pos_ship - 1 }, };
            var posibleMovesUD = new List<int>() { { pos_ship - 16 }, { pos_ship + 16 }, };
            var allPosibleMoves = new List<int>();
            allPosibleMoves.AddRange(posibleMovesLR);
            allPosibleMoves.AddRange(posibleMovesUD);
            // Interseccióno los movimientos posibles con los que tengo registrados
            // siempre un barco debe de tener minimo 1 pieza conectada
            var moves = allPosibleMoves.Intersect(selectedShipKeys);

            // si tiene 2 piezas conectadas tengo q saber la orientación del barco
            if (selectedShipKeys.Count >= 2 && moves.Any())
            {
                var orientation = GetOrientation(selectedShipKeys[0], selectedShipKeys[1]);

                switch (orientation)
                {
                    case Orientation.Horizontal:
                        return (
                            IsHorizontalMove(selectedShipKeys.Max(), pos_ship)
                            || IsHorizontalMove(selectedShipKeys.Min(), pos_ship)
                        );
                    case Orientation.Vertical:
                        return (
                            IsVerticalMove(selectedShipKeys.Max(), pos_ship)
                            || IsVerticalMove(selectedShipKeys.Min(), pos_ship)
                        );
                    default:
                        break;
                }
            }
            return moves.Any();
        }

        private Orientation GetOrientation(int m1, int m2){
            // Determinar la orientación en función de las coordenadas de los dos movimientos.
            if (Math.Abs(m1 - m2) == 1)
            {
                return Orientation.Horizontal;
            }
            else
            {
                return Orientation.Vertical;
            }
        }

        private bool IsHorizontalMove(int move1, int move2){
            return Math.Abs(move1 - move2) == 1;
        }

        private bool IsVerticalMove(int move1, int move2){
            return Math.Abs(move1 - move2) == 16;
        }

}
