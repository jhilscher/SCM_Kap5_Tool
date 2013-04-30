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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fahrrad));
            this.Tabs = new System.Windows.Forms.TabControl();
            this.tab_xml = new System.Windows.Forms.TabPage();
            this.pufferP3 = new System.Windows.Forms.NumericUpDown();
            this.pufferP2 = new System.Windows.Forms.NumericUpDown();
            this.pufferP1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.panelXML = new System.Windows.Forms.Panel();
            this.save = new System.Windows.Forms.PictureBox();
            this.xmlOffenOK = new System.Windows.Forms.PictureBox();
            this.toolAusfueren = new System.Windows.Forms.PictureBox();
            this.xmlTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.titleXmlLaden = new System.Windows.Forms.Label();
            this.pfadText = new System.Windows.Forms.Label();
            this.xml_suchen = new System.Windows.Forms.Button();
            this.bildSpeichOk = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.prognoseSpeichern = new System.Windows.Forms.Button();
            this.upDownP33 = new System.Windows.Forms.NumericUpDown();
            this.upDownP23 = new System.Windows.Forms.NumericUpDown();
            this.upDownP13 = new System.Windows.Forms.NumericUpDown();
            this.upDownAW3 = new System.Windows.Forms.NumericUpDown();
            this.upDownP32 = new System.Windows.Forms.NumericUpDown();
            this.upDownP22 = new System.Windows.Forms.NumericUpDown();
            this.upDownP12 = new System.Windows.Forms.NumericUpDown();
            this.upDownAW2 = new System.Windows.Forms.NumericUpDown();
            this.upDownP31 = new System.Windows.Forms.NumericUpDown();
            this.upDownP21 = new System.Windows.Forms.NumericUpDown();
            this.upDownP11 = new System.Windows.Forms.NumericUpDown();
            this.upDownAW1 = new System.Windows.Forms.NumericUpDown();
            this.prognose1 = new System.Windows.Forms.Label();
            this.prognose2 = new System.Windows.Forms.Label();
            this.prognose3 = new System.Windows.Forms.Label();
            this.aktulleWoche = new System.Windows.Forms.Label();
            this.p3 = new System.Windows.Forms.Label();
            this.p2 = new System.Windows.Forms.Label();
            this.p1 = new System.Windows.Forms.Label();
            this.titlePrognose = new System.Windows.Forms.Label();
            this.tab_info = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.infoLable = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.spracheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deutschToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englischToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.Tabs.SuspendLayout();
            this.tab_xml.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pufferP3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pufferP2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pufferP1)).BeginInit();
            this.panelXML.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.save)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xmlOffenOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolAusfueren)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bildSpeichOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownAW3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownAW2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownAW1)).BeginInit();
            this.tab_info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.tab_xml);
            this.Tabs.Controls.Add(this.tab_info);
            resources.ApplyResources(this.Tabs, "Tabs");
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            // 
            // tab_xml
            // 
            this.tab_xml.BackColor = System.Drawing.Color.Transparent;
            this.tab_xml.Controls.Add(this.pufferP3);
            this.tab_xml.Controls.Add(this.pufferP2);
            this.tab_xml.Controls.Add(this.pufferP1);
            this.tab_xml.Controls.Add(this.label1);
            this.tab_xml.Controls.Add(this.panelXML);
            this.tab_xml.Controls.Add(this.bildSpeichOk);
            this.tab_xml.Controls.Add(this.pictureBox1);
            this.tab_xml.Controls.Add(this.prognoseSpeichern);
            this.tab_xml.Controls.Add(this.upDownP33);
            this.tab_xml.Controls.Add(this.upDownP23);
            this.tab_xml.Controls.Add(this.upDownP13);
            this.tab_xml.Controls.Add(this.upDownAW3);
            this.tab_xml.Controls.Add(this.upDownP32);
            this.tab_xml.Controls.Add(this.upDownP22);
            this.tab_xml.Controls.Add(this.upDownP12);
            this.tab_xml.Controls.Add(this.upDownAW2);
            this.tab_xml.Controls.Add(this.upDownP31);
            this.tab_xml.Controls.Add(this.upDownP21);
            this.tab_xml.Controls.Add(this.upDownP11);
            this.tab_xml.Controls.Add(this.upDownAW1);
            this.tab_xml.Controls.Add(this.prognose1);
            this.tab_xml.Controls.Add(this.prognose2);
            this.tab_xml.Controls.Add(this.prognose3);
            this.tab_xml.Controls.Add(this.aktulleWoche);
            this.tab_xml.Controls.Add(this.p3);
            this.tab_xml.Controls.Add(this.p2);
            this.tab_xml.Controls.Add(this.p1);
            this.tab_xml.Controls.Add(this.titlePrognose);
            resources.ApplyResources(this.tab_xml, "tab_xml");
            this.tab_xml.Name = "tab_xml";
            // 
            // pufferP3
            // 
            this.pufferP3.BackColor = System.Drawing.Color.MistyRose;
            this.pufferP3.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.pufferP3, "pufferP3");
            this.pufferP3.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.pufferP3.Name = "pufferP3";
            this.pufferP3.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // pufferP2
            // 
            this.pufferP2.BackColor = System.Drawing.Color.MistyRose;
            this.pufferP2.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.pufferP2, "pufferP2");
            this.pufferP2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.pufferP2.Name = "pufferP2";
            this.pufferP2.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // pufferP1
            // 
            this.pufferP1.BackColor = System.Drawing.Color.MistyRose;
            this.pufferP1.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.pufferP1, "pufferP1");
            this.pufferP1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.pufferP1.Name = "pufferP1";
            this.pufferP1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.pufferP1.ValueChanged += new System.EventHandler(this.pufferP1_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panelXML
            // 
            this.panelXML.Controls.Add(this.save);
            this.panelXML.Controls.Add(this.xmlOffenOK);
            this.panelXML.Controls.Add(this.toolAusfueren);
            this.panelXML.Controls.Add(this.xmlTextBox);
            this.panelXML.Controls.Add(this.pictureBox2);
            this.panelXML.Controls.Add(this.titleXmlLaden);
            this.panelXML.Controls.Add(this.pfadText);
            this.panelXML.Controls.Add(this.xml_suchen);
            resources.ApplyResources(this.panelXML, "panelXML");
            this.panelXML.Name = "panelXML";
            // 
            // save
            // 
            this.save.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.save, "save");
            this.save.Name = "save";
            this.save.TabStop = false;
            this.toolTip.SetToolTip(this.save, resources.GetString("save.ToolTip"));
            // 
            // xmlOffenOK
            // 
            resources.ApplyResources(this.xmlOffenOK, "xmlOffenOK");
            this.xmlOffenOK.Name = "xmlOffenOK";
            this.xmlOffenOK.TabStop = false;
            this.toolTip.SetToolTip(this.xmlOffenOK, resources.GetString("xmlOffenOK.ToolTip"));
            // 
            // toolAusfueren
            // 
            this.toolAusfueren.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.toolAusfueren, "toolAusfueren");
            this.toolAusfueren.Name = "toolAusfueren";
            this.toolAusfueren.TabStop = false;
            this.toolTip.SetToolTip(this.toolAusfueren, resources.GetString("toolAusfueren.ToolTip"));
            this.toolAusfueren.Click += new System.EventHandler(this.toolAusfueren_Click);
            // 
            // xmlTextBox
            // 
            resources.ApplyResources(this.xmlTextBox, "xmlTextBox");
            this.xmlTextBox.Name = "xmlTextBox";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // titleXmlLaden
            // 
            resources.ApplyResources(this.titleXmlLaden, "titleXmlLaden");
            this.titleXmlLaden.Name = "titleXmlLaden";
            // 
            // pfadText
            // 
            resources.ApplyResources(this.pfadText, "pfadText");
            this.pfadText.ForeColor = System.Drawing.Color.Red;
            this.pfadText.Name = "pfadText";
            // 
            // xml_suchen
            // 
            resources.ApplyResources(this.xml_suchen, "xml_suchen");
            this.xml_suchen.Name = "xml_suchen";
            this.toolTip.SetToolTip(this.xml_suchen, resources.GetString("xml_suchen.ToolTip"));
            this.xml_suchen.UseVisualStyleBackColor = true;
            this.xml_suchen.Click += new System.EventHandler(this.xml_suchen_Click);
            // 
            // bildSpeichOk
            // 
            resources.ApplyResources(this.bildSpeichOk, "bildSpeichOk");
            this.bildSpeichOk.Name = "bildSpeichOk";
            this.bildSpeichOk.TabStop = false;
            this.toolTip.SetToolTip(this.bildSpeichOk, resources.GetString("bildSpeichOk.ToolTip"));
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // prognoseSpeichern
            // 
            resources.ApplyResources(this.prognoseSpeichern, "prognoseSpeichern");
            this.prognoseSpeichern.Name = "prognoseSpeichern";
            this.toolTip.SetToolTip(this.prognoseSpeichern, resources.GetString("prognoseSpeichern.ToolTip"));
            this.prognoseSpeichern.UseVisualStyleBackColor = true;
            this.prognoseSpeichern.Click += new System.EventHandler(this.prognoseSpeichern_Click);
            // 
            // upDownP33
            // 
            this.upDownP33.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.upDownP33, "upDownP33");
            this.upDownP33.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.upDownP33.Name = "upDownP33";
            this.upDownP33.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.upDownP33.ValueChanged += new System.EventHandler(this.upDownP33_ValueChanged);
            // 
            // upDownP23
            // 
            this.upDownP23.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.upDownP23, "upDownP23");
            this.upDownP23.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.upDownP23.Name = "upDownP23";
            this.upDownP23.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.upDownP23.ValueChanged += new System.EventHandler(this.upDownP23_ValueChanged);
            // 
            // upDownP13
            // 
            this.upDownP13.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.upDownP13, "upDownP13");
            this.upDownP13.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.upDownP13.Name = "upDownP13";
            this.upDownP13.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.upDownP13.ValueChanged += new System.EventHandler(this.upDownP13_ValueChanged);
            // 
            // upDownAW3
            // 
            this.upDownAW3.BackColor = System.Drawing.Color.Honeydew;
            this.upDownAW3.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.upDownAW3, "upDownAW3");
            this.upDownAW3.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.upDownAW3.Name = "upDownAW3";
            this.upDownAW3.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.upDownAW3.ValueChanged += new System.EventHandler(this.upDownAW3_ValueChanged);
            // 
            // upDownP32
            // 
            this.upDownP32.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.upDownP32, "upDownP32");
            this.upDownP32.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.upDownP32.Name = "upDownP32";
            this.upDownP32.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.upDownP32.ValueChanged += new System.EventHandler(this.upDownP32_ValueChanged);
            // 
            // upDownP22
            // 
            this.upDownP22.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.upDownP22, "upDownP22");
            this.upDownP22.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.upDownP22.Name = "upDownP22";
            this.upDownP22.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.upDownP22.ValueChanged += new System.EventHandler(this.upDownP22_ValueChanged);
            // 
            // upDownP12
            // 
            this.upDownP12.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.upDownP12, "upDownP12");
            this.upDownP12.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.upDownP12.Name = "upDownP12";
            this.upDownP12.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.upDownP12.ValueChanged += new System.EventHandler(this.upDownP12_ValueChanged);
            // 
            // upDownAW2
            // 
            this.upDownAW2.BackColor = System.Drawing.Color.Honeydew;
            this.upDownAW2.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.upDownAW2, "upDownAW2");
            this.upDownAW2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.upDownAW2.Name = "upDownAW2";
            this.upDownAW2.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.upDownAW2.ValueChanged += new System.EventHandler(this.upDownAW2_ValueChanged);
            // 
            // upDownP31
            // 
            this.upDownP31.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.upDownP31, "upDownP31");
            this.upDownP31.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.upDownP31.Name = "upDownP31";
            this.upDownP31.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.upDownP31.ValueChanged += new System.EventHandler(this.upDownP31_ValueChanged);
            // 
            // upDownP21
            // 
            this.upDownP21.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.upDownP21, "upDownP21");
            this.upDownP21.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.upDownP21.Name = "upDownP21";
            this.upDownP21.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.upDownP21.ValueChanged += new System.EventHandler(this.upDownP21_ValueChanged);
            // 
            // upDownP11
            // 
            this.upDownP11.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.upDownP11, "upDownP11");
            this.upDownP11.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.upDownP11.Name = "upDownP11";
            this.upDownP11.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.upDownP11.ValueChanged += new System.EventHandler(this.upDownP11_ValueChanged);
            // 
            // upDownAW1
            // 
            this.upDownAW1.BackColor = System.Drawing.Color.Honeydew;
            this.upDownAW1.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.upDownAW1, "upDownAW1");
            this.upDownAW1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.upDownAW1.Name = "upDownAW1";
            this.upDownAW1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.upDownAW1.ValueChanged += new System.EventHandler(this.upDownAW1_ValueChanged);
            // 
            // prognose1
            // 
            resources.ApplyResources(this.prognose1, "prognose1");
            this.prognose1.Name = "prognose1";
            // 
            // prognose2
            // 
            resources.ApplyResources(this.prognose2, "prognose2");
            this.prognose2.Name = "prognose2";
            // 
            // prognose3
            // 
            resources.ApplyResources(this.prognose3, "prognose3");
            this.prognose3.Name = "prognose3";
            // 
            // aktulleWoche
            // 
            resources.ApplyResources(this.aktulleWoche, "aktulleWoche");
            this.aktulleWoche.Name = "aktulleWoche";
            // 
            // p3
            // 
            resources.ApplyResources(this.p3, "p3");
            this.p3.Name = "p3";
            // 
            // p2
            // 
            resources.ApplyResources(this.p2, "p2");
            this.p2.Name = "p2";
            // 
            // p1
            // 
            resources.ApplyResources(this.p1, "p1");
            this.p1.Name = "p1";
            // 
            // titlePrognose
            // 
            resources.ApplyResources(this.titlePrognose, "titlePrognose");
            this.titlePrognose.Name = "titlePrognose";
            // 
            // tab_info
            // 
            resources.ApplyResources(this.tab_info, "tab_info");
            this.tab_info.BackColor = System.Drawing.Color.Transparent;
            this.tab_info.Controls.Add(this.listView1);
            this.tab_info.Controls.Add(this.pictureBox3);
            this.tab_info.Controls.Add(this.infoLable);
            this.tab_info.Controls.Add(this.comboBox1);
            this.tab_info.Name = "tab_info";
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Control;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.Name = "listView1";
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // pictureBox3
            // 
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // infoLable
            // 
            resources.ApplyResources(this.infoLable, "infoLable");
            this.infoLable.Name = "infoLable";
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            resources.GetString("comboBox1.Items"),
            resources.GetString("comboBox1.Items1"),
            resources.GetString("comboBox1.Items2")});
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.Transparent;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spracheToolStripMenuItem,
            this.einstellungenToolStripMenuItem});
            resources.ApplyResources(this.menu, "menu");
            this.menu.Name = "menu";
            // 
            // spracheToolStripMenuItem
            // 
            this.spracheToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deutschToolStripMenuItem,
            this.englischToolStripMenuItem});
            this.spracheToolStripMenuItem.Name = "spracheToolStripMenuItem";
            resources.ApplyResources(this.spracheToolStripMenuItem, "spracheToolStripMenuItem");
            // 
            // deutschToolStripMenuItem
            // 
            this.deutschToolStripMenuItem.Name = "deutschToolStripMenuItem";
            resources.ApplyResources(this.deutschToolStripMenuItem, "deutschToolStripMenuItem");
            // 
            // englischToolStripMenuItem
            // 
            this.englischToolStripMenuItem.Name = "englischToolStripMenuItem";
            resources.ApplyResources(this.englischToolStripMenuItem, "englischToolStripMenuItem");
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hilfeToolStripMenuItem});
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            resources.ApplyResources(this.einstellungenToolStripMenuItem, "einstellungenToolStripMenuItem");
            // 
            // hilfeToolStripMenuItem
            // 
            this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            resources.ApplyResources(this.hilfeToolStripMenuItem, "hilfeToolStripMenuItem");
            // 
            // Fahrrad
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.menu);
            this.Controls.Add(this.Tabs);
            this.HelpButton = true;
            this.Name = "Fahrrad";
            this.Tabs.ResumeLayout(false);
            this.tab_xml.ResumeLayout(false);
            this.tab_xml.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pufferP3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pufferP2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pufferP1)).EndInit();
            this.panelXML.ResumeLayout(false);
            this.panelXML.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.save)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xmlOffenOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolAusfueren)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bildSpeichOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownAW3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownAW2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownP11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownAW1)).EndInit();
            this.tab_info.ResumeLayout(false);
            this.tab_info.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage tab_xml;
        private System.Windows.Forms.TabPage tab_info;
        private System.Windows.Forms.Button xml_suchen;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label pfadText;
        private System.Windows.Forms.Label titlePrognose;
        private System.Windows.Forms.Label titleXmlLaden;
        private System.Windows.Forms.Label aktulleWoche;
        private System.Windows.Forms.Label p3;
        private System.Windows.Forms.Label p2;
        private System.Windows.Forms.Label p1;
        private System.Windows.Forms.NumericUpDown upDownAW3;
        private System.Windows.Forms.NumericUpDown upDownAW2;
        private System.Windows.Forms.NumericUpDown upDownAW1;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem spracheToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deutschToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englischToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown upDownP33;
        private System.Windows.Forms.NumericUpDown upDownP23;
        private System.Windows.Forms.NumericUpDown upDownP13;
        private System.Windows.Forms.NumericUpDown upDownP32;
        private System.Windows.Forms.NumericUpDown upDownP22;
        private System.Windows.Forms.NumericUpDown upDownP12;
        private System.Windows.Forms.NumericUpDown upDownP31;
        private System.Windows.Forms.NumericUpDown upDownP21;
        private System.Windows.Forms.NumericUpDown upDownP11;
        private System.Windows.Forms.Label prognose1;
        private System.Windows.Forms.Label prognose2;
        private System.Windows.Forms.Label prognose3;
        private System.Windows.Forms.Button prognoseSpeichern;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox xmlTextBox;
        private System.Windows.Forms.PictureBox toolAusfueren;
        private System.Windows.Forms.PictureBox bildSpeichOk;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label infoLable;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox xmlOffenOK;
        private System.Windows.Forms.Panel panelXML;
        private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown pufferP3;
        private System.Windows.Forms.NumericUpDown pufferP2;
        private System.Windows.Forms.NumericUpDown pufferP1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox save;
    }
}

