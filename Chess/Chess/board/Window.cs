using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.board {
    class Window {
        public static void printBoard(Board board) {
            for (int l = 0; l < board.Line; l++) {
                for (int c = 0; c < board.Column; c++) {
                    if (board.piece(l, c) == null) {
                        Console.Write("- ");
                    }
                    else {
                        Console.Write(board.piece(l, c) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
