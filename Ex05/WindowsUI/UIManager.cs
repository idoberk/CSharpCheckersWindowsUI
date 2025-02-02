using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ex05.GameLogic;

namespace Ex05.WindowsUI
{
    public class UIManager
    {
        public enum eBoardSize
        {
            Small = 6,
            Medium = 8,
            Large = 10
        }

        private List<PiecePosition> m_Player1PiecesPositions = new List<PiecePosition>();
        private List<PiecePosition> m_Player2PiecesPositions = new List<PiecePosition>();

        private readonly FormGame r_FormGame = new FormGame();
        private readonly GameManager r_GameManager = new GameManager();

        public void RunGame()
        {
            subscribeFormRelatedEvents();
            subscribeGameLogicRelatedEvents();
            r_FormGame.ShowDialog();
        }

        private void subscribeFormRelatedEvents()
        {
            r_FormGame.MoveExecuted += ExecuteMove;
            r_FormGame.GameSettingsPassed += GameSettingsPassed;
            r_FormGame.GetValidMoves += ValidMovesRequested;
        }

        private void subscribeGameLogicRelatedEvents()
        {
            r_GameManager.NewGameRoundStarted += NewGameRoundStarted;
            r_GameManager.BoardUpdated += BoardUpdated;
            r_GameManager.GameOver += GameOver;
        }

        private void updateEnabledTiles(List<MovePiece> i_PossibleMoves)
        {
            r_GameManager.CreateValidMoves();

            List<PiecePosition> currentPlayerPieces = r_GameManager.GameBoard.GetPiecesPositionsList(r_GameManager.CurrentPlayer.PlayerNumber);

            r_FormGame.UpdateEnabledPictureBoxes(currentPlayerPieces, i_PossibleMoves);
        }

        private void ValidMovesRequested(object sender, ValidMovesEventArgs e)
        {
            List<MovePiece> validMovesList = r_GameManager.GetValidMovesForPiece(e.PiecePosition);
            updateEnabledTiles(validMovesList);
        }

        private void ExecuteMove(object sender, EventArgs e)
        {
            MoveExecutedEventArgs moveToExecute = e as MoveExecutedEventArgs;
            MovePiece movePiece = new MovePiece(moveToExecute.FromPosition, moveToExecute.ToPosition);
            bool isMoveExecuted = r_GameManager.IsMoveExecuted(movePiece, out bool isCapture);

            if (!isMoveExecuted && isCapture)
            {
                MessageBox.Show("A capture move is available and must be taken!", "Invalid move!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void highlightCurrentPlayer()
        {
            r_FormGame.HighlightCurrentPlayerLabel(r_GameManager.CurrentPlayer.Name);
        }

        private void GameSettingsPassed(object sender, EventArgs e) 
        {
            GameSettingsEventArgs gameSettings = e as GameSettingsEventArgs;

            r_GameManager.InitGameSettings(gameSettings.Player1Name, gameSettings.Player2Name, gameSettings.BoardSize, gameSettings.IsComputer);
        }

        private void NewGameRoundStarted(object sender, EventArgs e)
        {
            int player1Score = r_GameManager.Player1.Score;
            int player2Score = r_GameManager.Player2.Score;

            m_Player1PiecesPositions = r_GameManager.GameBoard.Player1Pieces;
            m_Player2PiecesPositions = r_GameManager.GameBoard.Player2Pieces;
            r_FormGame.ResetBoard();
            r_GameManager.ResetGame();
            r_FormGame.EnableCurrentPlayerPictureBoxes(m_Player1PiecesPositions);
            r_FormGame.UpdatePlayerScoreLabel(player1Score, player2Score);
            highlightCurrentPlayer();
        }

        private void BoardUpdated(object sender, EventArgs e)
        {
            BoardUpdateEventArgs boardUpdate = e as BoardUpdateEventArgs;

            r_FormGame.RemovePieceImage(boardUpdate.LastMove, boardUpdate.IsCapture, boardUpdate.MovingPlayer);

            if(r_GameManager.CurrentPlayer.PlayerNumber == (int)Player.ePlayerNumber.Player1)
            {
                r_FormGame.EnableCurrentPlayerPictureBoxes(m_Player1PiecesPositions);
            }
            else
            {
                r_FormGame.EnableCurrentPlayerPictureBoxes(m_Player2PiecesPositions);
            }

            highlightCurrentPlayer();
        }

        private void GameOver(object sender, EventArgs e)
        {
            GameOverEventArgs gameOver = e as GameOverEventArgs;

            bool isNewGame = r_FormGame.ShowGameOverMessage(gameOver.WinningMessage);

            if (isNewGame)
            {
                r_GameManager.NewGameRoundStarted(sender, e);
            }
            else
            {
                r_FormGame.Close();
            }
        }
    }
}