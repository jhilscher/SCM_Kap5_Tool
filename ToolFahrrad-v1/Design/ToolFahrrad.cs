using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.IO;
using ToolFahrrad_v1.Komponenten;
using ToolFahrrad_v1.Properties;
using ToolFahrrad_v1.Verwaltung;
using ToolFahrrad_v1.Windows;
using ToolFahrrad_v1.XML;

namespace ToolFahrrad_v1.Design
{
    public partial class Fahrrad : Form
    {
        private String _culInfo = Thread.CurrentThread.CurrentUICulture.Name;
        readonly DataContainer _instance = DataContainer.Instance;
        readonly XmlDatei _xml = new XmlDatei();
        readonly Produktionsplanung _pp = new Produktionsplanung();
        readonly Bestellverwaltung _bv = new Bestellverwaltung();
        List<Bestellposition> _bp;
        List<DvPosition> _dirv;
        List<int[]> _xmlAp;
        private bool _bestellungUpdate = false;
        private bool _dvUpdate = false;
        private bool _okPrognose = false;
        private bool _okXml = false;
        private Rectangle _dragBoxSrc;
        private int _rowIndexSrc;
        private int _rowIndexTar;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Fahrrad() {
            InitializeComponent();
        }

        /// <summary>
        /// Ausführen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolAusfueren_Click(object sender, EventArgs e) {
            Ausführen();
            toolAusfueren.Visible = false;
            save.Visible = true;
            tab1.Visible = true;
            tab2.Visible = true;
            panelXMLerstellen.Visible = true;
            arbPlatzAusfueren.Visible = true;
            DataGridViewAP.Visible = true;
            _bestellungUpdate = false;
            _dvUpdate = false;
            xMLexportToolStripMenuItem.Enabled = true;
            xml_export.Visible = true;

            //Periode
            if (_culInfo.Contains("de")) {
                aktulleWoche.Text = Resources.Fahrrad_toolAusfueren_Click_Periode_ + _xml.Period;
                prognose1.Text = Resources.Fahrrad_toolAusfueren_Click_Periode_ + (Convert.ToInt32(_xml.Period) + 1);
                prognose2.Text = Resources.Fahrrad_toolAusfueren_Click_Periode_ + (Convert.ToInt32(_xml.Period) + 2);
                prognose3.Text = Resources.Fahrrad_toolAusfueren_Click_Periode_ + (Convert.ToInt32(_xml.Period) + 3);
                GetInfo("Tool wurde erfolgreich ausgeführt");
            }
            else {
                aktulleWoche.Text = Resources.Fahrrad_toolAusfueren_Click_Periode_en + _xml.Period;
                prognose1.Text = Resources.Fahrrad_toolAusfueren_Click_Periode_en + (Convert.ToInt32(_xml.Period) + 1);
                prognose2.Text = Resources.Fahrrad_toolAusfueren_Click_Periode_en + (Convert.ToInt32(_xml.Period) + 2);
                prognose3.Text = Resources.Fahrrad_toolAusfueren_Click_Periode_en + (Convert.ToInt32(_xml.Period) + 3);
                GetInfo("tool was successful");

            }
        }
        private void Ausführen() {
            _bv.ClearBvPositionen();
            if ((_instance.GetTeil(4) as ETeil).Puffer != -1) {
                foreach (ETeil t in _instance.ListeETeile) {
                    t.Aufgeloest = false;
                    t.KdhUpdate = false;
                    t.InBearbeitung = 0;
                    t.InWartschlange = 0;
                    t.KdhProduktionsmenge = new Dictionary<string, int>();
                    if ((_instance.GetTeil(t.Nummer) as ETeil).IstEndProdukt == false) {
                        (_instance.GetTeil(t.Nummer) as ETeil).Puffer = -1;
                        (_instance.GetTeil(t.Nummer) as ETeil).KDHaufNULL();
                    }
                }
            }
            _pp.AktPeriode = Convert.ToInt32(_xml.Period);
            _pp.Aufloesen();
            foreach (Arbeitsplatz a in _instance.ArbeitsplatzList) {
                a.Geaendert = false;
                a.RuestNew = 0;
                a.RuestungCustom = 0;
            }
            for (int index = 1; index < 4; ++index) {
                DispositionDarstellung(index);
            }
            Information();
            XmlVorbereitung(100); // 100=all
        }

