using System;

namespace Ex05.GameLogic
{
    public class GameOverEventArgs : EventArgs
    {
        private string m_WinningMessage;

        public string WinningMessage
        {
            get { return m_WinningMessage; }
        }

        public GameOverEventArgs(string i_WinningMessage)
        {
            m_WinningMessage = i_WinningMessage;
        }
    }
}
