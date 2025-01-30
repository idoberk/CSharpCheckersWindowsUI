using Ex05.WindowsUI;
using System;
using System.Collections.Generic;
using static Ex05.Player;

namespace Ex05
{
    // TODO: Check encapsulation.
    // TODO: Remove all console related code.
    public class GameManager
    {
        public EventHandler NewGameRoundStarted;

        private GameBoard m_GameBoard;
        //private Player m_Player1;
        //private Player m_Player2;
        private Player m_CurrentPlayer;
        private Player m_NextPlayer;
        private List<MovePiece> m_CaptureMoves = new List<MovePiece>();
        private List<MovePiece> m_RegularMoves = new List<MovePiece>();
        private const int k_KingValue = 4;
        private const int k_RegularValue = 1;
        private PiecePosition m_LastMovePosition;
        private bool m_IsTurnFinished;
        private bool m_GameOver;
        private static readonly Random sr_RandomMove = new Random();

        public GameBoard GameBoard
        {
            get { return m_GameBoard; }
        }

        public bool HasCaptureMoves
        {
            get { return m_CaptureMoves.Count > 0; }
        }

        public Player CurrentPlayer
        {
            get { return m_CurrentPlayer; }
        }

        public Player NextPlayer
        {
            get { return m_NextPlayer; }
        }

        public List<MovePiece> CaptureMoves
        {
            get { return m_CaptureMoves; }
        }

        //public void StartGame()
        //{
        //    string playerInput = string.Empty;
        //    m_IsTurnFinished = false;
        //    bool isGameFinished = false;

        //    m_GameOver = false;
        //////    WelcomeMessage();

        //////    string player1Name = GetPlayerName();

        //////m_Player1 = new Player(player1Name, (char) ePlayerPieceType.OPlayer, ePlayerType.Human, 1);
        //////m_CurrentPlayer = m_Player1;
        //////    m_GameBoard = new GameBoard(GetBoardSize());

        //////    int player2Type = GetPlayerType();
        //////    string player2Name = player2Type == 1 ? GetPlayerName() : "Computer";

        //////    m_Player2 = new Player(player2Name, (char)ePlayerPieceType.XPlayer, (ePlayerType)player2Type, 2);
        //////    m_NextPlayer = m_Player2;
        //    DisplayGame(m_GameBoard);
        //    DisplayCurrentPlayerTurn(m_CurrentPlayer);
        //    while (!isGameFinished)
        //    {
        //        if (playerInput == "Q" || playerInput == "q" || m_GameOver)
        //        {
        //            m_GameOver = false;
        //            UpdateScore();
        //            DisplayPlayerScores(m_Player1, m_Player2);

        //            if (!IsAnotherGame())
        //            {
        //                isGameFinished = true;
        //            }
        //            else
        //            {
        //                playerInput = string.Empty;
        //                ClearScreen();
        //                resetGame();
        //                DisplayGame(m_GameBoard);
        //                DisplayCurrentPlayerTurn(m_CurrentPlayer);
        //            }
        //        }
        //        else
        //        {
        //            m_IsTurnFinished = false;

        //            while (!m_IsTurnFinished)
        //            {
        //                if (IsGameOver())
        //                {
        //                    m_GameOver = true;
        //                    DisplayWinnerMessage(m_NextPlayer);
        //                    break;
        //                }
        //                if (m_CurrentPlayer.IsComputer())
        //                {
        //                    computerMove();
        //                }
        //                else
        //                {
        //                    playerInput = GetPlayerInput();

        //                    if (playerInput == "Q" || playerInput == "q")
        //                    {
        //                        m_IsTurnFinished = true;
        //                        DisplayWinnerMessage(m_NextPlayer);
        //                        break;
        //                    }

        //                    playerMove(playerInput);
        //                }
        //            }
        //        }
        //    }

        public void InitGameSettings(string i_Player1Name, string i_Player2Name, int i_BoardSize, bool i_IsComputer)
        {
            int player2Type = i_IsComputer ? (int)ePlayerType.Computer : (int)ePlayerType.Human;

            m_CurrentPlayer = new Player(i_Player1Name, (char)ePlayerPieceType.OPlayer, ePlayerType.Human, (int)ePlayerNumber.Player1);
            m_NextPlayer = new Player(i_Player2Name, (char)ePlayerPieceType.XPlayer, (ePlayerType)player2Type, (int)ePlayerNumber.Player2);
            m_GameBoard = new GameBoard(i_BoardSize);

            OnNewGameRoundStarted();
        }

