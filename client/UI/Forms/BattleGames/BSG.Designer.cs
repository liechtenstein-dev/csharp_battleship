namespace TrabajoPractico.Forms.BattleGames
{
    partial class BSG
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
            this.game1 = new TrabajoPractico.Forms.BattleGames.UserControls.Game();
            this.SuspendLayout();
            // 
            // game1
            // 
            this.game1.Location = new System.Drawing.Point(22, 12);
            this.game1.Name = "game1";
            this.game1.Size = new System.Drawing.Size(879, 577);
            this.game1.TabIndex = 0;
            // 
            // BSG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1036, 631);
            this.Controls.Add(this.game1);
            this.Name = "BSG";
            this.Text = "BSG";
            this.Load += new System.EventHandler(this.BSG_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.Game game1;
    }
}