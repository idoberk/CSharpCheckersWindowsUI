namespace Ex05.WindowsUI
{
    partial class FormGameSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LabelBoardSize = new System.Windows.Forms.Label();
            this.RadioButtonSmallBoard = new System.Windows.Forms.RadioButton();
            this.RadioButtonMediumBoard = new System.Windows.Forms.RadioButton();
            this.RadioButtonLargeBoard = new System.Windows.Forms.RadioButton();
            this.LabelPlayers = new System.Windows.Forms.Label();
            this.LabelPlayer1 = new System.Windows.Forms.Label();
            this.TextboxPlayer1 = new System.Windows.Forms.TextBox();
            this.CheckboxPlayer2 = new System.Windows.Forms.CheckBox();
            this.TextboxPlayer2 = new System.Windows.Forms.TextBox();
            this.ButtonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelBoardSize
            // 
            this.LabelBoardSize.AutoSize = true;
            this.LabelBoardSize.Location = new System.Drawing.Point(13, 13);
            this.LabelBoardSize.Name = "LabelBoardSize";
            this.LabelBoardSize.Size = new System.Drawing.Size(61, 13);
            this.LabelBoardSize.TabIndex = 0;
            this.LabelBoardSize.Text = "Board Size:";
            this.LabelBoardSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RadioButtonSmallBoard
            // 
            this.RadioButtonSmallBoard.Checked = true;
            this.RadioButtonSmallBoard.FlatAppearance.BorderSize = 0;
            this.RadioButtonSmallBoard.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.RadioButtonSmallBoard.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RadioButtonSmallBoard.Location = new System.Drawing.Point(26, 35);
            this.RadioButtonSmallBoard.Name = "RadioButtonSmallBoard";
            this.RadioButtonSmallBoard.Size = new System.Drawing.Size(48, 17);
            this.RadioButtonSmallBoard.TabIndex = 1;
            this.RadioButtonSmallBoard.TabStop = true;
            this.RadioButtonSmallBoard.Text = "6 x 6";
            this.RadioButtonSmallBoard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButtonSmallBoard.UseVisualStyleBackColor = true;
            // 
            // RadioButtonMediumBoard
            // 
            this.RadioButtonMediumBoard.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RadioButtonMediumBoard.Location = new System.Drawing.Point(80, 35);
            this.RadioButtonMediumBoard.Name = "RadioButtonMediumBoard";
            this.RadioButtonMediumBoard.Size = new System.Drawing.Size(48, 17);
            this.RadioButtonMediumBoard.TabIndex = 2;
            this.RadioButtonMediumBoard.Text = "8 x 8";
            this.RadioButtonMediumBoard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButtonMediumBoard.UseVisualStyleBackColor = true;
            // 
            // RadioButtonLargeBoard
            // 
            this.RadioButtonLargeBoard.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RadioButtonLargeBoard.Location = new System.Drawing.Point(134, 35);
            this.RadioButtonLargeBoard.Name = "RadioButtonLargeBoard";
            this.RadioButtonLargeBoard.Size = new System.Drawing.Size(60, 17);
            this.RadioButtonLargeBoard.TabIndex = 3;
            this.RadioButtonLargeBoard.Text = "10 x 10";
            this.RadioButtonLargeBoard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButtonLargeBoard.UseVisualStyleBackColor = true;
            // 
            // LabelPlayers
            // 
            this.LabelPlayers.AutoSize = true;
            this.LabelPlayers.Location = new System.Drawing.Point(13, 60);
            this.LabelPlayers.Name = "LabelPlayers";
            this.LabelPlayers.Size = new System.Drawing.Size(44, 13);
            this.LabelPlayers.TabIndex = 4;
            this.LabelPlayers.Text = "Players:";
            // 
            // LabelPlayer1
            // 
            this.LabelPlayer1.Location = new System.Drawing.Point(22, 85);
            this.LabelPlayer1.Name = "LabelPlayer1";
            this.LabelPlayer1.Size = new System.Drawing.Size(50, 20);
            this.LabelPlayer1.TabIndex = 5;
            this.LabelPlayer1.Text = "Player 1:";
            this.LabelPlayer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextboxPlayer1
            // 
            this.TextboxPlayer1.Location = new System.Drawing.Point(99, 85);
            this.TextboxPlayer1.Name = "TextboxPlayer1";
            this.TextboxPlayer1.Size = new System.Drawing.Size(89, 20);
            this.TextboxPlayer1.TabIndex = 6;
            // 
            // CheckboxPlayer2
            // 
            this.CheckboxPlayer2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CheckboxPlayer2.Location = new System.Drawing.Point(26, 115);
            this.CheckboxPlayer2.Name = "CheckboxPlayer2";
            this.CheckboxPlayer2.Size = new System.Drawing.Size(67, 20);
            this.CheckboxPlayer2.TabIndex = 7;
            this.CheckboxPlayer2.Text = "Player 2:";
            this.CheckboxPlayer2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxPlayer2.UseVisualStyleBackColor = true;
            this.CheckboxPlayer2.CheckedChanged += new System.EventHandler(this.CheckboxPlayer2_CheckedChanged);
            // 
            // TextboxPlayer2
            // 
            this.TextboxPlayer2.Enabled = false;
            this.TextboxPlayer2.Location = new System.Drawing.Point(99, 115);
            this.TextboxPlayer2.Name = "TextboxPlayer2";
            this.TextboxPlayer2.Size = new System.Drawing.Size(89, 20);
            this.TextboxPlayer2.TabIndex = 8;
            this.TextboxPlayer2.Text = "[Computer]";
            // 
            // ButtonDone
            // 
            this.ButtonDone.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonDone.Location = new System.Drawing.Point(113, 146);
            this.ButtonDone.Name = "ButtonDone";
            this.ButtonDone.Size = new System.Drawing.Size(75, 23);
            this.ButtonDone.TabIndex = 9;
            this.ButtonDone.Text = "Done";
            this.ButtonDone.UseVisualStyleBackColor = true;
            this.ButtonDone.Click += new System.EventHandler(this.ButtonDone_Click);
            // 
            // FormGameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 181);
            this.Controls.Add(this.ButtonDone);
            this.Controls.Add(this.TextboxPlayer2);
            this.Controls.Add(this.CheckboxPlayer2);
            this.Controls.Add(this.TextboxPlayer1);
            this.Controls.Add(this.LabelPlayer1);
            this.Controls.Add(this.LabelPlayers);
            this.Controls.Add(this.RadioButtonLargeBoard);
            this.Controls.Add(this.RadioButtonMediumBoard);
            this.Controls.Add(this.RadioButtonSmallBoard);
            this.Controls.Add(this.LabelBoardSize);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGameSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormGameSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelBoardSize;
        private System.Windows.Forms.RadioButton RadioButtonSmallBoard;
        private System.Windows.Forms.RadioButton RadioButtonMediumBoard;
        private System.Windows.Forms.RadioButton RadioButtonLargeBoard;
        private System.Windows.Forms.Label LabelPlayers;
        private System.Windows.Forms.Label LabelPlayer1;
        private System.Windows.Forms.TextBox TextboxPlayer1;
        private System.Windows.Forms.CheckBox CheckboxPlayer2;
        private System.Windows.Forms.TextBox TextboxPlayer2;
        private System.Windows.Forms.Button ButtonDone;
    }
}