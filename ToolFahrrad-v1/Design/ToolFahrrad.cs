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
            instance.GetTeil(1).VerbrauchAktuell = Convert.ToInt32(upDownAW1.Value);
            instance.GetTeil(2).VerbrauchAktuell = Convert.ToInt32(upDownAW2.Value);
            instance.GetTeil(3).VerbrauchAktuell = Convert.ToInt32(upDownAW3.Value);

            instance.GetTeil(1).VerbrauchPrognose1 = Convert.ToInt32(upDownP11.Value);
            instance.GetTeil(1).VerbrauchPrognose2 = Convert.ToInt32(upDownP12.Value);
            instance.GetTeil(1).VerbrauchPrognose3 = Convert.ToInt32(upDownP13.Value);

            instance.GetTeil(2).VerbrauchPrognose1 = Convert.ToInt32(upDownP21.Value);
            instance.GetTeil(2).VerbrauchPrognose2 = Convert.ToInt32(upDownP22.Value);
            instance.GetTeil(2).VerbrauchPrognose3 = Convert.ToInt32(upDownP23.Value);

            instance.GetTeil(3).VerbrauchPrognose1 = Convert.ToInt32(upDownP31.Value);
            instance.GetTeil(3).VerbrauchPrognose2 = Convert.ToInt32(upDownP32.Value);
            instance.GetTeil(3).VerbrauchPrognose3 = Convert.ToInt32(upDownP33.Value);

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
        private void Info(int action)
        {
            listView1.Items.Clear();
            if (!listView1.Columns.Count.Equals(0))
            {
                while (listView1.Columns.Count > 0)
                {
                    listView1.Columns.RemoveAt(0);
                }
            }
            listView1.Columns.Add("NR.", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("Bezeichnung", 160);
            listView1.Columns.Add("Bestand", 90, HorizontalAlignment.Center);
            listView1.Columns.Add("in %", 90, HorizontalAlignment.Center);
            ListViewItem lvi;
            if (action == 0) //Kaufteile
            {
                listView1.Columns.Add("Zugang", 90, HorizontalAlignment.Center);
                foreach (var a in instance.ListeKTeile)
                {
                    lvi = new ListViewItem(a.Nummer + "");
                    lvi.UseItemStyleForSubItems = false;
                    lvi.SubItems.Add(a.Verwendung + " - " + a.Bezeichnung);
                    lvi.SubItems.Add(a.Lagerstand + "");
                    lvi.SubItems.Add(a.Verhaeltnis + "");
                    if (a.LagerZugang.Equals(0))
                        lvi.SubItems.Add("");
                    else
                        lvi.SubItems.Add(a.LagerZugang + "");

                    if (a.Verhaeltnis <= 30)
                        lvi.SubItems[3].BackColor = Color.Salmon;
                    else if (a.Verhaeltnis > 30 && a.Verhaeltnis < 50)
                        lvi.SubItems[3].BackColor = Color.NavajoWhite;
                    else if (a.Verhaeltnis > 100)
                        lvi.SubItems[3].BackColor = Color.Khaki;
                    else
                        lvi.SubItems[3].BackColor = Color.PaleGreen;
                    listView1.Items.Add(lvi);
                }
            }
            else if (action == 1)
            {
                listView1.Columns.Add("Warteschlange", 90, HorizontalAlignment.Center);
                listView1.Columns.Add("in Bearbeitung", 90, HorizontalAlignment.Center);
                foreach (var a in instance.ListeETeile)
                {
                    lvi = new ListViewItem(a.Nummer + "");
                    lvi.UseItemStyleForSubItems = false;
                    lvi.SubItems.Add(a.Verwendung + " - " + a.Bezeichnung);
                    lvi.SubItems.Add(a.Lagerstand + "");
                    lvi.SubItems.Add(a.Verhaeltnis + "");
                    if (a.InWartschlange.Equals(0))
                        lvi.SubItems.Add("");
                    else
                        lvi.SubItems.Add(a.InWartschlange + "");
                    if (a.InBearbeitung.Equals(0))
                        lvi.SubItems.Add("");
                    else
                        lvi.SubItems.Add(a.InBearbeitung + "");


                    if (a.Verhaeltnis <= 30)
                        lvi.SubItems[3].BackColor = Color.Salmon;
                    else if (a.Verhaeltnis > 30 && a.Verhaeltnis < 50)
                        lvi.SubItems[3].BackColor = Color.NavajoWhite;
                    else if (a.Verhaeltnis > 100)
                        lvi.SubItems[3].BackColor = Color.Khaki;
                    else
                        lvi.SubItems[3].BackColor = Color.PaleGreen;
                    listView1.Items.Add(lvi);
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (okPrognose == true && okXml == true)
            {
                Info(comboBox1.SelectedIndex);
            }
        }
    }
}
