using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.board {
    class ChessPosition {
        public char Column { get; set; }
        public int Line { get; set; }

        public ChessPosition() { }

        public ChessPosition(char column, int line) { 
            Column = column;
            Line = line;
        }

        public Position ToPosition() {
            return new Position(8 - Line, Column - 'A');
        }

        public override string ToString() {
            return $"{Column} {Line}";
        }
    }
}
