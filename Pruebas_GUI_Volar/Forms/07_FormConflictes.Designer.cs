namespace Pruebas_GUI_Volar
{
    partial class finestraConflictes
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
            this.LabelConflicte = new System.Windows.Forms.Label();
            this.solucionarConflicte = new System.Windows.Forms.Button();
            this.noSolucionarConflicte = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelConflicte
            // 
            this.LabelConflicte.AutoSize = true;
            this.LabelConflicte.Location = new System.Drawing.Point(81, 62);
            this.LabelConflicte.Name = "LabelConflicte";
            this.LabelConflicte.Size = new System.Drawing.Size(327, 20);
            this.LabelConflicte.TabIndex = 0;
            this.LabelConflicte.Text = "S\'ha detectat un conflicte. Vols soucionar-lo?";
            // 
            // solucionarConflicte
            // 
            this.solucionarConflicte.Location = new System.Drawing.Point(62, 154);
            this.solucionarConflicte.Name = "solucionarConflicte";
            this.solucionarConflicte.Size = new System.Drawing.Size(229, 35);
            this.solucionarConflicte.TabIndex = 1;
            this.solucionarConflicte.Text = "Si, soluciona el conflicte";
            this.solucionarConflicte.UseVisualStyleBackColor = true;
            this.solucionarConflicte.Click += new System.EventHandler(this.solucionarConflicte_Click);
            // 
            // noSolucionarConflicte
            // 
            this.noSolucionarConflicte.Location = new System.Drawing.Point(345, 154);
            this.noSolucionarConflicte.Name = "noSolucionarConflicte";
            this.noSolucionarConflicte.Size = new System.Drawing.Size(229, 35);
            this.noSolucionarConflicte.TabIndex = 2;
            this.noSolucionarConflicte.Text = "No, no solucionis el conflicte";
            this.noSolucionarConflicte.UseVisualStyleBackColor = true;
            this.noSolucionarConflicte.Click += new System.EventHandler(this.noSolucionarConflicte_Click);
            // 
            // finestraConflictes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(642, 315);
            this.Controls.Add(this.noSolucionarConflicte);
            this.Controls.Add(this.solucionarConflicte);
            this.Controls.Add(this.LabelConflicte);
            this.Name = "finestraConflictes";
            this.Text = "FormConflictes";
            this.Load += new System.EventHandler(this.FormConflictes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelConflicte;
        private System.Windows.Forms.Button solucionarConflicte;
        private System.Windows.Forms.Button noSolucionarConflicte;
    }
}