namespace ToolFahrrad_v1
{
    partial class TeilInformation
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ausgabe = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kaufteil N";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(163, 9);
            this.label2.MinimumSize = new System.Drawing.Size(20, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 37);
            this.label2.TabIndex = 2;
            // 
            // ausgabe
            // 
            this.ausgabe.AutoSize = true;
            this.ausgabe.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ausgabe.Location = new System.Drawing.Point(19, 66);
            this.ausgabe.MinimumSize = new System.Drawing.Size(100, 0);
            this.ausgabe.Name = "ausgabe";
            this.ausgabe.Size = new System.Drawing.Size(100, 18);
            this.ausgabe.TabIndex = 4;
            // 
            // TeilInformation
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.ausgabe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(300, 600);
            this.Name = "TeilInformation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label ausgabe;
    }
}