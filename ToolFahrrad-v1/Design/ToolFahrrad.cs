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
            for (int i = 0; i < 6; ++i)
            {
                this.dataGridViewKTeil.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
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
                ++index;
            }

            // Eteile
            this.DataGriedViewRemove(dataGridViewETeil);
            index = 0;
            for (int i = 0; i < 8; ++i)
            {
                this.dataGridViewETeil.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
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
                dataGridViewETeil.Rows[index].Cells[7].Value = a.ProduktionsMenge;
                ++index;
            }

            // Arbeitsplätze
            this.DataGriedViewRemove(dataGridViewAPlatz);
            index = 0;
            for (int i = 0; i < 8; ++i)
            {
                this.dataGridViewAPlatz.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            foreach (var a in instance.ArbeitsplatzList)
            {
                dataGridViewAPlatz.Rows.Add();
                dataGridViewAPlatz.Rows[index].Cells[0].Value = a.GetNummerArbeitsplatz;
                dataGridViewAPlatz.Rows[index].Cells[1].Value = a.Leerzeit + " (" + a.AnzRuestung + ") ";
                dataGridViewAPlatz.Rows[index].Cells[3].Value = a.GetBenoetigteZeit;
                dataGridViewAPlatz.Rows[index].Cells[4].Value = a.GetRuestZeit + " (" + a.WerkZeitJeStk.Count() + ") ";
                dataGridViewAPlatz.Rows[index].Cells[5].Value = a.GetBenoetigteZeit + a.GetRuestZeit;
                if ((a.GetRuestZeit + a.GetBenoetigteZeit) <= a.zeit)
                    dataGridViewAPlatz.Rows[index].Cells[6].Value = imageList1.Images[2];
                else if ((a.GetRuestZeit + a.GetBenoetigteZeit) > a.zeit && (a.GetRuestZeit + a.GetBenoetigteZeit) <= (a.zeit + a.zeit / 2))
                    dataGridViewAPlatz.Rows[index].Cells[6].Value = imageList1.Images[1];
                else
                    dataGridViewAPlatz.Rows[index].Cells[6].Value = imageList1.Images[0];
                dataGridViewAPlatz.Rows[index].Cells[7].Value = a.zeit - (a.GetRuestZeit + a.GetBenoetigteZeit);
                ++index;
            }
        }

        private void toolAusfueren_Click(object sender, EventArgs e)
        {
            pp.Aufloesen();
            if (!lableDazu.Text.Contains("aus der Periode"))
                lableDazu.Text = lableDazu.Text + "aus der Periode " + xml.period;

            this.Information();
            this.toolAusfueren.Visible = false;
            this.save.Visible = true;
            this.Tab.Visible = true;
        }

        private void pictureEditEteile_Click(object sender, EventArgs e)
        {
            dataGridViewETeil.Columns[7].DefaultCellStyle.BackColor = Color.LightBlue;
            dataGridViewETeil.Columns[7].ReadOnly = false;
            this.pictureSaveETeile.Visible = true;
            this.pictureResetETeil.Visible = true;
            this.pictureReadOnly.Visible = true;
            this.pictureEditEteile.Visible = false;
        }

        private void pictureSaveETeile_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Wollen Sie sicher was ändern?", "Änderungen", MessageBoxButtons.OKCancel);
            string text = string.Empty;
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                foreach (DataGridViewRow row in dataGridViewETeil.Rows)
                {
                    int prodMengeAlt = (instance.GetTeil(Convert.ToInt32(row.Cells[0].Value.ToString())) as ETeil).ProduktionsMenge;
                    int prodMengeNeu = Convert.ToInt32(row.Cells[7].Value.ToString());
                    if (!prodMengeAlt.Equals(prodMengeNeu))
                    {
                        (instance.GetTeil(Convert.ToInt32(row.Cells[0].Value.ToString())) as ETeil).ProduktionsMenge = prodMengeNeu;
                        text += row.Cells[1].Value.ToString() + ": von " + prodMengeAlt + " auf " + prodMengeNeu + "\n";
                    }
                }

                this.pictureEditEteile.Visible = true;
                this.pictureResetETeil.Visible = false;
                this.pictureSaveETeile.Visible = false;
                this.pictureReadOnly.Visible = false;

                dataGridViewETeil.Columns[7].DefaultCellStyle.BackColor = Color.White;
                dataGridViewETeil.Columns[7].ReadOnly = true;

                Information();
                if(!text.Equals(string.Empty))
                    result = MessageBox.Show(text, "Message", MessageBoxButtons.OK);
                else
                    result = MessageBox.Show("keine Spalte wurde geändert", "Message", MessageBoxButtons.OK);
            }
        }

        private void pictureResetETeil_Click(object sender, EventArgs e)
        {
            Information();
        }

        private void pictureReadOnly_Click(object sender, EventArgs e)
        {
            this.pictureEditEteile.Visible = true;
            this.pictureResetETeil.Visible = false;
            this.pictureSaveETeile.Visible = false;
            this.pictureReadOnly.Visible = false;

            dataGridViewETeil.Columns[7].DefaultCellStyle.BackColor = Color.White;
            dataGridViewETeil.Columns[7].ReadOnly = true;
        }
    }
}