        public bool IsMoveExecuted(MovePiece i_AttemptedMove)
        {
            getValidMoves();

            if (m_LastMovePosition != null)
            {
                m_CaptureMoves.Clear();
                m_RegularMoves.Clear();
                char lastMovedPiece = m_GameBoard.GetPieceAtPosition(m_LastMovePosition);

                addMovesToList(m_LastMovePosition, lastMovedPiece, i_IsCapture: true);
            }

            bool isMoveExecuted = false;

            if (i_AttemptedMove.IsMoveInList(i_AttemptedMove, m_CaptureMoves))
            {
                PerformMove(i_AttemptedMove, i_IsCapture: true);
                isMoveExecuted = true;
            }
            else if (i_AttemptedMove.IsMoveInList(i_AttemptedMove, m_RegularMoves))
            {
                PerformMove(i_AttemptedMove, i_IsCapture: false);
                isMoveExecuted = true;
            }

            return isMoveExecuted;
        }

        private void getValidMoves()
        {
            List<PiecePosition> currentPlayerPieces = m_GameBoard.GetPiecesPositionsList(m_CurrentPlayer.PlayerNumber);
            bool isCapture = true;

            m_RegularMoves.Clear();
            m_CaptureMoves.Clear();
            getPossibleMoves(currentPlayerPieces, isCapture);

            if (m_CaptureMoves.Count == 0)
            {
                isCapture = false;
                getPossibleMoves(currentPlayerPieces, isCapture);
            }
        }

        public void PerformMove(MovePiece i_PlayerMove, bool i_IsCapture)
        {
            if (i_IsCapture)
            {
                m_GameBoard.CapturePiece(i_PlayerMove);
                m_LastMovePosition = i_PlayerMove.ToPosition;
                m_CaptureMoves.Clear();
                addMovesToList(i_PlayerMove.ToPosition, m_GameBoard.GetPieceAtPosition(m_LastMovePosition), i_IsCapture);
                // UpdateLastMove(parseMoveToString(i_PlayerMove));

                if (m_CaptureMoves.Count == 0)
                {
                    m_LastMovePosition = null;
                    m_IsTurnFinished = true;
                }
            }
            else
            {
                m_GameBoard.MovePlayerPiece(i_PlayerMove);
                m_LastMovePosition = null;
                // UpdateLastMove(parseMoveToString(i_PlayerMove));
                m_IsTurnFinished = true;
            }

            if (m_IsTurnFinished)
            {
                switchTurn();
            }
        }

        //public void UpdateLastMove(string i_PlayerMoveInput)
        //{
        //    if (m_CurrentPlayer == m_Player1)
        //    {
        //        m_Player1.LastMove = i_PlayerMoveInput;
        //    }
        //    else
        //    {
        //        m_Player2.LastMove = i_PlayerMoveInput;
        //    }
        //}

        private string parseMoveToString(MovePiece i_PlayedMove)
        {
            string playedMove = string.Empty;
            char fromRow = (char)('A' + i_PlayedMove.FromPosition.Row);
            char fromCol = (char)('a' + i_PlayedMove.FromPosition.Col);
            char toRow = (char)('A' + i_PlayedMove.ToPosition.Row);
            char toCol = (char)('a' + i_PlayedMove.ToPosition.Col);

            playedMove = $"{fromRow}{fromCol}>{toRow}{toCol}";

            return playedMove;
        }

        private void getPossibleMoves(List<PiecePosition> i_CurrentPlayerPieces, bool i_IsCapture)
        {
            foreach (PiecePosition piecePosition in i_CurrentPlayerPieces)
            {
                char piece = m_GameBoard.GetPieceAtPosition(piecePosition);

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
                        bool isCaptureAvailable = IsCaptureAvailable(possibleMove);

                        if (i_IsCapture && isCaptureAvailable)
                        {
                            m_CaptureMoves.Add(possibleMove);
                        }
                        else
                        {
                            m_RegularMoves.Add(possibleMove);
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

            if (isMoveValid && !m_GameBoard.IsValidMove(moveInput))
            {
                isMoveValid = false;
            }

            if (isMoveValid)
            {
                bool isCaptureMove = IsCaptureAvailable(moveInput);
                bool isValidDistance = IsValidMoveDistance(i_FromPosition, i_ToPosition, isCaptureMove);

                if (!isValidDistance)
                {
                    isMoveValid = false;
                }
            }

            return isMoveValid;
        }

        private bool isPlayerPiece(PiecePosition i_PiecePosition)
        {
            char charAtPosition = m_GameBoard.GetPieceAtPosition(i_PiecePosition);
            char currentPlayerPiece = m_CurrentPlayer.PlayerPiece;

            if (IsPieceKing(charAtPosition))
            {
                //currentPlayerPiece = currentPlayerPiece == m_Player1.PlayerPiece ? (char)ePlayerPieceType.OPlayerKing : (char)ePlayerPieceType.XPlayerKing;
                currentPlayerPiece = currentPlayerPiece == CurrentPlayer.PlayerPiece ? (char)ePlayerPieceType.OPlayerKing : (char)ePlayerPieceType.XPlayerKing;
            }

            bool isPlayerPiece = charAtPosition == currentPlayerPiece;

            return isPlayerPiece;
        }

        private bool isValidDirection(PiecePosition i_FromPosition, PiecePosition i_ToPosition)
        {
            char currentPlayerPiece = m_GameBoard.GetPieceAtPosition(i_FromPosition);
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

        public bool IsCaptureAvailable(MovePiece i_MovePiece)
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
                    char capturedPiece = m_GameBoard.GetPieceAtPosition(capturedPosition);

                    if (capturedPiece != (char)ePlayerPieceType.Empty
                        && !isPlayerPiece(capturedPosition))
                    {
                        isCapturingMove = true;
                    }
                }
            }

            return isCapturingMove;
        }

