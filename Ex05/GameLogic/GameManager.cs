using System;
using System.Collections.Generic;
using System.Text;
using Ex05.GameLogic;
using static Ex05.Player;

namespace Ex05
{
    public class GameManager
    {
        public EventHandler NewGameRoundStarted;
        public EventHandler BoardUpdated;
        public EventHandler GameOver;

        private GameBoard m_GameBoard;
        private Player m_Player1;
        private Player m_Player2;
        private Player m_CurrentPlayer;
        private Player m_NextPlayer;
        private readonly List<MovePiece> r_CaptureMoves = new List<MovePiece>();
        private readonly List<MovePiece> r_RegularMoves = new List<MovePiece>();
        private const int k_KingValue = 4;
        private const int k_RegularValue = 1;
        private PiecePosition m_LastMovePosition;
        private bool m_IsTurnFinished;
        private static readonly Random sr_RandomMove = new Random();

        public GameBoard GameBoard
        {
            get { return m_GameBoard; }
        }

        public bool HasCaptureMoves
        {
            get { return r_CaptureMoves.Count > 0; }
        }

        public Player CurrentPlayer
        {
            get { return m_CurrentPlayer; }
            private set { m_CurrentPlayer = value; }
        }

        public Player NextPlayer
        {
            get { return m_NextPlayer; }
            private set { m_NextPlayer = value; }
        }

        public Player Player1
        {
            get { return m_Player1; }
            private set { m_Player1 = value; }
        }

        public Player Player2
        {
            get { return m_Player2; }
            private set { m_Player2 = value; }
        }

        public List<MovePiece> RegularMoves
        {
            get { return r_RegularMoves; }
        }

        public List<MovePiece> CaptureMoves
        {
            get { return r_CaptureMoves; }
        }

        public void InitGameSettings(string i_Player1Name, string i_Player2Name, int i_BoardSize, bool i_IsComputer)
        {
            int player2Type = i_IsComputer ? (int)ePlayerType.Computer : (int)ePlayerType.Human;

            m_Player1 = new Player(i_Player1Name, (char)ePlayerPieceType.OPlayer, ePlayerType.Human, (int)ePlayerNumber.Player1);
            m_Player2 = new Player(i_Player2Name, (char)ePlayerPieceType.XPlayer, (ePlayerType)player2Type, (int)ePlayerNumber.Player2);
            m_GameBoard = new GameBoard(i_BoardSize);

            OnNewGameRoundStarted();
        }

        public bool IsMoveExecuted(MovePiece i_AttemptedMove, out bool i_IsCapture)
        {
            i_IsCapture = true;

            if (m_LastMovePosition != null)
            {
                CaptureMoves.Clear();
                RegularMoves.Clear();
                char lastMovedPiece = GameBoard.GetPieceAtPosition(m_LastMovePosition);

                addMovesToList(m_LastMovePosition, lastMovedPiece, i_IsCapture);
            }

            bool isMoveExecuted = false;

            if (i_AttemptedMove.IsMoveInList(i_AttemptedMove, CaptureMoves))
            {
                performMove(i_AttemptedMove, i_IsCapture);
                isMoveExecuted = true;
            }
            else if (i_AttemptedMove.IsMoveInList(i_AttemptedMove, RegularMoves))
            {
                i_IsCapture = false;
                performMove(i_AttemptedMove, i_IsCapture);
                isMoveExecuted = true;
            }

            return isMoveExecuted;
        }

        public List<MovePiece> GetValidMovesForPiece(PiecePosition i_PiecePosition)
        {
            List<MovePiece> validMovesList = new List<MovePiece>();
            char currentPiece = GameBoard.GetPieceAtPosition(i_PiecePosition);
            bool isCapture = true;

            RegularMoves.Clear();
            CaptureMoves.Clear();
            addMovesToList(i_PiecePosition, currentPiece, isCapture);

            if(HasCaptureMoves)
            {
                validMovesList.AddRange(CaptureMoves);
            }
            else
            {
                isCapture = false;
                addMovesToList(i_PiecePosition, currentPiece, isCapture);
                validMovesList.AddRange(RegularMoves);
            }

            return validMovesList;
        }

