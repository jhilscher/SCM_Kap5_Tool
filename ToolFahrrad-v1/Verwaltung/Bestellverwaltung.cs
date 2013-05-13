using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFahrrad_v1
{
    public class Bestellverwaltung
    {
        // Instance of DataContainer class
        DataContainer dc = DataContainer.Instance;
        // Constructor
        public Bestellverwaltung()
        {
            berechneVerbrauchKTeile();
        }
        // Calculate forecast of consumption of KTeil
        public void berechneVerbrauchKTeile( )
        {
            /*foreach (KTeil kt in dc.ListeKTeile)
            {
                // Calculate forecast of consumption of KTeil
                kt.berechnungVerbrauchPrognose(aktPeriode, dc.VerwendeAbweichung);
            }*/
        }
        // Create list of orders
        public void erzeugeBestellListe()
        {
            foreach (KTeil kt in dc.ListeKTeile)
            {
                /*if (kt.VerbrauchPrognose1OA <= 0)
                {
                    Bestellposition bp = new Bestellposition(kt, berechneMenge(kt), true);
                    this.dc.Bestellungen.Add(bp);
                }
                else if (kt.VerbrauchPrognose1MA <= 0)
                {
                    Bestellposition bp = new Bestellposition(kt, berechneMenge(kt), true);
                    this.dc.Bestellungen.Add(bp);
                }
                else if (kt.VerbrauchPrognose2OA <= 0)
                {
                    Bestellposition bp = new Bestellposition(kt, berechneMenge(kt), false);
                    this.dc.Bestellungen.Add(bp);
                }
                else if (kt.VerbrauchPrognose3MA <= 0 )
                {
                    
                }
                else if (kt.VerbrauchPrognose3OA <= 0)
                {
                    
                }*/
            }
        }
        public int berechneMenge(KTeil kt)
        {
            // "Felge cpl K&D&H"
            if (kt.Preis == 22)
            {
                return (( Convert.ToInt32(Math.Round((double)((kt.VertriebPer0 + 50 / 4) / 10)))) * 10);
            }
            // 6,5: "Kette D&H"; 8: "Freilauf KDH"
            else if (kt.Preis == 6.5 || kt.Preis == 8)
            {
                return (( Convert.ToInt32(Math.Round((double)((kt.VertriebPer0 + 50 / 3) / 10)))) * 10);
            }
            // "Kette K" or "Sattel KDH"
            else if (kt.Preis == 5)
            {
                return (( Convert.ToInt32(Math.Round(((double)(kt.VertriebPer0 + 50 / 2) / 10)))) * 10);
            }
            else
            {
                return (int)(kt.DiskontMenge);
            }
        }
    }
}
