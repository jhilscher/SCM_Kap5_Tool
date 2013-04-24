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
using System.Drawing;

namespace ToolFahrrad_v1
{
    public partial class Fahrrad : Form
    {
        private Sprache sprache;
        DataContainer instance = DataContainer.Instance;
        XMLDatei xml = new XMLDatei();
        private bool okPrognose = false;
        private bool okXml = false;
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
                    xmlTextBox.Text = openFileDialog.FileName;
                    pfadText.ForeColor = Color.ForestGreen;
                    bildOK.Visible = true;
                    pfadText.Visible = false;
                    okXml = true;
                }
                else
                {
                    xmlTextBox.Text = openFileDialog.FileName;
                    pfadText.ForeColor = Color.Red;
                    bildOK.Visible = false;
                    pfadText.Visible = true;
                    okXml = false;
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
            bildSpeichOk.Visible = true;
            okPrognose = true;
        }


        private void TextVisibleFalse()
        {
            bildSpeichOk.Visible = false;
            okPrognose = false;
        }

        private void upDownAW1_ValueChanged(object sender, EventArgs e)
        {
            TextVisibleFalse();
        }

        private void upDownAW2_ValueChanged(object sender, EventArgs e)
        {
            TextVisibleFalse();
        }

        private void upDownAW3_ValueChanged(object sender, EventArgs e)
        {
            TextVisibleFalse();
        }

        private void upDownP13_ValueChanged(object sender, EventArgs e)
        {
            TextVisibleFalse();
        }

        private void upDownP12_ValueChanged(object sender, EventArgs e)
        {
            TextVisibleFalse();
        }

        private void upDownP11_ValueChanged(object sender, EventArgs e)
        {
            TextVisibleFalse();
        }

        private void upDownP21_ValueChanged(object sender, EventArgs e)
        {
            TextVisibleFalse();
        }

        private void upDownP22_ValueChanged(object sender, EventArgs e)
        {
            TextVisibleFalse();
        }

        private void upDownP23_ValueChanged(object sender, EventArgs e)
        {
            TextVisibleFalse();
        }

        private void upDownP33_ValueChanged(object sender, EventArgs e)
        {
            TextVisibleFalse();
        }

        private void upDownP32_ValueChanged(object sender, EventArgs e)
        {
            TextVisibleFalse();
        }

        private void upDownP31_ValueChanged(object sender, EventArgs e)
        {
            TextVisibleFalse();
        }


        /// <summary>
        /// Info Seite
        /// </summary>
        private void Info()
        {
            ListViewItem lvi;
            //    lvi = new ListViewItem("ID");
            //lvi.SubItems.Add("12");
            //lvi.SubItems.Add("124");

            //listView1.Items.Add(lvi);
            //lvi = new ListViewItem("ID3");
            //lvi.SubItems.Add("123");
            //lvi.SubItems.Add("1243");

            //listView1.Items.Add(lvi);


             if (okPrognose == true && okXml == true)
            {
                foreach (var a in instance.Liste_teile) 
                {
                    lvi = new ListViewItem(a.Value.Nummer + "");
                    lvi.UseItemStyleForSubItems = false;
                    lvi.SubItems.Add(a.Value.Bezeichnung);
                    lvi.SubItems.Add(a.Value.Lagerstand + "");
                    lvi.SubItems.Add(a.Value.Verhaeltnis + "");
                    if (a.Value.Verhaeltnis <= 30)
                        lvi.SubItems[3].BackColor = Color.Salmon;
                    if (a.Value.Verhaeltnis > 30 && a.Value.Verhaeltnis < 50)
                        lvi.SubItems[3].BackColor = Color.PaleGreen;
                    if (a.Value.Verhaeltnis > 100)
                        lvi.SubItems[3].BackColor = Color.NavajoWhite;
                    //else
                    //    lvi.BackColor = Color.Green;
                    

                    listView1.Items.Add(lvi);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Info();
        }
    }
}