        /// <summary>
        /// Informationsdarstellung
        /// </summary> 
        //TODO: Information()
        private void Information() {
            // KTeile
            #region KTEILE
            //Bruttobedarf
            foreach (KTeil k in _instance.ListeKTeile) {
                k.BruttoBedarfPer0 = 0;
                k.BruttoBedarfPer1 = 0;
                k.BruttoBedarfPer2 = 0;
                k.BruttoBedarfPer3 = 0;
            }
            for (int i = 1; i < 4; ++i) {
                _pp.RekursAufloesenKTeile(i, null, _instance.GetTeil(i) as ETeil);
            }

            DataGriedViewRemove(dataGridViewKTeil);
            int index = 0;
            for (int i = 4; i < 8; ++i) {
                dataGridViewKTeil.Columns[i].HeaderText = Resources.Fahrrad_Information_P + (Convert.ToInt32(_xml.Period) + (i - 4));
            }

            dataGridViewKTeil.Columns[8].HeaderText = Resources.Fahrrad_Information_B + ((Convert.ToInt32(_xml.Period)) + 1);
            dataGridViewKTeil.Columns[9].HeaderText = Resources.Fahrrad_Information_B + ((Convert.ToInt32(_xml.Period)) + 2);
            dataGridViewKTeil.Columns[10].HeaderText = Resources.Fahrrad_Information_B + (Convert.ToInt32(_xml.Period) + 3);
            dataGridViewKTeil.Columns[11].HeaderText = Resources.Fahrrad_Information_B + (Convert.ToInt32(_xml.Period) + 4);

            foreach (var a in _instance.ListeKTeile) {
                //Lagerzugang berechnen
                List<List<int>> list = a.OffeneBestellungen;
                int lagerZugang = list.Sum(l => l[2]);

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
                if (a.BestandPer1 < 0) {
                    dataGridViewKTeil.Rows[index].Cells[8].Style.ForeColor = Color.Red;
                    dataGridViewKTeil.Rows[index].Cells[8].Style.Font = new Font(dataGridViewKTeil.Rows[index].Cells[8].InheritedStyle.Font, FontStyle.Bold);
                }
                dataGridViewKTeil.Rows[index].Cells[9].Value = a.BestandPer2;
                if (a.BestandPer2 < 0) {
                    dataGridViewKTeil.Rows[index].Cells[9].Style.ForeColor = Color.Red;
                    dataGridViewKTeil.Rows[index].Cells[9].Style.Font = new Font(dataGridViewKTeil.Rows[index].Cells[9].InheritedStyle.Font, FontStyle.Bold);
                }
                dataGridViewKTeil.Rows[index].Cells[10].Value = a.BestandPer3;
                if (a.BestandPer3 < 0) {
                    dataGridViewKTeil.Rows[index].Cells[10].Style.ForeColor = Color.Red;
                    dataGridViewKTeil.Rows[index].Cells[10].Style.Font = new Font(dataGridViewKTeil.Rows[index].Cells[10].InheritedStyle.Font, FontStyle.Bold);
                }
                dataGridViewKTeil.Rows[index].Cells[11].Value = a.BestandPer4;
                if (a.BestandPer4 < 0) {
                    dataGridViewKTeil.Rows[index].Cells[11].Style.ForeColor = Color.Red;
                    dataGridViewKTeil.Rows[index].Cells[11].Style.Font = new Font(dataGridViewKTeil.Rows[index].Cells[11].InheritedStyle.Font, FontStyle.Bold);
                }
                //Farbe
                for (int i = 0; i < 12; ++i) {
                    if (i >= 0 && i < 4)
                        dataGridViewKTeil.Columns[i].DefaultCellStyle.BackColor = Color.FloralWhite;
                    else if (i > 3 && i < 8)
                        dataGridViewKTeil.Columns[i].DefaultCellStyle.BackColor = Color.Honeydew;
                }
                ++index;
            }
            #endregion
            // Eteile
            #region ETeile
            DataGriedViewRemove(dataGridViewETeil);
            index = 0;
            foreach (var a in _instance.ListeETeile) {
                dataGridViewETeil.Rows.Add();
                dataGridViewETeil.Rows[index].Cells[0].Value = a.Nummer;
                dataGridViewETeil.Rows[index].Cells[1].Value = a.Verwendung + " - " + a.Bezeichnung;
                dataGridViewETeil.Rows[index].Cells[2].Value = a.Lagerstand;
                dataGridViewETeil.Rows[index].Cells[3].Value = a.Verhaeltnis + "%";
                if (a.Verhaeltnis < 40)
                    dataGridViewETeil.Rows[index].Cells[4].Value = imageListAmpel.Images[0];
                else if (a.Verhaeltnis <= 100)
                    dataGridViewETeil.Rows[index].Cells[4].Value = imageListAmpel.Images[2];
                else
                    dataGridViewETeil.Rows[index].Cells[4].Value = imageListAmpel.Images[1];
                dataGridViewETeil.Rows[index].Cells[5].Value = a.InWartschlange;
                dataGridViewETeil.Rows[index].Cells[6].Value = a.InBearbeitung;
                if (!a.Verwendung.Equals("KDH"))
                    dataGridViewETeil.Rows[index].Cells[7].Value = a.ProduktionsMengePer0;
                else {
                    int prodMenge = a.KdhProduktionsmenge.Sum(pair => pair.Value);
                    dataGridViewETeil.Rows[index].Cells[7].Value = prodMenge;
                }

                //Farbe
                for (int i = 0; i < 8; ++i) {
                    dataGridViewETeil.Columns[i].DefaultCellStyle.BackColor = i == 7 ? Color.Honeydew : Color.FloralWhite;
                }
                if (a.Verwendung.Equals("KDH")) {
                    dataGridViewETeil.Rows[index].DefaultCellStyle.BackColor = Color.LemonChiffon;
                }

                ++index;
            }
            #endregion
            // Arbeitsplätze
            #region Arbetsplatz
            DataGriedViewRemove(DataGridViewAP);
            index = 0;
            _xmlAp = new List<int[]>();
            foreach (var a in _instance.ArbeitsplatzList) {
                var apXml = new int[3];


                DataGridViewAP.Rows.Add();
                DataGridViewAP.Rows[index].Cells[4].Value = imageListPlusMinus.Images[0];
                DataGridViewAP.Rows[index].Cells[5].Value = imageListPlusMinus.Images[1];
                DataGridViewAP.Rows[index].Cells[0].Value = a.GetNummerArbeitsplatz;
                apXml[0] = a.GetNummerArbeitsplatz;
                DataGridViewAP.Rows[index].Cells[1].Value = a.Leerzeit + " (" + a.RuestungVorPeriode + ") ";
                var prMenge = 0;
                var sum = 0;
                foreach (KeyValuePair<int, int> kvp in a.WerkZeiten) {
                    {
                        if (!(_instance.GetTeil(kvp.Key) as ETeil).Verwendung.Contains("KDH")) {
                            prMenge += (_instance.GetTeil(kvp.Key) as ETeil).ProduktionsMengePer0;
                        }
                        else {
                            prMenge += (_instance.GetTeil(kvp.Key) as ETeil).KdhProduktionsmenge.Sum(pair => pair.Value);
                        }
                        if (prMenge < 0)
                            prMenge = 0;
                        sum += kvp.Value * prMenge;
                        prMenge = 0;
                    }
                }
                DataGridViewAP.Rows[index].Cells[2].Value = sum + " min";

                prMenge = 0;
                int val = 0;
                string key = "";
                if (a.Geaendert == false) {
                    foreach (KeyValuePair<int, int> kvp in a.RuestZeiten) {
                        if (!(_instance.GetTeil(kvp.Key) as ETeil).Verwendung.Contains("KDH")) {
                            if ((_instance.GetTeil(kvp.Key) as ETeil).ProduktionsMengePer0 >= 0)
                                val += kvp.Value;
                        }
                        else {
                            foreach (KeyValuePair<string, int> pair in (_instance.GetTeil(kvp.Key) as ETeil).KdhProduktionsmenge) {
                                prMenge += pair.Value;
                                string[] split = pair.Key.Split('-');
                                if (prMenge >= 0) {
                                    if (key != split[1] || val == 0)
                                        val += kvp.Value;
                                }
                                key = split[1];
                            }
                        }
                    }
                    a.Geaendert = true;
                    a.RuestungCustom = val;
                }

                DataGridViewAP.Rows[index].Cells[3].Value = a.RuestungCustom;
                int gesammt = a.RuestungCustom + sum;
                DataGridViewAP.Rows[index].Cells[6].Value = gesammt + " min";
                DataGridViewAP.Rows[index].Cells[10].Value = imageListAmpel.Images[2];

                if (gesammt <= a.ZeitErsteSchicht) { // newTeim <= 2400 
                    DataGridViewAP.Rows[index].Cells[7].Value = imageListAmpel.Images[2];
                    apXml[1] = 1;
                    apXml[2] = 0;
                }
                else if (gesammt > _instance.ErsteSchichtMitUeberStunden) // gesammt > 3600
                {
                    if (gesammt > _instance.ZweiteSchichtMitUeberstunden) {   // gesammt > 6000
                        if (gesammt > 7200)
                            DataGridViewAP.Rows[index].Cells[10].Value = imageListAmpel.Images[0]; //gesammt > 7200
                        else if (gesammt < 7200)
                            DataGridViewAP.Rows[index].Cells[10].Value = imageListAmpel.Images[1]; //gesammt < 7200
                        else
                            DataGridViewAP.Rows[index].Cells[10].Value = imageListAmpel.Images[2]; //gesammt = 7200
                        apXml[1] = 3; // 3 SCHICHT
                        if (gesammt < 7200)
                            apXml[2] = gesammt - a.ZeitZweiteSchicht;
                        else
                            apXml[2] = 7200 - a.ZeitZweiteSchicht;
                    }
                    if (gesammt < _instance.ZweiteSchichtMitUeberstunden) {
                        DataGridViewAP.Rows[index].Cells[7].Value = imageListAmpel.Images[0];
                        DataGridViewAP.Rows[index].Cells[8].Value = true;
                        if (gesammt > a.ZeitZweiteSchicht)
                            apXml[2] = gesammt - a.ZeitZweiteSchicht;
                        else
                            apXml[2] = 0;
                        apXml[1] = 2;
                    }
                    else if (gesammt > _instance.ZweiteSchichtMitUeberstunden) {
                        DataGridViewAP.Rows[index].Cells[7].Value = imageListAmpel.Images[0];
                        DataGridViewAP.Rows[index].Cells[9].Value = true;
                        apXml[1] = 3;
                        apXml[2] = gesammt - _instance.ZweiteSchichtMitUeberstunden;
                    }
                }
                else if (gesammt > a.ZeitErsteSchicht && gesammt <= _instance.ErsteSchichtMitUeberStunden) { // 2400 < newTime < 3600 Überstunden
                    DataGridViewAP.Rows[index].Cells[7].Value = imageListAmpel.Images[1];
                    apXml[1] = 1;
                    apXml[2] = gesammt - a.ZeitErsteSchicht;
                }
                else {
                    DataGridViewAP.Rows[index].Cells[7].Value = imageListAmpel.Images[2];
                }

                _xmlAp.Add(apXml);
                //Farbe
                for (int i = 0; i < 11; ++i) {
                    if (i < 2)
                        DataGridViewAP.Columns[i].DefaultCellStyle.BackColor = Color.FloralWhite;
                    else if (i == 2 || i > 3)
                        DataGridViewAP.Columns[i].DefaultCellStyle.BackColor = Color.LightYellow;
                }
                ++index;
            }
            _instance.ApKapazitaet = _xmlAp;
            #endregion

            //Bestellung
            #region Bestellung
            DataGriedViewRemove(dataGridViewBestellung);
            if (_bestellungUpdate == false)
                _bv.GeneriereBestellListe();
            var bp = _bv.BvPositionen;
            index = 0;
            foreach (var a in bp) {
                dataGridViewBestellung.Rows.Add();
                dataGridViewBestellung.Rows[index].Cells[0].Value = a.Kaufteil.Nummer;
                dataGridViewBestellung.Rows[index].Cells[1].Value = a.Menge;
                if (a.Eil) {
                    dataGridViewBestellung.Rows[index].Cells[2].Value = true;
                }

                //Farbe
                for (int i = 0; i < 4; ++i) {
                    dataGridViewBestellung.Columns[i].DefaultCellStyle.BackColor = i == 0 ? Color.FloralWhite : Color.LightYellow;
                }
                ++index;
            }
            #endregion

            #region Direktverkauf
            DataGriedViewRemove(dataGridViewDirektverkauf);
            if (_dvUpdate == false)
                _bv.GeneriereListeDv();
            List<DvPosition> dv = _bv.DvPositionen;
            index = 0;
            foreach (var a in dv) {
                dataGridViewDirektverkauf.Rows.Add();
                dataGridViewDirektverkauf.Rows[index].Cells[0].Value = a.DvTeilNr;
                dataGridViewDirektverkauf.Rows[index].Cells[1].Value = a.DvMenge;
                dataGridViewDirektverkauf.Rows[index].Cells[2].Value = a.DvPreis;
                dataGridViewDirektverkauf.Rows[index].Cells[3].Value = a.DvStrafe;

                ++index;
            }
            #endregion

            //Produktionsaufträge
            #region Produktionsaufträge
            DataGriedViewRemove(dataGridViewProduktAuftrag);
            _pp.OptimiereProdListe();
            Dictionary<int, int> prList = _pp.ProdListe;
            index = 0;
            foreach (var i in prList) {
                dataGridViewProduktAuftrag.Rows.Add();
                dataGridViewProduktAuftrag.Rows[index].Cells[0].Value = i.Key;
                dataGridViewProduktAuftrag.Rows[index].Cells[1].Value = i.Value;
                ++index;
            }

            #endregion

        }

