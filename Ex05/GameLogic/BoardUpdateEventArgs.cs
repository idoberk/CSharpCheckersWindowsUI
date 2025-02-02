using System;

namespace Ex05.GameLogic
{
    public class BoardUpdateEventArgs : EventArgs
    {
        private MovePiece m_LastMove;
        private Player m_MovingPlayer;
        private bool m_IsCapture;

        public MovePiece LastMove
        {
            get { return m_LastMove; }
        }

        public Player MovingPlayer
        {
            get { return m_MovingPlayer; }
        }

        public bool IsCapture
        {
            get { return m_IsCapture; }
        }

        public BoardUpdateEventArgs(MovePiece i_MovePiece, Player i_MovingPlayer, bool i_IsCapture)
        {
            m_LastMove = i_MovePiece;
            m_MovingPlayer = i_MovingPlayer;
            m_IsCapture = i_IsCapture;
        }
    }
}
