namespace Ex05
{
    public class Player
    {
        private static readonly int sr_MaxPlayerNameLength = 20;
        private string m_PlayerName;
        private char m_PlayerPiece;
        private ePlayerType m_PlayerType;
        private int m_PlayerNumber;
        private string m_LastMove;
        private int m_Score = 0;

        public string Name
        {
            get { return m_PlayerName; }
            set { m_PlayerName = value; }
        }

        public char PlayerPiece
        {
            get { return m_PlayerPiece; }
            set { m_PlayerPiece = value; }
        }

        public ePlayerType PlayerType
        {
            get { return m_PlayerType; }
            set { m_PlayerType = value; }
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public int PlayerNumber
        {
            get { return m_PlayerNumber; }
            set { m_PlayerNumber = value; }
        }

        public string LastMove
        {
            get { return m_LastMove; }
            set { m_LastMove = value; }
        }

        public Player(string i_PlayerName, char i_PlayerPiece, ePlayerType i_PlayerType, int i_PlayerNumber)
        {
            Name = i_PlayerName;
            PlayerPiece = i_PlayerPiece;
            PlayerType = i_PlayerType == ePlayerType.Human ? ePlayerType.Human : ePlayerType.Computer;
            PlayerNumber = i_PlayerNumber;
            Score = 0;
            LastMove = string.Empty;
        }

        public enum ePlayerPieceType
        {
            Empty = ' ',
            OPlayer = 'O',
            OPlayerKing = 'U',
            XPlayer = 'X',
            XPlayerKing = 'K'
        }

        public enum ePlayerType
        {
            Human = 1,
            Computer
        }

        public enum ePlayerNumber
        {
            Player1 = 1,
            Player2
        }

        public static bool IsPlayerNameValid(string i_PlayerName)
        {
            bool validPlayerName = i_PlayerName.Length <= sr_MaxPlayerNameLength && !(i_PlayerName.Contains(" "))
                                   && i_PlayerName != string.Empty;

            return validPlayerName;
        }

        public static bool IsPieceKing(char i_Piece)
        {
            bool isKing = (i_Piece == (char)ePlayerPieceType.OPlayerKing || i_Piece == (char)ePlayerPieceType.XPlayerKing);

            return isKing;
        }

        public bool IsComputer()
        {
            return m_PlayerType == ePlayerType.Computer;
        }
    }
}