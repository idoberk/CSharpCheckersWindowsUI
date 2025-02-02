using System.Windows.Forms;

namespace Ex05.WindowsUI
{
    public class PictureBoxPlayerPieces : PictureBox
    {
        private bool m_IsPictureBoxEnabled;
        private PiecePosition m_PlayerPiecePosition;

        public PictureBoxPlayerPieces(int i_Row, int i_Col)
        {
            m_PlayerPiecePosition = new PiecePosition(i_Row, i_Col);
        }

        public bool IsPictureBoxEnabled
        {
            get { return m_IsPictureBoxEnabled; }
            set { m_IsPictureBoxEnabled = value; }
        }

        public PiecePosition PlayerPiecePosition
        {
            get { return m_PlayerPiecePosition; }
        }
    }
}