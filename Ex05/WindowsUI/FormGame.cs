using System.Windows.Forms;
using System.Drawing;
using System;

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

        public FormGame()
        {
            InitializeComponent();
            this.Load += FormGame_Load;
        }

        void r_FormGameSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            //m_PictureBoxMatrix = new PictureBox[r_FormGameSettings.BoardSize, r_FormGameSettings.BoardSize];
            m_PictureBoxMatrix = new PictureBoxPlayerPieces[r_FormGameSettings.BoardSize, r_FormGameSettings.BoardSize];
            setFormGameBoardSize();
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
            int numRows = (r_FormGameSettings.BoardSize - 2) / 2;

            setBoardUpperPart(numRows);
            setBoardMiddlePart(numRows);
            setBoardLowerPart(numRows);
        }

        private void setBoardUpperPart(int i_NumberOfRows)
        {
            for(int i = 0; i < i_NumberOfRows; i++)
            {
                for(int j = 0; j < r_FormGameSettings.BoardSize; j++)
                {
                    //PictureBox currentPictureBox = new PictureBox();
                    PictureBoxPlayerPieces currentPictureBox = new PictureBoxPlayerPieces(i, j);

                    //setGameBoardCell(ref currentPictureBox, i, j);
                    setGameBoardCell(ref currentPictureBox, i, j);

                    if((i + j) % 2 == 1)
                    {
                        setPictureBoxInnerImage(currentPictureBox, i, j);
                    }

                    // currentPictureBox.Click += pictureBox_Click;
                    m_PictureBoxMatrix[i, j] = currentPictureBox;
                    this.Controls.Add(currentPictureBox);
                }
            }
        }

        private void setBoardMiddlePart(int i_NumberOfRows)
        {
            for(int i = i_NumberOfRows; i <= i_NumberOfRows + 1; i++)
            {
                for(int j = 0; j < r_FormGameSettings.BoardSize; j++)
                {
                    //PictureBox currentPictureBox = new PictureBox();
                    PictureBoxPlayerPieces currentPictureBox = new PictureBoxPlayerPieces(i, j);

                    setGameBoardCell(ref currentPictureBox, i, j);
                    m_PictureBoxMatrix[i, j] = currentPictureBox;
                    this.Controls.Add(currentPictureBox);
                }
            }
        }

        private void setBoardLowerPart(int i_NumberOfRows)
        {
            for(int i = r_FormGameSettings.BoardSize - i_NumberOfRows; i < r_FormGameSettings.BoardSize; i++)
            {
                for(int j = 0; j < r_FormGameSettings.BoardSize; j++)
                {
                    //PictureBox currentPictureBox = new PictureBox();
                    PictureBoxPlayerPieces currentPictureBox = new PictureBoxPlayerPieces(i, j);

                    setGameBoardCell(ref currentPictureBox, i, j);

                    if((i + j) % 2 == 1)
                    {
                        setPictureBoxInnerImage(currentPictureBox, i, j);
                    }

                    // currentPictureBox.Click += pictureBox_Click;
                    m_PictureBoxMatrix[i, j] = currentPictureBox;
                    this.Controls.Add(currentPictureBox);
                }
            }
        }

        private void setGameBoardCell(ref PictureBoxPlayerPieces io_CurrentPictureBox, int i_CurrentRow, int i_CurrentCol)
        {
            io_CurrentPictureBox.Size = new Size(k_PictureBoxDimension, k_PictureBoxDimension);
            io_CurrentPictureBox.Location = new Point(k_PictureBoxDimension * i_CurrentCol + 10, k_PictureBoxDimension * i_CurrentRow + 40);
            io_CurrentPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            io_CurrentPictureBox.BackgroundImage = (i_CurrentRow + i_CurrentCol) % 2 == 0 ? Properties.Resources.WhiteTile : Properties.Resources.GreyTile;

            if (io_CurrentPictureBox.BackgroundImage == Properties.Resources.WhiteTile)
            {
                io_CurrentPictureBox.Enabled = false;
                io_CurrentPictureBox.IsPictureBoxEnabled = false;
            }
            
        }

        private void setPictureBoxInnerImage(PictureBoxPlayerPieces i_CurrentPictureBox, int i_CurrentRow, int i_CurrentCol)
        {
            //PictureBox playerPieceImage = new PictureBox();
            PictureBoxPlayerPieces playerPieceImage = new PictureBoxPlayerPieces(i_CurrentRow, i_CurrentCol);

            playerPieceImage.Size = i_CurrentPictureBox.Size;
            playerPieceImage.Location = new Point(0, 0);
            playerPieceImage.SizeMode = PictureBoxSizeMode.StretchImage;
            playerPieceImage.BackColor = Color.Transparent;
            playerPieceImage.Image = i_CurrentRow < r_FormGameSettings.BoardSize / 2 ? Properties.Resources.BlackRegularPiece : Properties.Resources.BlueRegularPiece;
            playerPieceImage.Enabled = true;
            playerPieceImage.IsPictureBoxEnabled = true;
            playerPieceImage.Click += pictureBox_Click;
            i_CurrentPictureBox.IsPictureBoxEnabled = true;

            i_CurrentPictureBox.Controls.Add(playerPieceImage);
        }

        private void initPlayerLabels()
        {
            int player1AlignedCell = 1;
            int player2AlignedCell = 4;

            if(r_FormGameSettings.BoardSize == 8)
            {
                player1AlignedCell = 2;
                player2AlignedCell = 5;
            }
            else if(r_FormGameSettings.BoardSize == 10)
            {
                player1AlignedCell = 3;
                player2AlignedCell = 6;
            }

            PictureBox pictureBoxPlayer1Alignment = m_PictureBoxMatrix[0, player1AlignedCell];
            PictureBox pictureBoxPlayer2Alignment = m_PictureBoxMatrix[0, player2AlignedCell];

            m_LabelPlayer1NameAndScore.Text = $"{r_FormGameSettings.Player1Name}: 0"; //{};//Score}";
            m_LabelPlayer1NameAndScore.AutoSize = true;
            m_LabelPlayer2NameAndScore.Text = $"{r_FormGameSettings.Player2Name}: 0"; //{};//Score}";
            m_LabelPlayer2NameAndScore.AutoSize = true;
            m_LabelPlayer1NameAndScore.Location = new Point(pictureBoxPlayer1Alignment.Left - 3, pictureBoxPlayer1Alignment.Top - 30);
            m_LabelPlayer2NameAndScore.Location = new Point(pictureBoxPlayer2Alignment.Left - 3, pictureBoxPlayer2Alignment.Top - 30);

            m_LabelPlayer1NameAndScore.Font = new Font("Arial", 12, FontStyle.Bold);
            m_LabelPlayer2NameAndScore.Font = new Font("Arial", 12, FontStyle.Bold);

            this.Controls.Add(m_LabelPlayer1NameAndScore);
            this.Controls.Add(m_LabelPlayer2NameAndScore);
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            //PictureBox pictureBoxPressed = sender as PictureBox;
            PictureBoxPlayerPieces pictureBoxPressed = sender as PictureBoxPlayerPieces;

            if (pictureBoxPressed.IsPictureBoxEnabled)
            {
                if (m_PictureBoxPressed == null)// && !m_IsPictureBoxPressed)
                {
                    m_PictureBoxPressed = pictureBoxPressed;
                    //m_IsPictureBoxPressed = true;
                    pictureBoxPressed.IsPictureBoxEnabled = false;
                    m_PictureBoxPressed.BackgroundImage = Properties.Resources.PressedTile;
                }
            }
            else if(m_PictureBoxPressed == pictureBoxPressed)
            {
                m_PictureBoxPressed = null;
                //m_IsPictureBoxPressed = false;
                pictureBoxPressed.IsPictureBoxEnabled = true;
                pictureBoxPressed.BackgroundImage = Properties.Resources.GreyTile;
            }
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            r_FormGameSettings.FormClosed += r_FormGameSettings_FormClosed;
            r_FormGameSettings.ShowDialog();
        }
    }
}
