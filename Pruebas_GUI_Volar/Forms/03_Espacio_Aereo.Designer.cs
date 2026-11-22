namespace Pruebas_GUI_Volar
{
    partial class _03_Espacio_Aereo
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
            this.components = new System.ComponentModel.Container();
            this.EspAereo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.CicloBut = new System.Windows.Forms.Button();
            this.LoadBut = new System.Windows.Forms.Button();
            this.simulacionTimer = new System.Windows.Forms.Timer(this.components);
            this.IniciarAutoSim = new System.Windows.Forms.Button();
            this.DetenerAutoSim = new System.Windows.Forms.Button();
            this.DataGridBut = new System.Windows.Forms.Button();
            this.Restartbut = new System.Windows.Forms.Button();
            this.CanviVelocitat = new System.Windows.Forms.Button();
            this.GuardarSimulación = new System.Windows.Forms.Button();
            this.Retrocedir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EspAereo
            // 
            this.EspAereo.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.EspAereo.Location = new System.Drawing.Point(592, 78);
            this.EspAereo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EspAereo.Name = "EspAereo";
            this.EspAereo.Size = new System.Drawing.Size(890, 906);
            this.EspAereo.TabIndex = 0;
            this.EspAereo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EspAereo_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(546, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(490, 1194);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "599";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(588, 48);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 20);
            this.label3.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1652, 48);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 20);
            this.label4.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(594, 17);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 32);
            this.label5.TabIndex = 6;
            this.label5.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1630, 25);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 32);
            this.label6.TabIndex = 7;
            this.label6.Text = "599";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Chartreuse;
            this.label7.Location = new System.Drawing.Point(968, 12);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(282, 46);
            this.label7.TabIndex = 8;
            this.label7.Text = "Espacio Aereo";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Chartreuse;
            this.label8.Location = new System.Drawing.Point(158, 25);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(185, 46);
            this.label8.TabIndex = 9;
            this.label8.Text = "Acciones";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CicloBut
            // 
            this.CicloBut.Location = new System.Drawing.Point(56, 255);
            this.CicloBut.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CicloBut.Name = "CicloBut";
            this.CicloBut.Size = new System.Drawing.Size(126, 49);
            this.CicloBut.TabIndex = 11;
            this.CicloBut.Text = "Ciclo Accion";
            this.CicloBut.UseVisualStyleBackColor = true;
            this.CicloBut.Click += new System.EventHandler(this.CicloBut_Click);
            // 
            // LoadBut
            // 
            this.LoadBut.Location = new System.Drawing.Point(56, 138);
            this.LoadBut.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.LoadBut.Name = "LoadBut";
            this.LoadBut.Size = new System.Drawing.Size(236, 49);
            this.LoadBut.TabIndex = 13;
            this.LoadBut.Text = "Cargar Datos Vuelos";
            this.LoadBut.UseVisualStyleBackColor = true;
            this.LoadBut.Click += new System.EventHandler(this.LoadBut_Click);
            // 
            // simulacionTimer
            // 
            this.simulacionTimer.Tick += new System.EventHandler(this.SimulacionTimer_Tick);
            // 
            // IniciarAutoSim
            // 
            this.IniciarAutoSim.Location = new System.Drawing.Point(56, 312);
            this.IniciarAutoSim.Name = "IniciarAutoSim";
            this.IniciarAutoSim.Size = new System.Drawing.Size(236, 49);
            this.IniciarAutoSim.TabIndex = 15;
            this.IniciarAutoSim.Text = "Iniciar/Continuar Simulación";
            this.IniciarAutoSim.UseVisualStyleBackColor = true;
            // 
            // DetenerAutoSim
            // 
            this.DetenerAutoSim.Location = new System.Drawing.Point(56, 368);
            this.DetenerAutoSim.Name = "DetenerAutoSim";
            this.DetenerAutoSim.Size = new System.Drawing.Size(200, 49);
            this.DetenerAutoSim.TabIndex = 16;
            this.DetenerAutoSim.Text = "Detener Simulación";
            this.DetenerAutoSim.UseVisualStyleBackColor = true;
            // 
            // DataGridBut
            // 
            this.DataGridBut.Location = new System.Drawing.Point(56, 425);
            this.DataGridBut.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DataGridBut.Name = "DataGridBut";
            this.DataGridBut.Size = new System.Drawing.Size(184, 49);
            this.DataGridBut.TabIndex = 17;
            this.DataGridBut.Text = "Datos y distancias";
            this.DataGridBut.UseVisualStyleBackColor = true;
            this.DataGridBut.Click += new System.EventHandler(this.DataGridBut_Click);
            // 
            // Restartbut
            // 
            this.Restartbut.Location = new System.Drawing.Point(56, 197);
            this.Restartbut.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Restartbut.Name = "Restartbut";
            this.Restartbut.Size = new System.Drawing.Size(188, 49);
            this.Restartbut.TabIndex = 18;
            this.Restartbut.Text = "Restart Simulation";
            this.Restartbut.UseVisualStyleBackColor = true;
            this.Restartbut.Click += new System.EventHandler(this.Restartbut_Click_1);
            // 
            // CanviVelocitat
            // 
            this.CanviVelocitat.Location = new System.Drawing.Point(56, 482);
            this.CanviVelocitat.Name = "CanviVelocitat";
            this.CanviVelocitat.Size = new System.Drawing.Size(184, 49);
            this.CanviVelocitat.TabIndex = 19;
            this.CanviVelocitat.Text = "Cambiar Velocidad";
            this.CanviVelocitat.UseVisualStyleBackColor = true;
            this.CanviVelocitat.Click += new System.EventHandler(this.CanviVelocitat_Click);
            // 
            // GuardarSimulación
            // 
            this.GuardarSimulación.Location = new System.Drawing.Point(56, 537);
            this.GuardarSimulación.Name = "GuardarSimulación";
            this.GuardarSimulación.Size = new System.Drawing.Size(184, 49);
            this.GuardarSimulación.TabIndex = 20;
            this.GuardarSimulación.Text = "Guardar Simulación";
            this.GuardarSimulación.UseVisualStyleBackColor = true;
            this.GuardarSimulación.Click += new System.EventHandler(this.GuardarSimulación_Click);
            // 
            // Retrocedir
            // 
            this.Retrocedir.Location = new System.Drawing.Point(56, 592);
            this.Retrocedir.Name = "Retrocedir";
            this.Retrocedir.Size = new System.Drawing.Size(184, 49);
            this.Retrocedir.TabIndex = 21;
            this.Retrocedir.Text = "Retroceder";
            this.Retrocedir.UseVisualStyleBackColor = true;
            this.Retrocedir.Click += new System.EventHandler(this.Retrocedir_Click);
            // 
            // _03_Espacio_Aereo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1820, 1050);
            this.Controls.Add(this.Retrocedir);
            this.Controls.Add(this.GuardarSimulación);
            this.Controls.Add(this.CanviVelocitat);
            this.Controls.Add(this.Restartbut);
            this.Controls.Add(this.DataGridBut);
            this.Controls.Add(this.IniciarAutoSim);
            this.Controls.Add(this.DetenerAutoSim);
            this.Controls.Add(this.LoadBut);
            this.Controls.Add(this.CicloBut);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EspAereo);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "_03_Espacio_Aereo";
            this.Text = "_03_Espacio_Aereo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel EspAereo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button CicloBut;
        private System.Windows.Forms.Button LoadBut;
        private System.Windows.Forms.Timer simulacionTimer;
        private System.Windows.Forms.Button IniciarAutoSim;
        private System.Windows.Forms.Button DetenerAutoSim;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button DataGridBut;
        private System.Windows.Forms.Button Restartbut;
        private System.Windows.Forms.Button CanviVelocitat;
        private System.Windows.Forms.Button GuardarSimulación;
        private System.Windows.Forms.Button Retrocedir;
    }
}