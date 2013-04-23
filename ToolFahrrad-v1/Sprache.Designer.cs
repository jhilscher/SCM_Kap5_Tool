namespace ToolFahrrad_v1
{
    partial class Sprache
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
            this.spracheOK = new System.Windows.Forms.Button();
            this.de = new System.Windows.Forms.RadioButton();
            this.en = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // spracheOK
            // 
            this.spracheOK.Location = new System.Drawing.Point(120, 54);
            this.spracheOK.Name = "spracheOK";
            this.spracheOK.Size = new System.Drawing.Size(75, 23);
            this.spracheOK.TabIndex = 2;
            this.spracheOK.Text = "OK";
            this.spracheOK.UseVisualStyleBackColor = true;
            this.spracheOK.Click += new System.EventHandler(this.spracheOK_Click);
            // 
            // de
            // 
            this.de.AutoSize = true;
            this.de.Checked = true;
            this.de.Location = new System.Drawing.Point(13, 13);
            this.de.Name = "de";
            this.de.Size = new System.Drawing.Size(65, 17);
            this.de.TabIndex = 3;
            this.de.TabStop = true;
            this.de.Text = "Deutsch";
            this.de.UseVisualStyleBackColor = true;
            // 
            // en
            // 
            this.en.AutoSize = true;
            this.en.Location = new System.Drawing.Point(13, 37);
            this.en.Name = "en";
            this.en.Size = new System.Drawing.Size(65, 17);
            this.en.TabIndex = 4;
            this.en.Text = "Englisch";
            this.en.UseVisualStyleBackColor = true;
            // 
            // Sprache
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 85);
            this.Controls.Add(this.en);
            this.Controls.Add(this.de);
            this.Controls.Add(this.spracheOK);
            this.Name = "Sprache";
            this.Text = "sprache";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button spracheOK;
        private System.Windows.Forms.RadioButton de;
        private System.Windows.Forms.RadioButton en;
    }
}