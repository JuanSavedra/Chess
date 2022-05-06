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
    }
}
