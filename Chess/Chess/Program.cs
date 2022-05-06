using chess;
using Chess.board;
using Chess.board.exceptions;

namespace Chess {
    class Program {
        static void Main(string[] args) {
            ChessPosition pos = new ChessPosition('C', 7);
            Console.WriteLine(pos);
            Console.WriteLine(pos.ToPosition());

            /*
            try {
                Board board = new Board(8, 8);
                board.InsertPiece(new Tower(board, Color.Black), new Position(0, 0));
                board.InsertPiece(new Tower(board, Color.Black), new Position(1, 3));
                board.InsertPiece(new King(board, Color.Black), new Position(2, 4));
                //board.InsertPiece(new Tower(board, Color.Black), new Position(0, 0)); //Erro (Existent Piece)
                //board.InsertPiece(new Tower(board, Color.Black), new Position(1, 9)); //Erro (Invalid Position)
                Window.printBoard(board);
            }
            catch (BoardException e) {
                Console.WriteLine(e.Message);
            }
            */
        }
    }
}