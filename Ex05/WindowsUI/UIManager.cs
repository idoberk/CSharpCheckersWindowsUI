namespace Ex05.WindowsUI
{
    public class UIManager
    {
        // TODO: Check if eBoardSize can become a global enum class to be used in GameLogic and UI.
        public enum eBoardSize
        {
            Small = 6,
            Medium = 8,
            Large = 10
        }

        private readonly FormGame r_FormGame = new FormGame();
        private readonly GameManager r_GameManager = new GameManager();

        public void Run()
        {
            r_FormGame.ShowDialog();
        }
    }
}
