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
        public Fahrrad()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-EN");
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "xml-Datei öffnen (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                pfadText.Text = "xml wurde gefunden: " + openFileDialog.FileName;
        }
    }
}
