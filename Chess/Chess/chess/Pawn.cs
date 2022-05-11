using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.board;

namespace chess {
    class Pawn : Piece {
        private ChessGame game;

        public Pawn(Board board, Color color, ChessGame game) : base(board, color) {
            this.game = game;
        }

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

                //Jogada especial en passant
                if (position.Line == 3) {
                    Position left = new Position(position.Line, position.Column - 1);
                    if (board.ValidPosition(left) && 
                        ExistEnemy(left) && 
                        board.piece(left) == game.VulnerableEnPassant) {
                        mat[left.Line - 1, left.Column] = true;
                    }
                    Position right = new Position(position.Line, position.Column + 1);
                    if (board.ValidPosition(right) && 
                        ExistEnemy(right) && 
                        board.piece(right) == game.VulnerableEnPassant) {
                        mat[right.Line - 1, right.Column] = true;
                    }
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

                //Jogada especial en passant
                if (position.Line == 4) {
                    Position left = new Position(position.Line, position.Column - 1);
                    if (board.ValidPosition(left) &&
                        ExistEnemy(left) &&
                        board.piece(left) == game.VulnerableEnPassant) {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    Position right = new Position(position.Line, position.Column + 1);
                    if (board.ValidPosition(right) &&
                        ExistEnemy(right) &&
                        board.piece(right) == game.VulnerableEnPassant) {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}
