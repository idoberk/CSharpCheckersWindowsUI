using System.Collections.Generic;
using static Ex05.Player;

namespace Ex05
{
    public class GameBoard
    {
        private readonly char[,] r_Board;
        private readonly int r_BoardSize;
        private List<PiecePosition> m_Player1Pieces;
        private List<PiecePosition> m_Player2Pieces;

        public GameBoard(int i_GameBoardSize)
        {
            r_BoardSize = i_GameBoardSize;
            r_Board = new char[r_BoardSize, r_BoardSize];
            m_Player1Pieces = new List<PiecePosition>();
            m_Player2Pieces = new List<PiecePosition>();
        
            InitializeBoard();
        }

        public int GetBoardSize
        {
            get { return r_BoardSize; }
        }
        
        public List<PiecePosition> Player1Pieces
        {
            get { return m_Player1Pieces; }
            set { m_Player1Pieces = value; }
        }

        public List<PiecePosition> Player2Pieces
        {
            get { return m_Player2Pieces; }
            set { m_Player2Pieces = value; }
        }

        public List<PiecePosition> GetPiecesPositionsList(int i_PlayerNumber)
        {
            return i_PlayerNumber == (int)ePlayerNumber.Player1 ? new List<PiecePosition>(Player1Pieces) : new List<PiecePosition>(Player2Pieces);
        }

        public char GetPieceAtPosition(PiecePosition i_PiecePosition)
        {
            return r_Board[i_PiecePosition.Row, i_PiecePosition.Col];
        }

        public void InitializeBoard()
        {
            int numRows = (r_BoardSize - 2) / 2;

            for(int i = 0; i < numRows; i++)
            {
                for(int j = 0; j < r_BoardSize; j++)
                {
                    if((i + j) % 2 == 1)
                    {
                        r_Board[i, j] = (char)ePlayerPieceType.OPlayer;
                        m_Player1Pieces.Add(new PiecePosition(i, j));
                    } else
                    {
                        r_Board[i, j] = (char)ePlayerPieceType.Empty;
                    }
                }
            }
            
            for(int i = numRows; i <= numRows + 1; i++)
            {
                for(int j = 0; j < r_BoardSize; j++)
                {
                    r_Board[i, j] = (char)ePlayerPieceType.Empty;
                }
            }

            for(int i = r_BoardSize - numRows; i < r_BoardSize; i++)
            {
                for(int j = 0; j < r_BoardSize; j++)
                {
                    if((i + j) % 2 == 1)
                    {
                        r_Board[i, j] = (char)ePlayerPieceType.XPlayer;
                        m_Player2Pieces.Add(new PiecePosition(i, j));
                    } else
                    {
                        r_Board[i, j] = (char)ePlayerPieceType.Empty;
                    }
                }
            }
        }

        public void MovePlayerPiece(MovePiece i_MovePiece)
        {
            char playerPiece = '\0';

            if(i_MovePiece.ToPosition.Row == GetBoardSize - 1 || i_MovePiece.ToPosition.Row == 0)
            {
                makeKing(i_MovePiece.FromPosition);
            }

            playerPiece = GetPieceAtPosition(i_MovePiece.FromPosition);
            r_Board[i_MovePiece.ToPosition.Row, i_MovePiece.ToPosition.Col] = r_Board[i_MovePiece.FromPosition.Row, i_MovePiece.FromPosition.Col];
            r_Board[i_MovePiece.FromPosition.Row, i_MovePiece.FromPosition.Col] = (char)ePlayerPieceType.Empty;
            updatePiecePosition(i_MovePiece.FromPosition, i_MovePiece.ToPosition, playerPiece);
        }

        private void updatePiecePosition(PiecePosition i_FromPosition, PiecePosition i_ToPosition, char i_CurrentPlayerPiece)
        {
            List<PiecePosition> updatedList = i_CurrentPlayerPiece == (char)ePlayerPieceType.OPlayer ||
                i_CurrentPlayerPiece == (char)ePlayerPieceType.OPlayerKing ? m_Player1Pieces : m_Player2Pieces;

            for(int i = 0; i < updatedList.Count; i++)
            {
                if(PiecePosition.IsEqualPosition(updatedList[i], i_FromPosition))
                {
                    updatedList[i] = i_ToPosition;
                    break;
                }
            }
        }

        public void CapturePiece(MovePiece i_MovePiece)
        {
            int middleRow = (i_MovePiece.FromPosition.Row + i_MovePiece.ToPosition.Row) / 2;
            int middleCol = (i_MovePiece.FromPosition.Col + i_MovePiece.ToPosition.Col) / 2;
            char capturedPiece = r_Board[middleRow, middleCol];
            PiecePosition capturedPiecePosition = new PiecePosition(middleRow, middleCol);

            MovePlayerPiece(i_MovePiece);
            removePiecePosition(capturedPiecePosition, capturedPiece);
            r_Board[middleRow, middleCol] = (char)ePlayerPieceType.Empty;
        }

        private void removePiecePosition(PiecePosition i_Position, char i_CurrentPlayerPiece)
        {
            List<PiecePosition> updatedList = i_CurrentPlayerPiece == (char)ePlayerPieceType.OPlayer ||
                i_CurrentPlayerPiece == (char)ePlayerPieceType.OPlayerKing ? m_Player1Pieces : m_Player2Pieces;

            for(int i = updatedList.Count - 1; i >= 0; i--)
            {
                if(PiecePosition.IsEqualPosition(updatedList[i], i_Position))
                {
                    updatedList.RemoveAt(i);
                    break;
                }
            }
        }

        private void makeKing(PiecePosition i_PiecePosition)
        {
            char currentPiece = GetPieceAtPosition(i_PiecePosition);
            if(!IsPieceKing(currentPiece))
            {
                r_Board[i_PiecePosition.Row, i_PiecePosition.Col] =
                currentPiece == (char)ePlayerPieceType.OPlayer
                    ? (char)ePlayerPieceType.OPlayerKing
                    : (char)ePlayerPieceType.XPlayerKing;
            }
        }

        public bool IsValidMove(MovePiece i_MovePiece)
        {
            bool isValid = true;

            if(!isMoveInBoardBoundaries(i_MovePiece.FromPosition, i_MovePiece.ToPosition))
            {
                isValid = false;
            }

            if(isValid && !isPositionAvailable(i_MovePiece.ToPosition))
            {
                isValid = false;
            }

            return isValid;
        }

        private bool isMoveInBoardBoundaries(PiecePosition i_FromPosition, PiecePosition i_ToPosition)
        {
            bool isFromRowMoveValid = i_FromPosition.Row >= 0 && i_FromPosition.Row < GetBoardSize;
            bool isFromColMoveValid = i_FromPosition.Col >= 0 && i_FromPosition.Col < GetBoardSize;
            bool isToRowMoveValid = i_ToPosition.Row >= 0 && i_ToPosition.Row < GetBoardSize;
            bool isToColMoveValid = i_ToPosition.Col >= 0 && i_ToPosition.Col < GetBoardSize;

            return isFromRowMoveValid && isFromColMoveValid && isToRowMoveValid && isToColMoveValid;
        }

        private bool isPositionAvailable(PiecePosition i_ToPosition)
        {
            return GetPieceAtPosition(i_ToPosition) == (char)ePlayerPieceType.Empty;
        }
    }
}
