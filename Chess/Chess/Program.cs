using chess;
using Chess.board;
using Chess.board.exceptions;

namespace Chess {
    class Program {
        static void Main(string[] args) {
            try {
                ChessGame game = new ChessGame();

                while (!game.Finished) {
                    try {
                        Console.Clear();
                        Window.PrintGame(game);

                        Console.Write("\nOrigin: ");
                        Position origin = Window.ReadChessPosition().ToPosition();
                        game.ValidOriginPosition(origin);

                        bool[,] possiblePositions = game.board.piece(origin).PossibleMovements();

                        Console.Clear();

                        Window.PrintBoard(game.board, possiblePositions);
                        Console.Write("\nDestine: ");
                        Position destine = Window.ReadChessPosition().ToPosition();
                        game.ValidDestinePosition(origin, destine);

                        game.MakePlay(origin, destine);
                    }
                    catch (BoardException e) { 
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

                Window.PrintBoard(game.board);
            }
            catch (BoardException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}