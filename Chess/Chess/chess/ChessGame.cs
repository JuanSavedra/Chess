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
        public Piece VulnerableEnPassant { get; private set; }

        private HashSet<Piece> pieces;
        private HashSet<Piece> capturateds;

        public ChessGame() {
            board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            VulnerableEnPassant = null;
            pieces = new HashSet<Piece>();
            capturateds = new HashSet<Piece>();
            InsertPiece();
        }

        public Piece ExecuteMovements(Position origin, Position destine) { 
            Piece piece = board.RemovePiece(origin);
            piece.IncrementQuantityMovements();
            Piece capturatedPiece = board.RemovePiece(destine);
            board.InsertPiece(piece, destine);
            if (capturatedPiece != null) {
                capturateds.Add(capturatedPiece);
            }

            //Jogada especial # Roque pequeno
            if (piece is King && destine.Column == origin.Column + 2) {
                Position originTower = new Position(origin.Line, origin.Column + 3);
                Position destineTower = new Position(origin.Line, origin.Column + 1);
                Piece tower = board.RemovePiece(originTower);
                tower.IncrementQuantityMovements();
                board.InsertPiece(tower, destineTower);
            }

            //Jogada especial # Roque grande
            if (piece is King && destine.Column == origin.Column - 2) {
                Position originTower = new Position(origin.Line, origin.Column - 4);
                Position destineTower = new Position(origin.Line, origin.Column - 1);
                Piece tower = board.RemovePiece(originTower);
                tower.IncrementQuantityMovements();
                board.InsertPiece(tower, destineTower);
            }

            //Jogada especial # En passant
            if (piece is Pawn) {
                if (origin.Column != destine.Column && capturatedPiece == null) {
                    Position positionPawn;
                    if (piece.color == Color.White) {
                        positionPawn = new Position(destine.Line + 1, destine.Column);
                    }
                    else {
                        positionPawn = new Position(destine.Line - 1, destine.Column);
                    }
                    capturatedPiece = board.RemovePiece(positionPawn);
                    capturateds.Add(capturatedPiece);
                }
            }

            return capturatedPiece;
        }

        public void UndoMovement(Position origin, Position destine, Piece capturatedPiece) {
            Piece piece = board.RemovePiece(destine);
            piece.DecrementQuantityMovements();
            if (capturatedPiece != null) {
                board.InsertPiece(capturatedPiece, destine);
                capturateds.Remove(capturatedPiece);
            }
            board.InsertPiece(piece, origin);

            //Jogada especial # Roque pequeno
            if (piece is King && destine.Column == origin.Column + 2) {
                Position originTower = new Position(origin.Line, origin.Column + 3);
                Position destineTower = new Position(origin.Line, origin.Column + 1);
                Piece tower = board.RemovePiece(destineTower);
                tower.DecrementQuantityMovements();
                board.InsertPiece(tower, originTower);
            }

            //Jogada especial # Roque grande
            if (piece is King && destine.Column == origin.Column - 2) {
                Position originTower = new Position(origin.Line, origin.Column - 4);
                Position destineTower = new Position(origin.Line, origin.Column - 1);
                Piece tower = board.RemovePiece(destineTower);
                tower.DecrementQuantityMovements();
                board.InsertPiece(tower, originTower);
            }

            //Jogada especial # En passant
            if (piece is Pawn) {
                if (origin.Column != destine.Column && capturatedPiece == VulnerableEnPassant) {
                    Piece pawn = board.RemovePiece(destine);
                    Position positionPawn;
                    if (piece.color == Color.White) {
                        positionPawn = new Position(3, destine.Column);
                    }
                    else {
                        positionPawn = new Position(4, destine.Column);
                    }
                    board.InsertPiece(pawn, positionPawn);
                }
            }   
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

            if (CheckmateTest(Adversary(CurrentPlayer))) {
                Finished = true;
            }
            else {
                Shift++;
                ChangePlayer();
            }

            Piece piece = board.piece(destine);
            
            //Jogada especial # En passant
            if (piece is Pawn && (destine.Line == origin.Line - 2 || destine.Line == origin.Line + 2)) {
                VulnerableEnPassant = piece;
            }
            else {
                VulnerableEnPassant = null;
            }
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
            if (!board.piece(origin).IsPossibleMove(destine)) {
                throw new BoardException("Invalid destine position");
            }
        }

        public void InsertNewPiece(char column, int line, Piece piece) {
            board.InsertPiece(piece, new ChessPosition(column, line).ToPosition());
            pieces.Add(piece);
        }

        public bool CheckmateTest(Color color) {
            if (!IsInCheck(color)) {
                return false;
            }

            foreach(Piece x in PiecesInGame(color)) {
                bool[,] mat = x.PossibleMovements();
                for (int l = 0; l < board.Line; l++) {
                    for (int c = 0; c < board.Line; c++) {
                        if (mat[l, c]) {
                            Position origin = x.position;
                            Position destine = new Position(l, c);
                            Piece capturatedPiece = ExecuteMovements(origin, destine);
                            bool checkTest = IsInCheck(color);
                            UndoMovement(origin, destine, capturatedPiece);
                            if (!IsInCheck(color)) {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
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
            foreach (Piece x in pieces) {
                if (x.color == color) {
                    aux.Add(x);
                }
            }

            aux.ExceptWith(CapturatedPieces(color));
            return aux;
        }

        private void ChangePlayer() {
            if (CurrentPlayer == Color.White) {
                CurrentPlayer = Color.Black;
            }
            else {
                CurrentPlayer = Color.White;
            }
        }

        private Color Adversary(Color color) {
            if (color == Color.White) {
                return Color.Black;
            } 
            else {
                return Color.White;
            }
        }

        private Piece PieceKing(Color color) {
            foreach (Piece x in PiecesInGame(color)) { 
                if (x is King) {
                    return x;
                }
            }

            return null;
        }

        public bool IsInCheck(Color color) {
            Piece K = PieceKing(color);

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
            /* Linha 1 */
            InsertNewPiece('A', 1, new Tower(board, Color.White));
            InsertNewPiece('B', 1, new Horse(board, Color.White));
            InsertNewPiece('C', 1, new Bishop(board, Color.White));
            InsertNewPiece('D', 1, new Queen(board, Color.White));
            InsertNewPiece('E', 1, new King(board, Color.White, this));
            InsertNewPiece('F', 1, new Bishop(board, Color.White));
            InsertNewPiece('G', 1, new Horse(board, Color.White));
            InsertNewPiece('H', 1, new Tower(board, Color.White));
            /* Linha 2 */
            InsertNewPiece('A', 2, new Pawn(board, Color.White));
            InsertNewPiece('B', 2, new Pawn(board, Color.White));
            InsertNewPiece('C', 2, new Pawn(board, Color.White));
            InsertNewPiece('D', 2, new Pawn(board, Color.White));
            InsertNewPiece('E', 2, new Pawn(board, Color.White));
            InsertNewPiece('F', 2, new Pawn(board, Color.White));
            InsertNewPiece('G', 2, new Pawn(board, Color.White));
            InsertNewPiece('H', 2, new Pawn(board, Color.White));

            //Peças pretas
            /* Linha 1 */
            InsertNewPiece('A', 8, new Tower(board, Color.Black));
            InsertNewPiece('B', 8, new Horse(board, Color.Black));
            InsertNewPiece('C', 8, new Bishop(board, Color.Black));
            InsertNewPiece('D', 8, new Queen(board, Color.Black));
            InsertNewPiece('E', 8, new King(board, Color.Black, this));
            InsertNewPiece('F', 8, new Bishop(board, Color.Black));
            InsertNewPiece('G', 8, new Horse(board, Color.Black));
            InsertNewPiece('H', 8, new Tower(board, Color.Black));
            /* Linha 2 */
            InsertNewPiece('A', 7, new Pawn(board, Color.Black));
            InsertNewPiece('B', 7, new Pawn(board, Color.Black));
            InsertNewPiece('C', 7, new Pawn(board, Color.Black));
            InsertNewPiece('D', 7, new Pawn(board, Color.Black));
            InsertNewPiece('E', 7, new Pawn(board, Color.Black));
            InsertNewPiece('F', 7, new Pawn(board, Color.Black));
            InsertNewPiece('G', 7, new Pawn(board, Color.Black));
            InsertNewPiece('H', 7, new Pawn(board, Color.Black));
        }
    }
}
