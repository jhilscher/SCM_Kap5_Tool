using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolFahrrad_v1
{
    public partial class TeilInformation : Form
    {
        // Get current language
        private String culInfo = Thread.CurrentThread.CurrentUICulture.Name;
        // -----------------------------------------------------------------
        private const String de = "de";
        private const String deKTeil = "Kaufteil Nr.: ";
        private const String enKTeil = "Ordering part No.: ";
        private const String deAPlatz = "Arbeitsplatz Nr.: ";
        private const String enAPlatz = "Workstation No.: ";
        private const String deLDauer = "Lieferdauer: ";
        private const String enLDauer = "Delivery duration: ";
        private const String deDMenge = "Diskontmenge: ";
        private const String enDMenge = "Discount amount: ";
        private const String dePreis = "Preis: ";
        private const String enPreis = "Price: ";
        private const String deBKosten = "Bestellkosten: ";
        private const String enBKosten = "Order costs: ";
        private const String deSumme = "Summe: ";
        private const String enSumme = "Sum: ";
        private const String deRZeit = "Rüstungszeit ";
        private const String enRZeit = "Setup time: ";
        private const String deKBedarf = "Kapazitätsbedarf: ";
        private const String enKBedarf = "Capacity demand: ";
        private const String deTeil = "Teil ";
        private const String enTeil = "Part ";
        private const String deInTeil = "In Teil ";
        private const String enInTeil = "In part ";
        // -----------------------------------------------------------------
        private int _nummer;
        DataContainer dc = DataContainer.Instance;
        // Constructor
        public TeilInformation(string name, int nummer)
        {
            InitializeComponent();
            this._nummer = nummer;
            // Set label to ordering part
            if (name.Equals("kteil"))
            {
                label1.Text = culInfo.Contains(de) ? deKTeil + _nummer : enKTeil + _nummer;
            }
            // Set label to working station
            else if (name.Equals("arbeitsplatz"))
            {
                label1.Text = culInfo.Contains(de) ? deAPlatz + _nummer : enAPlatz + _nummer;
            }
        }
        internal void GetTeilvonETeilMitMenge()
        {
            ausgabe.Text =
                (culInfo.Contains(de) ? deLDauer : enLDauer) + (dc.GetTeil(_nummer) as KTeil).Lieferdauer
                + " (" + (dc.GetTeil(_nummer) as KTeil).AbweichungLieferdauer + ")\n"
                + (culInfo.Contains(de) ? deDMenge : enDMenge)
                + (dc.GetTeil(_nummer) as KTeil).DiskontMenge + "\n" + (culInfo.Contains(de) ? dePreis : enPreis)
                + (dc.GetTeil(_nummer) as KTeil).Preis + "\n" + (culInfo.Contains(de) ? deBKosten : enBKosten)
                + (dc.GetTeil(_nummer) as KTeil).Bestellkosten + "\n\n";
            List<ETeil> list = (dc.GetTeil(_nummer) as KTeil).IstTeilVon;
            int sum = 0;
            int val = 0;
            int pm = 0;
            foreach (ETeil e in list)
            {
                foreach (KeyValuePair<Teil, int> kvp in e.Zusammensetzung)
                {
                    if (kvp.Key.Nummer == _nummer)
                    {
                        if (!e.Verwendung.Contains("KDH"))
                        {
                            if (e.ProduktionsMengePer0 < 0)
                            {
                                pm = 0;
                            }
                            else
                            {
                                pm = e.ProduktionsMengePer0;
                            }
                            ausgabe.Text
                                += (culInfo.Contains(de) ? deInTeil : enInTeil) + e.Nummer + "   " + kvp.Value
                                + " * " + pm + " = " + kvp.Value * pm + "\n";
                            sum += kvp.Value * pm;
                        }
                        else
                        {
                            foreach (KeyValuePair<string, int> pair in e.KdhProduktionsmenge)
                            {
                                val += pair.Value;
                            }
                            ausgabe.Text
                                += (culInfo.Contains(de) ? deInTeil : enInTeil) + e.Nummer + "  " + kvp.Value
                                + " * " + val + " = " + kvp.Value * val + "\n";
                            sum += kvp.Value * val;
                            val = 0;
                        }
                    }
                }
            }
            ausgabe.Text += (culInfo.Contains(de) ? deSumme : enSumme) + sum;
        }
        internal void GetZeitInformation()
        {
            ausgabe2.Text = (culInfo.Contains(de) ? deRZeit : enRZeit) + "\n";
            int sum = 0;
            int val = 0;
            int prMenge = 0;
            foreach (KeyValuePair<int, int> kvp in dc.GetArbeitsplatz(_nummer).Ruest_zeiten)
            {
                if (!(dc.GetTeil(kvp.Key) as ETeil).Verwendung.Contains("KDH"))
                {
                    if ((dc.GetTeil(kvp.Key) as ETeil).ProduktionsMengePer0 >= 0)
                    {
                        val = kvp.Value;
                    }
                    else
                    {
                        val = 0;
                    }
                }
                else
                {
                    foreach (KeyValuePair<string, int> pair in (dc.GetTeil(kvp.Key) as ETeil).KdhProduktionsmenge)
                    {
                        prMenge += pair.Value;
                        if (prMenge >= 0)
                        {
                            val = kvp.Value;
                        }
                        else
                        {
                            val = 0;
                        }
                    }
                }
                ausgabe2.Text += (culInfo.Contains(de) ? deTeil : enTeil) + kvp.Key + ": " + val + " min. \n";
                sum += val;
            }
            ausgabe2.Text += (culInfo.Contains(de) ? deSumme : enSumme) + sum + "\n\n";
            ausgabe.Text += (culInfo.Contains(de) ? deKBedarf : enKBedarf) + "\n";
            sum = 0;
            prMenge = 0;
            foreach (KeyValuePair<int, int> kvp in dc.GetArbeitsplatz(_nummer).Werk_zeiten)
            {
                {
                    if (!(dc.GetTeil(kvp.Key) as ETeil).Verwendung.Contains("KDH"))
                    {
                        prMenge = (dc.GetTeil(kvp.Key) as ETeil).ProduktionsMengePer0;
                    }
                    else
                    {
                        foreach (KeyValuePair<string, int> pair in (dc.GetTeil(kvp.Key) as ETeil).KdhProduktionsmenge)
                        {
                            prMenge += pair.Value;
                        }
                    }
                    if (prMenge < 0)
                    {
                        prMenge = 0;
                    }
                    ausgabe.Text
                        += (culInfo.Contains(de) ? deTeil : enTeil) + kvp.Key + ": " + kvp.Value + " * " + prMenge
                        + " = " + kvp.Value * prMenge + " min.\n";
                    sum += kvp.Value * prMenge;
                    prMenge = 0;
                }
            }
            ausgabe.Text += (culInfo.Contains(de) ? deSumme : enSumme) + sum + "\n\n";
        }
    }
}
