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
        DataContainer instance = DataContainer.Instance;
        XMLDatei xml = new XMLDatei();
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
            {
                if (xml.ReadDatei(openFileDialog.FileName) == true)
                {
                    pfadText.Text = "xml wurde gefunden: " + openFileDialog.FileName;
                }
                
            }
        }

        private void englischToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            Controls.Clear();
            Events.Dispose();
            InitializeComponent();
        }

        private void prognoseSpeichern_Click(object sender, EventArgs e)
        {
            instance.ArrayAktuelleWoche = new int[] { Convert.ToInt32(upDownAW1.Value), Convert.ToInt32(upDownAW2.Value), Convert.ToInt32(upDownAW3.Value) };
            instance.ArrayPrognose = new int[,] { 
                                                    { Convert.ToInt32(upDownP11.Value), Convert.ToInt32(upDownP12.Value), Convert.ToInt32(upDownP13.Value) }, 
                                                    { Convert.ToInt32(upDownP21.Value), Convert.ToInt32(upDownP22.Value), Convert.ToInt32(upDownP23.Value) }, 
                                                    { Convert.ToInt32(upDownP31.Value), Convert.ToInt32(upDownP32.Value), Convert.ToInt32(upDownP33.Value) } 
                                                };
            prognoseText.Text = "erfolgreich gespeichert";
        }
    }
}
