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
        Bestellverwaltung bv = new Bestellverwaltung();
        List<Bestellposition> bp;
        private bool bestellungUpdate = false;
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
            this.bestellungUpdate = false;
        }
        private void ausführen() {
            if ((instance.GetTeil(4) as ETeil).Puffer != -1) {
                foreach (Teil t in instance.ListeETeile) {
                    t.Aufgeloest = false;
                    (t as ETeil).KdhUpdate = false;
                    (t as ETeil).InBearbeitung = 0;
                    (t as ETeil).InWartschlange = 0;
                    (t as ETeil).KdhProduktionsmenge = new Dictionary<string, int>();
                    if ((instance.GetTeil(t.Nummer) as ETeil).IstEndProdukt == false) {
                        (instance.GetTeil(t.Nummer) as ETeil).Puffer = -1;
                        (instance.GetTeil(t.Nummer) as ETeil).KDHaufNULL();
                    }
                }
            }
            pp.AktPeriode = Convert.ToInt32(xml.period);
            pp.Aufloesen();
            foreach (Arbeitsplatz a in instance.ArbeitsplatzList) {
                a.Geaendert = false;
                a.RuestNew = 0;
                a.RuestungCustom = 0;
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
                et = instance.GetTeil(n) as ETeil;
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
            if (index == 2) {
                #region P2
                //P2
                n = 2;
                et = instance.GetTeil(n) as ETeil;
                p2vw_0.Text = et.VertriebPer0.ToString();
                p2r_0.Text = et.Puffer.ToString();
                p2ls_0.Text = et.Lagerstand.ToString();
                p2iws_0.Text = et.InWartschlange.ToString();
                p2ib_0.Text = et.InBearbeitung.ToString();
                p2pm_0.Text = et.ProduktionsMengePer0.ToString();
                //26
                n = 26;
                et = instance.GetTeil(n) as ETeil;
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
                et = instance.GetTeil(n) as ETeil;
                p2vw_56.Text = et.VertriebPer0.ToString();
                p2plus_56.Text = et.VaterInWarteschlange.ToString();
                p2r_56.Text = et.Puffer.ToString();
                p2ls_56.Text = et.Lagerstand.ToString();
                p2iws_56.Text = et.InWartschlange.ToString();
                p2ib_56.Text = et.InBearbeitung.ToString();
                p2pm_56.Text = et.ProduktionsMengePer0.ToString();
                //16
                n = 16;
                et = instance.GetTeil(n) as ETeil;
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
                et = instance.GetTeil(n) as ETeil;
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
                et = instance.GetTeil(n) as ETeil;
                p2vw_55.Text = et.VertriebPer0.ToString();
                p2plus_55.Text = et.VaterInWarteschlange.ToString();
                p2r_55.Text = et.Puffer.ToString();
                p2ls_55.Text = et.Lagerstand.ToString();
                p2iws_55.Text = et.InWartschlange.ToString();
                p2ib_55.Text = et.InBearbeitung.ToString();
                p2pm_55.Text = et.ProduktionsMengePer0.ToString();
                //5
                n = 5;
                et = instance.GetTeil(n) as ETeil;
                p2vw_5.Text = et.VertriebPer0.ToString();
                p2plus_5.Text = et.VaterInWarteschlange.ToString();
                p2r_5.Text = et.Puffer.ToString();
                p2ls_5.Text = et.Lagerstand.ToString();
                p2iws_5.Text = et.InWartschlange.ToString();
                p2ib_5.Text = et.InBearbeitung.ToString();
                p2pm_5.Text = et.ProduktionsMengePer0.ToString();
                //11
                n = 11;
                et = instance.GetTeil(n) as ETeil;
                p2vw_11.Text = et.VertriebPer0.ToString();
                p2plus_11.Text = et.VaterInWarteschlange.ToString();
                p2r_11.Text = et.Puffer.ToString();
                p2ls_11.Text = et.Lagerstand.ToString();
                p2iws_11.Text = et.InWartschlange.ToString();
                p2ib_11.Text = et.InBearbeitung.ToString();
                p2pm_11.Text = et.ProduktionsMengePer0.ToString();
                //54
                n = 54;
                et = instance.GetTeil(n) as ETeil;
                p2vw_54.Text = et.VertriebPer0.ToString();
                p2plus_54.Text = et.VaterInWarteschlange.ToString();
                p2r_54.Text = et.Puffer.ToString();
                p2ls_54.Text = et.Lagerstand.ToString();
                p2iws_54.Text = et.InWartschlange.ToString();
                p2ib_54.Text = et.InBearbeitung.ToString();
                p2pm_54.Text = et.ProduktionsMengePer0.ToString();
                //8
                n = 8;
                et = instance.GetTeil(n) as ETeil;
                p2vw_8.Text = et.VertriebPer0.ToString();
                p2plus_8.Text = et.VaterInWarteschlange.ToString();
                p2r_8.Text = et.Puffer.ToString();
                p2ls_8.Text = et.Lagerstand.ToString();
                p2iws_8.Text = et.InWartschlange.ToString();
                p2ib_8.Text = et.InBearbeitung.ToString();
                p2pm_8.Text = et.ProduktionsMengePer0.ToString();
                //14
                n = 14;
                et = instance.GetTeil(n) as ETeil;
                p2vw_14.Text = et.VertriebPer0.ToString();
                p2plus_14.Text = et.VaterInWarteschlange.ToString();
                p2r_14.Text = et.Puffer.ToString();
                p2ls_14.Text = et.Lagerstand.ToString();
                p2iws_14.Text = et.InWartschlange.ToString();
                p2ib_14.Text = et.InBearbeitung.ToString();
                p2pm_14.Text = et.ProduktionsMengePer0.ToString();
                //19
                n = 19;
                et = instance.GetTeil(n) as ETeil;
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
                et = instance.GetTeil(n) as ETeil;
                p3vw_0.Text = et.VertriebPer0.ToString();
                p3r_0.Text = et.Puffer.ToString();
                p3ls_0.Text = et.Lagerstand.ToString();
                p3iws_0.Text = et.InWartschlange.ToString();
                p3ib_0.Text = et.InBearbeitung.ToString();
                p3pm_0.Text = et.ProduktionsMengePer0.ToString();
                //26
                n = 26;
                et = instance.GetTeil(n) as ETeil;
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
                et = instance.GetTeil(n) as ETeil;
                p3vw_31.Text = et.VertriebPer0.ToString();
                p3plus_31.Text = et.VaterInWarteschlange.ToString();
                p3r_31.Text = et.Puffer.ToString();
                p3ls_31.Text = et.Lagerstand.ToString();
                p3iws_31.Text = et.InWartschlange.ToString();
                p3ib_31.Text = et.InBearbeitung.ToString();
                p3pm_31.Text = et.ProduktionsMengePer0.ToString();
                //16
                n = 16;
                et = instance.GetTeil(n) as ETeil;
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
                et = instance.GetTeil(n) as ETeil;
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
                et = instance.GetTeil(n) as ETeil;
                p3vw_30.Text = et.VertriebPer0.ToString();
                p3plus_30.Text = et.VaterInWarteschlange.ToString();
                p3r_30.Text = et.Puffer.ToString();
                p3ls_30.Text = et.Lagerstand.ToString();
                p3iws_30.Text = et.InWartschlange.ToString();
                p3ib_30.Text = et.InBearbeitung.ToString();
                p3pm_30.Text = et.ProduktionsMengePer0.ToString();
                //6
                n = 6;
                et = instance.GetTeil(n) as ETeil;
                p3vw_6.Text = et.VertriebPer0.ToString();
                p3plus_6.Text = et.VaterInWarteschlange.ToString();
                p3r_6.Text = et.Puffer.ToString();
                p3ls_6.Text = et.Lagerstand.ToString();
                p3iws_6.Text = et.InWartschlange.ToString();
                p3ib_6.Text = et.InBearbeitung.ToString();
                p3pm_6.Text = et.ProduktionsMengePer0.ToString();
                //12
                n = 12;
                et = instance.GetTeil(n) as ETeil;
                p3vw_12.Text = et.VertriebPer0.ToString();
                p3plus_12.Text = et.VaterInWarteschlange.ToString();
                p3r_12.Text = et.Puffer.ToString();
                p3ls_12.Text = et.Lagerstand.ToString();
                p3iws_12.Text = et.InWartschlange.ToString();
                p3ib_12.Text = et.InBearbeitung.ToString();
                p3pm_12.Text = et.ProduktionsMengePer0.ToString();
                //29
                n = 29;
                et = instance.GetTeil(n) as ETeil;
                p3vw_29.Text = et.VertriebPer0.ToString();
                p3plus_29.Text = et.VaterInWarteschlange.ToString();
                p3r_29.Text = et.Puffer.ToString();
                p3ls_29.Text = et.Lagerstand.ToString();
                p3iws_29.Text = et.InWartschlange.ToString();
                p3ib_29.Text = et.InBearbeitung.ToString();
                p3pm_29.Text = et.ProduktionsMengePer0.ToString();
                //9
                n = 9;
                et = instance.GetTeil(n) as ETeil;
                p3vw_9.Text = et.VertriebPer0.ToString();
                p3plus_9.Text = et.VaterInWarteschlange.ToString();
                p3r_9.Text = et.Puffer.ToString();
                p3ls_9.Text = et.Lagerstand.ToString();
                p3iws_9.Text = et.InWartschlange.ToString();
                p3ib_9.Text = et.InBearbeitung.ToString();
                p3pm_9.Text = et.ProduktionsMengePer0.ToString();
                //15
                n = 15;
                et = instance.GetTeil(n) as ETeil;
                p3vw_15.Text = et.VertriebPer0.ToString();
                p3plus_15.Text = et.VaterInWarteschlange.ToString();
                p3r_15.Text = et.Puffer.ToString();
                p3ls_15.Text = et.Lagerstand.ToString();
                p3iws_15.Text = et.InWartschlange.ToString();
                p3ib_15.Text = et.InBearbeitung.ToString();
                p3pm_15.Text = et.ProduktionsMengePer0.ToString();
                //20
                n = 20;
                et = instance.GetTeil(n) as ETeil;
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
            #region KTEILE
            //Bruttobedarf
            foreach (KTeil k in instance.ListeKTeile) {
                k.BruttoBedarfPer0 = 0;
                k.BruttoBedarfPer1 = 0;
                k.BruttoBedarfPer2 = 0;
                k.BruttoBedarfPer3 = 0;
            }
            for (int i = 1; i < 4; ++i) {
                pp.RekursAufloesenKTeile(i, null, instance.GetTeil(i) as ETeil);
            }

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
            #endregion
            // Eteile
            #region ETeile
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
                if (!a.Verwendung.Equals("KDH"))
                    dataGridViewETeil.Rows[index].Cells[7].Value = a.ProduktionsMengePer0;
                else {
                    int prodMenge = 0;
                    foreach (KeyValuePair<string, int> pair in a.KdhProduktionsmenge) {
                        prodMenge += pair.Value;
                    }
                    dataGridViewETeil.Rows[index].Cells[7].Value = prodMenge;
                }

                //Farbe
                for (int i = 0; i < 8; ++i) {
                    if (i == 7)
                        dataGridViewETeil.Columns[i].DefaultCellStyle.BackColor = Color.Honeydew;
                    else
                        dataGridViewETeil.Columns[i].DefaultCellStyle.BackColor = Color.FloralWhite;
                }
                if (a.Verwendung.Equals("KDH")) {
                    dataGridViewETeil.Rows[index].DefaultCellStyle.BackColor = Color.LemonChiffon;
                }

                ++index;
            }
            #endregion
            // Arbeitsplätze
            #region Arbetsplatz
            this.DataGriedViewRemove(dataGridViewAPlatz);
            index = 0;
            foreach (var a in instance.ArbeitsplatzList) {
                dataGridViewAPlatz.Rows.Add();
                dataGridViewAPlatz.Rows[index].Cells[0].Value = a.GetNummerArbeitsplatz;
                dataGridViewAPlatz.Rows[index].Cells[1].Value = a.Leerzeit + " (" + a.RuestungVorPeriode + ") ";
                //TODO KDH ZEIT UMRECHNEN
                int prMenge = 0;
                int val = 0;
                int sum = 0;
                foreach (KeyValuePair<int, int> kvp in a.Werk_zeiten) {
                    {
                        if (!(instance.GetTeil(kvp.Key) as ETeil).Verwendung.Contains("KDH")) {

                            prMenge += (instance.GetTeil(kvp.Key) as ETeil).ProduktionsMengePer0;
                        }
                        else {
                            foreach (KeyValuePair<string, int> pair in (instance.GetTeil(kvp.Key) as ETeil).KdhProduktionsmenge) {
                                prMenge += pair.Value;
                            }
                        }
                        if (prMenge < 0)
                            prMenge = 0;
                        sum += kvp.Value * prMenge;
                        prMenge = 0;
                    }
                }
                dataGridViewAPlatz.Rows[index].Cells[2].Value = sum + " min";
                //TODO KDH RÜST UMRECHNEN
                prMenge = 0;
                val = 0;
                string key = "";
                if (a.Geaendert == false) {
                    foreach (KeyValuePair<int, int> kvp in a.Ruest_zeiten) {
                        if (!(instance.GetTeil(kvp.Key) as ETeil).Verwendung.Contains("KDH")) {
                            if ((instance.GetTeil(kvp.Key) as ETeil).ProduktionsMengePer0 >= 0)
                                val += kvp.Value;
                        }
                        else {
                            foreach (KeyValuePair<string, int> pair in (instance.GetTeil(kvp.Key) as ETeil).KdhProduktionsmenge) {
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

                dataGridViewAPlatz.Rows[index].Cells[3].Value = a.RuestungCustom;
                int gesammt = a.RuestungCustom + sum;
                dataGridViewAPlatz.Rows[index].Cells[4].Value = gesammt + " min";
                dataGridViewAPlatz.Rows[index].Cells[8].Value = imageList1.Images[2];
                if (gesammt <= a.zeit) // newTeim <= 2400 
                    dataGridViewAPlatz.Rows[index].Cells[5].Value = imageList1.Images[2];
                else if (gesammt > instance.ErsteSchicht) // gesammt > 3600
                {
                    if (gesammt > 7200)
                        dataGridViewAPlatz.Rows[index].Cells[8].Value = imageList1.Images[0];
                    else if (gesammt > instance.ZweiteSchicht && gesammt < 7200)
                        dataGridViewAPlatz.Rows[index].Cells[8].Value = imageList1.Images[1];
                    else
                        dataGridViewAPlatz.Rows[index].Cells[8].Value = imageList1.Images[2];

                    if (gesammt < instance.ZweiteSchicht) {
                        dataGridViewAPlatz.Rows[index].Cells[5].Value = imageList1.Images[0];
                        dataGridViewAPlatz.Rows[index].Cells[6].Value = true;
                    }
                    else if (gesammt > instance.ZweiteSchicht) {
                        dataGridViewAPlatz.Rows[index].Cells[5].Value = imageList1.Images[0];
                        dataGridViewAPlatz.Rows[index].Cells[7].Value = true;
                    }
                }
                else if (gesammt > a.zeit && gesammt <= instance.ErsteSchicht) // 2400 < newTime < 3600 Überstunden
                {
                    dataGridViewAPlatz.Rows[index].Cells[5].Value = imageList1.Images[1];
                }
                else {
                    dataGridViewAPlatz.Rows[index].Cells[5].Value = imageList1.Images[2];
                }
                //Farbe
                for (int i = 0; i < 9; ++i) {
                    if (i < 2)
                        dataGridViewAPlatz.Columns[i].DefaultCellStyle.BackColor = Color.FloralWhite;
                    else if (i == 2 || i > 3)
                        dataGridViewAPlatz.Columns[i].DefaultCellStyle.BackColor = Color.LightYellow;
                }
                ++index;
            }
            #endregion

            //Bestellung
            #region Bestellung
            this.DataGriedViewRemove(dataGridViewBestellung);
            if(this.bestellungUpdate == false)
                bv.generiereBestellListe();
            List<Bestellposition> bp = bv.BvPositionen;
            index = 0;
            foreach (var a in bp) {
                dataGridViewBestellung.Rows.Add();
                dataGridViewBestellung.Rows[index].Cells[0].Value = a.Kaufteil.Nummer;
                dataGridViewBestellung.Rows[index].Cells[1].Value = a.Menge;
                if (a.Eil == true) {
                    dataGridViewBestellung.Rows[index].Cells[2].Value = true;
                }
                ++index;
            }
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewKTeil_CellContentClick_1(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == 0) {
                TeilInformation ti = new TeilInformation("kteil", Convert.ToInt32(dataGridViewKTeil.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
                ti.GetTeilvonETeilMitMenge();
                ti.Show();
            }
        }

        private void dataGridViewAPlatz_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == 0) {
                TeilInformation ti = new TeilInformation("arbeitsplatz", Convert.ToInt32(dataGridViewAPlatz.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
                ti.GetZeitInformation();
                ti.Show();
            }
        }

        private void prodMenge(int index, int nr, int reserve) {
            ETeil et = instance.GetTeil(nr) as ETeil;
            if (et.IstEndProdukt == true)
                et.ProduktionsMengePer0 = et.VertriebPer0 + reserve - et.Lagerstand - et.InWartschlange - et.InBearbeitung;
            else if (et.IstEndProdukt != true && et.Verwendung.Contains("KDH") == false) {
                if (et.VertriebPer0 < 0) {
                    et.VertriebPer0 = 0;
                }
                et.ProduktionsMengePer0 = et.VertriebPer0 + et.VaterInWarteschlange + reserve - et.Lagerstand - et.InWartschlange - et.InBearbeitung;
            }
            else {
                int pufTemp = 0;
                foreach (KeyValuePair<int, int[]> pair in et.KdhPuffer) {
                    if (pair.Key.Equals(nr)) {
                        pufTemp = pair.Value[index - 1];
                    }
                }
                int pmTemp = 0;
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
            (instance.GetTeil(1) as ETeil).VertriebPer0 = Convert.ToInt32(p1vw_0.Text);
            (instance.GetTeil(1) as ETeil).Puffer = Convert.ToInt32(p1r_0.Text);
            prodMenge(1, 1, Convert.ToInt32(p1r_0.Text));
            (instance.GetTeil(51) as ETeil).Puffer = Convert.ToInt32(p1r_51.Text);
            prodMenge(1, 51, Convert.ToInt32(p1r_51.Text));
            (instance.GetTeil(50) as ETeil).Puffer = Convert.ToInt32(p1r_50.Text);
            prodMenge(1, 50, Convert.ToInt32(p1r_50.Text));
            (instance.GetTeil(4) as ETeil).Puffer = Convert.ToInt32(p1r_4.Text);
            prodMenge(1, 4, Convert.ToInt32(p1r_4.Text));
            (instance.GetTeil(10) as ETeil).Puffer = Convert.ToInt32(p1r_10.Text);
            prodMenge(1, 10, Convert.ToInt32(p1r_10.Text));
            (instance.GetTeil(49) as ETeil).Puffer = Convert.ToInt32(p1r_49.Text);
            prodMenge(1, 49, Convert.ToInt32(p1r_49.Text));
            (instance.GetTeil(7) as ETeil).Puffer = Convert.ToInt32(p1r_7.Text);
            prodMenge(1, 7, Convert.ToInt32(p1r_7.Text));
            (instance.GetTeil(13) as ETeil).Puffer = Convert.ToInt32(p1r_13.Text);
            prodMenge(1, 13, Convert.ToInt32(p1r_13.Text));
            (instance.GetTeil(18) as ETeil).Puffer = Convert.ToInt32(p1r_18.Text);
            prodMenge(1, 18, Convert.ToInt32(p1r_18.Text));
            (instance.GetTeil(26) as ETeil).KdhPuffer[(instance.GetTeil(26) as ETeil).KdhPuffer.Keys.ToList()[0]][0] = Convert.ToInt32(p1r_26.Text);
            prodMenge(1, 26, Convert.ToInt32(p1r_26.Text));
            (instance.GetTeil(16) as ETeil).KdhPuffer[(instance.GetTeil(16) as ETeil).KdhPuffer.Keys.ToList()[1]][0] = Convert.ToInt32(p1r_16.Text);
            prodMenge(1, 16, Convert.ToInt32(p1r_16.Text));
            (instance.GetTeil(17) as ETeil).KdhPuffer[(instance.GetTeil(17) as ETeil).KdhPuffer.Keys.ToList()[2]][0] = Convert.ToInt32(p1r_17.Text);
            prodMenge(1, 17, Convert.ToInt32(p1r_17.Text));

            DispositionDarstellung(1);
            Information();
        }
        private void p2ETAusfueren_Click(object sender, EventArgs e) {
            (instance.GetTeil(2) as ETeil).VertriebPer0 = Convert.ToInt32(p2vw_0.Text);
            (instance.GetTeil(2) as ETeil).Puffer = Convert.ToInt32(p2r_0.Text);
            prodMenge(2, 2, Convert.ToInt32(p2r_0.Text));
            (instance.GetTeil(56) as ETeil).Puffer = Convert.ToInt32(p2r_56.Text);
            prodMenge(2, 56, Convert.ToInt32(p2r_56.Text));
            (instance.GetTeil(55) as ETeil).Puffer = Convert.ToInt32(p2r_55.Text);
            prodMenge(2, 55, Convert.ToInt32(p2r_55.Text));
            (instance.GetTeil(5) as ETeil).Puffer = Convert.ToInt32(p2r_5.Text);
            prodMenge(2, 5, Convert.ToInt32(p2r_5.Text));
            (instance.GetTeil(11) as ETeil).Puffer = Convert.ToInt32(p2r_11.Text);
            prodMenge(2, 11, Convert.ToInt32(p2r_11.Text));
            (instance.GetTeil(54) as ETeil).Puffer = Convert.ToInt32(p2r_54.Text);
            prodMenge(2, 54, Convert.ToInt32(p2r_54.Text));
            (instance.GetTeil(8) as ETeil).Puffer = Convert.ToInt32(p2r_8.Text);
            prodMenge(2, 8, Convert.ToInt32(p2r_8.Text));
            (instance.GetTeil(14) as ETeil).Puffer = Convert.ToInt32(p2r_14.Text);
            prodMenge(2, 14, Convert.ToInt32(p2r_14.Text));
            (instance.GetTeil(19) as ETeil).Puffer = Convert.ToInt32(p2r_19.Text);
            prodMenge(2, 19, Convert.ToInt32(p2r_19.Text));
            (instance.GetTeil(26) as ETeil).KdhPuffer[(instance.GetTeil(26) as ETeil).KdhPuffer.Keys.ToList()[0]][1] = Convert.ToInt32(p2r_26.Text);
            prodMenge(2, 26, Convert.ToInt32(p2r_26.Text));
            (instance.GetTeil(16) as ETeil).KdhPuffer[(instance.GetTeil(16) as ETeil).KdhPuffer.Keys.ToList()[1]][1] = Convert.ToInt32(p2r_16.Text);
            prodMenge(2, 16, Convert.ToInt32(p2r_16.Text));
            (instance.GetTeil(17) as ETeil).KdhPuffer[(instance.GetTeil(17) as ETeil).KdhPuffer.Keys.ToList()[2]][1] = Convert.ToInt32(p2r_17.Text);
            prodMenge(2, 17, Convert.ToInt32(p2r_17.Text));

            DispositionDarstellung(2);
            Information();
        }
        private void p3ETAusfueren_Click(object sender, EventArgs e) {
            (instance.GetTeil(3) as ETeil).VertriebPer0 = Convert.ToInt32(p3vw_0.Text);
            (instance.GetTeil(3) as ETeil).Puffer = Convert.ToInt32(p3r_0.Text);
            prodMenge(3, 3, Convert.ToInt32(p3r_0.Text));

            (instance.GetTeil(31) as ETeil).Puffer = Convert.ToInt32(p3r_31.Text);
            prodMenge(3, 31, Convert.ToInt32(p3r_31.Text));

            (instance.GetTeil(30) as ETeil).Puffer = Convert.ToInt32(p3r_30.Text);
            prodMenge(3, 30, Convert.ToInt32(p3r_30.Text));

            (instance.GetTeil(6) as ETeil).Puffer = Convert.ToInt32(p3r_6.Text);
            prodMenge(3, 6, Convert.ToInt32(p3r_6.Text));

            (instance.GetTeil(12) as ETeil).Puffer = Convert.ToInt32(p3r_12.Text);
            prodMenge(3, 12, Convert.ToInt32(p3r_12.Text));

            (instance.GetTeil(29) as ETeil).Puffer = Convert.ToInt32(p3r_29.Text);
            prodMenge(3, 29, Convert.ToInt32(p3r_29.Text));

            (instance.GetTeil(9) as ETeil).Puffer = Convert.ToInt32(p3r_9.Text);
            prodMenge(3, 9, Convert.ToInt32(p3r_9.Text));

            (instance.GetTeil(15) as ETeil).Puffer = Convert.ToInt32(p3r_15.Text);
            prodMenge(3, 15, Convert.ToInt32(p3r_15.Text));

            (instance.GetTeil(20) as ETeil).Puffer = Convert.ToInt32(p3r_20.Text);
            prodMenge(3, 20, Convert.ToInt32(p3r_20.Text));

            (instance.GetTeil(26) as ETeil).KdhPuffer[(instance.GetTeil(26) as ETeil).KdhPuffer.Keys.ToList()[0]][2] = Convert.ToInt32(p3r_26.Text);
            prodMenge(3, 26, Convert.ToInt32(p3r_26.Text));
            (instance.GetTeil(16) as ETeil).KdhPuffer[(instance.GetTeil(16) as ETeil).KdhPuffer.Keys.ToList()[1]][2] = Convert.ToInt32(p3r_16.Text);
            prodMenge(3, 16, Convert.ToInt32(p3r_16.Text));
            (instance.GetTeil(17) as ETeil).KdhPuffer[(instance.GetTeil(17) as ETeil).KdhPuffer.Keys.ToList()[2]][2] = Convert.ToInt32(p3r_17.Text);
            prodMenge(3, 17, Convert.ToInt32(p3r_17.Text));

            DispositionDarstellung(3);
            Information();
        }

        private void arbPlatzAusfueren_Click(object sender, EventArgs e) {
            for (int index = 0; index < dataGridViewAPlatz.Rows.Count; ++index) {
                instance.GetArbeitsplatz(Convert.ToInt32(dataGridViewAPlatz.Rows[index].Cells[0].Value.ToString())).RuestungCustom =
                    (Convert.ToInt32(dataGridViewAPlatz.Rows[index].Cells[3].Value.ToString()));
            }
            Information();
        }

        private void pictureBox3_Click(object sender, EventArgs e) {
            Bestellposition bbp;
            bp = new List<Bestellposition>();


            //for (int index = 0; index < dataGridViewBestellung.Rows.Count; ++index) {

            //    string a = dataGridViewBestellung.Rows[index].Cells[3].Value.ToString();

            //}

            DataGridViewCheckBoxCell check = new DataGridViewCheckBoxCell();          


            foreach (DataGridViewRow row in dataGridViewBestellung.Rows) {
                check = (DataGridViewCheckBoxCell)row.Cells[3];
                if (check.Value == null)
                    check.Value = false;                
                if (check.Value.ToString() != "true") {
                    bbp = new Bestellposition(instance.GetTeil(Convert.ToInt32(row.Cells[0].Value.ToString())) as KTeil,
                    Convert.ToInt32(row.Cells[1].Value.ToString()),
                    row.Cells[2].Selected);
                    bp.Add(bbp);
                }
            }
            this.bestellungUpdate = true;
            bv.SetBvPositionen(bp);
            Information();
        }

        private void zurueck_Click(object sender, EventArgs e) {
            this.bestellungUpdate = false;
            bv.clearBvPositionen();
            Information();
        }
    }
}


