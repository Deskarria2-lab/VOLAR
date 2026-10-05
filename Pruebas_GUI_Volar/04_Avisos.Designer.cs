namespace Pruebas_GUI_Volar
{
    partial class AvisosBox
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
            this.titulo = new System.Windows.Forms.Label();
            this.av_text = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titulo
            // 
            this.titulo.AutoSize = true;
            this.titulo.Location = new System.Drawing.Point(109, 35);
            this.titulo.Name = "titulo";
            this.titulo.Size = new System.Drawing.Size(50, 16);
            this.titulo.TabIndex = 0;
            this.titulo.Text = "AVISO:";
            // 
            // av_text
            // 
            this.av_text.AutoSize = true;
            this.av_text.Location = new System.Drawing.Point(109, 142);
            this.av_text.Name = "av_text";
            this.av_text.Size = new System.Drawing.Size(44, 16);
            this.av_text.TabIndex = 1;
            this.av_text.Text = "label2";
            // 
            // AvisosBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(640, 360);
            this.Controls.Add(this.av_text);
            this.Controls.Add(this.titulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AvisosBox";
            this.Text = "Avisos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titulo;
        private System.Windows.Forms.Label av_text;
    }
}