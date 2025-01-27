namespace Ex05
{
    public class PiecePosition
    {
        private int m_Row;
        private int m_Col;

        public int Row
        {
            get { return m_Row; }
            set { m_Row = value; }
        }

        public int Col
        {
            get { return m_Col; }
            set { m_Col = value; }
        }

        public PiecePosition(int i_Row, int i_Col)
        {
            Row = i_Row;
            Col = i_Col;
        }

        public static bool IsEqualPosition(PiecePosition i_FromPosition, PiecePosition i_ToPosition)
        {
            return i_FromPosition.Row == i_ToPosition.Row && i_FromPosition.Col == i_ToPosition.Col;
        }
    }
}
