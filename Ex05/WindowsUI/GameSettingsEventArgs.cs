using System;

namespace Ex05.WindowsUI
{
    public class GameSettingsEventArgs : EventArgs
    {
        private bool m_IsComputer;
        private int m_BoardSize;
        private string m_Player1Name;
        private string m_Player2Name;

        public GameSettingsEventArgs(string i_Player1Name, string i_Player2Name, int i_BoardSize, bool i_IsComputer)
        {
            m_IsComputer = i_IsComputer;
            m_BoardSize = i_BoardSize;
            m_Player1Name = i_Player1Name;
            m_Player2Name = i_Player2Name;
        }

        public string Player1Name
        {
            get { return m_Player1Name; }
        }

        public string Player2Name
        {
            get { return m_Player2Name; }
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
        }

        public bool IsComputer
        {
            get { return m_IsComputer; }
        }
    }
}