        private void getValidMoves()
        {
            List<PiecePosition> currentPlayerPieces = GameBoard.GetPiecesPositionsList(CurrentPlayer.PlayerNumber);
            bool isCapture = true;

            RegularMoves.Clear();
            CaptureMoves.Clear();
            getPossibleMoves(currentPlayerPieces, isCapture);

            if (!HasCaptureMoves)
            {
                isCapture = false;
                getPossibleMoves(currentPlayerPieces, isCapture);
            }
        }

        public void CreateValidMoves()
        {
            getValidMoves();
        }

        private void performMove(MovePiece i_PlayerMove, bool i_IsCapture)
        {
            Player movingPlayer = CurrentPlayer;

            if (i_IsCapture)
            {
                GameBoard.CapturePiece(i_PlayerMove);
                m_LastMovePosition = i_PlayerMove.ToPosition;
                CaptureMoves.Clear();
                addMovesToList(i_PlayerMove.ToPosition, GameBoard.GetPieceAtPosition(m_LastMovePosition), i_IsCapture);

                if (!HasCaptureMoves)
                {
                    m_LastMovePosition = null;
                    m_IsTurnFinished = true;
                }
                else
                {
                    m_IsTurnFinished = false;
                }
            }
            else
            {
                GameBoard.MovePlayerPiece(i_PlayerMove);
                m_LastMovePosition = null;
                m_IsTurnFinished = true;
            }

            if (m_IsTurnFinished)
            {
                switchTurn();
            }

            OnBoardUpdated(new BoardUpdateEventArgs(i_PlayerMove, movingPlayer, i_IsCapture));

            if (isGameOver())
            {
                handleGameOver();
            }

            else if (CurrentPlayer.IsComputer())
            {
                computerMove();
            }
        }

        private void getPossibleMoves(List<PiecePosition> i_CurrentPlayerPieces, bool i_IsCapture)
        {
            foreach (PiecePosition piecePosition in i_CurrentPlayerPieces)
            {
                char piece = GameBoard.GetPieceAtPosition(piecePosition);

                addMovesToList(piecePosition, piece, i_IsCapture);
            }
        }

        private void addMovesToList(PiecePosition i_CurrentPiecePosition, char i_CurrentPiece, bool i_IsCapture)
        {
            int[] moveDirection = getPieceDirection(i_CurrentPiece);
            int moveDistance = i_IsCapture ? 2 : 1;

            foreach (int rowDirection in moveDirection)
            {
                foreach (int colDirection in new[] { -moveDistance, moveDistance })
                {
                    PiecePosition newPosition = new PiecePosition(
                    i_CurrentPiecePosition.Row + rowDirection * moveDistance,
                    i_CurrentPiecePosition.Col + colDirection);
                    MovePiece possibleMove = new MovePiece(i_CurrentPiecePosition, newPosition);

                    if (isValidMove(possibleMove.FromPosition, possibleMove.ToPosition))
                    {
                        bool isCaptureAvailable = this.isCaptureAvailable(possibleMove);

                        if (i_IsCapture && isCaptureAvailable)
                        {
                            CaptureMoves.Add(possibleMove);
                        }
                        else
                        {
                            RegularMoves.Add(possibleMove);
                        }
                    }
                }
            }
        }

