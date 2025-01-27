using System.Drawing;
using System.Windows.Forms;
using static Ex05.WindowsUI.UIManager;

namespace Ex05.WindowsUI
{
    public partial class FormGameSettings : Form
    {
        public FormGameSettings()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
        }

        public string Player1Name
        {
            get { return TextboxPlayer1.Text; } 
            set { TextboxPlayer1.Text = value; }
        }

        public string Player2Name
        {
            get { return TextboxPlayer2.Text; }
            set { TextboxPlayer2.Text = value; }
        }

        public int BoardSize
        {
            get
            {
                int size = 0;
                if (RadioButtonSmallBoard.Checked)
                {
                    size = (int)eBoardSize.Small;
                }
                else if (RadioButtonMediumBoard.Checked)
                {
                    size = (int)eBoardSize.Medium;
                }
                else
                {
                    size = (int)eBoardSize.Large;
                }

                return size;
            }
        }

        private void CheckboxPlayer2_CheckedChanged(object sender, System.EventArgs e)
        {
            TextboxPlayer2.Enabled = !TextboxPlayer2.Enabled;

            this.TextboxPlayer2.Text = TextboxPlayer2.Enabled ? string.Empty : "[Computer]";
        }

        private void ButtonDone_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TextboxPlayer1.Text) || string.IsNullOrEmpty(this.TextboxPlayer2.Text))
            {
                MessageBox.Show(
                    "All fields must be filled!",
                    "Empty fields found",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.Close();
            }
        }
    }
}