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

        public bool ExistPossibleMovements() {
            bool[,] mat = PossibleMovements();
            for (int l = 0; l < board.Line; l++) {
                for (int c = 0; c < board.Column; c++) {
                    if (mat[l, c]) {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CanMoveFor(Position pos) {
            return PossibleMovements()[pos.Line, pos.Column];
        }

        public abstract bool[,] PossibleMovements();

        public void IncrementQuantityMovements() {
            QuantityMovements++;
        }
    }
}
