﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05.WindowsUI
{
    public partial class FormGame : Form
    {
        private const int k_PictureBoxDimension = 80;
        private readonly Label labelPlayer1NameAndScore = new Label();
        private readonly Label labelPlayer2NameAndScore = new Label();
        private PictureBoxPlayerPieces pictureBoxPressed = null;
        private PictureBoxPlayerPieces[,] pictureBoxMatrix;
        private readonly FormGameSettings r_FormGameSettings = new FormGameSettings();
        private readonly List<PictureBoxPlayerPieces> r_HighlightedMoves = new List<PictureBoxPlayerPieces>();

        public event EventHandler GameSettingsPassed;
        public event EventHandler MoveExecuted;
        public event EventHandler<ValidMovesEventArgs> GetValidMoves; 

        public FormGame()
        {
            InitializeComponent();
        }

        private void r_FormGameSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing && r_FormGameSettings.DialogResult != DialogResult.OK)
            {
                this.Close();
            }

            pictureBoxMatrix = new PictureBoxPlayerPieces[r_FormGameSettings.BoardSize, r_FormGameSettings.BoardSize];

            initFormGame();
            GameSettingsEventArgs gameSettingsEventArgs = new GameSettingsEventArgs(
                r_FormGameSettings.Player1Name,
                r_FormGameSettings.Player2Name,
                r_FormGameSettings.BoardSize,
                r_FormGameSettings.IsComputer);
            OnGameSettingsPassed(gameSettingsEventArgs);
        }

        protected virtual void OnGameSettingsPassed(GameSettingsEventArgs i_GameSettingsEventArgs)
        {
            if (GameSettingsPassed != null)
            {
                GameSettingsPassed(this, i_GameSettingsEventArgs);
            }
        }

        protected virtual void OnMoveExecuted(MoveExecutedEventArgs i_MoveExecutedEventArgs)
        {
            if (MoveExecuted != null)
            {
                MoveExecuted(this, i_MoveExecutedEventArgs);
            }
        }

        protected virtual void OnValidMovesRequested(ValidMovesEventArgs i_ValidMovesEventArgs)
        {
            if (i_ValidMovesEventArgs != null)
            {
                GetValidMoves(this, i_ValidMovesEventArgs);
            }
        }

        private void initFormGame()
        {
            setPictureBoxTable();
            initPlayerLabels();
            this.Size = new Size(k_PictureBoxDimension * r_FormGameSettings.BoardSize + 40, k_PictureBoxDimension * r_FormGameSettings.BoardSize + 90);
            centerForm();
        }

        private void centerForm()
        {
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }

        private void setPictureBoxTable()
        {
            initializeEmptyBoard();
            placePlayerPieces();
        }

        private void initializeEmptyBoard()
        {
            for (int row = 0; row < r_FormGameSettings.BoardSize; row++)
            {
                for (int col = 0; col < r_FormGameSettings.BoardSize; col++)
                {
                    createBoardTiles(row, col);
                }
            }
        }

        private void createBoardTiles(int i_Row, int i_Col)
        {
            PictureBoxPlayerPieces currentTile = new PictureBoxPlayerPieces(i_Row, i_Col);
            bool isPlayableTile = (i_Row + i_Col) % 2 == 1;

            currentTile.Size = new Size(k_PictureBoxDimension, k_PictureBoxDimension);
            currentTile.Location = new Point(k_PictureBoxDimension * i_Col + 10, k_PictureBoxDimension * i_Row + 40);
            currentTile.SizeMode = PictureBoxSizeMode.StretchImage;
            currentTile.BackgroundImage = isPlayableTile ? Properties.Resources.GreyTile : Properties.Resources.WhiteTile;
            currentTile.Enabled = true;
            currentTile.IsPictureBoxEnabled = false;
            currentTile.Click += pictureBox_Click;

            pictureBoxMatrix[i_Row, i_Col] = currentTile;
            this.Controls.Add(currentTile);
        }

        private void placePlayerPieces()
        {
            int numPieceRows = (r_FormGameSettings.BoardSize - 2) / 2;

            for (int row = 0; row < r_FormGameSettings.BoardSize; row++)
            {
                if (row >= numPieceRows && row < r_FormGameSettings.BoardSize - numPieceRows)
                {
                    continue;
                }

                for (int col = 0; col < r_FormGameSettings.BoardSize; col++)
                {
                    if ((row + col) % 2 == 1)
                    {
                        bool isTopHalf = row < r_FormGameSettings.BoardSize / 2;

                        pictureBoxMatrix[row, col].BackColor = Color.Transparent;
                        pictureBoxMatrix[row, col].BackgroundImage = Properties.Resources.GreyTile;
                        pictureBoxMatrix[row, col].Image = isTopHalf ? Properties.Resources.BlackRegularPiece : Properties.Resources.BlueRegularPiece;
                    }
                }
            }
        }

        public void ResetBoard()
        {
            for (int row = 0; row < r_FormGameSettings.BoardSize; row++)
            {
                for (int col = 0; col < r_FormGameSettings.BoardSize; col++)
                {
                    pictureBoxMatrix[row, col].Image = null;

                    bool isPlayableTile = (row + col) % 2 == 1;

                    pictureBoxMatrix[row, col].BackgroundImage = isPlayableTile ? Properties.Resources.GreyTile : Properties.Resources.WhiteTile;
                }
            }

            pictureBoxPressed = null;
            placePlayerPieces();
        }

        private void disableAllTiles()
        {
            for(int row = 0; row < r_FormGameSettings.BoardSize; row++)
            {
                for(int col = 0; col < r_FormGameSettings.BoardSize; col++)
                {
                    // pictureBoxMatrix[row, col].Enabled = false;
                    pictureBoxMatrix[row, col].IsPictureBoxEnabled = false;
                }
            }
        }

        public void UpdateEnabledPictureBoxes(List<PiecePosition> i_CurrentPlayerPiecesPositions, List<MovePiece> i_PossibleMoves)
        {
            EnableCurrentPlayerPictureBoxes(i_CurrentPlayerPiecesPositions);

            foreach(MovePiece possibleMove in i_PossibleMoves)
            {
                pictureBoxMatrix[possibleMove.ToPosition.Row, possibleMove.ToPosition.Col].Enabled = true;
                pictureBoxMatrix[possibleMove.ToPosition.Row, possibleMove.ToPosition.Col].IsPictureBoxEnabled = true;
                pictureBoxMatrix[possibleMove.ToPosition.Row, possibleMove.ToPosition.Col].BackgroundImage = Properties.Resources.PossibleMovesTile;
                r_HighlightedMoves.Add(pictureBoxMatrix[possibleMove.ToPosition.Row, possibleMove.ToPosition.Col]);
            }
        }

        private void removeHighLightedPossibleMoves()
        {
            foreach (PictureBoxPlayerPieces highLightedBox in r_HighlightedMoves)
            {
                highLightedBox.BackgroundImage = Properties.Resources.GreyTile;
            }

            r_HighlightedMoves.Clear();
        }

        public void EnableCurrentPlayerPictureBoxes(List<PiecePosition> i_PositionsToEnable)
        {
            disableAllTiles();

            foreach(PiecePosition currentPosition in i_PositionsToEnable)
            {
                pictureBoxMatrix[currentPosition.Row, currentPosition.Col].Enabled = true;
                pictureBoxMatrix[currentPosition.Row, currentPosition.Col].IsPictureBoxEnabled = true;
            }
        }

        public void RemovePieceImage(MovePiece i_MovePiece, bool i_IsCapture, Player i_CurrentPlayer)
        {
            Image playerPieceImage = pictureBoxMatrix[i_MovePiece.FromPosition.Row, i_MovePiece.FromPosition.Col].Image;
            bool isKing = isPieceKing(i_MovePiece.ToPosition);

            if (isKing)
            {
                playerPieceImage = getKingPieceImage(i_MovePiece, i_CurrentPlayer);
            }

            pictureBoxMatrix[i_MovePiece.FromPosition.Row, i_MovePiece.FromPosition.Col].Image = null;

            if (i_IsCapture)
            {
                int middleRow = (i_MovePiece.FromPosition.Row + i_MovePiece.ToPosition.Row) / 2;
                int middleCol = (i_MovePiece.FromPosition.Col + i_MovePiece.ToPosition.Col) / 2;
                pictureBoxMatrix[middleRow, middleCol].Image = null;
            }

            pictureBoxMatrix[i_MovePiece.ToPosition.Row, i_MovePiece.ToPosition.Col].Image = playerPieceImage;
        }

        private bool isPieceKing(PiecePosition i_PiecePosition)
        {
            return i_PiecePosition.Row == 0 || i_PiecePosition.Row == r_FormGameSettings.BoardSize - 1;
        }

        private Image getKingPieceImage(MovePiece i_MovePiece, Player i_CurrentPlayer)
        {
            Image playerPieceImage = pictureBoxMatrix[i_MovePiece.FromPosition.Row, i_MovePiece.FromPosition.Col].Image;
            
            if (i_CurrentPlayer.PlayerPiece == (char)Player.ePlayerPieceType.OPlayer || i_CurrentPlayer.PlayerPiece == (char)Player.ePlayerPieceType.OPlayerKing)
            {
                playerPieceImage = Properties.Resources.BlackKingPiece;
            }
            else if(i_CurrentPlayer.PlayerPiece == (char)Player.ePlayerPieceType.XPlayer || i_CurrentPlayer.PlayerPiece == (char)Player.ePlayerPieceType.XPlayerKing)
            {
                playerPieceImage = Properties.Resources.BlueKingPiece;
            }

            return playerPieceImage;
        }

        private void setPlayerLabelsLocation()
        {
            int player1AlignedCell = 1;
            int player2AlignedCell = 4;

            if(r_FormGameSettings.BoardSize == 8)
            {
                player1AlignedCell = 2;
                player2AlignedCell = 5;
            } else if(r_FormGameSettings.BoardSize == 10)
            {
                player1AlignedCell = 3;
                player2AlignedCell = 6;
            }

            PictureBox pictureBoxPlayer1Alignment = pictureBoxMatrix[0, player1AlignedCell];
            PictureBox pictureBoxPlayer2Alignment = pictureBoxMatrix[0, player2AlignedCell];

            labelPlayer1NameAndScore.AutoSize = true;
            labelPlayer2NameAndScore.AutoSize = true;

            labelPlayer1NameAndScore.Location = new Point(pictureBoxPlayer1Alignment.Left - 3, pictureBoxPlayer1Alignment.Top - 30);
            labelPlayer2NameAndScore.Location = new Point(pictureBoxPlayer2Alignment.Left - 3, labelPlayer1NameAndScore.Top);
        }

        private void initPlayerLabels()
        {
            setPlayerLabelsLocation();

            labelPlayer1NameAndScore.Text = $"{r_FormGameSettings.Player1Name}: 0";

            if (r_FormGameSettings.IsComputer)
            {
                r_FormGameSettings.Player2Name = "Computer";
            }

            labelPlayer2NameAndScore.Text = $"{r_FormGameSettings.Player2Name}: 0";

            this.Controls.Add(labelPlayer1NameAndScore);
            this.Controls.Add(labelPlayer2NameAndScore);
        }

        public void UpdatePlayerScoreLabel(int i_Player1Score, int i_Player2Score)
        {
            labelPlayer1NameAndScore.Text = $"{r_FormGameSettings.Player1Name}: {i_Player1Score}";
            labelPlayer2NameAndScore.Text = $"{r_FormGameSettings.Player2Name}: {i_Player2Score}";
        }

        public void HighlightCurrentPlayerLabel(string i_PlayerName)
        {
            if (i_PlayerName == r_FormGameSettings.Player1Name)
            {
                labelPlayer1NameAndScore.ForeColor = Color.Red;
                labelPlayer2NameAndScore.ForeColor = Color.Black;
            }
            else
            {
                labelPlayer1NameAndScore.ForeColor = Color.Black;
                labelPlayer2NameAndScore.ForeColor = Color.Red;
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBoxPlayerPieces clickedPiece = sender as PictureBoxPlayerPieces;
            
            if (clickedPiece == null)
            {
                return;
            }

            if(pictureBoxPressed != null && !clickedPiece.IsPictureBoxEnabled)
            {
                showInvalidMoveMessage();
            }

            if (clickedPiece == pictureBoxPressed)
            {
                deselectCurrentPiece();
            }

            else if(pictureBoxPressed != null && clickedPiece.IsPictureBoxEnabled)
            {
                if(clickedPiece.Image != null)
                {
                    deselectCurrentPiece();
                    selectNewPiece(clickedPiece);
                }
                else
                {
                    MoveExecutedEventArgs moveToExecute = new MoveExecutedEventArgs(pictureBoxPressed.PlayerPiecePosition, clickedPiece.PlayerPiecePosition);
                    OnMoveExecuted(moveToExecute);
                    deselectCurrentPiece();
                }
            }
            else if(pictureBoxPressed == null && clickedPiece.IsPictureBoxEnabled && clickedPiece.Image != null)
            {
                selectNewPiece(clickedPiece);
            }
        }

        private void deselectCurrentPiece()
        {
            removeHighLightedPossibleMoves();

            if (pictureBoxPressed != null)
            {
                pictureBoxPressed.BackgroundImage = Properties.Resources.GreyTile;
                pictureBoxPressed = null;
            }
        }

        private void selectNewPiece(PictureBoxPlayerPieces i_PieceToSelect)
        {
            ValidMovesEventArgs validMoves = new ValidMovesEventArgs(i_PieceToSelect.PlayerPiecePosition);

            OnValidMovesRequested(validMoves);

            pictureBoxPressed = i_PieceToSelect;

            if(pictureBoxPressed != null)
            {
                pictureBoxPressed.BackgroundImage = Properties.Resources.PressedTile;
            }
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            r_FormGameSettings.FormClosed += r_FormGameSettings_FormClosed;
            r_FormGameSettings.ShowDialog();
        }

        public bool ShowGameOverMessage(string i_MessageToDisplay)
        {
            bool isAnswerYes = false;
            DialogResult userAnswer = MessageBox.Show(i_MessageToDisplay, "Game Over!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (userAnswer == DialogResult.Yes)
            {
                isAnswerYes = true;
            }

            return isAnswerYes;
        }

        private void showInvalidMoveMessage()
        {
            MessageBox.Show("You can only move to one of the highlighted tiles!", "Invalid Move", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
