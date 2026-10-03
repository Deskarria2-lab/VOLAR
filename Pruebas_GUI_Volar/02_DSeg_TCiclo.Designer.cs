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
            this.Save_Dt.Location = new System.Drawing.Point(65, 286);
            this.Save_Dt.Name = "Save_Dt";
            this.Save_Dt.Size = new System.Drawing.Size(108, 39);
            this.Save_Dt.TabIndex = 0;
            this.Save_Dt.Text = "Guardar";
            this.Save_Dt.UseVisualStyleBackColor = true;
            this.Save_Dt.Click += new System.EventHandler(this.button1_Click);
            // 
            // DistBox
            // 
            this.DistBox.Location = new System.Drawing.Point(15, 130);
            this.DistBox.Name = "DistBox";
            this.DistBox.Size = new System.Drawing.Size(108, 22);
            this.DistBox.TabIndex = 1;
            // 
            // TimeBox
            // 
            this.TimeBox.Location = new System.Drawing.Point(15, 216);
            this.TimeBox.Name = "TimeBox";
            this.TimeBox.Size = new System.Drawing.Size(108, 22);
            this.TimeBox.TabIndex = 2;
            // 
            // Titulo_Ajustes
            // 
            this.Titulo_Ajustes.AutoSize = true;
            this.Titulo_Ajustes.Font = new System.Drawing.Font("Proxy 1", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Titulo_Ajustes.Location = new System.Drawing.Point(60, 32);
            this.Titulo_Ajustes.Name = "Titulo_Ajustes";
            this.Titulo_Ajustes.Size = new System.Drawing.Size(115, 25);
            this.Titulo_Ajustes.TabIndex = 3;
            this.Titulo_Ajustes.Text = "Ajustes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Proxy 1", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Distancia de Seguridad";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Proxy 1", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tiempo de Ciclo";
            // 
            // DSeg_TCiclo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(240, 356);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Titulo_Ajustes);
            this.Controls.Add(this.TimeBox);
            this.Controls.Add(this.DistBox);
            this.Controls.Add(this.Save_Dt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DSeg_TCiclo";
            this.Text = "DSeg_TCiclo";
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