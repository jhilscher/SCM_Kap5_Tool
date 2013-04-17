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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fahrrad));
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
            resources.ApplyResources(this.Tabs, "Tabs");
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            // 
            // xmlInput
            // 
            this.xmlInput.BackColor = System.Drawing.Color.Transparent;
            this.xmlInput.Controls.Add(this.pfadText);
            this.xmlInput.Controls.Add(this.xml_suchen);
            resources.ApplyResources(this.xmlInput, "xmlInput");
            this.xmlInput.Name = "xmlInput";
            // 
            // pfadText
            // 
            resources.ApplyResources(this.pfadText, "pfadText");
            this.pfadText.Name = "pfadText";
            // 
            // xml_suchen
            // 
            resources.ApplyResources(this.xml_suchen, "xml_suchen");
            this.xml_suchen.Name = "xml_suchen";
            this.xml_suchen.UseVisualStyleBackColor = true;
            this.xml_suchen.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // Fahrrad
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Tabs);
            this.Name = "Fahrrad";
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

