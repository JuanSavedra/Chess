using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.board;

namespace chess {
    class King : Piece {
        public King(Board board, Color color) : base(board, color) { }

        public override string ToString() {
            return "K";
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
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //Diagonal Direita Superior
            pos.DefineValues(position.Line - 1, position.Column + 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //Direita
            pos.DefineValues(position.Line, position.Column + 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //Diagonal Direita Inferior
            pos.DefineValues(position.Line + 1, position.Column + 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //Abaixo
            pos.DefineValues(position.Line + 1, position.Column);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //Diagonal Esquerda Inferior
            pos.DefineValues(position.Line + 1, position.Column - 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //Esquerda
            pos.DefineValues(position.Line, position.Column - 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //Diagonal Esquerda Superior
            pos.DefineValues(position.Line - 1, position.Column - 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }
    }
}
