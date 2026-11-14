namespace Pruebas_GUI_Volar
{
    partial class Options
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datosDeVueloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dSegYTCicloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.espacioAereoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcionesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.datosDeVueloToolStripMenuItem,
            this.dSegYTCicloToolStripMenuItem,
            this.espacioAereoToolStripMenuItem});
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(85, 24);
            this.opcionesToolStripMenuItem.Text = "Opciones";
            // 
            // datosDeVueloToolStripMenuItem
            // 
            this.datosDeVueloToolStripMenuItem.Name = "datosDeVueloToolStripMenuItem";
            this.datosDeVueloToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.datosDeVueloToolStripMenuItem.Text = "Datos de Vuelo";
            this.datosDeVueloToolStripMenuItem.Click += new System.EventHandler(this.datosDeVueloToolStripMenuItem_Click);
            // 
            // dSegYTCicloToolStripMenuItem
            // 
            this.dSegYTCicloToolStripMenuItem.Name = "dSegYTCicloToolStripMenuItem";
            this.dSegYTCicloToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.dSegYTCicloToolStripMenuItem.Text = "D.Seg y T.Ciclo";
            this.dSegYTCicloToolStripMenuItem.Click += new System.EventHandler(this.dSegYTCicloToolStripMenuItem_Click);
            // 
            // espacioAereoToolStripMenuItem
            // 
            this.espacioAereoToolStripMenuItem.Name = "espacioAereoToolStripMenuItem";
            this.espacioAereoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.espacioAereoToolStripMenuItem.Text = "Espacio Aereo";
            this.espacioAereoToolStripMenuItem.Click += new System.EventHandler(this.espacioAereoToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Proxy 1", 25.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(220, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(359, 54);
            this.label1.TabIndex = 1;
            this.label1.Text = "VOLAR V1.0.1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Proxy 1", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(211, 286);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(381, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "Version Pruebas GUI V1";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Options";
            this.Text = "Options";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem datosDeVueloToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dSegYTCicloToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem espacioAereoToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

