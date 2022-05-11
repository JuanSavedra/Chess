using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.board;

namespace chess {
    class Pawn : Piece {
        public Pawn(Board board, Color color) : base(board, color) { }

        public override string ToString() {
            return "P";
        }

        private bool ExistEnemy(Position pos) {
            Piece p = board.piece(pos);
            return p != null && p.color != color;
        }

        private bool Free(Position pos) { 
            return board.piece(pos) == null;
        }

        public override bool[,] PossibleMovements() {
            Position pos = new Position(0, 0);
            bool[,] mat = new bool[board.Line, board.Column];

            if (color == Color.White) {
                pos.DefineValues(position.Line - 1, position.Column);
                if (board.ValidPosition(pos) && Free(pos)) { 
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(position.Line - 2, position.Column);
                if (board.ValidPosition(pos) && Free(pos) && QuantityMovements == 0) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(position.Line - 1, position.Column - 1);
                if (board.ValidPosition(pos) && ExistEnemy(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(position.Line - 1, position.Column + 1);
                if (board.ValidPosition(pos) && ExistEnemy(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
            }
            else {
                pos.DefineValues(position.Line + 1, position.Column);
                if (board.ValidPosition(pos) && Free(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(position.Line + 2, position.Column);
                if (board.ValidPosition(pos) && Free(pos) && QuantityMovements == 0) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(position.Line + 1, position.Column - 1);
                if (board.ValidPosition(pos) && ExistEnemy(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(position.Line + 1, position.Column + 1);
                if (board.ValidPosition(pos) && ExistEnemy(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
            }

            return mat;
        }
    }
}
