using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFahrrad_v1
{
    public class Bestellverwaltung
    {
        // Instance of DataContainer class
        DataContainer dc;
        int aktPeriode;
        List<Bestellposition> bvPositionen;
        // Constructor
        public Bestellverwaltung()
        {
            dc = DataContainer.Instance;
            bvPositionen = new List<Bestellposition>();
        }
        // Getter / Setter
        public int AktPeriode
        {
            get { return aktPeriode; }
            set { aktPeriode = value; }
        }
        public List<Bestellposition> BvPositionen
        {
            get { return bvPositionen; }
        }
        // Clear list bvPositionen
        public void clearBvPositionen()
        {
            bvPositionen.Clear();
        }
        // Create list of orders
        public void generiereBestellListe()
        {
            // Calculate Bestellposition for each KTeil and when necessary add new Bestellposition to DataContainer dc
            foreach (KTeil kt in dc.ListeKTeile)
            {
                double lieferDauer = kt.Lieferdauer + kt.AbweichungLieferdauer * dc.VerwendeAbweichung;
                int teilMengeSumme = kt.Lagerstand;
                // Actual period
                if (kt.BestandPer1 < 0)
                {
                    bvPositionen.Add(new Bestellposition(kt, kt.BruttoBedarfPer0 - kt.Lagerstand, true));
                    teilMengeSumme = teilMengeSumme - kt.BruttoBedarfPer0 + (kt.BruttoBedarfPer0 - kt.Lagerstand);
                }
                // Actual + 1 period
                if (kt.BestandPer2 < 0)
                {
                    // Check if Lieferdauer of KTeil will be in time
                    if (lieferDauer >= 1.0 && lieferDauer < 1.8)
                    {
                        int bestellMenge = berechneMenge(dc.VerwendeDiskount, kt.BruttoBedarfPer1 - kt.BestandPer1, kt.DiskontMenge);
                        bvPositionen.Add(new Bestellposition(kt, bestellMenge, false));
                        teilMengeSumme = teilMengeSumme - kt.BruttoBedarfPer1 + bestellMenge;
                    }
                    else if (lieferDauer >= 1.8)
                    {
                        // Check needed amount
                        bvPositionen.Add(new Bestellposition(kt, kt.BruttoBedarfPer1 - kt.BestandPer1, true));
                        teilMengeSumme = teilMengeSumme - kt.BruttoBedarfPer1 + (kt.BruttoBedarfPer1 - kt.BestandPer1);
                    }
                }
                // Actual + 2 period
                if (kt.BestandPer3 < 0)
                {

                }
            }
        }
        private int berechneMenge(double verwDiskont, int bestellMenge, int diskont)
        {
            int outputMenge = 0;
            // Member verwDiskont need to be percent -> devide with 100
            // Check if param verwDiskont is either 0 or 100: 0 = take bestellMenge, 100 = take diskount
            if (bestellMenge > diskont || verwDiskont == 0)
            {
                outputMenge = bestellMenge;
            }
            else if (verwDiskont == 100)
            {
                outputMenge = diskont;
            }
            else
            {
                verwDiskont = verwDiskont / 100;
                verwDiskont = 1 - verwDiskont;
                // 300 * 0,5
                int vergleichDiskont = Convert.ToInt32(Math.Round(verwDiskont * diskont, 0));
                if (vergleichDiskont < bestellMenge)
                {
                    outputMenge = diskont;
                }
                else
                {
                    outputMenge = bestellMenge;
                }
            }
            // Return output
            return outputMenge;
        }
    }
}
