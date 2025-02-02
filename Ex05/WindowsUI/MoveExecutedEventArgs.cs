using System;

namespace Ex05.WindowsUI
{
    public class MoveExecutedEventArgs : EventArgs
    {
        private PiecePosition m_FromPosition;
        private PiecePosition m_ToPosition;

        public MoveExecutedEventArgs(PiecePosition i_FromPosition, PiecePosition i_ToPosition)
        {
            m_FromPosition = new PiecePosition(i_FromPosition.Row, i_FromPosition.Col);
            m_ToPosition = new PiecePosition(i_ToPosition.Row, i_ToPosition.Col);
        }

        public PiecePosition FromPosition
        {
            get { return m_FromPosition; }
        }

        public PiecePosition ToPosition
        {
            get { return m_ToPosition; }
        }

        //private MovePiece m_MoveAttempt;

        //public MoveExecutedEventArgs(MovePiece i_MoveAttempt)
        //{
        //    m_MoveAttempt = new MovePiece(i_MoveAttempt.FromPosition, i_MoveAttempt.ToPosition);
        //}

        //public MovePiece MoveAttempt
        //{
        //    get { return m_MoveAttempt; }
        //}
    }
}
