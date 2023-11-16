namespace TrabajoPractico.Forms
{
    partial class BattleGameForm
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
            if (disposing && (components != null))
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.shipSelector1 = new TrabajoPractico.Forms.BattleGames.UserControls.ShipSelector();
            this.gameBoard1 = new TrabajoPractico.Forms.BattleGame.GameBoard();
            this.SuspendLayout();
            // 
            // shipSelector1
            // 
            this.shipSelector1.BackColor = System.Drawing.Color.Transparent;
            this.shipSelector1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.shipSelector1.Location = new System.Drawing.Point(575, 510);
            this.shipSelector1.Name = "shipSelector1";
            this.shipSelector1.Size = new System.Drawing.Size(155, 91);
            this.shipSelector1.TabIndex = 3;
            // 
            // gameBoard1
            // 
            this.gameBoard1.BackColor = System.Drawing.Color.Black;
            this.gameBoard1.Location = new System.Drawing.Point(575, 199);
            this.gameBoard1.Name = "gameBoard1";
            this.gameBoard1.Size = new System.Drawing.Size(418, 305);
            this.gameBoard1.TabIndex = 2;
            // 
            // BattleGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(1057, 681);
            this.Controls.Add(this.shipSelector1);
            this.Controls.Add(this.gameBoard1);
            this.Name = "BattleGameForm";
            this.Text = "BattleGame";
            this.Load += new System.EventHandler(this.BattleGame_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private BattleGame.GameBoard gameBoard1;
        private BattleGames.UserControls.ShipSelector shipSelector1;
    }
}