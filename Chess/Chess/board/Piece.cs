using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.board {
    abstract class Piece {
        public Position position { get; set; }
        public Color color { get; set; }
        public int QuantityMovements { get; set; }
        public Board board { get; set; }

        public Piece() { }

        public Piece(Board board, Color color) { 
            this.position = null;
            this.color = color;
            this.board = board;
            this.QuantityMovements = 0;
        }

        public abstract bool[,] PossibleMovements();

        public void IncrementQuantityMovements() {
            QuantityMovements++;
        }
    }
}
