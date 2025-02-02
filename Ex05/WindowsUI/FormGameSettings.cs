using System.Windows.Forms;
using static Ex05.WindowsUI.UIManager;

namespace Ex05.WindowsUI
{
    public partial class FormGameSettings : Form
    {
        public FormGameSettings()
        {
            InitializeComponent();
        }

        public string Player1Name
        {
            get { return textboxPlayer1.Text; } 
            set { textboxPlayer1.Text = value; }
        }

        public string Player2Name
        {
            get { return textboxPlayer2.Text; }
            set { textboxPlayer2.Text = value; }
        }

        public bool IsComputer
        {
            get { return !checkboxPlayer2.Checked; }
        }

        public int BoardSize
        {
            get
            {
                int size = 0;
                if (radioButtonSmallBoard.Checked)
                {
                    size = (int)eBoardSize.Small;
                }
                else if (radioButtonMediumBoard.Checked)
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
            textboxPlayer2.Enabled = !textboxPlayer2.Enabled;

            this.textboxPlayer2.Text = textboxPlayer2.Enabled ? string.Empty : "[Computer]";
        }

        private void ButtonDone_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textboxPlayer1.Text) || string.IsNullOrEmpty(this.textboxPlayer2.Text))
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