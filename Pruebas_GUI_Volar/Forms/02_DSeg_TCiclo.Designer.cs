namespace Pruebas_GUI_Volar
{
    partial class DSeg_TCiclo
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
            this.Save_Dt = new System.Windows.Forms.Button();
            this.DistBox = new System.Windows.Forms.TextBox();
            this.TimeBox = new System.Windows.Forms.TextBox();
            this.Titulo_Ajustes = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Save_Dt
            // 
            this.Save_Dt.Location = new System.Drawing.Point(49, 232);
            this.Save_Dt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Save_Dt.Name = "Save_Dt";
            this.Save_Dt.Size = new System.Drawing.Size(81, 32);
            this.Save_Dt.TabIndex = 0;
            this.Save_Dt.Text = "Guardar";
            this.Save_Dt.UseVisualStyleBackColor = true;
            this.Save_Dt.Click += new System.EventHandler(this.button1_Click);
            // 
            // DistBox
            // 
            this.DistBox.Location = new System.Drawing.Point(11, 106);
            this.DistBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DistBox.Name = "DistBox";
            this.DistBox.Size = new System.Drawing.Size(82, 20);
            this.DistBox.TabIndex = 1;
            // 
            // TimeBox
            // 
            this.TimeBox.Location = new System.Drawing.Point(11, 176);
            this.TimeBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TimeBox.Name = "TimeBox";
            this.TimeBox.Size = new System.Drawing.Size(82, 20);
            this.TimeBox.TabIndex = 2;
            // 
            // Titulo_Ajustes
            // 
            this.Titulo_Ajustes.AutoSize = true;
            this.Titulo_Ajustes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Titulo_Ajustes.Location = new System.Drawing.Point(45, 26);
            this.Titulo_Ajustes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Titulo_Ajustes.Name = "Titulo_Ajustes";
            this.Titulo_Ajustes.Size = new System.Drawing.Size(129, 37);
            this.Titulo_Ajustes.TabIndex = 3;
            this.Titulo_Ajustes.Text = "Ajustes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Distancia de Seguridad";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 152);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tiempo de Ciclo";
            // 
            // DSeg_TCiclo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(180, 289);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Titulo_Ajustes);
            this.Controls.Add(this.TimeBox);
            this.Controls.Add(this.DistBox);
            this.Controls.Add(this.Save_Dt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "DSeg_TCiclo";
            this.Text = "DSeg_TCiclo";
            this.Load += new System.EventHandler(this.DSeg_TCiclo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Save_Dt;
        private System.Windows.Forms.TextBox DistBox;
        private System.Windows.Forms.TextBox TimeBox;
        private System.Windows.Forms.Label Titulo_Ajustes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}