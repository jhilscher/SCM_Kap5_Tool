namespace ToolFahrrad_v1.Windows
{
    partial class TeilInformation
    {
        private System.Windows.Forms.Label label1;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeilInformation));
            this.label1 = new System.Windows.Forms.Label();
            this.ausgabe = new System.Windows.Forms.Label();
            this.ausgabe2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.MinimumSize = new System.Drawing.Size(100, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 37);
            this.label1.TabIndex = 1;
            // 
            // ausgabe
            // 
            this.ausgabe.AutoSize = true;
            this.ausgabe.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ausgabe.Location = new System.Drawing.Point(16, 53);
            this.ausgabe.MinimumSize = new System.Drawing.Size(100, 0);
            this.ausgabe.Name = "ausgabe";
            this.ausgabe.Size = new System.Drawing.Size(100, 18);
            this.ausgabe.TabIndex = 4;
            // 
            // ausgabe2
            // 
            this.ausgabe2.AutoSize = true;
            this.ausgabe2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ausgabe2.Location = new System.Drawing.Point(197, 53);
            this.ausgabe2.MinimumSize = new System.Drawing.Size(100, 0);
            this.ausgabe2.Name = "ausgabe2";
            this.ausgabe2.Size = new System.Drawing.Size(100, 18);
            this.ausgabe2.TabIndex = 5;
            // 
            // TeilInformation
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(379, 261);
            this.Controls.Add(this.ausgabe2);
            this.Controls.Add(this.ausgabe);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(500, 600);
            this.Name = "TeilInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label ausgabe;
        private System.Windows.Forms.Label ausgabe2;
    }
}