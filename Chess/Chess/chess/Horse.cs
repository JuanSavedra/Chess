using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.board;

namespace chess {
    class Horse : Piece {
        public Horse(Board board, Color color) : base(board, color) { }

        public override string ToString() {
            return "H";
        }

        private bool CanMove(Position pos) {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] PossibleMovements() {
            Position pos = new Position(0, 0);
            bool[,] mat = new bool[board.Line, board.Column];

            pos.DefineValues(position.Line - 1, position.Column - 2);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(position.Line - 2, position.Column - 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(position.Line - 2, position.Column + 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(position.Line - 1, position.Column + 2);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(position.Line + 1, position.Column + 2);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(position.Line + 2, position.Column + 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(position.Line + 2, position.Column - 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(position.Line + 1, position.Column - 2);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }
    }
}
