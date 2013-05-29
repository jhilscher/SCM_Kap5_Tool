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
using System.IO;

namespace ToolFahrrad_v1
{
    public partial class Fahrrad : Form
    {
        DataContainer instance = DataContainer.Instance;
        XMLDatei xml = new XMLDatei();
        Produktionsplanung pp = new Produktionsplanung();
        private bool okPrognose = false;
        private bool okXml = false;
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Fahrrad() {
            //if (sprache.GetCheck == "EN")
            //{
            //    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            //}
            InitializeComponent();
        }

        /// <summary>
        /// Ausführen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolAusfueren_Click(object sender, EventArgs e) {
            ausführen();
            this.toolAusfueren.Visible = false;
            this.save.Visible = true;
            this.tab1.Visible = true;
            this.tab2.Visible = true;
        }
        private void ausführen() {
            if ((instance.GetTeil(4) as ETeil).Puffer != -1) {
                foreach (Teil t in instance.ListeETeile) {
                    t.Aufgeloest = false;
                    (t as ETeil).KdhUpdate = false;
                    (t as ETeil).InBearbeitung = 0;
                    (t as ETeil).InWartschlange = 0;
                    if ((instance.GetTeil(t.Nummer) as ETeil).IstEndProdukt == false)
                        (instance.GetTeil(t.Nummer) as ETeil).Puffer = -1;
                }
            }
            pp.AktPeriode = Convert.ToInt32(xml.period);
            pp.Aufloesen();
            foreach (Arbeitsplatz a in instance.ArbeitsplatzList) {
                a.Geaendert = false;
                a.RuestNew = 0;
            }
            for (int index = 1; index < 4; ++index) {
                this.DispositionDarstellung(index);
            }
            this.Information();
        }

