using System;

namespace Ex05.WindowsUI
{
    public class ValidMovesEventArgs : EventArgs
    {
        private PiecePosition m_PiecePosition;

        public PiecePosition PiecePosition
        {
            get { return m_PiecePosition; }
            set { m_PiecePosition = value; }
        }

        public ValidMovesEventArgs(PiecePosition i_PiecePosition)
        {
            PiecePosition = i_PiecePosition;
        }
    }
}
