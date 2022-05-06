using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.board;

namespace chess {
    class ChessGame {
        public Board board { get; private set; }
        public bool Finished { get; private set; }
        private int m_round;
        private Color m_actuallyPlayer;

        public ChessGame() {
            board = new Board(8, 8);
            m_round = 1;
            m_actuallyPlayer = Color.White;
            Finished = false;
            InsertPiece();
        }

        public void ExecuteMovements(Position origin, Position destine) { 
            Piece p = board.RemovePiece(origin);
            p.IncrementQuantityMovements();
            Piece capturatedPiece = board.RemovePiece(destine);
            board.InsertPiece(p, destine);
            //Alterações depois
        }

        private void InsertPiece() {
            //Peças brancas
            board.InsertPiece(new Tower(board, Color.White), new ChessPosition('C', 1).ToPosition());
            board.InsertPiece(new Tower(board, Color.White), new ChessPosition('C', 2).ToPosition());
            board.InsertPiece(new King(board, Color.White), new ChessPosition('D', 1).ToPosition());
            board.InsertPiece(new Tower(board, Color.White), new ChessPosition('D', 2).ToPosition());
            board.InsertPiece(new Tower(board, Color.White), new ChessPosition('E', 1).ToPosition());
            board.InsertPiece(new Tower(board, Color.White), new ChessPosition('E', 2).ToPosition());

            //Peças pretas
            board.InsertPiece(new Tower(board, Color.Black), new ChessPosition('C', 7).ToPosition());
            board.InsertPiece(new Tower(board, Color.Black), new ChessPosition('C', 8).ToPosition());
            board.InsertPiece(new Tower(board, Color.Black), new ChessPosition('D', 7).ToPosition());
            board.InsertPiece(new King(board, Color.Black), new ChessPosition('D', 8).ToPosition());
            board.InsertPiece(new Tower(board, Color.Black), new ChessPosition('E', 7).ToPosition());
            board.InsertPiece(new Tower(board, Color.Black), new ChessPosition('E', 8).ToPosition());
        }
    }
}