        private void DispositionDarstellung(int index) {
            ETeil et;
            int n;
            if (index == 1) {
                #region P1
                //P1
                n = 1;
                et = instance.GetTeil(n) as ETeil;
                p1vw_0.Text = et.VertriebPer0.ToString();
                p1r_0.Text = et.Puffer.ToString();
                p1ls_0.Text = et.Lagerstand.ToString();
                p1iws_0.Text = et.InWartschlange.ToString();
                p1ib_0.Text = et.InBearbeitung.ToString();
                p1pm_0.Text = et.ProduktionsMengePer0.ToString();
                //26
                n = 26;
                et = instance.GetTeil(n) as ETeil;
                p1vw_26.Text = p1pm_0.Text;
                p1plus_26.Text = p1iws_0.Text;
                if (et.KdhPuffer.ContainsKey(n)) {
                    p1r_26.Text = et.KdhPuffer[n][index - 1].ToString();
                }
                if (et.KdhProduktionsmenge.ContainsKey(index.ToString() + "-" + Convert.ToString(n))) {
                    p1pm_26.Text = et.KdhProduktionsmenge[index.ToString() + "-" + Convert.ToString(n)].ToString();
                }
                p1ls_26.Text = et.Lagerstand.ToString();
                p1iws_26.Text = et.InWartschlange.ToString();
                p1ib_26.Text = et.InBearbeitung.ToString();
                //51
                n = 51;
                et = instance.GetTeil(n) as ETeil;
                p1vw_51.Text = et.VertriebPer0.ToString();
                p1plus_51.Text = et.VaterInWarteschlange.ToString();
                p1r_51.Text = et.Puffer.ToString();
                p1ls_51.Text = et.Lagerstand.ToString();
                p1iws_51.Text = et.InWartschlange.ToString();
                p1ib_51.Text = et.InBearbeitung.ToString();
                p1pm_51.Text = et.ProduktionsMengePer0.ToString();
                //16
                n = 16;
                et = instance.GetTeil(n) as ETeil;
                p1vw_16.Text = p1pm_0.Text;
                p1plus_16.Text = p1iws_51.Text;
                if (et.KdhPuffer.ContainsKey(n)) {
                    p1r_16.Text = et.KdhPuffer[n][index - 1].ToString();
                }
                if (et.KdhProduktionsmenge.ContainsKey(index.ToString() + "-" + Convert.ToString(n))) {
                    p1pm_16.Text = et.KdhProduktionsmenge[index.ToString() + "-" + Convert.ToString(n)].ToString();
                }
                p1ls_16.Text = et.Lagerstand.ToString();
                p1iws_16.Text = et.InWartschlange.ToString();
                p1ib_16.Text = et.InBearbeitung.ToString();
                //17 
                n = 17;
                et = instance.GetTeil(n) as ETeil;
                p1vw_17.Text = p1pm_0.Text;
                p1plus_17.Text = p1iws_51.Text;
                if (et.KdhPuffer.ContainsKey(n)) {
                    p1r_17.Text = et.KdhPuffer[n][index - 1].ToString();
                }
                if (et.KdhProduktionsmenge.ContainsKey(index.ToString() + "-" + Convert.ToString(n))) {
                    p1pm_17.Text = et.KdhProduktionsmenge[index.ToString() + "-" + Convert.ToString(n)].ToString();
                }
                p1ls_17.Text = et.Lagerstand.ToString();
                p1iws_17.Text = et.InWartschlange.ToString();
                p1ib_17.Text = et.InBearbeitung.ToString();
                //50
                n = 50;
                et = instance.GetTeil(n) as ETeil;
                p1vw_50.Text = et.VertriebPer0.ToString();
                p1plus_50.Text = et.VaterInWarteschlange.ToString();
                p1r_50.Text = et.Puffer.ToString();
                p1ls_50.Text = et.Lagerstand.ToString();
                p1iws_50.Text = et.InWartschlange.ToString();
                p1ib_50.Text = et.InBearbeitung.ToString();
                p1pm_50.Text = et.ProduktionsMengePer0.ToString();
                //4
                n = 4;
                et = instance.GetTeil(n) as ETeil;
                p1vw_4.Text = et.VertriebPer0.ToString();
                p1plus_4.Text = et.VaterInWarteschlange.ToString();
                p1r_4.Text = et.Puffer.ToString();
                p1ls_4.Text = et.Lagerstand.ToString();
                p1iws_4.Text = et.InWartschlange.ToString();
                p1ib_4.Text = et.InBearbeitung.ToString();
                p1pm_4.Text = et.ProduktionsMengePer0.ToString();
                //10
                n = 10;
                et = instance.GetTeil(n) as ETeil;
                p1vw_10.Text = et.VertriebPer0.ToString();
                p1plus_10.Text = et.VaterInWarteschlange.ToString();
                p1r_10.Text = et.Puffer.ToString();
                p1ls_10.Text = et.Lagerstand.ToString();
                p1iws_10.Text = et.InWartschlange.ToString();
                p1ib_10.Text = et.InBearbeitung.ToString();
                p1pm_10.Text = et.ProduktionsMengePer0.ToString();
                //49
                n = 49;
                et = instance.GetTeil(n) as ETeil;
                p1vw_49.Text = et.VertriebPer0.ToString();
                p1plus_49.Text = et.VaterInWarteschlange.ToString();
                p1r_49.Text = et.Puffer.ToString();
                p1ls_49.Text = et.Lagerstand.ToString();
                p1iws_49.Text = et.InWartschlange.ToString();
                p1ib_49.Text = et.InBearbeitung.ToString();
                p1pm_49.Text = et.ProduktionsMengePer0.ToString();
                //7
                n = 7;
                et = instance.GetTeil(n) as ETeil;
                p1vw_7.Text = et.VertriebPer0.ToString();
                p1plus_7.Text = et.VaterInWarteschlange.ToString();
                p1r_7.Text = et.Puffer.ToString();
                p1ls_7.Text = et.Lagerstand.ToString();
                p1iws_7.Text = et.InWartschlange.ToString();
                p1ib_7.Text = et.InBearbeitung.ToString();
                p1pm_7.Text = et.ProduktionsMengePer0.ToString();
                //13
                n = 13;
                et = instance.GetTeil(n) as ETeil;
                p1vw_13.Text = et.VertriebPer0.ToString();
                p1plus_13.Text = et.VaterInWarteschlange.ToString();
                p1r_13.Text = et.Puffer.ToString();
                p1ls_13.Text = et.Lagerstand.ToString();
                p1iws_13.Text = et.InWartschlange.ToString();
                p1ib_13.Text = et.InBearbeitung.ToString();
                p1pm_13.Text = et.ProduktionsMengePer0.ToString();
                //18
                n = 18;
                et = instance.GetTeil(n) as ETeil;
                p1vw_18.Text = et.VertriebPer0.ToString();
                p1plus_18.Text = et.VaterInWarteschlange.ToString();
                p1r_18.Text = et.Puffer.ToString();
                p1ls_18.Text = et.Lagerstand.ToString();
                p1iws_18.Text = et.InWartschlange.ToString();
                p1ib_18.Text = et.InBearbeitung.ToString();
                p1pm_18.Text = et.ProduktionsMengePer0.ToString();

                #endregion

            }
        }

