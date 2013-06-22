using System;
using System.Collections.Generic;
using System.Linq;
using ToolFahrrad_v1.Komponenten;

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
                // Initialization of members
                double perBeginn = 1.0;
                double lieferungNorm = Math.Ceiling(kt.Lieferdauer + (kt.AbweichungLieferdauer * (_dc.VerwendeAbweichung / 100)));
                double lieferungEil = kt.Lieferdauer / 2;
                int mengeNorm = 0;
                int mengeEil = 0;
                List<int> bestaendeKTeil = new List<int>();
                bestaendeKTeil.Add(kt.BestandPer1);
                bestaendeKTeil.Add(kt.BestandPer2);
                bestaendeKTeil.Add(kt.BestandPer3);
                bestaendeKTeil.Add(kt.BestandPer4);
                int index = 0;
                // Check if quantity is low and create order
                while (lieferungNorm >= perBeginn)
                {
                    if (bestaendeKTeil[index] <= 0)
                    {
                        if (perBeginn == lieferungNorm)
                        {
                            if (bestaendeKTeil[index] < 0)
                            {
                                mengeNorm += berechneMenge(bestaendeKTeil[index] * (-1), kt.DiskontMenge);
                            }
                            else
                            {
                                if (bestaendeKTeil[index + 1] < 0)
                                {
                                    mengeNorm += berechneMenge(bestaendeKTeil[index + 1] * (-1), kt.DiskontMenge);
                                }
                            }
                        }
                        else
                        {
                            if (bestaendeKTeil[index] < 0)
                            {
                                mengeEil += bestaendeKTeil[index] * (-1);
                            }
                        }
                    }
                    // Increment period members
                    ++perBeginn;
                    ++index;
                }
                // Create one or more orders
                if (mengeNorm > 0)
                {
                    _bvPositionen.Add(new Bestellposition(kt, mengeNorm, false));
                }
                if (mengeEil > 0)
                {
                    _bvPositionen.Add(new Bestellposition(kt, mengeEil, true));
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
        private int berechneMenge(int bestellMenge, int diskont)
        {
            double verwDiskont = _dc.VerwendeDiskount;
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
