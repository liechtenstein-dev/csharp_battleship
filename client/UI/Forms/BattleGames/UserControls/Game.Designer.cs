namespace TrabajoPractico.Forms.BattleGames.UserControls
{
    partial class Game
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.attackBoard1 = new TrabajoPractico.Forms.BattleGames.UserControls.AttackBoard();
            this.gameBoard1 = new TrabajoPractico.Forms.BattleGame.GameBoard();
            this.shipSelector1 = new TrabajoPractico.Forms.BattleGames.UserControls.ShipSelector();
            this.buttonAttackAction1 = new TrabajoPractico.Forms.BattleGames.UserControls.ButtonAttackAction();
            this.SuspendLayout();
            // 
            // attackBoard1
            // 
            this.attackBoard1.Location = new System.Drawing.Point(0, 60);
            this.attackBoard1.Name = "attackBoard1";
            this.attackBoard1.Size = new System.Drawing.Size(418, 327);
            this.attackBoard1.TabIndex = 0;
            this.attackBoard1.Visible = false;
            // 
            // gameBoard1
            // 
            this.gameBoard1.Location = new System.Drawing.Point(443, 82);
            this.gameBoard1.Name = "gameBoard1";
            this.gameBoard1.Size = new System.Drawing.Size(418, 305);
            this.gameBoard1.TabIndex = 1;
            // 
            // shipSelector1
            // 
            this.shipSelector1.BackColor = System.Drawing.Color.Transparent;
            this.shipSelector1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.shipSelector1.Location = new System.Drawing.Point(443, 415);
            this.shipSelector1.Name = "shipSelector1";
            this.shipSelector1.Size = new System.Drawing.Size(167, 117);
            this.shipSelector1.TabIndex = 2;
            // 
            // buttonAttackAction1
            // 
            this.buttonAttackAction1.Location = new System.Drawing.Point(13, 415);
            this.buttonAttackAction1.Name = "buttonAttackAction1";
            this.buttonAttackAction1.Size = new System.Drawing.Size(74, 24);
            this.buttonAttackAction1.TabIndex = 3;
            this.buttonAttackAction1.Visible = false;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.buttonAttackAction1);
            this.Controls.Add(this.shipSelector1);
            this.Controls.Add(this.gameBoard1);
            this.Controls.Add(this.attackBoard1);
            this.Name = "Game";
            this.Size = new System.Drawing.Size(879, 577);
            this.Load += new System.EventHandler(this.Game_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AttackBoard attackBoard1;
        private BattleGame.GameBoard gameBoard1;
        private ShipSelector shipSelector1;
        private ButtonAttackAction buttonAttackAction1;
    }
}