        /// <summary>
        /// Dropdownmenu Startseite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upDownAW1_ValueChanged(object sender, EventArgs e) {
            TextVisibleFalse();
        }
        private void upDownAW2_ValueChanged(object sender, EventArgs e) {
            TextVisibleFalse();
        }
        private void upDownAW3_ValueChanged(object sender, EventArgs e) {
            TextVisibleFalse();
        }
        private void upDownP13_ValueChanged(object sender, EventArgs e) {
            TextVisibleFalse();
        }
        private void upDownP12_ValueChanged(object sender, EventArgs e) {
            TextVisibleFalse();
        }
        private void upDownP11_ValueChanged(object sender, EventArgs e) {
            TextVisibleFalse();
        }
        private void upDownP21_ValueChanged(object sender, EventArgs e) {
            TextVisibleFalse();
        }
        private void upDownP22_ValueChanged(object sender, EventArgs e) {
            TextVisibleFalse();
        }
        private void upDownP23_ValueChanged(object sender, EventArgs e) {
            TextVisibleFalse();
        }
        private void upDownP33_ValueChanged(object sender, EventArgs e) {
            TextVisibleFalse();
        }
        private void upDownP32_ValueChanged(object sender, EventArgs e) {
            TextVisibleFalse();
        }
        private void upDownP31_ValueChanged(object sender, EventArgs e) {
            TextVisibleFalse();
        }
        private void pufferP1_ValueChanged(object sender, EventArgs e) {
            TextVisibleFalse();
        }
        private void pufferP2_ValueChanged(object sender, EventArgs e) {
            TextVisibleFalse();
        }
        private void pufferP3_ValueChanged(object sender, EventArgs e) {
            TextVisibleFalse();
        }

        private void prognoseSpeichern_Click(object sender, EventArgs e) {
            instance.GetTeil(1).VertriebPer0 = Convert.ToInt32(upDownAW1.Value);
            instance.GetTeil(2).VertriebPer0 = Convert.ToInt32(upDownAW2.Value);
            instance.GetTeil(3).VertriebPer0 = Convert.ToInt32(upDownAW3.Value);

            (instance.GetTeil(1) as ETeil).Puffer = Convert.ToInt32(pufferP1.Value);
            (instance.GetTeil(2) as ETeil).Puffer = Convert.ToInt32(pufferP2.Value);
            (instance.GetTeil(3) as ETeil).Puffer = Convert.ToInt32(pufferP3.Value);

            instance.GetTeil(1).VerbrauchPer1 = Convert.ToInt32(upDownP11.Value);
            instance.GetTeil(1).VerbrauchPer2 = Convert.ToInt32(upDownP21.Value);
            instance.GetTeil(1).VerbrauchPer3 = Convert.ToInt32(upDownP31.Value);

            instance.GetTeil(2).VerbrauchPer1 = Convert.ToInt32(upDownP12.Value);
            instance.GetTeil(2).VerbrauchPer2 = Convert.ToInt32(upDownP22.Value);
            instance.GetTeil(2).VerbrauchPer3 = Convert.ToInt32(upDownP32.Value);

            instance.GetTeil(3).VerbrauchPer1 = Convert.ToInt32(upDownP13.Value);
            instance.GetTeil(3).VerbrauchPer2 = Convert.ToInt32(upDownP23.Value);
            instance.GetTeil(3).VerbrauchPer3 = Convert.ToInt32(upDownP33.Value);

            this.bildSpeichOk.Visible = true;
            this.panelXML.Visible = true;
            this.okPrognose = true;
            if (this.okXml == true)
                this.toolAusfueren.Visible = true;
        }

        /// <summary>
        /// XML-Bearbeitung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xml_suchen_Click(object sender, EventArgs e) {
            xmlOeffnen();
        }
        private void dateiÖffnenToolStripMenuItem_Click(object sender, EventArgs e) {
            xmlOeffnen();
        }

        /// <summary>
        /// MENU
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void englischToolStripMenuItem_Click_1(object sender, EventArgs e) {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            Controls.Clear();
            Events.Dispose();
            InitializeComponent();
        }
        private void gewichtungToolStripMenuItem_Click(object sender, EventArgs e) {
            Einstellungen einstellungen = new Einstellungen();
            einstellungen.Show();
        }
        private void startSeiteToolStripMenuItem_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://scsim.de/");
        }
        private void englischToolStripMenuItem1_Click(object sender, EventArgs e) {
            ChangeLanguage("englisch");
            englischToolStripMenuItem1.Checked = true;
            deutschToolStripMenuItem1.Checked = false;
        }
        private void deutschToolStripMenuItem1_Click(object sender, EventArgs e) {
            ChangeLanguage("deutsch");
            englischToolStripMenuItem1.Checked = false;
            deutschToolStripMenuItem1.Checked = true;
        }
        private void ChangeLanguage(string language) {
            switch (language) {
                case "deutsch":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");
                    Controls.Clear();
                    Events.Dispose();
                    InitializeComponent();
                    okXml = false;
                    break;
                case "englisch":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                    Controls.Clear();
                    Events.Dispose();
                    InitializeComponent();
                    okXml = false;
                    break;
            }
        }
        private void schließenToolStripMenuItem_Click_1(object sender, EventArgs e) {
            this.Close();
        }
        private void handbuchToolStripMenuItem_Click(object sender, EventArgs e) {
            //string path = Directory.GetCurrentDirectory() + @"\chm\dv_aspnetmmc.chm";
            //Help.ShowHelp(this, path, HelpNavigator.TableOfContents, "");
        }

        /// <summary>
        /// EDIT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picEditEteile_Click(object sender, EventArgs e) {
            Dictionary<PictureBox, bool> dic = new Dictionary<PictureBox, bool>() 
            {
                {this.picSaveETeile, true},
                {this.picResetETeil, true},
                {this.picReadOnlyETeile, true}
            };
            if (rbReserve.Checked == true) {
                picEdit(dic, 8, dataGridViewETeil);
            }
        }
        private void picEditAPlatz_Click(object sender, EventArgs e) {
            Dictionary<PictureBox, bool> dic = new Dictionary<PictureBox, bool>() 
            {
                {this.picSaveAPlatz, true},
                {this.picResetAPlatz, true},
                {this.picReadOnlyAPlatz, true}
            };
            if (rbRuestzeit.Checked == true) {
                picEdit(dic, 5, dataGridViewAPlatz);
            }
        }

        /// <summary>
        /// SAVE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureSaveETeile_Click(object sender, EventArgs e) {
            DialogResult result = GetMessage(null, "Änderungen");
            string text = string.Empty;
            if (result == System.Windows.Forms.DialogResult.OK) {
                Dictionary<int, string> ids = new Dictionary<int, string>();
                foreach (DataGridViewRow row in dataGridViewETeil.Rows) {
                    if (rbReserve.Checked == true) {
                        int reserveAlt = (instance.GetTeil(Convert.ToInt32(row.Cells[0].Value.ToString())) as ETeil).Puffer;
                        int reserveNeu = Convert.ToInt32(row.Cells[8].Value.ToString());
                        if (!reserveAlt.Equals(reserveNeu)) {
                            ids.Add(Convert.ToInt32(row.Cells[0].Value.ToString()), reserveAlt + ">" + reserveNeu + ">" + row.Cells[1].Value.ToString());
                        }
                    }
                }

                int a = 0;
                if (ids.Count() > 0) {
                    foreach (KeyValuePair<int, string> pair in ids) {
                        if (instance.GetTeil(pair.Key).Verwendung.Equals("KDH"))
                            ++a;
                        string[] change = pair.Value.Split('>');
                        (instance.GetTeil(pair.Key) as ETeil).FeldGeandert(0, Convert.ToInt32(change[1]), 0);
                        text += change[2] + ": von " + change[0] + " auf " + change[1] + "\n";
                    }
                }

                Dictionary<PictureBox, bool> dic = new Dictionary<PictureBox, bool>() 
                {
                    {this.picEditEteile, true},
                    {this.picResetETeil, false},
                    {this.picSaveETeile, false},
                    {this.picReadOnlyETeile, false}
                };

                picSave(dic, 8, dataGridViewETeil);

                //KDH null
                foreach (Teil t in instance.ListeETeile) {
                    if (t.Verwendung.Equals("KDH") && a == 0) {
                        (t as ETeil).KdhUpdate = false;
                        (instance.GetTeil(t.Nummer) as ETeil).Puffer = -1;
                    }
                }

                pp.Aufloesen();
                Information();
                if (!text.Equals(string.Empty))
                    result = GetMessage(text, "Message");
                else
                    result = GetMessage("keine Spalte wurde geändert", "Message");
            }
        }
        private void picSaveAPlatz_Click(object sender, EventArgs e) {
            DialogResult result = GetMessage(null, "Änderungen");
            string text = string.Empty;
            if (result == System.Windows.Forms.DialogResult.OK) {
                Dictionary<int, string> ids = new Dictionary<int, string>();
                foreach (DataGridViewRow row in dataGridViewAPlatz.Rows) {
                    if (rbRuestzeit.Checked == true) {
                        int ruestAlt = (instance.GetArbeitsplatz(Convert.ToInt32(row.Cells[0].Value.ToString()))).RuestungCustom;
                        int ruestNeu = Convert.ToInt32(row.Cells[5].Value.ToString());
                        if (!ruestAlt.Equals(ruestNeu)) {
                            ids.Add(Convert.ToInt32(row.Cells[0].Value.ToString()), ruestAlt + ">" + ruestNeu);
                        }
                    }
                }

                if (ids.Count() > 0) {
                    foreach (KeyValuePair<int, string> pair in ids) {
                        string[] change = pair.Value.Split('>');
                        (instance.GetArbeitsplatz(pair.Key)).CustomRuestungGeaendert(Convert.ToInt32(change[1]));
                        text += pair.Key + ": von " + change[0] + " auf " + change[1] + "\n";
                    }
                }
                Dictionary<PictureBox, bool> dic = new Dictionary<PictureBox, bool>() 
                {
                    {this.picEditsAPlatz, true},
                    {this.picResetAPlatz, false},
                    {this.picSaveAPlatz, false},
                    {this.picReadOnlyAPlatz, false}
                };
                picSave(dic, 5, dataGridViewAPlatz);
                Information();
                if (!text.Equals(string.Empty))
                    result = GetMessage(text, "Message");
                else
                    result = GetMessage("keine Spalte wurde geändert", "Message");
            }
        }



        /// <summary>
        /// RESET
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureResetETeil_Click(object sender, EventArgs e) {
            Information();
            dataGridViewETeil.Columns[8].DefaultCellStyle.BackColor = Color.Honeydew;
        }
        private void picResetAPlatz_Click(object sender, EventArgs e) {
            Information();
            dataGridViewAPlatz.Columns[4].DefaultCellStyle.BackColor = Color.Honeydew;
        }

        /// <summary>
        /// READONLY
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureReadOnly_Click(object sender, EventArgs e) {
            Dictionary<PictureBox, bool> dic = new Dictionary<PictureBox, bool>() 
            {
                {this.picEditEteile, true},
                {this.picResetETeil, false},
                {this.picSaveETeile, false},
                {this.picReadOnlyETeile, false}
            };
            picReadOnly(dic, 8, dataGridViewETeil);
        }
        private void picReadOnlyAPlatz_Click(object sender, EventArgs e) {
            Dictionary<PictureBox, bool> dic = new Dictionary<PictureBox, bool>() 
            {
                {this.picEditsAPlatz, true},
                {this.picResetAPlatz, false},
                {this.picSaveAPlatz, false},
                {this.picReadOnlyAPlatz, false}
            };
            picReadOnly(dic, 5, dataGridViewAPlatz);
        }


        /// <summary>
        /// CHECK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbMitOhne_CheckedChanged(object sender, System.EventArgs e) {
            if (cbMitOhne.Checked == true)
                instance.BerechneKindTeil = true;
            else
                instance.BerechneKindTeil = false;
        }

        ////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// HILFSMETHODEN
        /// </summary>
        private void TextVisibleFalse() {
            this.bildSpeichOk.Visible = false;
            this.okPrognose = false;
            this.toolAusfueren.Visible = false;
            this.save.Visible = false;
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

        private void xmlOeffnen() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "xml-Datei öffnen (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                if (xml.ReadDatei(openFileDialog.FileName) == true) {
                    xmlTextBox.Text = openFileDialog.FileName;
                    pfadText.ForeColor = Color.ForestGreen;
                    xmlOffenOK.Visible = true;
                    pfadText.Visible = false;
                    okXml = true;
                    if (this.okPrognose == true)
                        toolAusfueren.Visible = true;
                }
                else {
                    xmlTextBox.Text = openFileDialog.FileName;
                    pfadText.ForeColor = Color.Red;
                    toolAusfueren.Visible = false;
                    xmlOffenOK.Visible = false;
                    save.Visible = false;
                    pfadText.Visible = true;
                    okXml = false;
                }
            }
            this.panelXML.Visible = true;
        }

        private void DataGriedViewRemove(DataGridView dgv) {
            if (dgv.DataSource != null) {
                dgv.DataSource = null;
            }
            else {
                dgv.Rows.Clear();
            }
        }

        private void Information() {
            // KTeile
            this.DataGriedViewRemove(dataGridViewKTeil);
            int index = 0;
            for (int i = 4; i < 8; ++i) {
                dataGridViewKTeil.Columns[i].HeaderText = "P" + (Convert.ToInt32(xml.period) + (i - 4));
            }

            dataGridViewKTeil.Columns[8].HeaderText = "B" + ((Convert.ToInt32(xml.period)) + 1);
            dataGridViewKTeil.Columns[10].HeaderText = "B" + ((Convert.ToInt32(xml.period)) + 2);
            dataGridViewKTeil.Columns[12].HeaderText = "B" + (Convert.ToInt32(xml.period) + 3);
            dataGridViewKTeil.Columns[14].HeaderText = "B" + (Convert.ToInt32(xml.period) + 4);

            foreach (var a in instance.ListeKTeile) {
                //Lagerzugang berechnen
                int lagerZugang = 0;
                List<List<int>> list = a.OffeneBestellungen;
                foreach (List<int> l in list) {
                    lagerZugang += l[2];
                }

                dataGridViewKTeil.Rows.Add();
                dataGridViewKTeil.Rows[index].Cells[0].Value = a.Nummer;
                dataGridViewKTeil.Rows[index].Cells[1].Value = a.Verwendung + " - " + a.Bezeichnung;
                dataGridViewKTeil.Rows[index].Cells[2].Value = a.Lagerstand;
                dataGridViewKTeil.Rows[index].Cells[3].Value = lagerZugang;
                dataGridViewKTeil.Rows[index].Cells[4].Value = a.BruttoBedarfPer0;
                dataGridViewKTeil.Rows[index].Cells[5].Value = a.BruttoBedarfPer1;
                dataGridViewKTeil.Rows[index].Cells[6].Value = a.BruttoBedarfPer2;
                dataGridViewKTeil.Rows[index].Cells[7].Value = a.BruttoBedarfPer3;

                dataGridViewKTeil.Rows[index].Cells[8].Value = a.BestandPer1;
                if (a.BestandPer1 - a.BruttoBedarfPer0 < 0)
                    dataGridViewKTeil.Rows[index].Cells[9].Value = imageList1.Images[0];
                else if (a.BestandPer1 - a.BruttoBedarfPer0 > 0)
                    dataGridViewKTeil.Rows[index].Cells[9].Value = imageList1.Images[2];
                else
                    dataGridViewKTeil.Rows[index].Cells[9].Value = imageList1.Images[1];
                dataGridViewKTeil.Rows[index].Cells[10].Value = a.BestandPer2;
                if (a.BestandPer2 - a.BruttoBedarfPer1 < 0)
                    dataGridViewKTeil.Rows[index].Cells[11].Value = imageList1.Images[0];
                else if (a.BestandPer2 - a.BruttoBedarfPer1 > 0)
                    dataGridViewKTeil.Rows[index].Cells[11].Value = imageList1.Images[2];
                else
                    dataGridViewKTeil.Rows[index].Cells[11].Value = imageList1.Images[1];
                dataGridViewKTeil.Rows[index].Cells[12].Value = a.BestandPer3;
                if (a.BestandPer3 - a.BruttoBedarfPer2 < 0)
                    dataGridViewKTeil.Rows[index].Cells[13].Value = imageList1.Images[0];
                else if (a.BestandPer3 - a.BruttoBedarfPer2 > 0)
                    dataGridViewKTeil.Rows[index].Cells[13].Value = imageList1.Images[2];
                else
                    dataGridViewKTeil.Rows[index].Cells[13].Value = imageList1.Images[1];
                dataGridViewKTeil.Rows[index].Cells[14].Value = a.BestandPer4;
                if (a.BestandPer4 - a.BruttoBedarfPer3 < 0)
                    dataGridViewKTeil.Rows[index].Cells[15].Value = imageList1.Images[0];
                else if (a.BestandPer4 - a.BruttoBedarfPer3 > 0)
                    dataGridViewKTeil.Rows[index].Cells[15].Value = imageList1.Images[2];
                else
                    dataGridViewKTeil.Rows[index].Cells[15].Value = imageList1.Images[1];

                //Farbe
                for (int i = 0; i < 16; ++i) {
                    if (i >= 0 && i < 4)
                        dataGridViewKTeil.Columns[i].DefaultCellStyle.BackColor = Color.FloralWhite;
                    else if (i == 8 || i == 10 || i == 12 || i == 14)
                        dataGridViewKTeil.Columns[i].DefaultCellStyle.BackColor = Color.LightYellow;
                    else if (i > 3 && i < 8)
                        dataGridViewKTeil.Columns[i].DefaultCellStyle.BackColor = Color.Honeydew;
                }
                ++index;
            }

            // Eteile
            this.DataGriedViewRemove(dataGridViewETeil);
            index = 0;

            foreach (var a in instance.ListeETeile) {
                dataGridViewETeil.Rows.Add();
                dataGridViewETeil.Rows[index].Cells[0].Value = a.Nummer;
                dataGridViewETeil.Rows[index].Cells[1].Value = a.Verwendung + " - " + a.Bezeichnung;
                dataGridViewETeil.Rows[index].Cells[2].Value = a.Lagerstand;
                dataGridViewETeil.Rows[index].Cells[3].Value = a.Verhaeltnis + "%";
                if (a.Verhaeltnis < 40)
                    dataGridViewETeil.Rows[index].Cells[4].Value = imageList1.Images[0];
                else if (a.Verhaeltnis <= 100)
                    dataGridViewETeil.Rows[index].Cells[4].Value = imageList1.Images[2];
                else
                    dataGridViewETeil.Rows[index].Cells[4].Value = imageList1.Images[1];
                dataGridViewETeil.Rows[index].Cells[5].Value = a.InWartschlange;
                dataGridViewETeil.Rows[index].Cells[6].Value = a.InBearbeitung;
                dataGridViewETeil.Rows[index].Cells[7].Value = a.ProduktionsMengePer0;
                dataGridViewETeil.Rows[index].Cells[8].Value = a.Puffer;

                //Farbe
                for (int i = 0; i < 9; ++i) {
                    if (i == 4 || i == 7)
                        dataGridViewETeil.Columns[i].DefaultCellStyle.BackColor = Color.LightYellow;
                    else if (i == 8) {
                        dataGridViewETeil.Columns[i].DefaultCellStyle.BackColor = Color.Honeydew;
                    }
                    else
                        dataGridViewETeil.Columns[i].DefaultCellStyle.BackColor = Color.FloralWhite;
                }

                if (a.Nummer == 16 || a.Nummer == 17 || a.Nummer == 26) {
                    dataGridViewETeil.Rows[index].DefaultCellStyle.BackColor = Color.Yellow;
                }

                ++index;
            }

            // Arbeitsplätze
            this.DataGriedViewRemove(dataGridViewAPlatz);
            index = 0;
            foreach (var a in instance.ArbeitsplatzList) {
                int gesammtZeit = (int)(a.GetRuestZeit * a.RuestungCustom);
                int gesammt = gesammtZeit + a.GetBenoetigteZeit;

                dataGridViewAPlatz.Rows.Add();
                dataGridViewAPlatz.Rows[index].Cells[0].Value = a.GetNummerArbeitsplatz;
                (instance.GetArbeitsplatz(a.GetNummerArbeitsplatz)).CustomRuestungGeaendert(-1);
                dataGridViewAPlatz.Rows[index].Cells[1].Value = a.Leerzeit + " (" + a.RuestungVorPeriode + ") ";
                dataGridViewAPlatz.Rows[index].Cells[2].Value = a.GetBenoetigteZeit + " min";
                dataGridViewAPlatz.Rows[index].Cells[3].Value = (int)(a.GetRuestZeit * a.RuestungCustom) + " min";
                dataGridViewAPlatz.Rows[index].Cells[4].Value = a.RuestNew;
                dataGridViewAPlatz.Rows[index].Cells[5].Value = a.RuestungCustom;
                dataGridViewAPlatz.Rows[index].Cells[6].Value = gesammt + " min";
                dataGridViewAPlatz.Rows[index].Cells[10].Value = imageList1.Images[2];
                if (gesammt <= a.zeit) // newTeim <= 2400 
                    dataGridViewAPlatz.Rows[index].Cells[7].Value = imageList1.Images[2];
                else if (gesammt > instance.ErsteSchicht) // gesammt > 3600
                {
                    if (gesammt > 7200)
                        dataGridViewAPlatz.Rows[index].Cells[10].Value = imageList1.Images[0];
                    else if (gesammt > instance.ZweiteSchicht && gesammt < 7200)
                        dataGridViewAPlatz.Rows[index].Cells[10].Value = imageList1.Images[1];
                    else
                        dataGridViewAPlatz.Rows[index].Cells[10].Value = imageList1.Images[2];

                    if (gesammt < instance.ZweiteSchicht) {
                        dataGridViewAPlatz.Rows[index].Cells[7].Value = imageList1.Images[0];
                        dataGridViewAPlatz.Rows[index].Cells[8].Value = true;
                    }
                    else if (gesammt > instance.ZweiteSchicht) {
                        dataGridViewAPlatz.Rows[index].Cells[7].Value = imageList1.Images[0];
                        dataGridViewAPlatz.Rows[index].Cells[9].Value = true;
                    }
                }
                else if (gesammt > a.zeit && gesammt <= instance.ErsteSchicht) // 2400 < newTime < 3600 Überstunden
                {
                    dataGridViewAPlatz.Rows[index].Cells[7].Value = imageList1.Images[1];
                }
                else {
                    dataGridViewAPlatz.Rows[index].Cells[7].Value = imageList1.Images[2];
                }

                //Farbe
                for (int i = 0; i < 11; ++i) {
                    if (i < 2 || i == 4)
                        dataGridViewAPlatz.Columns[i].DefaultCellStyle.BackColor = Color.FloralWhite;
                    else if (i == 2 || i == 3 || (i >= 6 && i <= 11))
                        dataGridViewAPlatz.Columns[i].DefaultCellStyle.BackColor = Color.LightYellow;
                    else
                        dataGridViewAPlatz.Columns[i].DefaultCellStyle.BackColor = Color.Honeydew;
                }

                ++index;
            }
        }

        private void picEdit(Dictionary<PictureBox, bool> dic, int index, DataGridView dataGrid) {
            dataGrid.Columns[index].DefaultCellStyle.BackColor = Color.LightBlue;
            dataGrid.Columns[index].ReadOnly = false;
            foreach (KeyValuePair<PictureBox, bool> pair in dic) {
                pair.Key.Visible = pair.Value;
            }
        }
        private void picReadOnly(Dictionary<PictureBox, bool> dic, int index, DataGridView dataGrid) {
            foreach (KeyValuePair<PictureBox, bool> pair in dic) {
                pair.Key.Visible = pair.Value;
            }
            dataGrid.Columns[index].DefaultCellStyle.BackColor = Color.Honeydew;
            dataGrid.Columns[index].ReadOnly = true;
        }
        private void picSave(Dictionary<PictureBox, bool> dic, int index, DataGridView dataGrid) {
            foreach (KeyValuePair<PictureBox, bool> pair in dic) {
                pair.Key.Visible = pair.Value;
            }
            dataGrid.Columns[index].DefaultCellStyle.BackColor = Color.Honeydew;
            dataGrid.Columns[index].ReadOnly = true;
        }
        private System.Windows.Forms.DialogResult GetMessage(string t, string s) {
            if (s.Contains("Änderungen"))
                return MessageBox.Show("Wollen Sie sicher was ändern?", s, MessageBoxButtons.OKCancel);
            else
                return MessageBox.Show(t, s, MessageBoxButtons.OK);
        }

        private void dataGridViewKTeil_CellContentClick_1(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == 0) {
                TeilInformation ti = new TeilInformation(Convert.ToInt32(dataGridViewKTeil.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
                ti.GetTeilvonETeilMitMenge();
                ti.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            Bestellverwaltung bv = new Bestellverwaltung();
            bv.generiereBestellListe();
            List<Bestellposition> bp = bv.BvPositionen;
        }
    }
}
