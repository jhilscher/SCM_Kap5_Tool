﻿namespace ToolFahrrad_v1
{
    partial class Einstellungen
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_abweichung = new System.Windows.Forms.TabPage();
            this.lbl_10 = new System.Windows.Forms.Label();
            this.trackBarAbweichung = new System.Windows.Forms.TrackBar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_ok = new System.Windows.Forms.Button();
            this.lbl_50 = new System.Windows.Forms.Label();
            this.lbl_100 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tab_abweichung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAbweichung)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_abweichung);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(356, 265);
            this.tabControl1.TabIndex = 0;
            // 
            // tab_abweichung
            // 
            this.tab_abweichung.BackColor = System.Drawing.Color.Transparent;
            this.tab_abweichung.Controls.Add(this.lbl_100);
            this.tab_abweichung.Controls.Add(this.lbl_50);
            this.tab_abweichung.Controls.Add(this.btn_ok);
            this.tab_abweichung.Controls.Add(this.lbl_10);
            this.tab_abweichung.Controls.Add(this.trackBarAbweichung);
            this.tab_abweichung.Location = new System.Drawing.Point(4, 22);
            this.tab_abweichung.Name = "tab_abweichung";
            this.tab_abweichung.Padding = new System.Windows.Forms.Padding(3);
            this.tab_abweichung.Size = new System.Drawing.Size(348, 239);
            this.tab_abweichung.TabIndex = 0;
            this.tab_abweichung.Text = "Abweichung";
            // 
            // lbl_10
            // 
            this.lbl_10.AutoSize = true;
            this.lbl_10.BackColor = System.Drawing.Color.Transparent;
            this.lbl_10.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_10.Location = new System.Drawing.Point(11, 51);
            this.lbl_10.Name = "lbl_10";
            this.lbl_10.Size = new System.Drawing.Size(19, 13);
            this.lbl_10.TabIndex = 1;
            this.lbl_10.Text = "10";
            // 
            // trackBarAbweichung
            // 
            this.trackBarAbweichung.BackColor = System.Drawing.SystemColors.Control;
            this.trackBarAbweichung.Location = new System.Drawing.Point(6, 19);
            this.trackBarAbweichung.Name = "trackBarAbweichung";
            this.trackBarAbweichung.Size = new System.Drawing.Size(336, 45);
            this.trackBarAbweichung.TabIndex = 0;
            this.trackBarAbweichung.Value = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(348, 239);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(267, 96);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "Speichern";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // lbl_50
            // 
            this.lbl_50.AutoSize = true;
            this.lbl_50.BackColor = System.Drawing.Color.Transparent;
            this.lbl_50.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_50.Location = new System.Drawing.Point(165, 51);
            this.lbl_50.Name = "lbl_50";
            this.lbl_50.Size = new System.Drawing.Size(19, 13);
            this.lbl_50.TabIndex = 3;
            this.lbl_50.Text = "50";
            // 
            // lbl_100
            // 
            this.lbl_100.AutoSize = true;
            this.lbl_100.BackColor = System.Drawing.Color.Transparent;
            this.lbl_100.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_100.Location = new System.Drawing.Point(317, 51);
            this.lbl_100.Name = "lbl_100";
            this.lbl_100.Size = new System.Drawing.Size(25, 13);
            this.lbl_100.TabIndex = 4;
            this.lbl_100.Text = "100";
            // 
            // Einstellungen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 289);
            this.Controls.Add(this.tabControl1);
            this.Name = "Einstellungen";
            this.Text = "Einstellungen";
            this.tabControl1.ResumeLayout(false);
            this.tab_abweichung.ResumeLayout(false);
            this.tab_abweichung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAbweichung)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_abweichung;
        private System.Windows.Forms.Label lbl_10;
        private System.Windows.Forms.TrackBar trackBarAbweichung;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Label lbl_50;
        private System.Windows.Forms.Label lbl_100;
    }
}