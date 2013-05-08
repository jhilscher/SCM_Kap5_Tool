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
using System;
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
        public Fahrrad()
        {
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
        private void toolAusfueren_Click(object sender, EventArgs e)
        {
            if ((instance.GetTeil(4) as ETeil).Puffer != -1)
            {
                foreach (Teil t in instance.ListeETeile)
                {
                    t.Aufgeloest = false;
                    if ((instance.GetTeil(t.Nummer) as ETeil).IstEndProdukt == false)
                        (instance.GetTeil(t.Nummer) as ETeil).Puffer = -1;
                }
            }
            pp.Aufloesen();
            if (!lableDazu.Text.Contains("aus der Periode"))
                lableDazu.Text = lableDazu.Text + "aus der Periode " + xml.period;

            this.Information();
            this.toolAusfueren.Visible = false;
            this.save.Visible = true;
            this.Tab.Visible = true;
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
            xmlOeffnen();
        }
        private void dateiÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xmlOeffnen();
        }
        private void xmlOeffnen()
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
            this.panelXML.Visible = true;
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
            instance.GetTeil(1).VertriebPer0 = Convert.ToInt32(upDownAW1.Value);
            instance.GetTeil(2).VertriebPer0 = Convert.ToInt32(upDownAW2.Value);
            instance.GetTeil(3).VertriebPer0 = Convert.ToInt32(upDownAW3.Value);

            (instance.GetTeil(1) as ETeil).Puffer = Convert.ToInt32(pufferP1.Value);
            (instance.GetTeil(2) as ETeil).Puffer = Convert.ToInt32(pufferP2.Value);
            (instance.GetTeil(3) as ETeil).Puffer = Convert.ToInt32(pufferP3.Value);

            instance.GetTeil(1).VerbrauchPer1 = Convert.ToInt32(upDownP11.Value);
            instance.GetTeil(1).VerbrauchPer2 = Convert.ToInt32(upDownP12.Value);
            instance.GetTeil(1).VerbrauchPer3 = Convert.ToInt32(upDownP13.Value);

            instance.GetTeil(2).VerbrauchPer1 = Convert.ToInt32(upDownP21.Value);
            instance.GetTeil(2).VerbrauchPer2 = Convert.ToInt32(upDownP22.Value);
            instance.GetTeil(2).VerbrauchPer3 = Convert.ToInt32(upDownP23.Value);

            instance.GetTeil(3).VerbrauchPer1 = Convert.ToInt32(upDownP31.Value);
            instance.GetTeil(3).VerbrauchPer2 = Convert.ToInt32(upDownP32.Value);
            instance.GetTeil(3).VerbrauchPer3 = Convert.ToInt32(upDownP33.Value);

            this.bildSpeichOk.Visible = true;
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


        private void DataGriedViewRemove(DataGridView dgv)
        {
            if (dgv.DataSource != null)
            {
                dgv.DataSource = null;
            }
            else
            {
                dgv.Rows.Clear();
            }
        }

        private void Information()
        {
            // KTeile
            this.DataGriedViewRemove(dataGridViewKTeil);
            int index = 0;
            foreach (var a in instance.ListeKTeile)
            {
                dataGridViewKTeil.Rows.Add();
                dataGridViewKTeil.Rows[index].Cells[0].Value = a.Nummer;
                dataGridViewKTeil.Rows[index].Cells[1].Value = a.Verwendung + " - " + a.Bezeichnung;
                dataGridViewKTeil.Rows[index].Cells[2].Value = a.Lagerstand;
                dataGridViewKTeil.Rows[index].Cells[3].Value = a.Verhaeltnis + "%";
                if (a.Verhaeltnis < 40)
                    dataGridViewKTeil.Rows[index].Cells[4].Value = imageList1.Images[0];
                else if (a.Verhaeltnis <= 100)
                    dataGridViewKTeil.Rows[index].Cells[4].Value = imageList1.Images[2];
                else
                    dataGridViewKTeil.Rows[index].Cells[4].Value = imageList1.Images[1];
                dataGridViewKTeil.Rows[index].Cells[5].Value = a.LagerZugang;

                //Farbe
                for (int i = 0; i < 6; ++i)
                {
                    if (i == 4)
                        dataGridViewETeil.Columns[i].DefaultCellStyle.BackColor = Color.LightYellow;
                    else
                        dataGridViewKTeil.Columns[i].DefaultCellStyle.BackColor = Color.FloralWhite;
                }
                ++index;
            }

            // Eteile
            this.DataGriedViewRemove(dataGridViewETeil);
            index = 0;
            foreach (var a in instance.ListeETeile)
            {
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
                for (int i = 0; i < 9; ++i)
                {
                    if (i == 4 || i == 7)
                        dataGridViewETeil.Columns[i].DefaultCellStyle.BackColor = Color.LightYellow;
                    else if (i == 8)
                    {
                        dataGridViewETeil.Columns[i].DefaultCellStyle.BackColor = Color.Honeydew;
                    }
                    else
                        dataGridViewETeil.Columns[i].DefaultCellStyle.BackColor = Color.FloralWhite;
                }
                ++index;
            }

            // Arbeitsplätze
            this.DataGriedViewRemove(dataGridViewAPlatz);
            index = 0;
            foreach (var a in instance.ArbeitsplatzList)
            {
                int gesammtZeit = (int)(a.GetRuestZeit * a.RuestungCustom);
                int gesammt = gesammtZeit + a.GetBenoetigteZeit;

                dataGridViewAPlatz.Rows.Add();
                dataGridViewAPlatz.Rows[index].Cells[0].Value = a.GetNummerArbeitsplatz;
                dataGridViewAPlatz.Rows[index].Cells[1].Value = a.Leerzeit + " (" + a.RuestungVorPeriode + ") ";
                dataGridViewAPlatz.Rows[index].Cells[2].Value = a.GetBenoetigteZeit + " min";
                dataGridViewAPlatz.Rows[index].Cells[3].Value = (int)(a.GetRuestZeit * a.RuestungCustom) + " min";
                dataGridViewAPlatz.Rows[index].Cells[4].Value = a.RuestungCustom;
                dataGridViewAPlatz.Rows[index].Cells[5].Value = gesammt + " min";
                if (gesammt <= a.zeit) // newTeim <= 2400 
                    dataGridViewAPlatz.Rows[index].Cells[6].Value = imageList1.Images[2];
                else if (gesammt > instance.ErsteSchicht) // newTime > 3600
                {
                    dataGridViewAPlatz.Rows[index].Cells[6].Value = imageList1.Images[0];
                    dataGridViewAPlatz.Rows[index].Cells[8].Value = true;
                }
                else if (gesammt > a.zeit && gesammt <= instance.ErsteSchicht) // 2400 < newTime < 3600 
                {
                    dataGridViewAPlatz.Rows[index].Cells[6].Value = imageList1.Images[1];
                }
                else
                {
                    dataGridViewAPlatz.Rows[index].Cells[6].Value = imageList1.Images[2];
                }
                dataGridViewAPlatz.Rows[index].Cells[7].Value = a.zeit - gesammt + " min";

                //Farbe
                for (int i = 0; i < 9; ++i)
                {
                    if (i < 2)
                        dataGridViewAPlatz.Columns[i].DefaultCellStyle.BackColor = Color.FloralWhite;
                    else if (i == 2 || i == 3 || (i >= 5 && i <= 8))
                        dataGridViewAPlatz.Columns[i].DefaultCellStyle.BackColor = Color.LightYellow;
                    else
                        dataGridViewAPlatz.Columns[i].DefaultCellStyle.BackColor = Color.Honeydew;
                }

                ++index;
            }
        }

        /// <summary>
        /// EDIT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureEditEteile_Click(object sender, EventArgs e)
        {
            if (rbReserve.Checked == true)
            {
                dataGridViewETeil.Columns[8].DefaultCellStyle.BackColor = Color.LightBlue;
                dataGridViewETeil.Columns[8].ReadOnly = false;
            }
            this.picSaveETeile.Visible = true;
            this.picResetETeil.Visible = true;
            this.picReadOnlyETeile.Visible = true;
        }
        private void picEditAPlatz_Click(object sender, System.EventArgs e)
        {
            dataGridViewAPlatz.Columns[4].DefaultCellStyle.BackColor = Color.LightBlue;
            dataGridViewAPlatz.Columns[4].ReadOnly = false;
            this.picSaveAPlatz.Visible = true;
            this.picResetAPlatz.Visible = true;
            this.picReadOnlyAPlatz.Visible = true;
            this.picEditAPlatz.Visible = false;
        }


        private void pictureSaveETeile_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Wollen Sie sicher was ändern?", "Änderungen", MessageBoxButtons.OKCancel);
            string text = string.Empty;
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                Dictionary<int, string> ids = new Dictionary<int, string>();
                foreach (DataGridViewRow row in dataGridViewETeil.Rows)
                {
                    if (rbReserve.Checked == true)
                    {                        
                        int reserveAlt = (instance.GetTeil(Convert.ToInt32(row.Cells[0].Value.ToString())) as ETeil).Puffer;
                        int reserveNeu = Convert.ToInt32(row.Cells[8].Value.ToString());
                        if (!reserveAlt.Equals(reserveNeu))
                        {
                            ids.Add(Convert.ToInt32(row.Cells[0].Value.ToString()), reserveAlt + ">" + reserveNeu + ">" + row.Cells[1].Value.ToString());
                        }
                    }
                }

                if (ids.Count() > 0)
                {
                    foreach (KeyValuePair<int, string> pair in ids)
                    {
                        string[] change = pair.Value.Split('>');
                        (instance.GetTeil(pair.Key) as ETeil).FeldGeandert(0, Convert.ToInt32(change[1]));
                        text += change[2] + ": von " + change[0] + " auf " + change[1] + "\n";
                    }
                }
                
                this.picEditEteile.Visible = true;
                this.picResetETeil.Visible = false;
                this.picSaveETeile.Visible = false;
                this.picReadOnlyETeile.Visible = false;

                dataGridViewETeil.Columns[8].DefaultCellStyle.BackColor = Color.Honeydew;
                dataGridViewETeil.Columns[8].ReadOnly = true;

                pp.Aufloesen();
                Information();
                if (!text.Equals(string.Empty))
                    result = MessageBox.Show(text, "Message", MessageBoxButtons.OK);
                else
                    result = MessageBox.Show("keine Spalte wurde geändert", "Message", MessageBoxButtons.OK);
            }
        }

        private void pictureResetETeil_Click(object sender, EventArgs e)
        {
            Information();
            if (rbReserve.Checked == true)
            {
                dataGridViewETeil.Columns[8].DefaultCellStyle.BackColor = Color.Honeydew;
            }
        }

        private void pictureReadOnly_Click(object sender, EventArgs e)
        {
            this.picEditEteile.Visible = true;
            this.picResetETeil.Visible = false;
            this.picSaveETeile.Visible = false;
            this.picReadOnlyETeile.Visible = false;

            dataGridViewETeil.Columns[7].DefaultCellStyle.BackColor = Color.Honeydew;
            dataGridViewETeil.Columns[7].ReadOnly = true;
            dataGridViewETeil.Columns[8].DefaultCellStyle.BackColor = Color.Honeydew;
            dataGridViewETeil.Columns[8].ReadOnly = true;
        }

        private void save_Click(object sender, EventArgs e)
        {

        }

        private void gewichtungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Einstellungen einstellungen = new Einstellungen();
            einstellungen.Show();
        }

        private void startSeiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://scsim.de/");
        }

        private void englischToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            englischToolStripMenuItem1.Checked = true;
            deutschToolStripMenuItem1.Checked = false;
        }

        private void deutschToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            englischToolStripMenuItem1.Checked = false;
            deutschToolStripMenuItem1.Checked = true;
        }

        private void schließenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void handbuchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string path = Directory.GetCurrentDirectory() + @"\chm\dv_aspnetmmc.chm";
            //Help.ShowHelp(this, path, HelpNavigator.TableOfContents, "");
        }

        private void cbMitOhne_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbMitOhne.Checked == true)
                instance.BerechneKindTeil = true;
            else
                instance.BerechneKindTeil = false;
        }
    }
}
