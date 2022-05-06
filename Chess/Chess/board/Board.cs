using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void InsertPiece(Piece p, Position pos) {
            pieces[pos.Line, pos.Column] = p;
            p.position = pos;
        }
    }
}
