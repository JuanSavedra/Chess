using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.board;

namespace chess {
    class King : Piece {
        private ChessGame game;

        public King(Board board, Color color, ChessGame game) : base(board, color) {
            this.game = game;
        }

        public override string ToString() {
            return "K";
        }

        private bool CanMove(Position pos) {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        private bool TestTowerSmallRook(Position pos) {
            Piece p = board.piece(pos);
            return p != null && p is Tower && p.color == color && QuantityMovements == 0;
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

            // Jogada Especial
            if (QuantityMovements == 0 && !game.Check) {
                //Roque pequeno
                Position posisitonTowerRight = new Position(position.Line, position.Column + 3);
                if (TestTowerSmallRook(posisitonTowerRight)) {
                    Position blankSpace1 = new Position(position.Line, position.Column + 1);
                    Position blankSpace2 = new Position(position.Line, position.Column + 2);
                    if (board.piece(blankSpace1) == null && board.piece(blankSpace2) == null) {
                        mat[position.Line, position.Column + 2] = true;
                    }
                }
                //Roque grande
                Position posisitonTowerLeft = new Position(position.Line, position.Column - 4);
                if (TestTowerSmallRook(posisitonTowerLeft)) {
                    Position blankSpace1 = new Position(position.Line, position.Column - 1);
                    Position blankSpace2 = new Position(position.Line, position.Column - 2);
                    Position blankSpace3 = new Position(position.Line, position.Column - 3);
                    if (board.piece(blankSpace1) == null && 
                        board.piece(blankSpace2) == null && 
                        board.piece(blankSpace3) == null) {
                        mat[position.Line, position.Column - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}
