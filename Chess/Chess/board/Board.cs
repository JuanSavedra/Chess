using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.board.exceptions;

namespace Chess.board {
    class Board {
        public int Line { get; set; }
        public int Column { get; set; }
        private Piece[,] pieces;

        public Board() { }

        public Board(int line, int column) { 
            this.Line = line;
            this.Column = column;   
            pieces = new Piece[line, column];
        }

        public Piece piece(int line, int column) {
            return pieces[line, column];
        }

        public Piece piece(Position pos) {
            return piece(pos.Line, pos.Column);
        }

        public bool ExistPiece(Position pos) {
            ValidatingPosition(pos);
            return piece(pos) != null;
        }

        public void InsertPiece(Piece p, Position pos) {
            if (ExistPiece(pos)) {
                throw new BoardException("There is already a piece in that position");
            }
            pieces[pos.Line, pos.Column] = p;
            p.position = pos;
        }

        public bool ValidPosition(Position pos) {
            if (pos.Line < 0 || pos.Line >= Line || pos.Column < 0 || pos.Column >= Column) {
                return false;
            }
            return true;
        }

        public void ValidatingPosition(Position pos) {
            if (!ValidPosition(pos)) {
                throw new BoardException("Invalid Position!");
            }
        }
    }
}
