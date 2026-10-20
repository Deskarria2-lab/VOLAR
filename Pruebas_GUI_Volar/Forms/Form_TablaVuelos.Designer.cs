namespace Pruebas_GUI_Volar.Forms
{
    partial class Form_TablaVuelos
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PosicionInicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PosicionFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Velocidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.PosicionInicial,
            this.PosicionFinal,
            this.Velocidad});
            this.dataGridView1.Location = new System.Drawing.Point(159, 69);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(885, 487);
            this.dataGridView1.TabIndex = 0;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 10;
            this.ID.Name = "ID";
            this.ID.Width = 200;
            // 
            // PosicionInicial
            // 
            this.PosicionInicial.HeaderText = "PosicionInicial";
            this.PosicionInicial.MinimumWidth = 10;
            this.PosicionInicial.Name = "PosicionInicial";
            this.PosicionInicial.Width = 200;
            // 
            // PosicionFinal
            // 
            this.PosicionFinal.HeaderText = "PosicionFinal";
            this.PosicionFinal.MinimumWidth = 10;
            this.PosicionFinal.Name = "PosicionFinal";
            this.PosicionFinal.Width = 200;
            // 
            // Velocidad
            // 
            this.Velocidad.HeaderText = "Velocidad";
            this.Velocidad.MinimumWidth = 10;
            this.Velocidad.Name = "Velocidad";
            this.Velocidad.Width = 200;
            // 
            // Form_TablaVuelos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 696);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form_TablaVuelos";
            this.Text = "Form_TablaVuelos";
            this.Load += new System.EventHandler(this.Form_TablaVuelos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PosicionInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn PosicionFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Velocidad;
    }
}