        /// <summary>
        /// Dropdownmenu Startseite PROGNOSE
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

        private void prognoseSpeichern_Click(object sender, EventArgs e) {
            _instance.GetTeil(1).VertriebPer0 = Convert.ToInt32(upDownAW1.Value);
            _instance.GetTeil(2).VertriebPer0 = Convert.ToInt32(upDownAW2.Value);
            _instance.GetTeil(3).VertriebPer0 = Convert.ToInt32(upDownAW3.Value);

            (_instance.GetTeil(1) as ETeil).Puffer = Convert.ToInt32(pufferP1.Value);
            (_instance.GetTeil(2) as ETeil).Puffer = Convert.ToInt32(pufferP2.Value);
            (_instance.GetTeil(3) as ETeil).Puffer = Convert.ToInt32(pufferP3.Value);

            _instance.GetTeil(1).VerbrauchPer1 = Convert.ToInt32(upDownP11.Value);
            _instance.GetTeil(1).VerbrauchPer2 = Convert.ToInt32(upDownP21.Value);
            _instance.GetTeil(1).VerbrauchPer3 = Convert.ToInt32(upDownP31.Value);

            _instance.GetTeil(2).VerbrauchPer1 = Convert.ToInt32(upDownP12.Value);
            _instance.GetTeil(2).VerbrauchPer2 = Convert.ToInt32(upDownP22.Value);
            _instance.GetTeil(2).VerbrauchPer3 = Convert.ToInt32(upDownP32.Value);

            _instance.GetTeil(3).VerbrauchPer1 = Convert.ToInt32(upDownP13.Value);
            _instance.GetTeil(3).VerbrauchPer2 = Convert.ToInt32(upDownP23.Value);
            _instance.GetTeil(3).VerbrauchPer3 = Convert.ToInt32(upDownP33.Value);

            bildSpeichOk.Visible = true;
            panelXML.Visible = true;
            _okPrognose = true;
            if (_okXml)
                toolAusfueren.Visible = true;

            GetInfo(_culInfo.Contains("de") ? "Prognose wurde gespeichert" : "Forecast has been saved");
        }

