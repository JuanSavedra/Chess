using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.board;

namespace chess {
    class Bishop : Piece {
        public Bishop(Board board, Color color) : base(board, color) { }

        public override string ToString() {
            return "B";
        }

        private bool CanMove(Position pos) {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] PossibleMovements() {
            Position pos = new Position(0, 0);
            bool[,] mat = new bool[board.Line, board.Column];

            pos.DefineValues(position.Line - 1, position.Column - 1);
            while (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color) {
                    break;
                }
                pos.DefineValues(pos.Line - 1, pos.Column - 1);
            }

            pos.DefineValues(position.Line - 1, position.Column + 1);
            while (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color) {
                    break;
                }
                pos.DefineValues(pos.Line - 1, pos.Column + 1);
            }

            pos.DefineValues(position.Line + 1, position.Column + 1);
            while (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color) {
                    break;
                }
                pos.DefineValues(pos.Line + 1, pos.Column + 1);
            }

            pos.DefineValues(position.Line + 1, position.Column - 1);
            while (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color) {
                    break;
                }
                pos.DefineValues(pos.Line + 1, pos.Column - 1);
            }

            return mat;
        }
    }
}
