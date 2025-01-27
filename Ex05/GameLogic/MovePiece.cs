using System.Collections.Generic;

namespace Ex05
{
    public class MovePiece
    {
        private PiecePosition m_FromPosition;
        private PiecePosition m_ToPosition;

        public PiecePosition FromPosition
        {
            get { return m_FromPosition; }
            set { m_FromPosition = value; }
        }

        public PiecePosition ToPosition
        {
            get { return m_ToPosition; }
            set { m_ToPosition = value; }
        }

        public MovePiece(PiecePosition i_FromPosition, PiecePosition i_ToPosition)
        {
            FromPosition = i_FromPosition;
            ToPosition = i_ToPosition;
        }

        public bool IsMoveInList(MovePiece i_MoveToCheck, List<MovePiece> i_MoveList)
        {
           bool isInList = false;

           foreach(MovePiece currentMove in i_MoveList)
           {
                if(IsEqualMove(currentMove, i_MoveToCheck))
                {
                    isInList = true;
                    break;
                }
           }

           return isInList;
        }

        public static bool IsEqualMove(MovePiece i_FirstMove, MovePiece i_SecondMove)
        {
            return PiecePosition.IsEqualPosition(i_FirstMove.FromPosition, i_SecondMove.FromPosition)
                && PiecePosition.IsEqualPosition(i_FirstMove.ToPosition, i_SecondMove.ToPosition);
        }
    }
}
