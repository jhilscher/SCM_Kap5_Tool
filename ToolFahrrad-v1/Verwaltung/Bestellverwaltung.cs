using System;
using System.Collections.Generic;
using System.Linq;

namespace ToolFahrrad_v1.Verwaltung
{
    public class Bestellverwaltung
    {
        // Instance of DataContainer class
        readonly DataContainer _dc;
        List<Bestellposition> _bvPositionen;
        List<DvPosition> _dvPositionen;
        // Constructor
        public Bestellverwaltung()
        {
            _dc = DataContainer.Instance;
            _bvPositionen = new List<Bestellposition>();
            _dvPositionen = new List<DvPosition>();
        }
        // Getter / Setter
        public List<Bestellposition> BvPositionen
        {
            get { return _bvPositionen; }
        }
        // Set new list bvPositionen
        public void SetBvPositionen(List<Bestellposition> newBvPositionen)
        {
            ClearBvPositionen();
            _bvPositionen = newBvPositionen;
        }
        // Clear list bvPositionen
        public void ClearBvPositionen()
        {
            _bvPositionen.Clear();
        }
        public List<DvPosition> DvPositionen
        {
            get { return _dvPositionen; }
        }

        public void SetDvPositionen(List<DvPosition> newDvPosition) {
            ClearDvPositionen();
            _dvPositionen = newDvPosition;
        }

        private void AddDvPosition(int nr, int menge, double preis, double strafe)
        {
            _dvPositionen.Add(new DvPosition(nr, menge, preis, strafe));
        }

        public void ClearDvPositionen()
        {
            _dvPositionen.Clear();
        }
        // Create list of orders
        public void GeneriereBestellListe()
        {
            // Calculate Bestellposition for each KTeil and when necessary add new Bestellposition to DataContainer dc
            foreach (KTeil kt in _dc.ListeKTeile)
            {
                const double startPeriod = 0.0;
                const double endPeriod = 0.8;
                int n = 0;
                double lieferDauer = kt.Lieferdauer + kt.AbweichungLieferdauer * (_dc.VerwendeAbweichung / 100);
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
                    if(_dc.DiskountGrenze >= kt.Preis && eil != true)
                    {
                        menge = kt.DiskontMenge;
                    }
                    else if (_dc.DiskountGrenze < kt.Preis && kt.Preis < _dc.GrenzeMenge)
                    {
                        menge = berechneMenge(_dc.VerwendeDiskount, kt.BruttoBedarfPer0 - kt.Lagerstand, kt.DiskontMenge);
                    }
                    else if (_dc.GrenzeMenge <= kt.Preis || eil)
                    {
                        menge = kt.BruttoBedarfPer0 - kt.Lagerstand;
                    }
                    if(menge != 0)
                    {
                        _bvPositionen.Add(new Bestellposition(kt, menge, eil));
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
                        if (_dc.DiskountGrenze >= kt.Preis)
                        {
                            menge = kt.DiskontMenge;
                        }
                        else if (_dc.DiskountGrenze < kt.Preis && kt.Preis < _dc.GrenzeMenge)
                        {
                            menge = berechneMenge(_dc.VerwendeDiskount, kt.BruttoBedarfPer1 - kt.BestandPer1, kt.DiskontMenge);
                        }
                        else if (_dc.GrenzeMenge <= kt.Preis)
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
                        _bvPositionen.Add(new Bestellposition(kt, menge, eil));
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
                        if (_dc.DiskountGrenze >= kt.Preis)
                        {
                            menge = kt.DiskontMenge;
                        }
                        else if (_dc.DiskountGrenze < kt.Preis && kt.Preis < _dc.GrenzeMenge)
                        {
                            menge = berechneMenge(_dc.VerwendeDiskount, kt.BruttoBedarfPer2 - kt.BestandPer2, kt.DiskontMenge);
                        }
                        else if (_dc.GrenzeMenge <= kt.Preis)
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
                        _bvPositionen.Add(new Bestellposition(kt, menge, eil));
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
                        if (_dc.DiskountGrenze >= kt.Preis)
                        {
                            menge = kt.DiskontMenge;
                        }
                        else if (_dc.DiskountGrenze < kt.Preis && kt.Preis < _dc.GrenzeMenge)
                        {
                            menge = berechneMenge(_dc.VerwendeDiskount, kt.BruttoBedarfPer3 - kt.BestandPer3, kt.DiskontMenge);
                        }
                        else if (_dc.GrenzeMenge <= kt.Preis)
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
                        _bvPositionen.Add(new Bestellposition(kt, menge, eil));
                        teilMengeSumme = teilMengeSumme - kt.BruttoBedarfPer3 + menge;
                    }
                }
            }
            OptimiereBvPositionen();
        }
        // Transfer list bvPositionen into data container
        public void LadeBvPositionenInDc()
        {
            _dc.Bestellungen = BvPositionen;
        }
        public void GeneriereListeDv()
        {
            _dvPositionen.Clear();
            foreach (ETeil et in _dc.ListeETeile)
            {
                if (et.ProduktionsMengePer0 < 0)
                {
                    AddDvPosition(et.Nummer, et.ProduktionsMengePer0 * -1, et.Wert, 0.0);
                }
            }
        }
        public void LadeDvPositioneninDc()
        {
            _dc.DVerkauf = DvPositionen;
        }
        private int berechneMenge(double verwDiskont, int bestellMenge, int diskont)
        {
            int outputMenge;
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
                outputMenge = vergleichDiskont < bestellMenge ? diskont : bestellMenge;
            }
            // Return output
            return outputMenge;
        }
        private void OptimiereBvPositionen()
        {
            // When found several "eil" orders for the same KTeil, delete orders and create only one with sum amount
            foreach (KTeil kt in _dc.ListeKTeile)
            {
                var eilPositionen = _bvPositionen.Where(bp => bp.Kaufteil.Nummer == kt.Nummer && bp.Eil).ToList();
                if (eilPositionen.Count() > 1)
                {
                    int bestellMenge = 0;
                    foreach (Bestellposition bp2 in eilPositionen)
                    {
                        bestellMenge += bp2.Menge;
                        _bvPositionen.Remove(bp2);
                    }
                    _bvPositionen.Add(new Bestellposition(kt, bestellMenge, true));
                }
            }
        }
    }
}