        /// <summary>
        /// Dispositionsdarstellung
        /// </summary>
        /// <param name="index"></param>
        private void DispositionDarstellung(int index) {
            _bv.ClearBvPositionen();
            ETeil et;
            int n;
            if (index == 1) {
                #region P1
                //P1
                n = 1;
                et = _instance.GetTeil(n) as ETeil;
                p1vw_0.Text = et.VertriebPer0.ToString();
                p1r_0.Text = et.Puffer.ToString();
                p1ls_0.Text = et.Lagerstand.ToString();
                p1iws_0.Text = et.InWartschlange.ToString();
                p1ib_0.Text = et.InBearbeitung.ToString();
                p1pm_0.Text = et.ProduktionsMengePer0.ToString();
                //26
                n = 26;
                et = _instance.GetTeil(n) as ETeil;
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
                et = _instance.GetTeil(n) as ETeil;
                p1vw_51.Text = et.VertriebPer0.ToString();
                p1plus_51.Text = et.VaterInWarteschlange.ToString();
                p1r_51.Text = et.Puffer.ToString();
                p1ls_51.Text = et.Lagerstand.ToString();
                p1iws_51.Text = et.InWartschlange.ToString();
                p1ib_51.Text = et.InBearbeitung.ToString();
                p1pm_51.Text = et.ProduktionsMengePer0.ToString();
                //16
                n = 16;
                et = _instance.GetTeil(n) as ETeil;
                p1vw_16.Text = p1pm_51.Text;
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
                et = _instance.GetTeil(n) as ETeil;
                p1vw_17.Text = p1pm_51.Text;
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
                et = _instance.GetTeil(n) as ETeil;
                p1vw_50.Text = et.VertriebPer0.ToString();
                p1plus_50.Text = et.VaterInWarteschlange.ToString();
                p1r_50.Text = et.Puffer.ToString();
                p1ls_50.Text = et.Lagerstand.ToString();
                p1iws_50.Text = et.InWartschlange.ToString();
                p1ib_50.Text = et.InBearbeitung.ToString();
                p1pm_50.Text = et.ProduktionsMengePer0.ToString();
                //4
                n = 4;
                et = _instance.GetTeil(n) as ETeil;
                p1vw_4.Text = et.VertriebPer0.ToString();
                p1plus_4.Text = et.VaterInWarteschlange.ToString();
                p1r_4.Text = et.Puffer.ToString();
                p1ls_4.Text = et.Lagerstand.ToString();
                p1iws_4.Text = et.InWartschlange.ToString();
                p1ib_4.Text = et.InBearbeitung.ToString();
                p1pm_4.Text = et.ProduktionsMengePer0.ToString();
                //10
                n = 10;
                et = _instance.GetTeil(n) as ETeil;
                p1vw_10.Text = et.VertriebPer0.ToString();
                p1plus_10.Text = et.VaterInWarteschlange.ToString();
                p1r_10.Text = et.Puffer.ToString();
                p1ls_10.Text = et.Lagerstand.ToString();
                p1iws_10.Text = et.InWartschlange.ToString();
                p1ib_10.Text = et.InBearbeitung.ToString();
                p1pm_10.Text = et.ProduktionsMengePer0.ToString();
                //49
                n = 49;
                et = _instance.GetTeil(n) as ETeil;
                p1vw_49.Text = et.VertriebPer0.ToString();
                p1plus_49.Text = et.VaterInWarteschlange.ToString();
                p1r_49.Text = et.Puffer.ToString();
                p1ls_49.Text = et.Lagerstand.ToString();
                p1iws_49.Text = et.InWartschlange.ToString();
                p1ib_49.Text = et.InBearbeitung.ToString();
                p1pm_49.Text = et.ProduktionsMengePer0.ToString();
                //7
                n = 7;
                et = _instance.GetTeil(n) as ETeil;
                p1vw_7.Text = et.VertriebPer0.ToString();
                p1plus_7.Text = et.VaterInWarteschlange.ToString();
                p1r_7.Text = et.Puffer.ToString();
                p1ls_7.Text = et.Lagerstand.ToString();
                p1iws_7.Text = et.InWartschlange.ToString();
                p1ib_7.Text = et.InBearbeitung.ToString();
                p1pm_7.Text = et.ProduktionsMengePer0.ToString();
                //13
                n = 13;
                et = _instance.GetTeil(n) as ETeil;
                p1vw_13.Text = et.VertriebPer0.ToString();
                p1plus_13.Text = et.VaterInWarteschlange.ToString();
                p1r_13.Text = et.Puffer.ToString();
                p1ls_13.Text = et.Lagerstand.ToString();
                p1iws_13.Text = et.InWartschlange.ToString();
                p1ib_13.Text = et.InBearbeitung.ToString();
                p1pm_13.Text = et.ProduktionsMengePer0.ToString();
                //18
                n = 18;
                et = _instance.GetTeil(n) as ETeil;
                p1vw_18.Text = et.VertriebPer0.ToString();
                p1plus_18.Text = et.VaterInWarteschlange.ToString();
                p1r_18.Text = et.Puffer.ToString();
                p1ls_18.Text = et.Lagerstand.ToString();
                p1iws_18.Text = et.InWartschlange.ToString();
                p1ib_18.Text = et.InBearbeitung.ToString();
                p1pm_18.Text = et.ProduktionsMengePer0.ToString();

                #endregion
            }
            if (index == 2) {
                #region P2
                //P2
                n = 2;
                et = _instance.GetTeil(n) as ETeil;
                p2vw_0.Text = et.VertriebPer0.ToString();
                p2r_0.Text = et.Puffer.ToString();
                p2ls_0.Text = et.Lagerstand.ToString();
                p2iws_0.Text = et.InWartschlange.ToString();
                p2ib_0.Text = et.InBearbeitung.ToString();
                p2pm_0.Text = et.ProduktionsMengePer0.ToString();
                //26
                n = 26;
                et = _instance.GetTeil(n) as ETeil;
                p2vw_26.Text = p2pm_0.Text;
                p2plus_26.Text = p2iws_0.Text;
                if (et.KdhPuffer.ContainsKey(n)) {
                    p2r_26.Text = et.KdhPuffer[n][index - 1].ToString();
                }
                if (et.KdhProduktionsmenge.ContainsKey(index.ToString() + "-" + Convert.ToString(n))) {
                    p2pm_26.Text = et.KdhProduktionsmenge[index.ToString() + "-" + Convert.ToString(n)].ToString();
                }
                //56
                n = 56;
                et = _instance.GetTeil(n) as ETeil;
                p2vw_56.Text = et.VertriebPer0.ToString();
                p2plus_56.Text = et.VaterInWarteschlange.ToString();
                p2r_56.Text = et.Puffer.ToString();
                p2ls_56.Text = et.Lagerstand.ToString();
                p2iws_56.Text = et.InWartschlange.ToString();
                p2ib_56.Text = et.InBearbeitung.ToString();
                p2pm_56.Text = et.ProduktionsMengePer0.ToString();
                //16
                n = 16;
                et = _instance.GetTeil(n) as ETeil;
                p2vw_16.Text = p2pm_56.Text;
                p2plus_16.Text = p2iws_56.Text;
                if (et.KdhPuffer.ContainsKey(n)) {
                    p2r_16.Text = et.KdhPuffer[n][index - 1].ToString();
                }
                if (et.KdhProduktionsmenge.ContainsKey(index.ToString() + "-" + Convert.ToString(n))) {
                    p2pm_16.Text = et.KdhProduktionsmenge[index.ToString() + "-" + Convert.ToString(n)].ToString();
                }
                //17
                n = 17;
                et = _instance.GetTeil(n) as ETeil;
                p2vw_17.Text = p2pm_56.Text;
                p2plus_17.Text = p2iws_56.Text;
                if (et.KdhPuffer.ContainsKey(n)) {
                    p2r_17.Text = et.KdhPuffer[n][index - 1].ToString();
                }
                if (et.KdhProduktionsmenge.ContainsKey(index.ToString() + "-" + Convert.ToString(n))) {
                    p2pm_17.Text = et.KdhProduktionsmenge[index.ToString() + "-" + Convert.ToString(n)].ToString();
                }
                //55
                n = 55;
                et = _instance.GetTeil(n) as ETeil;
                p2vw_55.Text = et.VertriebPer0.ToString();
                p2plus_55.Text = et.VaterInWarteschlange.ToString();
                p2r_55.Text = et.Puffer.ToString();
                p2ls_55.Text = et.Lagerstand.ToString();
                p2iws_55.Text = et.InWartschlange.ToString();
                p2ib_55.Text = et.InBearbeitung.ToString();
                p2pm_55.Text = et.ProduktionsMengePer0.ToString();
                //5
                n = 5;
                et = _instance.GetTeil(n) as ETeil;
                p2vw_5.Text = et.VertriebPer0.ToString();
                p2plus_5.Text = et.VaterInWarteschlange.ToString();
                p2r_5.Text = et.Puffer.ToString();
                p2ls_5.Text = et.Lagerstand.ToString();
                p2iws_5.Text = et.InWartschlange.ToString();
                p2ib_5.Text = et.InBearbeitung.ToString();
                p2pm_5.Text = et.ProduktionsMengePer0.ToString();
                //11
                n = 11;
                et = _instance.GetTeil(n) as ETeil;
                p2vw_11.Text = et.VertriebPer0.ToString();
                p2plus_11.Text = et.VaterInWarteschlange.ToString();
                p2r_11.Text = et.Puffer.ToString();
                p2ls_11.Text = et.Lagerstand.ToString();
                p2iws_11.Text = et.InWartschlange.ToString();
                p2ib_11.Text = et.InBearbeitung.ToString();
                p2pm_11.Text = et.ProduktionsMengePer0.ToString();
                //54
                n = 54;
                et = _instance.GetTeil(n) as ETeil;
                p2vw_54.Text = et.VertriebPer0.ToString();
                p2plus_54.Text = et.VaterInWarteschlange.ToString();
                p2r_54.Text = et.Puffer.ToString();
                p2ls_54.Text = et.Lagerstand.ToString();
                p2iws_54.Text = et.InWartschlange.ToString();
                p2ib_54.Text = et.InBearbeitung.ToString();
                p2pm_54.Text = et.ProduktionsMengePer0.ToString();
                //8
                n = 8;
                et = _instance.GetTeil(n) as ETeil;
                p2vw_8.Text = et.VertriebPer0.ToString();
                p2plus_8.Text = et.VaterInWarteschlange.ToString();
                p2r_8.Text = et.Puffer.ToString();
                p2ls_8.Text = et.Lagerstand.ToString();
                p2iws_8.Text = et.InWartschlange.ToString();
                p2ib_8.Text = et.InBearbeitung.ToString();
                p2pm_8.Text = et.ProduktionsMengePer0.ToString();
                //14
                n = 14;
                et = _instance.GetTeil(n) as ETeil;
                p2vw_14.Text = et.VertriebPer0.ToString();
                p2plus_14.Text = et.VaterInWarteschlange.ToString();
                p2r_14.Text = et.Puffer.ToString();
                p2ls_14.Text = et.Lagerstand.ToString();
                p2iws_14.Text = et.InWartschlange.ToString();
                p2ib_14.Text = et.InBearbeitung.ToString();
                p2pm_14.Text = et.ProduktionsMengePer0.ToString();
                //19
                n = 19;
                et = _instance.GetTeil(n) as ETeil;
                p2vw_19.Text = et.VertriebPer0.ToString();
                p2plus_19.Text = et.VaterInWarteschlange.ToString();
                p2r_19.Text = et.Puffer.ToString();
                p2ls_19.Text = et.Lagerstand.ToString();
                p2iws_19.Text = et.InWartschlange.ToString();
                p2ib_19.Text = et.InBearbeitung.ToString();
                p2pm_19.Text = et.ProduktionsMengePer0.ToString();
                #endregion
            }

            if (index == 3) {
                #region P3
                //p3
                n = 3;
                et = _instance.GetTeil(n) as ETeil;
                p3vw_0.Text = et.VertriebPer0.ToString();
                p3r_0.Text = et.Puffer.ToString();
                p3ls_0.Text = et.Lagerstand.ToString();
                p3iws_0.Text = et.InWartschlange.ToString();
                p3ib_0.Text = et.InBearbeitung.ToString();
                p3pm_0.Text = et.ProduktionsMengePer0.ToString();
                //26
                n = 26;
                et = _instance.GetTeil(n) as ETeil;
                p3vw_26.Text = p3pm_0.Text;
                p3plus_26.Text = p3iws_0.Text;
                if (et.KdhPuffer.ContainsKey(n)) {
                    p3r_26.Text = et.KdhPuffer[n][index - 1].ToString();
                }
                if (et.KdhProduktionsmenge.ContainsKey(index.ToString() + "-" + Convert.ToString(n))) {
                    p3pm_26.Text = et.KdhProduktionsmenge[index.ToString() + "-" + Convert.ToString(n)].ToString();
                }
                //31
                n = 31;
                et = _instance.GetTeil(n) as ETeil;
                p3vw_31.Text = et.VertriebPer0.ToString();
                p3plus_31.Text = et.VaterInWarteschlange.ToString();
                p3r_31.Text = et.Puffer.ToString();
                p3ls_31.Text = et.Lagerstand.ToString();
                p3iws_31.Text = et.InWartschlange.ToString();
                p3ib_31.Text = et.InBearbeitung.ToString();
                p3pm_31.Text = et.ProduktionsMengePer0.ToString();
                //16
                n = 16;
                et = _instance.GetTeil(n) as ETeil;
                p3vw_16.Text = p3pm_31.Text;
                p3plus_16.Text = p3iws_31.Text;
                if (et.KdhPuffer.ContainsKey(n)) {
                    p3r_16.Text = et.KdhPuffer[n][index - 1].ToString();
                }
                if (et.KdhProduktionsmenge.ContainsKey(index.ToString() + "-" + Convert.ToString(n))) {
                    p3pm_16.Text = et.KdhProduktionsmenge[index.ToString() + "-" + Convert.ToString(n)].ToString();
                }
                //17
                n = 17;
                et = _instance.GetTeil(n) as ETeil;
                p3vw_17.Text = p3pm_0.Text;
                p3plus_17.Text = p3iws_0.Text;
                if (et.KdhPuffer.ContainsKey(n)) {
                    p3r_17.Text = et.KdhPuffer[n][index - 1].ToString();
                }
                if (et.KdhProduktionsmenge.ContainsKey(index.ToString() + "-" + Convert.ToString(n))) {
                    p3pm_17.Text = et.KdhProduktionsmenge[index.ToString() + "-" + Convert.ToString(n)].ToString();
                }
                //30
                n = 30;
                et = _instance.GetTeil(n) as ETeil;
                p3vw_30.Text = et.VertriebPer0.ToString();
                p3plus_30.Text = et.VaterInWarteschlange.ToString();
                p3r_30.Text = et.Puffer.ToString();
                p3ls_30.Text = et.Lagerstand.ToString();
                p3iws_30.Text = et.InWartschlange.ToString();
                p3ib_30.Text = et.InBearbeitung.ToString();
                p3pm_30.Text = et.ProduktionsMengePer0.ToString();
                //6
                n = 6;
                et = _instance.GetTeil(n) as ETeil;
                p3vw_6.Text = et.VertriebPer0.ToString();
                p3plus_6.Text = et.VaterInWarteschlange.ToString();
                p3r_6.Text = et.Puffer.ToString();
                p3ls_6.Text = et.Lagerstand.ToString();
                p3iws_6.Text = et.InWartschlange.ToString();
                p3ib_6.Text = et.InBearbeitung.ToString();
                p3pm_6.Text = et.ProduktionsMengePer0.ToString();
                //12
                n = 12;
                et = _instance.GetTeil(n) as ETeil;
                p3vw_12.Text = et.VertriebPer0.ToString();
                p3plus_12.Text = et.VaterInWarteschlange.ToString();
                p3r_12.Text = et.Puffer.ToString();
                p3ls_12.Text = et.Lagerstand.ToString();
                p3iws_12.Text = et.InWartschlange.ToString();
                p3ib_12.Text = et.InBearbeitung.ToString();
                p3pm_12.Text = et.ProduktionsMengePer0.ToString();
                //29
                n = 29;
                et = _instance.GetTeil(n) as ETeil;
                p3vw_29.Text = et.VertriebPer0.ToString();
                p3plus_29.Text = et.VaterInWarteschlange.ToString();
                p3r_29.Text = et.Puffer.ToString();
                p3ls_29.Text = et.Lagerstand.ToString();
                p3iws_29.Text = et.InWartschlange.ToString();
                p3ib_29.Text = et.InBearbeitung.ToString();
                p3pm_29.Text = et.ProduktionsMengePer0.ToString();
                //9
                n = 9;
                et = _instance.GetTeil(n) as ETeil;
                p3vw_9.Text = et.VertriebPer0.ToString();
                p3plus_9.Text = et.VaterInWarteschlange.ToString();
                p3r_9.Text = et.Puffer.ToString();
                p3ls_9.Text = et.Lagerstand.ToString();
                p3iws_9.Text = et.InWartschlange.ToString();
                p3ib_9.Text = et.InBearbeitung.ToString();
                p3pm_9.Text = et.ProduktionsMengePer0.ToString();
                //15
                n = 15;
                et = _instance.GetTeil(n) as ETeil;
                p3vw_15.Text = et.VertriebPer0.ToString();
                p3plus_15.Text = et.VaterInWarteschlange.ToString();
                p3r_15.Text = et.Puffer.ToString();
                p3ls_15.Text = et.Lagerstand.ToString();
                p3iws_15.Text = et.InWartschlange.ToString();
                p3ib_15.Text = et.InBearbeitung.ToString();
                p3pm_15.Text = et.ProduktionsMengePer0.ToString();
                //20
                n = 20;
                et = _instance.GetTeil(n) as ETeil;
                p3vw_20.Text = et.VertriebPer0.ToString();
                p3plus_20.Text = et.VaterInWarteschlange.ToString();
                p3r_20.Text = et.Puffer.ToString();
                p3ls_20.Text = et.Lagerstand.ToString();
                p3iws_20.Text = et.InWartschlange.ToString();
                p3ib_20.Text = et.InBearbeitung.ToString();
                p3pm_20.Text = et.ProduktionsMengePer0.ToString();
                #endregion
            }
        }

