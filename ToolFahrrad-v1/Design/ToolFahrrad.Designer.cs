namespace ToolFahrrad_v1
{
    partial class Fahrrad
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.Tabs = new System.Windows.Forms.TabControl();
            this.xmlInput = new System.Windows.Forms.TabPage();
            this.pfadText = new System.Windows.Forms.Label();
            this.xml_suchen = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.Tabs.SuspendLayout();
            this.xmlInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.xmlInput);
            this.Tabs.Controls.Add(this.tabPage2);
            this.Tabs.Location = new System.Drawing.Point(-4, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(814, 437);
            this.Tabs.TabIndex = 0;
            // 
            // xmlInput
            // 
            this.xmlInput.BackColor = System.Drawing.Color.Transparent;
            this.xmlInput.Controls.Add(this.pfadText);
            this.xmlInput.Controls.Add(this.xml_suchen);
            this.xmlInput.Location = new System.Drawing.Point(4, 22);
            this.xmlInput.Name = "xmlInput";
            this.xmlInput.Padding = new System.Windows.Forms.Padding(3);
            this.xmlInput.Size = new System.Drawing.Size(806, 411);
            this.xmlInput.TabIndex = 0;
            this.xmlInput.Text = "XML laden";
            // 
            // pfadText
            // 
            this.pfadText.AutoSize = true;
            this.pfadText.Location = new System.Drawing.Point(147, 25);
            this.pfadText.MinimumSize = new System.Drawing.Size(145, 0);
            this.pfadText.Name = "pfadText";
            this.pfadText.Size = new System.Drawing.Size(145, 13);
            this.pfadText.TabIndex = 2;
            // 
            // xml_suchen
            // 
            this.xml_suchen.Location = new System.Drawing.Point(29, 19);
            this.xml_suchen.Name = "xml_suchen";
            this.xml_suchen.Size = new System.Drawing.Size(75, 23);
            this.xml_suchen.TabIndex = 1;
            this.xml_suchen.Text = "XML öffnen";
            this.xml_suchen.UseVisualStyleBackColor = true;
            this.xml_suchen.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(806, 411);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // Fahrrad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 431);
            this.Controls.Add(this.Tabs);
            this.Name = "Fahrrad";
            this.Text = "Simulation";
            this.Tabs.ResumeLayout(false);
            this.xmlInput.ResumeLayout(false);
            this.xmlInput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage xmlInput;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button xml_suchen;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label pfadText;
    }
}

