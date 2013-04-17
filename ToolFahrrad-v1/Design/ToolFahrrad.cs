using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace ToolFahrrad_v1
{
    public partial class Fahrrad : Form
    {
        private Sprache sprache;
        public Fahrrad()
        {
            sprache = new Sprache();
            Application.Run(sprache);
            if (sprache.GetCheck == "EN")
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            }
            InitializeComponent();
            
        }

        private void xml_suchen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "xml-Datei öffnen (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                pfadText.Text = "xml wurde gefunden: " + openFileDialog.FileName;
        }

        private void englischToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            Controls.Clear();
            Events.Dispose();
            InitializeComponent();
        }
    }
}
