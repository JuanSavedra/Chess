using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.board {
    class Window {
        public static void PrintBoard(Board board) {
            for (int l = 0; l < board.Line; l++) {
                Console.Write(8 - l + " ");
                for (int c = 0; c < board.Column; c++) {
                    if (board.piece(l, c) == null) {
                        Console.Write("- ");
                    }
                    else {
                        PrintPiece(board.piece(l, c));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static ChessPosition ReadChessPosition() {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + " ");
            return new ChessPosition(column, line);
        }

        public static void PrintPiece(Piece piece) {
            if (piece.color == Color.White) {
                Console.Write(piece);
            }
            else {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}
