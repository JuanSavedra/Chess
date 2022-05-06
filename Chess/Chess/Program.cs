using chess;
using Chess.board;
using Chess.board.exceptions;

namespace Chess {
    class Program {
        static void Main(string[] args) {
            try {
                ChessGame game = new ChessGame();

                while (!game.Finished) {
                    Console.Clear();
                    Window.PrintBoard(game.board);

                    Console.Write("\nOrigin: ");
                    Position origin = Window.ReadChessPosition().ToPosition();
                    Console.Write("Destine: ");
                    Position destine = Window.ReadChessPosition().ToPosition();

                    game.ExecuteMovements(origin, destine);
                }

                Window.PrintBoard(game.board);
            }
            catch (BoardException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}