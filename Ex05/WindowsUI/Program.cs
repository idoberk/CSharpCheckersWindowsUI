using System.Windows.Forms;

namespace Ex05.WindowsUI
{
    public class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            UIManager checkers = new UIManager();
            checkers.RunGame();
        }
    }
}