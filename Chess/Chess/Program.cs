using chess;
using Chess.board;
using Chess.board.exceptions;

namespace Chess {
    class Program {
        static void Main(string[] args) {
            try {
                ChessGame game = new ChessGame();

                while (!game.Finished) {
                    /* Commits que eu pulei
                    - Movimento do rei (FEITO)
                    - Movimento da torre (FEITO)
                    - Testes de possíveis movimentos (FEITO)
                    - Mudando o turno
                    */

                    Console.Clear();
                    Window.PrintBoard(game.board);
                    Console.WriteLine($"\nShift: {game.Shift}");
                    Console.WriteLine($"Waiting move: {game.CurrentPlayer}");

                    Console.Write("\nOrigin: ");
                    Position origin = Window.ReadChessPosition().ToPosition();
                    bool[,] possiblePositions = game.board.piece(origin).PossibleMovements();
                    
                    Console.Clear();

                    Window.PrintBoard(game.board, possiblePositions);
                    Console.Write("\nDestine: ");
                    Position destine = Window.ReadChessPosition().ToPosition();

                    game.MakePlay(origin, destine);
                }

                Window.PrintBoard(game.board);
            }
            catch (BoardException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}