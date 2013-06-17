using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ToolFahrrad_v1.Komponenten;

namespace ToolFahrrad_v1.Windows
{
    public partial class TeilInformation : Form
    {
        // Get current language
        private String _culInfo = Thread.CurrentThread.CurrentUICulture.Name;
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
        DataContainer _dc = DataContainer.Instance;
        // Constructor
        public TeilInformation(string name, int nummer)
        {
            InitializeComponent();
            _nummer = nummer;
            // Set label to ordering part
            if (name.Equals("kteil"))
            {
                label1.Text = _culInfo.Contains(de) ? deKTeil + _nummer : enKTeil + _nummer;
            }
            // Set label to working station
            else if (name.Equals("arbeitsplatz"))
            {
                label1.Text = _culInfo.Contains(de) ? deAPlatz + _nummer : enAPlatz + _nummer;
            }
        }
        internal void GetTeilvonETeilMitMenge()
        {
            ausgabe.Text =
                (_culInfo.Contains(de) ? deLDauer : enLDauer) + (_dc.GetTeil(_nummer) as KTeil).Lieferdauer
                + " (" + (_dc.GetTeil(_nummer) as KTeil).AbweichungLieferdauer + ")\n"
                + (_culInfo.Contains(de) ? deDMenge : enDMenge)
                + (_dc.GetTeil(_nummer) as KTeil).DiskontMenge + "\n" + (_culInfo.Contains(de) ? dePreis : enPreis)
                + (_dc.GetTeil(_nummer) as KTeil).Preis + "\n" + (_culInfo.Contains(de) ? deBKosten : enBKosten)
                + (_dc.GetTeil(_nummer) as KTeil).Bestellkosten + "\n\n";
            List<ETeil> list = (_dc.GetTeil(_nummer) as KTeil).IstTeilVon;
            int sum = 0;
            int val = 0;
            foreach (ETeil e in list)
            {
                foreach (KeyValuePair<Teil, int> kvp in e.Zusammensetzung)
                {
                    if (kvp.Key.Nummer == _nummer)
                    {
                        if (!e.Verwendung.Contains("KDH"))
                        {
                            int pm = 0;
                            pm = e.ProduktionsMengePer0 < 0 ? 0 : e.ProduktionsMengePer0;
                            ausgabe.Text
                                += (_culInfo.Contains(de) ? deInTeil : enInTeil) + e.Nummer + "   " + kvp.Value
                                + " * " + pm + " = " + kvp.Value * pm + "\n";
                            sum += kvp.Value * pm;
                        }
                        else
                        {
                            val += e.KdhProduktionsmenge.Sum(pair => pair.Value);
                            ausgabe.Text
                                += (_culInfo.Contains(de) ? deInTeil : enInTeil) + e.Nummer + "  " + kvp.Value
                                + " * " + val + " = " + kvp.Value * val + "\n";
                            sum += kvp.Value * val;
                            val = 0;
                        }
                    }
                }
            }
            ausgabe.Text += (_culInfo.Contains(de) ? deSumme : enSumme) + sum;
        }
        internal void GetZeitInformation()
        {
            ausgabe2.Text = (_culInfo.Contains(de) ? deRZeit : enRZeit) + "\n";
            int sum = 0;
            int val = 0;
            int prMenge = 0;
            foreach (KeyValuePair<int, int> kvp in _dc.GetArbeitsplatz(_nummer).RuestZeiten)
            {
                if (!(_dc.GetTeil(kvp.Key) as ETeil).Verwendung.Contains("KDH"))
                {
                    val = (_dc.GetTeil(kvp.Key) as ETeil).ProduktionsMengePer0 >= 0 ? kvp.Value : 0;
                }
                else
                {
                    foreach (KeyValuePair<string, int> pair in (_dc.GetTeil(kvp.Key) as ETeil).KdhProduktionsmenge)
                    {
                        prMenge += pair.Value;
                        val = prMenge >= 0 ? kvp.Value : 0;
                    }
                }
                ausgabe2.Text += (_culInfo.Contains(de) ? deTeil : enTeil) + kvp.Key + ": " + val + " min. \n";
                sum += val;
            }
            ausgabe2.Text += (_culInfo.Contains(de) ? deSumme : enSumme) + sum + "\n\n";
            ausgabe.Text += (_culInfo.Contains(de) ? deKBedarf : enKBedarf) + "\n";
            sum = 0;
            prMenge = 0;
            foreach (KeyValuePair<int, int> kvp in _dc.GetArbeitsplatz(_nummer).WerkZeiten)
            {
                {
                    if (!(_dc.GetTeil(kvp.Key) as ETeil).Verwendung.Contains("KDH"))
                    {
                        prMenge = (_dc.GetTeil(kvp.Key) as ETeil).ProduktionsMengePer0;
                    }
                    else
                    {
                        prMenge += (_dc.GetTeil(kvp.Key) as ETeil).KdhProduktionsmenge.Sum(pair => pair.Value);
                    }
                    if (prMenge < 0)
                    {
                        prMenge = 0;
                    }
                    ausgabe.Text
                        += (_culInfo.Contains(de) ? deTeil : enTeil) + kvp.Key + ": " + kvp.Value + " * " + prMenge
                        + " = " + kvp.Value * prMenge + " min.\n";
                    sum += kvp.Value * prMenge;
                    prMenge = 0;
                }
            }
            ausgabe.Text += (_culInfo.Contains(de) ? deSumme : enSumme) + sum + "\n\n";
        }
    }
}
