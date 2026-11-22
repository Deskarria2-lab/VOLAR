namespace GUI_VOLAR
{
    partial class _03_Avisos
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
            this.av_text = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // av_text
            // 
            this.av_text.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.av_text.AutoSize = true;
            this.av_text.Location = new System.Drawing.Point(202, 103);
            this.av_text.Name = "av_text";
            this.av_text.Size = new System.Drawing.Size(44, 16);
            this.av_text.TabIndex = 0;
            this.av_text.Text = "label1";
            // 
            // _03_Avisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(368, 186);
            this.Controls.Add(this.av_text);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_03_Avisos";
            this.Text = "_03_Avisos";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label av_text;
    }
}