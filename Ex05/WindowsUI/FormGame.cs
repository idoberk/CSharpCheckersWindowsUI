using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05.WindowsUI
{
    public partial class FormGame : Form
    {
        private const int k_PictureBoxDimension = 80;
        private bool m_IsPictureBoxPressed = false;
        private Label m_LabelPlayer1NameAndScore = new Label();
        private Label m_LabelPlayer2NameAndScore = new Label();
        //private PictureBox[,] m_PictureBoxMatrix;
        private PictureBoxPlayerPieces m_PictureBoxPressed = null;
        private PictureBoxPlayerPieces[,] m_PictureBoxMatrix;
        private readonly FormGameSettings r_FormGameSettings = new FormGameSettings();

        public event EventHandler GameSettingsPassed;

        public FormGame()
        {
            InitializeComponent();
            // this.Load += FormGame_Load;
        }

        void r_FormGameSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            //m_PictureBoxMatrix = new PictureBox[r_FormGameSettings.BoardSize, r_FormGameSettings.BoardSize];
            m_PictureBoxMatrix = new PictureBoxPlayerPieces[r_FormGameSettings.BoardSize, r_FormGameSettings.BoardSize];
            setFormGameBoardSize();
            GameSettingsEventArgs gameSettingsEventArgs = new GameSettingsEventArgs(r_FormGameSettings.Player1Name, r_FormGameSettings.Player2Name, r_FormGameSettings.BoardSize, r_FormGameSettings.IsComputer);
            OnGameSettingsPassed(gameSettingsEventArgs);

        }

        protected virtual void OnGameSettingsPassed(GameSettingsEventArgs i_GameSettingsEventArgs)
        {
            if (GameSettingsPassed != null)
            {
                GameSettingsPassed(this, i_GameSettingsEventArgs);
            }
        }

        private void setFormGameBoardSize()
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
            //int numRows = (r_FormGameSettings.BoardSize - 2) / 2;

            //setBoardUpperPart(numRows);
            //setBoardMiddlePart(numRows);
            //setBoardLowerPart(numRows);

            initializeEmptyBoard();
            placePlayerPieces();
        }

        private void initializeEmptyBoard()
        {
            for (int row = 0; row < r_FormGameSettings.BoardSize; row++)
            {
                for (int col = 0; col < r_FormGameSettings.BoardSize; col++)
                {
                    PictureBoxPlayerPieces currentBox = new PictureBoxPlayerPieces(row, col);

                    currentBox.Size = new Size(k_PictureBoxDimension, k_PictureBoxDimension);
                    currentBox.Location = new Point(k_PictureBoxDimension * col + 10, k_PictureBoxDimension * row + 40);
                    currentBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    if ((row + col) % 2 == 0)
                    {
                        currentBox.BackgroundImage = Properties.Resources.WhiteTile;
                        currentBox.Enabled = false;
                        currentBox.IsPictureBoxEnabled = false;
                    }
                    else
                    {
                        currentBox.BackgroundImage = Properties.Resources.GreyTile;
                        currentBox.Enabled = true;
                        currentBox.IsPictureBoxEnabled = true;
                    }

                    m_PictureBoxMatrix[row, col] = currentBox;
                    this.Controls.Add(currentBox);
                }
            }
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
                        PictureBoxPlayerPieces playerPiece = createPlayerPiece(row, col);

                        m_PictureBoxMatrix[row, col].Controls.Add(playerPiece);
                    }
                }
            }
        }

        private PictureBoxPlayerPieces createPlayerPiece(int i_Row, int i_Col)
        {
            PictureBoxPlayerPieces playerPiece = new PictureBoxPlayerPieces(i_Row, i_Col);

            playerPiece.Size = new Size(k_PictureBoxDimension, k_PictureBoxDimension);
            playerPiece.Location = new Point(0, 0);
            playerPiece.SizeMode = PictureBoxSizeMode.StretchImage;
            playerPiece.BackColor = Color.Transparent;
            playerPiece.Enabled = true;
            playerPiece.IsPictureBoxEnabled = true;

            bool isTopHalf = i_Row < r_FormGameSettings.BoardSize / 2;

            playerPiece.Image =
                isTopHalf ? Properties.Resources.BlackRegularPiece : Properties.Resources.BlueRegularPiece;

            playerPiece.Click += pictureBox_Click;

            return playerPiece;
        }

        public void DisableOpponentsPictureBoxes(List<PiecePosition> i_PositionsToDisable)
        {
            foreach (PiecePosition position in i_PositionsToDisable)
            {
                m_PictureBoxMatrix[position.Row, position.Col].IsPictureBoxEnabled = false;
                m_PictureBoxMatrix[position.Row, position.Col].Enabled = false;
            }
        }

        public void EnableCurrentPlayerPictureBoxes(List<PiecePosition> i_PositionToEnable)
        {
            foreach(PiecePosition position in i_PositionToEnable)
            {
                m_PictureBoxMatrix[position.Row, position.Col].IsPictureBoxEnabled = true;
                m_PictureBoxMatrix[position.Row, position.Col].Enabled = true;
            }
        }

        public void RemovePieceImage(List<PiecePosition> i_PositionToRemove)
        {

        }

        //private void setBoardUpperPart(int i_NumberOfRows)
        //{
        //    for(int i = 0; i < i_NumberOfRows; i++)
        //    {
        //        for(int j = 0; j < r_FormGameSettings.BoardSize; j++)
        //        {
        //            //PictureBox currentPictureBox = new PictureBox();
        //            PictureBoxPlayerPieces currentPictureBox = new PictureBoxPlayerPieces(i, j);

        //            //setGameBoardCell(ref currentPictureBox, i, j);
        //            setGameBoardCell(ref currentPictureBox, i, j);

        //            if((i + j) % 2 == 1)
        //            {
        //                setPictureBoxInnerImage(currentPictureBox, i, j);
        //            }

        //            // currentPictureBox.Click += pictureBox_Click;
        //            m_PictureBoxMatrix[i, j] = currentPictureBox;
        //            this.Controls.Add(currentPictureBox);
        //        }
        //    }
        //}

        //private void setBoardMiddlePart(int i_NumberOfRows)
        //{
        //    for(int i = i_NumberOfRows; i <= i_NumberOfRows + 1; i++)
        //    {
        //        for(int j = 0; j < r_FormGameSettings.BoardSize; j++)
        //        {
        //            //PictureBox currentPictureBox = new PictureBox();
        //            PictureBoxPlayerPieces currentPictureBox = new PictureBoxPlayerPieces(i, j);

        //            setGameBoardCell(ref currentPictureBox, i, j);
        //            m_PictureBoxMatrix[i, j] = currentPictureBox;
        //            this.Controls.Add(currentPictureBox);
        //        }
        //    }
        //}

        //private void setBoardLowerPart(int i_NumberOfRows)
        //{
        //    for(int i = r_FormGameSettings.BoardSize - i_NumberOfRows; i < r_FormGameSettings.BoardSize; i++)
        //    {
        //        for(int j = 0; j < r_FormGameSettings.BoardSize; j++)
        //        {
        //            //PictureBox currentPictureBox = new PictureBox();
        //            PictureBoxPlayerPieces currentPictureBox = new PictureBoxPlayerPieces(i, j);

        //            setGameBoardCell(ref currentPictureBox, i, j);

        //            if((i + j) % 2 == 1)
        //            {
        //                setPictureBoxInnerImage(currentPictureBox, i, j);
        //            }

        //            // currentPictureBox.Click += pictureBox_Click;
        //            m_PictureBoxMatrix[i, j] = currentPictureBox;
        //            this.Controls.Add(currentPictureBox);
        //        }
        //    }
        //}

        //private void setGameBoardCell(ref PictureBoxPlayerPieces io_CurrentPictureBox, int i_CurrentRow, int i_CurrentCol)
        //{
        //    io_CurrentPictureBox.Size = new Size(k_PictureBoxDimension, k_PictureBoxDimension);
        //    io_CurrentPictureBox.Location = new Point(k_PictureBoxDimension * i_CurrentCol + 10, k_PictureBoxDimension * i_CurrentRow + 40);
        //    io_CurrentPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        //    io_CurrentPictureBox.BackgroundImage = (i_CurrentRow + i_CurrentCol) % 2 == 0 ? Properties.Resources.WhiteTile : Properties.Resources.GreyTile;
        //    io_CurrentPictureBox.Enabled = false;
        //    io_CurrentPictureBox.IsPictureBoxEnabled = false;
        //    //if (io_CurrentPictureBox.BackgroundImage == Properties.Resources.WhiteTile)
        //    //{
        //    //    io_CurrentPictureBox.Enabled = false;
        //    //    io_CurrentPictureBox.IsPictureBoxEnabled = false;
        //    //}

        //}

        //private void setPictureBoxInnerImage(PictureBoxPlayerPieces i_CurrentPictureBox, int i_CurrentRow, int i_CurrentCol)
        //{
        //    //PictureBox playerPieceImage = new PictureBox();
        //    PictureBoxPlayerPieces playerPieceImage = new PictureBoxPlayerPieces(i_CurrentRow, i_CurrentCol);

        //    playerPieceImage.Size = i_CurrentPictureBox.Size;
        //    playerPieceImage.Location = new Point(0, 0);
        //    playerPieceImage.SizeMode = PictureBoxSizeMode.StretchImage;
        //    playerPieceImage.BackColor = Color.Transparent;
        //    playerPieceImage.Image = i_CurrentRow < r_FormGameSettings.BoardSize / 2 ? Properties.Resources.BlackRegularPiece : Properties.Resources.BlueRegularPiece;
        //    playerPieceImage.Enabled = true;
        //    playerPieceImage.IsPictureBoxEnabled = true;
        //    playerPieceImage.Click += pictureBox_Click;
        //    i_CurrentPictureBox.IsPictureBoxEnabled = true;
        //    i_CurrentPictureBox.Enabled = true;

        //    i_CurrentPictureBox.Controls.Add(playerPieceImage);
        //}

        private void setPlayerLabelsLocation()
        {
            int player1AlignedCell = 1;
            int player2AlignedCell = 4;

            if (r_FormGameSettings.BoardSize == 8)
            {
                player1AlignedCell = 2;
                player2AlignedCell = 5;
            }
            else if (r_FormGameSettings.BoardSize == 10)
            {
                player1AlignedCell = 3;
                player2AlignedCell = 6;
            }

            PictureBox pictureBoxPlayer1Alignment = m_PictureBoxMatrix[0, player1AlignedCell];
            PictureBox pictureBoxPlayer2Alignment = m_PictureBoxMatrix[0, player2AlignedCell];

            m_LabelPlayer1NameAndScore.AutoSize = true;
            m_LabelPlayer2NameAndScore.AutoSize = true;

            m_LabelPlayer1NameAndScore.Location = new Point(pictureBoxPlayer1Alignment.Left - 3, pictureBoxPlayer1Alignment.Top - 30);
            m_LabelPlayer2NameAndScore.Location = new Point(pictureBoxPlayer2Alignment.Left - 3, m_LabelPlayer1NameAndScore.Top);
        }

        private void initPlayerLabels()
        {
            setPlayerLabelsLocation();

            m_LabelPlayer1NameAndScore.Text = $"{r_FormGameSettings.Player1Name}: 0";

            if (r_FormGameSettings.IsComputer)
            {
                r_FormGameSettings.Player2Name = "Computer";
            }

            m_LabelPlayer2NameAndScore.Text = $"{r_FormGameSettings.Player2Name}: 0";

            this.Controls.Add(m_LabelPlayer1NameAndScore);
            this.Controls.Add(m_LabelPlayer2NameAndScore);
        }

        public void UpdatePlayerScoreLabel(int i_Player1Score, int i_Player2Score)
        {
            m_LabelPlayer1NameAndScore.Text = $"{r_FormGameSettings.Player1Name}: {i_Player1Score}";
            m_LabelPlayer2NameAndScore.Text = $"{r_FormGameSettings.Player2Name}: {i_Player2Score}";
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBoxPlayerPieces clickedPiece = sender as PictureBoxPlayerPieces;

            if (clickedPiece == null)
            {
                return;
            }

            if (clickedPiece == m_PictureBoxPressed)
            {
                deselectCurrentPiece();
            }
            else if (m_PictureBoxPressed != null && clickedPiece.IsPictureBoxEnabled)
            {
                deselectCurrentPiece();
                selectNewPiece(clickedPiece);
            }
            else if (m_PictureBoxPressed == null && clickedPiece.IsPictureBoxEnabled)
            {
                selectNewPiece(clickedPiece);
            }
        }

        private void deselectCurrentPiece()
        {
            if (m_PictureBoxPressed != null)
            {
                PictureBoxPlayerPieces parentPictureBox = m_PictureBoxPressed.Parent as PictureBoxPlayerPieces;

                if (parentPictureBox != null)
                {
                    parentPictureBox.BackgroundImage = Properties.Resources.GreyTile;
                }

                m_PictureBoxPressed = null;
            }
        }

        private void selectNewPiece(PictureBoxPlayerPieces i_PieceToSelect)
        {
            m_PictureBoxPressed = i_PieceToSelect;

            PictureBoxPlayerPieces parentPictureBox = i_PieceToSelect.Parent as PictureBoxPlayerPieces;

            if (parentPictureBox != null)
            {
                parentPictureBox.BackgroundImage = Properties.Resources.PressedTile;
            }
        }

        //private void pictureBox_Click(object sender, EventArgs e)
        //{
        //    //PictureBox pictureBoxPressed = sender as PictureBox;
        //    PictureBoxPlayerPieces pictureBoxPressed = sender as PictureBoxPlayerPieces;

        //    if (pictureBoxPressed.IsPictureBoxEnabled)
        //    {
        //        if (m_PictureBoxPressed == null)// && !m_IsPictureBoxPressed)
        //        {
        //            m_PictureBoxPressed = pictureBoxPressed;
        //            //m_IsPictureBoxPressed = true;
        //            pictureBoxPressed.IsPictureBoxEnabled = false;
        //            m_PictureBoxPressed.BackgroundImage = Properties.Resources.PressedTile;
        //        }
        //    }
        //    else if(m_PictureBoxPressed == pictureBoxPressed)
        //    {
        //        m_PictureBoxPressed = null;
        //        //m_IsPictureBoxPressed = false;
        //        pictureBoxPressed.IsPictureBoxEnabled = true;
        //        pictureBoxPressed.BackgroundImage = Properties.Resources.GreyTile;
        //    }
        //}

        private void FormGame_Load(object sender, EventArgs e)
        {
            r_FormGameSettings.FormClosed += r_FormGameSettings_FormClosed;
            r_FormGameSettings.ShowDialog();
        }
    }
}