        private bool isValidMove(PiecePosition i_FromPosition, PiecePosition i_ToPosition)
        {
            MovePiece moveInput = new MovePiece(i_FromPosition, i_ToPosition);
            bool isMoveValid = isPlayerPiece(i_FromPosition);

            if (isMoveValid && !isValidDirection(i_FromPosition, i_ToPosition))
            {
                isMoveValid = false;
            }

            if (isMoveValid && !isMoveDiagonal(i_FromPosition, i_ToPosition))
            {
                isMoveValid = false;
            }

            if (isMoveValid && !GameBoard.IsValidMove(moveInput))
            {
                isMoveValid = false;
            }

            if (isMoveValid)
            {
                bool isCaptureMove = isCaptureAvailable(moveInput);
                bool isValidDistance = isValidMoveDistance(i_FromPosition, i_ToPosition, isCaptureMove);

                if (!isValidDistance)
                {
                    isMoveValid = false;
                }
            }

            return isMoveValid;
        }

        private bool isPlayerPiece(PiecePosition i_PiecePosition)
        {
            char charAtPosition = GameBoard.GetPieceAtPosition(i_PiecePosition);
            char currentPlayerPiece = CurrentPlayer.PlayerPiece;

            if (IsPieceKing(charAtPosition))
            {
                currentPlayerPiece = currentPlayerPiece == Player1.PlayerPiece ? (char)ePlayerPieceType.OPlayerKing : (char)ePlayerPieceType.XPlayerKing;
            }

            bool isPlayerPiece = charAtPosition == currentPlayerPiece;

            return isPlayerPiece;
        }

        private bool isValidDirection(PiecePosition i_FromPosition, PiecePosition i_ToPosition)
        {
            char currentPlayerPiece = GameBoard.GetPieceAtPosition(i_FromPosition);
            int[] playerDirection = getPieceDirection(currentPlayerPiece);
            bool isValidDirection = false;

            if (IsPieceKing(currentPlayerPiece))
            {
                isValidDirection = true;
            }
            else
            {
                isValidDirection = Math.Sign(i_ToPosition.Row - i_FromPosition.Row) == playerDirection[0];
            }

            return isValidDirection;
        }

        private bool isMoveDiagonal(PiecePosition i_FromPosition, PiecePosition i_ToPosition)
        {
            int rowDifference = Math.Abs(i_ToPosition.Row - i_FromPosition.Row);
            int colDifference = Math.Abs(i_ToPosition.Col - i_FromPosition.Col);
            bool isDiagonal = rowDifference == colDifference;

            return isDiagonal;
        }

        private bool isCaptureAvailable(MovePiece i_MovePiece)
        {
            bool isCapturingMove = false;

            if (isMoveDiagonal(i_MovePiece.FromPosition, i_MovePiece.ToPosition))
            {
                int rowDiff = Math.Abs(i_MovePiece.ToPosition.Row - i_MovePiece.FromPosition.Row);

                if (rowDiff == 2)
                {
                    int middleRow = (i_MovePiece.FromPosition.Row + i_MovePiece.ToPosition.Row) / 2;
                    int middleCol = (i_MovePiece.FromPosition.Col + i_MovePiece.ToPosition.Col) / 2;
                    PiecePosition capturedPosition = new PiecePosition(middleRow, middleCol);
                    char capturedPiece = GameBoard.GetPieceAtPosition(capturedPosition);

                    if (capturedPiece != (char)ePlayerPieceType.Empty
                        && !isPlayerPiece(capturedPosition))
                    {
                        isCapturingMove = true;
                    }
                }
            }

            return isCapturingMove;
        }

        private bool isValidMoveDistance(PiecePosition i_FromPosition, PiecePosition i_ToPosition, bool i_IsCaptureMove)
        {
            bool isValidDistance = false;
            int moveDistance = i_IsCaptureMove ? 2 : Math.Abs(i_ToPosition.Row - i_FromPosition.Row);

            if ((moveDistance == 2 && i_IsCaptureMove) || moveDistance == 1)
            {
                isValidDistance = true;
            }

            return isValidDistance;
        }

