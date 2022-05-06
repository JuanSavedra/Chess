using chess;
using Chess.board;
using Chess.board.exceptions;

namespace Chess {
    class Program {
        static void Main(string[] args) {
            try {
                ChessGame game = new ChessGame();
                Window.printBoard(game.board);
            }
            catch (BoardException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}