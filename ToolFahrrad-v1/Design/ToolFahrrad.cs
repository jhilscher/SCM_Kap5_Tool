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
        DataContainer instance = DataContainer.Instance;
        XMLDatei xml = new XMLDatei();
        Produktionsplanung pp = new Produktionsplanung();
        private bool okPrognose = false;
        private bool okXml = false;
        public Fahrrad()
        {
            //if (sprache.GetCheck == "EN")
            //{
            //    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            //}
            InitializeComponent();
        }


        // F1

        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    if (keyData == Keys.F1)
        //    {
        //        MessageBox.Show("You pressed the F1 key");
        //        return true;    // indicate that you handled this keystroke
        //    }

        //    // Call the base class
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}



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
                    xmlOffenOK.Visible = true;
                    pfadText.Visible = false;
                    okXml = true;
                    if (!infoLable.Text.Contains("aus der Periode"))
                        infoLable.Text = infoLable.Text + " aus der Periode " + xml.period;
                    if (this.okPrognose == true)
                        toolAusfueren.Visible = true;
                }
                else
                {
                    xmlTextBox.Text = openFileDialog.FileName;
                    pfadText.ForeColor = Color.Red;
                    toolAusfueren.Visible = false;
                    xmlOffenOK.Visible = false;
                    save.Visible = false;
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
            instance.GetTeil(1).VertriebAktuell = Convert.ToInt32(upDownAW1.Value);
            instance.GetTeil(2).VertriebAktuell = Convert.ToInt32(upDownAW2.Value);
            instance.GetTeil(3).VertriebAktuell = Convert.ToInt32(upDownAW3.Value);

            instance.GetTeil(1).Pufferwert = Convert.ToInt32(pufferP1.Value);
            instance.GetTeil(2).Pufferwert = Convert.ToInt32(pufferP2.Value);
            instance.GetTeil(3).Pufferwert = Convert.ToInt32(pufferP3.Value);

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
            this.panelXML.Visible = true;
            this.okPrognose = true;
            if (this.okXml == true)
                this.toolAusfueren.Visible = true;
        }


        private void TextVisibleFalse()
        {
            this.bildSpeichOk.Visible = false;
            this.okPrognose = false;
            this.toolAusfueren.Visible = false;
            this.save.Visible = false;
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
        private void pufferP1_ValueChanged(object sender, EventArgs e)
        {
            TextVisibleFalse();
        }
        private void pufferP2_ValueChanged(object sender, EventArgs e)
        {
            TextVisibleFalse();
        }
        private void pufferP3_ValueChanged(object sender, EventArgs e)
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
            ListViewItem lvi;
            listView1.Columns.Add("NR.", 50, HorizontalAlignment.Center);
            if (action == 0 || action == 1)
            {
                listView1.Columns.Add("Bezeichnung", 160);
                listView1.Columns.Add("Bestand", 70, HorizontalAlignment.Center);
                listView1.Columns.Add("in %", 70, HorizontalAlignment.Center);
                
                if (action == 0) //Kaufteile
                {
                    listView1.Columns.Add("Zugang", 70, HorizontalAlignment.Center);
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
                    listView1.Columns.Add("Warteschlange", 70, HorizontalAlignment.Center);
                    listView1.Columns.Add("in Bearbeitung", 70, HorizontalAlignment.Center);
                    listView1.Columns.Add("Produktion", 70, HorizontalAlignment.Center);
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

                        lvi.SubItems.Add(a.ProduktionsMenge + "");

                        //Color
                        if (a.Verhaeltnis <= 30)
                            lvi.SubItems[3].BackColor = Color.Salmon;
                        else if (a.Verhaeltnis > 30 && a.Verhaeltnis < 50)
                            lvi.SubItems[3].BackColor = Color.NavajoWhite;
                        else if (a.Verhaeltnis > 100)
                            lvi.SubItems[3].BackColor = Color.Khaki;
                        else
                            lvi.SubItems[3].BackColor = Color.PaleGreen;
                        lvi.SubItems[6].BackColor = Color.SkyBlue;
                        listView1.Items.Add(lvi);
                    }
                }
            }
            else if (action == 2)
            {
                listView1.Columns.Add("Rüst", 70, HorizontalAlignment.Center);
                listView1.Columns.Add("Leerzeit", 70, HorizontalAlignment.Center);
                listView1.Columns.Add("Kapazitätsbedarf", 70, HorizontalAlignment.Center);
                foreach (var a in instance.ArbeitsplatzList)
                {
                    lvi = new ListViewItem(a.GetNummerArbeitsplatz + "");
                    lvi.SubItems.Add(a.AnzRuestung + "");
                    lvi.SubItems.Add(a.Leerzeit + "");
                    lvi.SubItems.Add(a.GetBenoetigteZeit + "");
                    listView1.Items.Add(lvi);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (okXml == true)
            {
                Info(comboBox1.SelectedIndex);
                this.editLink.Visible = true;
            }
        }

        private void toolAusfueren_Click(object sender, EventArgs e)
        {            
            pp.Aufloesen();
            double a = instance.GetArbeitsplatz(1).GetRuestZeit;
            this.toolAusfueren.Visible = false;
            this.save.Visible = true;
        }
    }
}
