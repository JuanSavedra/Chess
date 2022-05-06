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
    }
}
