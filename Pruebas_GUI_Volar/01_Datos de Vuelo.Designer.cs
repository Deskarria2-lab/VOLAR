namespace Pruebas_GUI_Volar
{
    partial class Datos_de_Vuelo
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Vuelo A");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Vuelo B");
            this.label1 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.XoBox = new System.Windows.Forms.TextBox();
            this.YoBox = new System.Windows.Forms.TextBox();
            this.XfBox = new System.Windows.Forms.TextBox();
            this.YfBox = new System.Windows.Forms.TextBox();
            this.IdBox = new System.Windows.Forms.TextBox();
            this.VelBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tituloVuelos = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1178, 208);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Font = new System.Drawing.Font("Proxy 1", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "flight_A";
            treeNode1.Text = "Vuelo A";
            treeNode2.Name = "flight_B";
            treeNode2.Text = "Vuelo B";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.treeView1.Size = new System.Drawing.Size(210, 578);
            this.treeView1.TabIndex = 2;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // XoBox
            // 
            this.XoBox.Location = new System.Drawing.Point(389, 292);
            this.XoBox.Name = "XoBox";
            this.XoBox.Size = new System.Drawing.Size(100, 27);
            this.XoBox.TabIndex = 3;
            // 
            // YoBox
            // 
            this.YoBox.Location = new System.Drawing.Point(631, 292);
            this.YoBox.Name = "YoBox";
            this.YoBox.Size = new System.Drawing.Size(100, 27);
            this.YoBox.TabIndex = 4;
            // 
            // XfBox
            // 
            this.XfBox.Location = new System.Drawing.Point(389, 453);
            this.XfBox.Name = "XfBox";
            this.XfBox.Size = new System.Drawing.Size(100, 27);
            this.XfBox.TabIndex = 5;
            // 
            // YfBox
            // 
            this.YfBox.Location = new System.Drawing.Point(631, 453);
            this.YfBox.Name = "YfBox";
            this.YfBox.Size = new System.Drawing.Size(100, 27);
            this.YfBox.TabIndex = 6;
            // 
            // IdBox
            // 
            this.IdBox.Location = new System.Drawing.Point(389, 136);
            this.IdBox.Name = "IdBox";
            this.IdBox.Size = new System.Drawing.Size(100, 27);
            this.IdBox.TabIndex = 7;
            // 
            // VelBox
            // 
            this.VelBox.Location = new System.Drawing.Point(631, 136);
            this.VelBox.Name = "VelBox";
            this.VelBox.Size = new System.Drawing.Size(100, 27);
            this.VelBox.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(355, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(522, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Velocidad";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(352, 299);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "X:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(330, 460);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 20);
            this.label5.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(572, 299);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 20);
            this.label6.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(572, 460);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 20);
            this.label7.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(232, 221);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "Posicion Inicial";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(234, 409);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "Posicion Final";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(513, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 20);
            this.label10.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(352, 460);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 20);
            this.label11.TabIndex = 18;
            this.label11.Text = "X:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(600, 299);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 20);
            this.label12.TabIndex = 19;
            this.label12.Text = "Y:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(600, 460);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 20);
            this.label13.TabIndex = 20;
            this.label13.Text = "Y:";
            // 
            // tituloVuelos
            // 
            this.tituloVuelos.AutoSize = true;
            this.tituloVuelos.Location = new System.Drawing.Point(492, 35);
            this.tituloVuelos.Name = "tituloVuelos";
            this.tituloVuelos.Size = new System.Drawing.Size(0, 20);
            this.tituloVuelos.TabIndex = 21;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(764, 505);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(108, 44);
            this.saveButton.TabIndex = 22;
            this.saveButton.Text = "Guardar";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // Datos_de_Vuelo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(930, 578);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.tituloVuelos);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.VelBox);
            this.Controls.Add(this.IdBox);
            this.Controls.Add(this.YfBox);
            this.Controls.Add(this.XfBox);
            this.Controls.Add(this.YoBox);
            this.Controls.Add(this.XoBox);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Datos_de_Vuelo";
            this.Text = "Datos_de_Vuelo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox XoBox;
        private System.Windows.Forms.TextBox YoBox;
        private System.Windows.Forms.TextBox XfBox;
        private System.Windows.Forms.TextBox YfBox;
        private System.Windows.Forms.TextBox IdBox;
        private System.Windows.Forms.TextBox VelBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label tituloVuelos;
        private System.Windows.Forms.Button saveButton;
    }
}