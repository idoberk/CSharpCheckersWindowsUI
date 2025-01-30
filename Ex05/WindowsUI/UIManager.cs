using System;
using System.Collections.Generic;

namespace Ex05.WindowsUI
{
    public class UIManager
    {
        // TODO: Check if eBoardSize can become a global enum class to be used in GameLogic and UI.
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

        public void Run()
        {
            r_FormGame.GameSettingsPassed += GameSettingsPassed;
            r_GameManager.NewGameRoundStarted += NewGameRoundStarted;
            r_FormGame.ShowDialog();
        }

        private void GameSettingsPassed(object sender, EventArgs e) 
        {
            GameSettingsEventArgs gameSettings = e as GameSettingsEventArgs;

            r_GameManager.InitGameSettings(gameSettings.Player1Name, gameSettings.Player2Name, gameSettings.BoardSize, gameSettings.IsComputer);
            // r_FormGame.UpdatePlayerScoreLabel(r_GameManager.CurrentPlayer.Score, r_GameManager.NextPlayer.Score);
        }

        private void NewGameRoundStarted(object sender, EventArgs e)
        {
            m_Player1PiecesPositions = r_GameManager.GameBoard.Player1Pieces;
            m_Player2PiecesPositions = r_GameManager.GameBoard.Player2Pieces;
            r_GameManager.ResetGame();
            r_FormGame.DisableOpponentsPictureBoxes(m_Player2PiecesPositions);
        }
    }
}