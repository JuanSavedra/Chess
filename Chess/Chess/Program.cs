using chess;
using Chess.board;
using Chess.board.exceptions;

namespace Chess {
    class Program {
        static void Main(string[] args) {
            try {
                //Tabuleiro
                Board board = new Board(8, 8);

                //Peças pretas
                board.InsertPiece(new Tower(board, Color.Black), new Position(0, 0));
                board.InsertPiece(new Tower(board, Color.Black), new Position(1, 3));
                board.InsertPiece(new King(board, Color.Black), new Position(2, 4));

                //Peças brancas
                board.InsertPiece(new Tower(board, Color.White), new Position(3, 5));
                board.InsertPiece(new King(board, Color.White), new Position(2, 3));
                board.InsertPiece(new Tower(board, Color.White), new Position(2, 7));
                Window.printBoard(board);
            }
            catch (BoardException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}