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
        // Set new list bvPositionen
        public void SetBvPositionen(List<Bestellposition> newBvPositionen)
        {
            clearBvPositionen();
            bvPositionen = newBvPositionen;
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
                double startPeriod = 0.0;
                double endPeriod = 0.8;
                int n = 0;
                double lieferDauer = kt.Lieferdauer + kt.AbweichungLieferdauer * (dc.VerwendeAbweichung / 100);
                int teilMengeSumme = kt.Lagerstand;
                bool eil = false;
                int menge = 0;
                // Actual period ---------------------------------------------------------------------------------------
                if (kt.BestandPer1 < 0)
                {
                    if(lieferDauer > endPeriod)
                    {
                        eil = true;
                    }
                    if(dc.DiskountGrenze >= kt.Preis && eil != true)
                    {
                        menge = kt.DiskontMenge;
                    }
                    else if (dc.DiskountGrenze < kt.Preis && kt.Preis < dc.GrenzeMenge)
                    {
                        menge = berechneMenge(dc.VerwendeDiskount, kt.BruttoBedarfPer0 - kt.Lagerstand, kt.DiskontMenge);
                    }
                    else if (dc.GrenzeMenge <= kt.Preis || eil == true)
                    {
                        menge = kt.BruttoBedarfPer0 - kt.Lagerstand;
                    }
                    if(menge != 0)
                    {
                        bvPositionen.Add(new Bestellposition(kt, menge, eil));
                        teilMengeSumme = teilMengeSumme - kt.BruttoBedarfPer0 + menge;
                    }
                }
                // Actual + 1 period -----------------------------------------------------------------------------------
                n++;
                eil = false;
                menge = 0;
                if (kt.BestandPer2 < 0)
                {
                    // Check if Lieferdauer of KTeil will be in time
                    if (lieferDauer >= (startPeriod + n) && lieferDauer < (endPeriod + n))
                    {
                        if (dc.DiskountGrenze >= kt.Preis)
                        {
                            menge = kt.DiskontMenge;
                        }
                        else if (dc.DiskountGrenze < kt.Preis && kt.Preis < dc.GrenzeMenge)
                        {
                            menge = berechneMenge(dc.VerwendeDiskount, kt.BruttoBedarfPer1 - kt.BestandPer1, kt.DiskontMenge);
                        }
                        else if (dc.GrenzeMenge <= kt.Preis)
                        {
                            menge = kt.BruttoBedarfPer1 - kt.BestandPer1;
                        }
                    }
                    else if (lieferDauer >= (endPeriod + n))
                    {
                        eil = true;
                        menge = kt.BruttoBedarfPer1 - kt.BestandPer1;
                    }
                    if (menge != 0)
                    {
                        bvPositionen.Add(new Bestellposition(kt, menge, eil));
                        teilMengeSumme = teilMengeSumme - kt.BruttoBedarfPer1 + menge;
                    }
                }
                // Actual + 2 period -----------------------------------------------------------------------------------
                n++;
                eil = false;
                menge = 0;
                if (kt.BestandPer3 < 0)
                {
                    // Check if Lieferdauer of KTeil will be in time
                    if (lieferDauer >= (startPeriod + n) && lieferDauer < (endPeriod + n))
                    {
                        if (dc.DiskountGrenze >= kt.Preis)
                        {
                            menge = kt.DiskontMenge;
                        }
                        else if (dc.DiskountGrenze < kt.Preis && kt.Preis < dc.GrenzeMenge)
                        {
                            menge = berechneMenge(dc.VerwendeDiskount, kt.BruttoBedarfPer2 - kt.BestandPer2, kt.DiskontMenge);
                        }
                        else if (dc.GrenzeMenge <= kt.Preis)
                        {
                            menge = kt.BruttoBedarfPer2 - kt.BestandPer2;
                        }
                    }
                    else if (lieferDauer >= (endPeriod + n))
                    {
                        eil = true;
                        menge = kt.BruttoBedarfPer2 - kt.BestandPer2;
                    }
                    if (menge != 0)
                    {
                        bvPositionen.Add(new Bestellposition(kt, menge, eil));
                        teilMengeSumme = teilMengeSumme - kt.BruttoBedarfPer2 + menge;
                    }
                }
                // Actual + 3 period -----------------------------------------------------------------------------------
                n++;
                eil = false;
                menge = 0;
                if (kt.BestandPer4 < 0)
                {
                    // Check if Lieferdauer of KTeil will be in time
                    if (lieferDauer >= (startPeriod + n) && lieferDauer < (endPeriod + n))
                    {
                        if (dc.DiskountGrenze >= kt.Preis)
                        {
                            menge = kt.DiskontMenge;
                        }
                        else if (dc.DiskountGrenze < kt.Preis && kt.Preis < dc.GrenzeMenge)
                        {
                            menge = berechneMenge(dc.VerwendeDiskount, kt.BruttoBedarfPer3 - kt.BestandPer3, kt.DiskontMenge);
                        }
                        else if (dc.GrenzeMenge <= kt.Preis)
                        {
                            menge = kt.BruttoBedarfPer3 - kt.BestandPer3;
                        }
                    }
                    else if (lieferDauer >= (endPeriod + n))
                    {
                        eil = true;
                        menge = kt.BruttoBedarfPer3 - kt.BestandPer3;
                    }
                    if (menge != 0)
                    {
                        bvPositionen.Add(new Bestellposition(kt, menge, eil));
                        teilMengeSumme = teilMengeSumme - kt.BruttoBedarfPer3 + menge;
                    }
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
                // Example: 300 * 0,5
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