        public bool IsValidMoveDistance(PiecePosition i_FromPosition, PiecePosition i_ToPosition, bool i_IsCaptureMove)
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
            Player temp = m_CurrentPlayer;
            m_CurrentPlayer = m_NextPlayer;
            m_NextPlayer = temp;
        }

        public void UpdateScore()
        {
            //int player1Value = calculatePiecesValue(m_Player1.PlayerNumber);
            //int player2Value = calculatePiecesValue(m_Player2.PlayerNumber);
            int player1Value = calculatePiecesValue(CurrentPlayer.PlayerNumber);
            int player2Value = calculatePiecesValue(NextPlayer.PlayerNumber);
            int scoreDifference = Math.Abs(player2Value - player1Value);

            if (m_CurrentPlayer != CurrentPlayer)//m_Player1)
            {
                CurrentPlayer.Score += scoreDifference;
            }
            else
            {
                NextPlayer.Score += scoreDifference;
            }
        }

        private int calculatePiecesValue(int i_PlayerNumber)
        {
            int totalPiecesValue = 0;
            List<PiecePosition> playerPieces = m_GameBoard.GetPiecesPositionsList(i_PlayerNumber);

            foreach (PiecePosition piecePosition in playerPieces)
            {
                char currentPiece = m_GameBoard.GetPieceAtPosition(piecePosition);

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

        public bool IsGameOver()
        {
            return hasNoRemainingPieces() || hasNoValidMoves();
        }

        private bool hasNoValidMoves()
        {
            getValidMoves();

            return m_CaptureMoves.Count == 0 && m_RegularMoves.Count == 0;
        }

        private bool hasNoRemainingPieces()
        {
            List<PiecePosition> playerPiecesList = m_GameBoard.GetPiecesPositionsList(m_CurrentPlayer.PlayerNumber);
            bool hasNoPieces = playerPiecesList.Count == 0;

            return hasNoPieces;
        }

        public void ResetGame()
        {
            //    m_CurrentPlayer = m_Player1;
            if (CurrentPlayer.PlayerNumber == (int)ePlayerNumber.Player2)
            {
                switchTurn();
            }

            // m_NextPlayer = m_Player2;
            m_CaptureMoves.Clear();
            m_RegularMoves.Clear();
            //m_Player1.LastMove = string.Empty;
            //m_Player2.LastMove = string.Empty;
            m_GameBoard.Player2Pieces.Clear();
            m_GameBoard.Player1Pieces.Clear();
            m_GameBoard.InitializeBoard();
        }

        private void computerMove()
        {
            getValidMoves();
            MovePiece randomMove = null;
            int randomIndex = 0;
            bool isCapture = false;

            if (m_CaptureMoves.Count > 0)
            {
                randomIndex = sr_RandomMove.Next(m_CaptureMoves.Count);
                randomMove = m_CaptureMoves[randomIndex];
                isCapture = true;
            }
            else
            {
                randomIndex = sr_RandomMove.Next(m_RegularMoves.Count);
                randomMove = m_RegularMoves[randomIndex];
            }

            string playerMoveInput = parseMoveToString(randomMove);
            PerformMove(randomMove, isCapture);
        }

        private void playerMove(string i_PlayerMoveInput)
        {
            if (true)
            {
                //if (!IsMoveExecuted(moveAttempt))
                //{
                    
                //}
            }
            else
            {
                
            }
        }

        protected virtual void OnNewGameRoundStarted()
        {
            EventArgs e = new EventArgs();

            if (NewGameRoundStarted != null)
            {
                NewGameRoundStarted(this, e);
            }
        }
    }
}