        private int[] getPieceDirection(char i_PlayerPiece)
        {
            int[] pieceDirection = null;

            if (IsPieceKing(i_PlayerPiece))
            {
                pieceDirection = new[] { -1, 1 };
            }
            else if (i_PlayerPiece == (char)ePlayerPieceType.OPlayer)
            {
                pieceDirection = new[] { 1 };
            }
            else
            {
                pieceDirection = new[] { -1 };
            }

            return pieceDirection;
        }

        private void switchTurn()
        {
            Player temp = CurrentPlayer;
            CurrentPlayer = NextPlayer;
            NextPlayer = temp;
        }

        private void updateScore()
        {
            int player1Value = calculatePiecesValue(Player1.PlayerNumber);
            int player2Value = calculatePiecesValue(Player2.PlayerNumber);
            int scoreDifference = Math.Abs(player2Value - player1Value);

            CurrentPlayer.Score += scoreDifference;
        }

        private int calculatePiecesValue(int i_PlayerNumber)
        {
            int totalPiecesValue = 0;
            List<PiecePosition> playerPieces = GameBoard.GetPiecesPositionsList(i_PlayerNumber);

            foreach (PiecePosition piecePosition in playerPieces)
            {
                char currentPiece = GameBoard.GetPieceAtPosition(piecePosition);

                if (IsPieceKing(currentPiece))
                {
                    totalPiecesValue += k_KingValue;
                }
                else
                {
                    totalPiecesValue += k_RegularValue;
                }
            }

            return totalPiecesValue;
        }

        private void handleGameOver()
        {
            StringBuilder gameResultMessage = new StringBuilder();

            switchTurn();

            if (isGameOver())
            {
                gameResultMessage.AppendLine("Tie!");
            }
            else
            {
                gameResultMessage.AppendLine($"{CurrentPlayer.Name} Won!");
            }

            updateScore();
            gameResultMessage.AppendLine("Another Round?");
            OnGameOver(new GameOverEventArgs(gameResultMessage.ToString()));
        }

        private bool isGameOver()
        {
            return hasNoRemainingPieces() || hasNoValidMoves();
        }

        private bool hasNoValidMoves()
        {
            getValidMoves();

            return CaptureMoves.Count == 0 && RegularMoves.Count == 0;
        }

        private bool hasNoRemainingPieces()
        {
            List<PiecePosition> playerPiecesList = GameBoard.GetPiecesPositionsList(CurrentPlayer.PlayerNumber);
            bool hasNoPieces = playerPiecesList.Count == 0;

            return hasNoPieces;
        }

        public void ResetGame()
        {
            CurrentPlayer = Player1;
            NextPlayer = Player2;
            CaptureMoves.Clear();
            RegularMoves.Clear();
            GameBoard.Player2Pieces.Clear();
            GameBoard.Player1Pieces.Clear();
            GameBoard.InitializeBoard();
        }

        private void computerMove()
        {
            getValidMoves();
            MovePiece randomMove = null;
            int randomIndex = 0;
            bool isCapture = false;

            if (CaptureMoves.Count > 0)
            {
                randomIndex = sr_RandomMove.Next(CaptureMoves.Count);
                randomMove = CaptureMoves[randomIndex];
                isCapture = true;
            }
            else
            {
                randomIndex = sr_RandomMove.Next(RegularMoves.Count);
                randomMove = RegularMoves[randomIndex];
            }

            performMove(randomMove, isCapture);
        }

        protected virtual void OnNewGameRoundStarted()
        {
            EventArgs e = new EventArgs();

            if (NewGameRoundStarted != null)
            {
                NewGameRoundStarted(this, e);
            }
        }

        protected virtual void OnBoardUpdated(BoardUpdateEventArgs e)
        {
            if(BoardUpdated != null)
            {
                BoardUpdated(this, e);
            }
        }

        protected virtual void OnGameOver(GameOverEventArgs e)
        {
            if (GameOver != null)
            {
                GameOver(this, e);
            }
        }
    }
}