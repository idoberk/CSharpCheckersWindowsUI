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
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.radioButtonSmallBoard = new System.Windows.Forms.RadioButton();
            this.radioButtonMediumBoard = new System.Windows.Forms.RadioButton();
            this.radioButtonLargeBoard = new System.Windows.Forms.RadioButton();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.textboxPlayer1 = new System.Windows.Forms.TextBox();
            this.checkboxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textboxPlayer2 = new System.Windows.Forms.TextBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Location = new System.Drawing.Point(13, 13);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(61, 13);
            this.labelBoardSize.TabIndex = 0;
            this.labelBoardSize.Text = "Board Size:";
            this.labelBoardSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radioButtonSmallBoard
            // 
            this.radioButtonSmallBoard.Checked = true;
            this.radioButtonSmallBoard.FlatAppearance.BorderSize = 0;
            this.radioButtonSmallBoard.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.radioButtonSmallBoard.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radioButtonSmallBoard.Location = new System.Drawing.Point(26, 35);
            this.radioButtonSmallBoard.Name = "radioButtonSmallBoard";
            this.radioButtonSmallBoard.Size = new System.Drawing.Size(48, 17);
            this.radioButtonSmallBoard.TabIndex = 1;
            this.radioButtonSmallBoard.TabStop = true;
            this.radioButtonSmallBoard.Text = "6 x 6";
            this.radioButtonSmallBoard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonSmallBoard.UseVisualStyleBackColor = true;
            // 
            // radioButtonMediumBoard
            // 
            this.radioButtonMediumBoard.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radioButtonMediumBoard.Location = new System.Drawing.Point(80, 35);
            this.radioButtonMediumBoard.Name = "radioButtonMediumBoard";
            this.radioButtonMediumBoard.Size = new System.Drawing.Size(48, 17);
            this.radioButtonMediumBoard.TabIndex = 2;
            this.radioButtonMediumBoard.Text = "8 x 8";
            this.radioButtonMediumBoard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonMediumBoard.UseVisualStyleBackColor = true;
            // 
            // radioButtonLargeBoard
            // 
            this.radioButtonLargeBoard.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radioButtonLargeBoard.Location = new System.Drawing.Point(134, 35);
            this.radioButtonLargeBoard.Name = "radioButtonLargeBoard";
            this.radioButtonLargeBoard.Size = new System.Drawing.Size(60, 17);
            this.radioButtonLargeBoard.TabIndex = 3;
            this.radioButtonLargeBoard.Text = "10 x 10";
            this.radioButtonLargeBoard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonLargeBoard.UseVisualStyleBackColor = true;
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Location = new System.Drawing.Point(13, 60);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(44, 13);
            this.labelPlayers.TabIndex = 4;
            this.labelPlayers.Text = "Players:";
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.Location = new System.Drawing.Point(22, 85);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(50, 20);
            this.labelPlayer1.TabIndex = 5;
            this.labelPlayer1.Text = "Player 1:";
            this.labelPlayer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textboxPlayer1
            // 
            this.textboxPlayer1.Location = new System.Drawing.Point(99, 85);
            this.textboxPlayer1.MaxLength = 12;
            this.textboxPlayer1.Name = "textboxPlayer1";
            this.textboxPlayer1.Size = new System.Drawing.Size(89, 20);
            this.textboxPlayer1.TabIndex = 6;
            // 
            // checkboxPlayer2
            // 
            this.checkboxPlayer2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkboxPlayer2.Location = new System.Drawing.Point(26, 115);
            this.checkboxPlayer2.Name = "checkboxPlayer2";
            this.checkboxPlayer2.Size = new System.Drawing.Size(67, 20);
            this.checkboxPlayer2.TabIndex = 7;
            this.checkboxPlayer2.Text = "Player 2:";
            this.checkboxPlayer2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkboxPlayer2.UseVisualStyleBackColor = true;
            this.checkboxPlayer2.CheckedChanged += new System.EventHandler(this.CheckboxPlayer2_CheckedChanged);
            // 
            // textboxPlayer2
            // 
            this.textboxPlayer2.Enabled = false;
            this.textboxPlayer2.Location = new System.Drawing.Point(99, 115);
            this.textboxPlayer2.MaxLength = 12;
            this.textboxPlayer2.Name = "textboxPlayer2";
            this.textboxPlayer2.Size = new System.Drawing.Size(89, 20);
            this.textboxPlayer2.TabIndex = 8;
            this.textboxPlayer2.Text = "[Computer]";
            // 
            // buttonDone
            // 
            this.buttonDone.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonDone.Location = new System.Drawing.Point(113, 146);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 9;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.ButtonDone_Click);
            // 
            // FormGameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 181);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textboxPlayer2);
            this.Controls.Add(this.checkboxPlayer2);
            this.Controls.Add(this.textboxPlayer1);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.radioButtonLargeBoard);
            this.Controls.Add(this.radioButtonMediumBoard);
            this.Controls.Add(this.radioButtonSmallBoard);
            this.Controls.Add(this.labelBoardSize);
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

        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.RadioButton radioButtonSmallBoard;
        private System.Windows.Forms.RadioButton radioButtonMediumBoard;
        private System.Windows.Forms.RadioButton radioButtonLargeBoard;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.TextBox textboxPlayer1;
        private System.Windows.Forms.CheckBox checkboxPlayer2;
        private System.Windows.Forms.TextBox textboxPlayer2;
        private System.Windows.Forms.Button buttonDone;
    }
}