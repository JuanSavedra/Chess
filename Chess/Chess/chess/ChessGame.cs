using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.board;
using Chess.board.exceptions;

namespace chess {
    class ChessGame {
        public Board board { get; private set; }
        public bool Finished { get; private set; }
        public int Shift { get; private set; }
        public Color CurrentPlayer { get; private set; }

        public ChessGame() {
            board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;
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

        public void MakePlay(Position origin, Position destine) {
            ExecuteMovements(origin, destine);
            Shift++;
            ChangePlayer();
        }

        public void ValidOriginPosition(Position pos) {
            if (board.piece(pos) == null) {
                throw new BoardException("Invalid origin position");
            }

            if (CurrentPlayer != board.piece(pos).color) {
                throw new BoardException("This piece is not your");
            }

            if (!board.piece(pos).ExistPossibleMovements()) {
                throw new BoardException("Not have possible movements");
            }
        }

        public void ValidDestinePosition(Position origin, Position destine) {
            if (!board.piece(origin).CanMoveFor(destine)) {
                throw new BoardException("Invalid destine position");
            }
        }

        private void ChangePlayer() {
            if (CurrentPlayer == Color.White) {
                CurrentPlayer = Color.Black;
            }
            else {
                CurrentPlayer = Color.White;
            }
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
