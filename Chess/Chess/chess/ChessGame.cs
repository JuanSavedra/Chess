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
        public bool Check { get; private set; }
        public int Shift { get; private set; }
        public Color CurrentPlayer { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> capturateds;

        public ChessGame() {
            board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            pieces = new HashSet<Piece>();
            capturateds = new HashSet<Piece>();
            InsertPiece();
        }

        public Piece ExecuteMovements(Position origin, Position destine) { 
            Piece p = board.RemovePiece(origin);
            p.IncrementQuantityMovements();
            Piece capturatedPiece = board.RemovePiece(destine);
            board.InsertPiece(p, destine);
            if (capturatedPiece != null) {
                capturateds.Add(capturatedPiece);
            }

            return capturatedPiece;
        }

        public void UndoMovement(Position origin, Position destine, Piece capturatedPiece) {
            Piece piece = board.RemovePiece(destine);
            piece.DecrementQuantityMovements();
            if (capturatedPiece == null) {
                board.InsertPiece(capturatedPiece, destine);
                capturateds.Remove(capturatedPiece);
            }
            board.InsertPiece(piece, origin);
        }

        public void MakePlay(Position origin, Position destine) {
            Piece capturatedPiece = ExecuteMovements(origin, destine);

            if (IsInCheck(CurrentPlayer)) {
                UndoMovement(origin, destine, capturatedPiece);
                throw new BoardException("You can't out yourself in check");
            }

            if (IsInCheck(Adversary(CurrentPlayer))) {
                Check = true;    
            }
            else {
                Check = false;
            }

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

        public void InsertNewPiece(char column, int line, Piece piece) {
            board.InsertPiece(piece, new ChessPosition(column, line).ToPosition());
            pieces.Add(piece);
        }

        public HashSet<Piece> CapturatedPieces(Color color) { 
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in capturateds) {
                if (x.color == color) {
                    aux.Add(x);
                }
            }

            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in capturateds) {
                if (x.color == color) {
                    aux.Add(x);
                }
            }

            aux.ExceptWith(CapturatedPieces(color));
            return aux;
        }

        private Color Adversary(Color color) {
            if (color == Color.White) {
                return Color.Black;
            } 
            else {
                return Color.White;
            }
        }

        private Piece King(Color color) {
            foreach (Piece x in PiecesInGame(color)) { 
                if (x is King) {
                    return x;
                }
            }

            return null;
        }

        public bool IsInCheck(Color color) {
            Piece K = King(color);

            if (K == null) {
                throw new BoardException("Not exists king in this color");
            }

            foreach(Piece x in PiecesInGame(Adversary(color))) {
                bool[,] mat = x.PossibleMovements();
                if (mat[K.position.Line, K.position.Column]) {
                    return true;
                }
            }

            return false;   
        }

        private void InsertPiece() {
            //Peças brancas
            InsertNewPiece('C', 1, new Tower(board, Color.White));
            InsertNewPiece('C', 2, new Tower(board, Color.White));
            InsertNewPiece('D', 1, new King(board, Color.White));
            InsertNewPiece('D', 2, new Tower(board, Color.White));
            InsertNewPiece('E', 1, new Tower(board, Color.White));
            InsertNewPiece('E', 2, new Tower(board, Color.White));

            //Peças pretas
            InsertNewPiece('C', 7, new Tower(board, Color.Black));
            InsertNewPiece('C', 8, new Tower(board, Color.Black));
            InsertNewPiece('D', 7, new Tower(board, Color.Black));
            InsertNewPiece('D', 8, new King(board, Color.Black));
            InsertNewPiece('E', 7, new Tower(board, Color.Black));
            InsertNewPiece('E', 8, new Tower(board, Color.Black));
        }
    }
}
