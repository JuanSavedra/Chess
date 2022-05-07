using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.board;

namespace chess {
    class Tower : Piece {
        public Tower(Board board, Color color) : base(board, color) { }

        public override string ToString() {
            return "T";
        }

        private bool CanMove(Position pos) {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] PossibleMovements() {
            bool[,] mat = new bool[board.Line, board.Column];
            Position pos = new Position(0, 0);

            //Acima
            pos.DefineValues(position.Line - 1, position.Column);
            while (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color) {
                    break;
                }
                pos.Line = pos.Line - 1;
            }

            //Direita
            pos.DefineValues(position.Line, position.Column + 1);
            while (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color) {
                    break;
                }
                pos.Column = pos.Column + 1;
            }

            //Abaixo
            pos.DefineValues(position.Line + 1, position.Column);
            while (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color) {
                    break;
                }
                pos.Line = pos.Line + 1;
            }

            //Esquerda
            pos.DefineValues(position.Line, position.Column - 1);
            while (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color) {
                    break;
                }
                pos.Column = pos.Column - 1;
            }

            return mat;
        }
    }
}
