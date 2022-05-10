using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.board;
using chess;

namespace Chess.board {
    class Window {
        public static void PrintBoard(Board board) {
            for (int l = 0; l < board.Line; l++) {
                Console.Write(8 - l + " ");
                for (int c = 0; c < board.Column; c++) {
                    PrintPiece(board.piece(l, c));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void PrintBoard(Board board, bool[,] possiblePositions) {
            ConsoleColor backgroundOrigin = Console.BackgroundColor;
            ConsoleColor backgroundAlterated = ConsoleColor.DarkGray;

            for (int l = 0; l < board.Line; l++) {
                Console.Write(8 - l + " ");
                for (int c = 0; c < board.Column; c++) {
                    if (possiblePositions[l, c]) { 
                        Console.BackgroundColor = backgroundAlterated;
                    }
                    else {
                        Console.BackgroundColor = backgroundOrigin;
                    }
                    PrintPiece(board.piece(l, c));
                    Console.BackgroundColor = backgroundOrigin;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
            Console.BackgroundColor = backgroundOrigin;
        }

        public static ChessPosition ReadChessPosition() {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + " ");
            return new ChessPosition(column, line);
        }

        public static void PrintGame(ChessGame game) {
            PrintBoard(game.board);
            PrintCapturatedPieces(game);
            Console.WriteLine($"\nShift: {game.Shift}");
            Console.WriteLine($"Waiting move: {game.CurrentPlayer}");
            if (!game.Finished) {
                if (game.Check) {
                    Console.WriteLine("Check!");
                }
            }
            else {
                Console.WriteLine("Checkmate!");
                Console.WriteLine($"Winner: {game.CurrentPlayer}");
            }
        }

        public static void PrintCapturatedPieces(ChessGame game) {
            Console.WriteLine("Capturated pieces: ");
            Console.Write("White: ");
            PrintSet(game.CapturatedPieces(Color.White));
            Console.Write("Black: ");
            PrintSet(game.CapturatedPieces(Color.Black));
        }

        public static void PrintSet(HashSet<Piece> set) {
            Console.Write("[");
            foreach (Piece x in set) {
                Console.Write(x + " ");
            }
            Console.Write("] ");
        }

        public static void PrintPiece(Piece piece) {
            if (piece == null) {
                Console.Write("- ");
            }
            else {
                if (piece.color == Color.White) {
                    Console.Write(piece);
                }
                else {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
