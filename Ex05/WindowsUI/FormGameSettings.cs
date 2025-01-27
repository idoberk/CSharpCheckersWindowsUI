using System.Drawing;
using System.Windows.Forms;

namespace Ex05.WindowsUI
{
    public partial class FormGameSettings : Form
    {
        public FormGameSettings()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
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
                    MessageBoxButtons.RetryCancel);
            }
            else
            {
                this.Close();
            }
        }
    }
}