        /// <summary>
        /// XML-Bearbeitung INPUT/OUTPUT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xml_suchen_Click(object sender, EventArgs e) {
            XmlOeffnen();
        }
        private void dateiÖffnenToolStripMenuItem_Click(object sender, EventArgs e) {
            XmlOeffnen();
        }
        private void XmlOeffnen() {
            var fileDialog = new OpenFileDialog { Filter = Resources.Fahrrad_XmlOeffnen_xml_Datei_öffnen____xml____xml };
            if (fileDialog.ShowDialog() == DialogResult.OK) {
                if (_xml.ReadDatei(fileDialog.FileName)) {
                    xmlTextBox.Text = fileDialog.FileName;
                    xmlOffenOK.Visible = true;
                    _okXml = true;
                    if (_okPrognose)
                        toolAusfueren.Visible = true;
                    GetInfo(_culInfo.Contains("de") ? "XML-Datei wurde importiert" : "XML-file is imported");
                }
                else {
                    xmlTextBox.Text = fileDialog.FileName;
                    toolAusfueren.Visible = false;
                    xmlOffenOK.Visible = false;
                    save.Visible = false;
                    _okXml = false;

                    MessageBox.Show(Resources.Fahrrad_XmlOeffnen_TEXT, Resources.Fahrrad_XmlOeffnen_Fehlermeldung,
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                }
            }
            panelXML.Visible = true;
        }
        private void xml_export_Click(object sender, EventArgs e) {
            XmlExport();
        }
        private void xMLexportToolStripMenuItem_Click(object sender, EventArgs e) {
            XmlExport();
        }
        private void XmlExport() {
            saveFileDialog1.Filter = Resources.Fahrrad_XmlOeffnen_xml_Datei_öffnen____xml____xml;
            saveFileDialog1.Title = Resources.Fahrrad_XmlExport_xml_Datei_erstellen;
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "") {
                _xml.WriteDatei(saveFileDialog1.FileName);
            }
        }

        private void gewichtungToolStripMenuItem_Click(object sender, EventArgs e) {
            var einstellungen = new Einstellungen();
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
                    _culInfo = Thread.CurrentThread.CurrentUICulture.Name;
                    Controls.Clear();
                    Events.Dispose();
                    InitializeComponent();
                    _okXml = false;
                    break;
                case "englisch":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                    _culInfo = Thread.CurrentThread.CurrentUICulture.Name;
                    Controls.Clear();
                    Events.Dispose();
                    InitializeComponent();
                    _okXml = false;
                    break;
            }
        }
        private void schließenToolStripMenuItem_Click_1(object sender, EventArgs e) {
            Close();
        }

        /// <summary>
        /// F1 / Handbuch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void handbuchToolStripMenuItem_Click(object sender, EventArgs e) {
            BenutzerHandbuch();

        }
        private void videoF2ToolStripMenuItem_Click(object sender, EventArgs e) {
            VideoTutorial();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            switch (keyData) {
                case Keys.F1:
                    BenutzerHandbuch();
                    break;
                case Keys.F2:
                    VideoTutorial();
                    break;
                case (Keys.Alt | Keys.E):
                    ChangeLanguage("englisch");
                    englischToolStripMenuItem1.Checked = true;
                    deutschToolStripMenuItem1.Checked = false;
                    break;
                case (Keys.Alt | Keys.D):
                    ChangeLanguage("deutsch");
                    englischToolStripMenuItem1.Checked = false;
                    deutschToolStripMenuItem1.Checked = true;
                    break;
                case (Keys.Control | Keys.E):
                    var einstellungen = new Einstellungen();
                    einstellungen.Show();
                    break;
                case (Keys.Alt | Keys.F4):
                    Close();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void BenutzerHandbuch() {
            string path = Directory.GetCurrentDirectory() + @"\chm\Handbuch_SS2013_P004_V2.docx";
            Help.ShowHelp(this, path, HelpNavigator.TableOfContents, "");
        }

        private void VideoTutorial() {
            string path = Directory.GetCurrentDirectory() + @"\chm\Schulung.exe";
            Help.ShowHelp(this, path, HelpNavigator.TableOfContents, "");
        }

        ////////////////////////////////////////////////////////////////////////////

        private void TextVisibleFalse() {
            bildSpeichOk.Visible = false;
            _okPrognose = false;
            toolAusfueren.Visible = false;
            save.Visible = false;
        }
        private void DataGriedViewRemove(DataGridView dgv) {
            if (dgv.DataSource != null) {
                dgv.DataSource = null;
            }
            else {
                dgv.Rows.Clear();
            }
        }
        private void XmlVorbereitung(int p) {
            int index;
            if (p == 100 || p == 1) { //1 = Vertriebswunsch
                DataGriedViewRemove(dataGridViewVertrieb);
                dataGridViewVertrieb.Rows.Add();
                for (int i = 0; i < 3; ++i) {
                    dataGridViewVertrieb.Rows[0].Cells[i].Value = _instance.GetTeil(i + 1).VertriebPer0;
                }
            }
            if (p == 100 || p == 3) {
                _bv.LadeBvPositionenInDc();
                index = 0;
                DataGriedViewRemove(dataGridViewEinkauf);
                foreach (Bestellposition b in _instance.Bestellungen) {
                    dataGridViewEinkauf.Rows.Add();
                    dataGridViewEinkauf.Rows[index].Cells[0].Value = b.Kaufteil.Nummer;
                    dataGridViewEinkauf.Rows[index].Cells[1].Value = b.Menge;
                    dataGridViewEinkauf.Rows[index].Cells[2].Value = b.Eil ? "E" : "N";
                    ++index;
                }
                ////
                DataGriedViewRemove(dataGridViewDirekt);
                if (dvVerwenden.Checked) {
                    _bv.LadeDvPositioneninDc();
                    index = 0;
                    foreach (DvPosition d in _instance.DVerkauf) {
                        dataGridViewDirekt.Rows.Add();
                        dataGridViewDirekt.Rows[index].Cells[0].Value = d.DvTeilNr;
                        dataGridViewDirekt.Rows[index].Cells[1].Value = d.DvMenge;
                        dataGridViewDirekt.Rows[index].Cells[2].Value = d.DvPreis;
                        dataGridViewDirekt.Rows[index].Cells[3].Value = d.DvStrafe;
                        ++index;
                    }
                }
            }
            if (p == 100 || p == 4) {
                //neue Liste erstellen und speichern
                if (p == 4)
                    _pp.ProdListe = SaveNeueListe();


                _pp.LoadProdListeInDc();

                index = 0;
                DataGriedViewRemove(dataGridViewPrAuftraege);
                foreach (var d in _instance.ListeProduktion) {
                    dataGridViewPrAuftraege.Rows.Add();
                    dataGridViewPrAuftraege.Rows[index].Cells[0].Value = d.Key;
                    dataGridViewPrAuftraege.Rows[index].Cells[1].Value = d.Value;
                    ++index;
                }
            }

            if (p == 100 || p == 5) {
                index = 0;
                DataGriedViewRemove(dataGridViewProduktKapazit);
                foreach (int[] i in _xmlAp) {
                    dataGridViewProduktKapazit.Rows.Add();
                    dataGridViewProduktKapazit.Rows[index].Cells[0].Value = i[0];
                    dataGridViewProduktKapazit.Rows[index].Cells[1].Value = i[1];
                    dataGridViewProduktKapazit.Rows[index].Cells[2].Value = i[2] / 5;
                    ++index;
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewKTeil_CellContentClick_1(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == 0) {
                var ti = new TeilInformation("kteil", Convert.ToInt32(dataGridViewKTeil.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
                ti.GetTeilvonETeilMitMenge();
                ti.Show();
            }
        }
        private void dataGridViewAPlatz_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            int zahl = Convert.ToInt32(DataGridViewAP.Rows[e.RowIndex].Cells[3].Value.ToString());
            switch (e.ColumnIndex) {
                case 0:
                    var ti = new TeilInformation("arbeitsplatz", Convert.ToInt32(DataGridViewAP.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
                    ti.GetZeitInformation();
                    ti.Show();
                    break;
                case 4:
                    if (zahl - 10 > 0)
                        DataGridViewAP.Rows[e.RowIndex].Cells[3].Value = zahl - 10;
                    else
                        DataGridViewAP.Rows[e.RowIndex].Cells[3].Value = 0;
                    break;
                case 5:
                    DataGridViewAP.Rows[e.RowIndex].Cells[3].Value = zahl + 10;
                    break;
            }
        }
        private void DataGridViewAP_CellMouseEnter(object sender, DataGridViewCellEventArgs e) {

            if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
                DataGridViewAP.Cursor = Cursors.Hand;
            else
                DataGridViewAP.Cursor = Cursors.Default;

        }
        private void ProdMenge(int index, int nr, int reserve) {
            var et = _instance.GetTeil(nr) as ETeil;
            if (et.IstEndProdukt)
                et.ProduktionsMengePer0 = et.VertriebPer0 + reserve - et.Lagerstand - et.InWartschlange - et.InBearbeitung;
            else if (et.IstEndProdukt != true && et.Verwendung.Contains("KDH") == false) {
                if (et.VertriebPer0 < 0) {
                    et.VertriebPer0 = 0;
                }
                et.ProduktionsMengePer0 = et.VertriebPer0 + et.VaterInWarteschlange + reserve - et.Lagerstand - et.InWartschlange - et.InBearbeitung;
            }
            else {
                var pufTemp = 0;
                foreach (KeyValuePair<int, int[]> pair in et.KdhPuffer.Where(pair => pair.Key.Equals(nr))) {
                    pufTemp = pair.Value[index - 1];
                }
                int pmTemp;
                if (index == 1) {
                    pmTemp = et.VertriebPer0 + et.KdhVaterInWarteschlange[nr][index - 1] + pufTemp - et.Lagerstand - et.InWartschlange - et.InBearbeitung;
                }
                else {
                    pmTemp = et.VertriebPer0 + et.KdhVaterInWarteschlange[nr][index - 1] + pufTemp;
                }
                if (et.KdhProduktionsmenge.ContainsKey(index.ToString() + "-" + Convert.ToString(nr))) {
                    et.KdhProduktionsmenge[index.ToString() + "-" + Convert.ToString(nr)] = pmTemp;
                }
            }
        }
        private void p1ETAusfueren_Click(object sender, EventArgs e) {
            (_instance.GetTeil(1) as ETeil).VertriebPer0 = Convert.ToInt32(p1vw_0.Text);
            (_instance.GetTeil(1) as ETeil).Puffer = Convert.ToInt32(p1r_0.Text);
            ProdMenge(1, 1, Convert.ToInt32(p1r_0.Text));
            (_instance.GetTeil(51) as ETeil).Puffer = Convert.ToInt32(p1r_51.Text);
            ProdMenge(1, 51, Convert.ToInt32(p1r_51.Text));
            (_instance.GetTeil(50) as ETeil).Puffer = Convert.ToInt32(p1r_50.Text);
            ProdMenge(1, 50, Convert.ToInt32(p1r_50.Text));
            (_instance.GetTeil(4) as ETeil).Puffer = Convert.ToInt32(p1r_4.Text);
            ProdMenge(1, 4, Convert.ToInt32(p1r_4.Text));
            (_instance.GetTeil(10) as ETeil).Puffer = Convert.ToInt32(p1r_10.Text);
            ProdMenge(1, 10, Convert.ToInt32(p1r_10.Text));
            (_instance.GetTeil(49) as ETeil).Puffer = Convert.ToInt32(p1r_49.Text);
            ProdMenge(1, 49, Convert.ToInt32(p1r_49.Text));
            (_instance.GetTeil(7) as ETeil).Puffer = Convert.ToInt32(p1r_7.Text);
            ProdMenge(1, 7, Convert.ToInt32(p1r_7.Text));
            (_instance.GetTeil(13) as ETeil).Puffer = Convert.ToInt32(p1r_13.Text);
            ProdMenge(1, 13, Convert.ToInt32(p1r_13.Text));
            (_instance.GetTeil(18) as ETeil).Puffer = Convert.ToInt32(p1r_18.Text);
            ProdMenge(1, 18, Convert.ToInt32(p1r_18.Text));
            (_instance.GetTeil(26) as ETeil).KdhPuffer[(_instance.GetTeil(26) as ETeil).KdhPuffer.Keys.ToList()[0]][0] = Convert.ToInt32(p1r_26.Text);
            ProdMenge(1, 26, Convert.ToInt32(p1r_26.Text));
            (_instance.GetTeil(16) as ETeil).KdhPuffer[(_instance.GetTeil(16) as ETeil).KdhPuffer.Keys.ToList()[1]][0] = Convert.ToInt32(p1r_16.Text);
            ProdMenge(1, 16, Convert.ToInt32(p1r_16.Text));
            (_instance.GetTeil(17) as ETeil).KdhPuffer[(_instance.GetTeil(17) as ETeil).KdhPuffer.Keys.ToList()[2]][0] = Convert.ToInt32(p1r_17.Text);
            ProdMenge(1, 17, Convert.ToInt32(p1r_17.Text));

            DispositionDarstellung(1);
            Information();
            XmlVorbereitung(100);

            GetInfo(_culInfo.Contains("de") ? "Daten wurde in XML übernommen" : "Take in XML data has been");
        }
        private void p2ETAusfueren_Click(object sender, EventArgs e) {
            (_instance.GetTeil(2) as ETeil).VertriebPer0 = Convert.ToInt32(p2vw_0.Text);
            (_instance.GetTeil(2) as ETeil).Puffer = Convert.ToInt32(p2r_0.Text);
            ProdMenge(2, 2, Convert.ToInt32(p2r_0.Text));
            (_instance.GetTeil(56) as ETeil).Puffer = Convert.ToInt32(p2r_56.Text);
            ProdMenge(2, 56, Convert.ToInt32(p2r_56.Text));
            (_instance.GetTeil(55) as ETeil).Puffer = Convert.ToInt32(p2r_55.Text);
            ProdMenge(2, 55, Convert.ToInt32(p2r_55.Text));
            (_instance.GetTeil(5) as ETeil).Puffer = Convert.ToInt32(p2r_5.Text);
            ProdMenge(2, 5, Convert.ToInt32(p2r_5.Text));
            (_instance.GetTeil(11) as ETeil).Puffer = Convert.ToInt32(p2r_11.Text);
            ProdMenge(2, 11, Convert.ToInt32(p2r_11.Text));
            (_instance.GetTeil(54) as ETeil).Puffer = Convert.ToInt32(p2r_54.Text);
            ProdMenge(2, 54, Convert.ToInt32(p2r_54.Text));
            (_instance.GetTeil(8) as ETeil).Puffer = Convert.ToInt32(p2r_8.Text);
            ProdMenge(2, 8, Convert.ToInt32(p2r_8.Text));
            (_instance.GetTeil(14) as ETeil).Puffer = Convert.ToInt32(p2r_14.Text);
            ProdMenge(2, 14, Convert.ToInt32(p2r_14.Text));
            (_instance.GetTeil(19) as ETeil).Puffer = Convert.ToInt32(p2r_19.Text);
            ProdMenge(2, 19, Convert.ToInt32(p2r_19.Text));
            (_instance.GetTeil(26) as ETeil).KdhPuffer[(_instance.GetTeil(26) as ETeil).KdhPuffer.Keys.ToList()[0]][1] = Convert.ToInt32(p2r_26.Text);
            ProdMenge(2, 26, Convert.ToInt32(p2r_26.Text));
            (_instance.GetTeil(16) as ETeil).KdhPuffer[(_instance.GetTeil(16) as ETeil).KdhPuffer.Keys.ToList()[1]][1] = Convert.ToInt32(p2r_16.Text);
            ProdMenge(2, 16, Convert.ToInt32(p2r_16.Text));
            (_instance.GetTeil(17) as ETeil).KdhPuffer[(_instance.GetTeil(17) as ETeil).KdhPuffer.Keys.ToList()[2]][1] = Convert.ToInt32(p2r_17.Text);
            ProdMenge(2, 17, Convert.ToInt32(p2r_17.Text));

            DispositionDarstellung(2);
            Information();
            XmlVorbereitung(100);
            GetInfo(_culInfo.Contains("de") ? "Daten wurde in XML übernommen" : "Take in XML data has been");
        }
        private void p3ETAusfueren_Click(object sender, EventArgs e) {
            (_instance.GetTeil(3) as ETeil).VertriebPer0 = Convert.ToInt32(p3vw_0.Text);
            (_instance.GetTeil(3) as ETeil).Puffer = Convert.ToInt32(p3r_0.Text);
            ProdMenge(3, 3, Convert.ToInt32(p3r_0.Text));
            (_instance.GetTeil(31) as ETeil).Puffer = Convert.ToInt32(p3r_31.Text);
            ProdMenge(3, 31, Convert.ToInt32(p3r_31.Text));
            (_instance.GetTeil(30) as ETeil).Puffer = Convert.ToInt32(p3r_30.Text);
            ProdMenge(3, 30, Convert.ToInt32(p3r_30.Text));
            (_instance.GetTeil(6) as ETeil).Puffer = Convert.ToInt32(p3r_6.Text);
            ProdMenge(3, 6, Convert.ToInt32(p3r_6.Text));
            (_instance.GetTeil(12) as ETeil).Puffer = Convert.ToInt32(p3r_12.Text);
            ProdMenge(3, 12, Convert.ToInt32(p3r_12.Text));
            (_instance.GetTeil(29) as ETeil).Puffer = Convert.ToInt32(p3r_29.Text);
            ProdMenge(3, 29, Convert.ToInt32(p3r_29.Text));
            (_instance.GetTeil(9) as ETeil).Puffer = Convert.ToInt32(p3r_9.Text);
            ProdMenge(3, 9, Convert.ToInt32(p3r_9.Text));
            (_instance.GetTeil(15) as ETeil).Puffer = Convert.ToInt32(p3r_15.Text);
            ProdMenge(3, 15, Convert.ToInt32(p3r_15.Text));
            (_instance.GetTeil(20) as ETeil).Puffer = Convert.ToInt32(p3r_20.Text);
            ProdMenge(3, 20, Convert.ToInt32(p3r_20.Text));
            (_instance.GetTeil(26) as ETeil).KdhPuffer[(_instance.GetTeil(26) as ETeil).KdhPuffer.Keys.ToList()[0]][2] = Convert.ToInt32(p3r_26.Text);
            ProdMenge(3, 26, Convert.ToInt32(p3r_26.Text));
            (_instance.GetTeil(16) as ETeil).KdhPuffer[(_instance.GetTeil(16) as ETeil).KdhPuffer.Keys.ToList()[1]][2] = Convert.ToInt32(p3r_16.Text);
            ProdMenge(3, 16, Convert.ToInt32(p3r_16.Text));
            (_instance.GetTeil(17) as ETeil).KdhPuffer[(_instance.GetTeil(17) as ETeil).KdhPuffer.Keys.ToList()[2]][2] = Convert.ToInt32(p3r_17.Text);
            ProdMenge(3, 17, Convert.ToInt32(p3r_17.Text));

            DispositionDarstellung(3);
            Information();
            XmlVorbereitung(100);
            GetInfo(_culInfo.Contains("de") ? "Daten wurde in XML übernommen" : "Take in XML data has been");
        }
        private void arbPlatzAusfueren_Click(object sender, EventArgs e) {
            _bv.ClearBvPositionen();
            for (int index = 0; index < DataGridViewAP.Rows.Count; ++index) {
                _instance.GetArbeitsplatz(Convert.ToInt32(DataGridViewAP.Rows[index].Cells[0].Value.ToString())).RuestungCustom =
                    (Convert.ToInt32(DataGridViewAP.Rows[index].Cells[3].Value.ToString()));
            }
            Information();
            XmlVorbereitung(5);
            GetInfo(_culInfo.Contains("de") ? "Daten wurde in XML übernommen" : "Take in XML data has been");
        }

        private void pictureBox3_Click(object sender, EventArgs e) {
            try {
                if (dataGridViewBestellung.AllowUserToAddRows) {
                    dataGridViewBestellung.AllowUserToAddRows = false;
                    kNr.ReadOnly = true;
                }
                _bp = new List<Bestellposition>();

                foreach (DataGridViewRow row in dataGridViewBestellung.Rows) {
                    var check = (DataGridViewCheckBoxCell)row.Cells[3];
                    var check2 = (DataGridViewCheckBoxCell)row.Cells[2];
                    bool c = check2.Value != null;
                    if (check.Value == null)
                        check.Value = false;


                    var value = row.Cells[0].Value;
                    if (value != null) {
                        var teil = _instance.GetTeil(Convert.ToInt32(value.ToString())) as KTeil;
                        if (teil != null) {
                            if (row.Cells[1].Value != null) {
                                if (check.Value.ToString() != "true") {
                                    var bbp = new Bestellposition(teil, Convert.ToInt32(row.Cells[1].Value.ToString()),
                                                                  c);
                                    _bp.Add(bbp);
                                }
                            }
                            else
                                MessageBox.Show(
                                    Resources.Fahrrad_pictureBox3_Click_Kaufteil_N + value +
                                    Resources.Fahrrad_pictureBox3_Click_, Resources.Fahrrad_XmlOeffnen_Fehlermeldung,
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                        else
                            MessageBox.Show(
                                Resources.Fahrrad_pictureBox3_Click_Kaufteil_N + value +
                                Resources.Fahrrad_pictureBox3_Click_1, Resources.Fahrrad_XmlOeffnen_Fehlermeldung,
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                    else {
                        MessageBox.Show(Resources.Fahrrad_pictureBox3_Click_Kaufteil_kann_nicht_NULL_sein,
                                        Resources.Fahrrad_XmlOeffnen_Fehlermeldung,
                                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                                        MessageBoxDefaultButton.Button1);
                    }

                }

                _bestellungUpdate = true;
                _bv.SetBvPositionen(_bp);
                Information();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private void zurueck_Click(object sender, EventArgs e) {
            if (dataGridViewBestellung.AllowUserToAddRows) {
                dataGridViewBestellung.AllowUserToAddRows = false;
                kNr.ReadOnly = true;
            }
            _bestellungUpdate = false;
            _bv.ClearBvPositionen();
            Information();
        }
        private void uebernehmenXML_Click(object sender, EventArgs e) {
            XmlVorbereitung(3);
            GetInfo(_culInfo.Contains("de") ? "Daten wurde in XML übernommen" : "Take in XML data has been");
        }

        private void pictureBox3_Click_1(object sender, EventArgs e) {
            XmlVorbereitung(4);
            GetInfo(_culInfo.Contains("de") ? "Daten wurde in XML übernommen" : "Take in XML data has been");
        }

        private void addNr_Click(object sender, EventArgs e) {
            dataGridViewBestellung.AllowUserToAddRows = true;
            kNr.ReadOnly = false;
        }

        private Dictionary<int, int> SaveNeueListe() {
            var prodListeNeu = dataGridViewProduktAuftrag.Rows.Cast<DataGridViewRow>().ToDictionary(row => Convert.ToInt32(row.Cells[0].Value.ToString()), row => Convert.ToInt32(row.Cells[1].Value.ToString()));
            return prodListeNeu;
        }

        private void saveAenderungen2_Click(object sender, EventArgs e) {
            if (dataGridViewDirektverkauf.AllowUserToAddRows) {
                dataGridViewDirektverkauf.AllowUserToAddRows = false;
                knr2.ReadOnly = true;
            }

            _dirv = new List<DvPosition>();

            foreach (DataGridViewRow row in dataGridViewDirektverkauf.Rows) {
                var ch = (DataGridViewCheckBoxCell)row.Cells[4];
                if (ch.Value == null)
                    ch.Value = false;
                if (ch.Value.ToString() != "true") {

                    var teil = _instance.GetTeil(Convert.ToInt32(row.Cells[0].Value.ToString())) as ETeil;
                    if (teil != null) {
                        object n = row.Cells[0].Value; //nr
                        object m = row.Cells[1].Value; //menge
                        object p = row.Cells[2].Value; //preis
                        object s = row.Cells[3].Value; //straffe

                        if (n != null && m != null && p != null && s != null) {
                            var dv = new DvPosition(Convert.ToInt32(n.ToString()), Convert.ToInt32(m.ToString()), Convert.ToDouble(p.ToString()), Convert.ToDouble(s.ToString()));
                            _dirv.Add(dv);
                        }
                        else {
                            MessageBox.Show(Resources.Fahrrad_saveAenderungen2_Click_kein_Fehld_darf_null_sein, Resources.Fahrrad_XmlOeffnen_Fehlermeldung,
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else {
                        MessageBox.Show(Resources.Fahrrad_saveAenderungen2_Click_Eigenfertigte_Teil_N1 + row.Cells[0].Value + Resources.Fahrrad_pictureBox3_Click_1, Resources.Fahrrad_XmlOeffnen_Fehlermeldung,
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                }
            }
            _dvUpdate = true;
            _bestellungUpdate = true;
            _bv.SetDvPositionen(_dirv);
            Information();
        }
        private void addNr2_Click(object sender, EventArgs e) {
            dataGridViewDirektverkauf.AllowUserToAddRows = true;
            knr2.ReadOnly = false;
        }
        private void zurueck2_Click(object sender, EventArgs e) {
            if (dataGridViewDirektverkauf.AllowUserToAddRows) {
                dataGridViewDirektverkauf.AllowUserToAddRows = false;
                knr2.ReadOnly = true;
            }
            _dvUpdate = false;
            _bestellungUpdate = true;
            _bv.ClearDvPositionen();
            Information();
        }

        private void pictureBox4_Click(object sender, EventArgs e) {
            _pp.OptimiereProdListe();
            Information();
        }

        ////////////////////////////////////////////////////////////////
        /// <summary>
        /// DragAndDrop
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool IsCellOrRowHeader(int x, int y) {
            DataGridViewHitTestType dgt = dataGridViewProduktAuftrag.HitTest(x, y).Type;
            return (dgt == DataGridViewHitTestType.Cell ||
                            dgt == DataGridViewHitTestType.RowHeader);
        }
        private void dataGridViewProduktAuftrag_MouseMove_1(object sender, MouseEventArgs e) {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
                if (!IsCellOrRowHeader(e.X, e.Y) && _rowIndexSrc >= 0) {
                    DragDropEffects dropEffect = dataGridViewProduktAuftrag.DoDragDrop(
                        dataGridViewProduktAuftrag.Rows[_rowIndexSrc], DragDropEffects.None);
                    return;
                }

                if (_dragBoxSrc != Rectangle.Empty &&
                        !_dragBoxSrc.Contains(e.X, e.Y)) {
                    DragDropEffects dropEffect = dataGridViewProduktAuftrag.DoDragDrop(
                dataGridViewProduktAuftrag.Rows[_rowIndexSrc], DragDropEffects.Move);
                }
            }
        }
        private void dataGridViewProduktAuftrag_MouseDown_1(object sender, MouseEventArgs e) {
            _rowIndexSrc = dataGridViewProduktAuftrag.HitTest(e.X, e.Y).RowIndex;
            if (_rowIndexSrc != -1) {
                Size dragSize = SystemInformation.DragSize;
                _dragBoxSrc = new Rectangle(new Point(e.X, e.Y), dragSize);
            }
            else
                _dragBoxSrc = Rectangle.Empty;
        }
        private void dataGridViewProduktAuftrag_DragDrop_1(object sender, DragEventArgs e) {
            Point clientPoint = dataGridViewProduktAuftrag.PointToClient(new Point(e.X, e.Y));
            _rowIndexTar = dataGridViewProduktAuftrag.HitTest(clientPoint.X, clientPoint.Y).RowIndex;
            if (e.Effect == DragDropEffects.Move) {
                var rowToMove = e.Data.GetData(
                    typeof(DataGridViewRow)) as DataGridViewRow;
                MoveRow(_rowIndexSrc, _rowIndexTar);
            }
        }
        void SwapCell(int c, int srcRow, int tarRow, out object tmp0, out object tmp1) {
            DataGridViewCell srcCell = dataGridViewProduktAuftrag.Rows[srcRow].Cells[c];
            DataGridViewCell tarCell = dataGridViewProduktAuftrag.Rows[tarRow].Cells[c];
            tmp0 = tarCell.Value;
            tmp1 = srcCell.Value;
            tarCell.Value = tmp1;
        }
        void MoveRow(int srcRow, int tarRow) {
            int cellCount = dataGridViewProduktAuftrag.Rows[srcRow].Cells.Count;
            for (int c = 0; c < cellCount; c++)
                ShiftRows(srcRow, tarRow, c);
        }
        private void ShiftRows(int srcRow, int tarRow, int c) {
            object tmp0, tmp1;
            SwapCell(c, srcRow, tarRow, out tmp0, out tmp1);
            int delta = tarRow < srcRow ? 1 : -1;
            for (int r = tarRow + delta; r != srcRow + delta; r += delta) {
                tmp1 = dataGridViewProduktAuftrag.Rows[r].Cells[c].Value;
                dataGridViewProduktAuftrag.Rows[r].Cells[c].Value = tmp0;
                tmp0 = tmp1;
            }
            dataGridViewProduktAuftrag.Rows[tarRow].Selected = true;
            dataGridViewProduktAuftrag.CurrentCell = dataGridViewProduktAuftrag.Rows[tarRow].Cells[0];
        }
        private void dataGridViewProduktAuftrag_DragOver_1(object sender, DragEventArgs e) {
            Point p = dataGridViewProduktAuftrag.PointToClient(new Point(e.X, e.Y));
            DataGridViewHitTestType dgt = dataGridViewProduktAuftrag.HitTest(p.X, p.Y).Type;
            e.Effect = IsCellOrRowHeader(p.X, p.Y) ? DragDropEffects.Move : DragDropEffects.None;
        }


        private void GetInfo(string text) {
            timer1.Stop();
            info.Text = text;
            if (text != "")
                timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e) {
            timer1.Stop();
            GetInfo("");
        }
    }
}


