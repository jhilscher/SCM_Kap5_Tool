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
                foreach (KTeil kt in this.dc.ListeKTeile)
                {
                    kt.berechnung_verbrauch_prognose();
                }
            }
            public void erzeugen_liste_bestellungen()
            {
                /*DataContainer data = DataContainer.Instance;
                List<Kaufteil> kaufteillist = data.KaufteilList;
                List<Bestellposition> bestellungenlist = data.Bestellung;*/
                //berechnung_verbrauch_prognose(data);
                foreach (KTeil kt in dc.ListeKTeile)
                {
                    if (kt.VerbrauchPrognose1OA <= 0)
                    {
                        Bestellposition bp = new Bestellposition(kt, menge_rechnung(kt), true);
                        this.dc.Bestellungen.Add(bp);
                    }
                    else if (kt.VerbrauchPrognose1MA <= 0)
                    {
                        Bestellposition bp = new Bestellposition(kt, menge_rechnung(kt), true);
                        this.dc.Bestellungen.Add(bp);
                    }
                    else if (kt.VerbrauchPrognose2OA <= 0)
                    {
                        Bestellposition bp = new Bestellposition(kt, menge_rechnung(kt), false);
                        this.dc.Bestellungen.Add(bp);
                    }
                    /*else if (k.verbrauch_prognose_after_after_mit_abweichung <= 0)
                    {
                        Bestellposition bp = new Bestellposition(k, menge_rechnung(k),false);
                        bestellungenlist.Add();
                    }*/
                }
            }
            public int menge_rechnung(KTeil kt)
            {
                int val;
                if (kt.Preis == 22)
                {
                    val = Convert.ToInt32(Math.Round((double)((kt.VerbrauchAktuell + 50 / 4) / 10)));
                    return val * 10;
                }
                else if (kt.Preis == 6.5 || kt.Preis == 8)
                {
                    val = Convert.ToInt32(Math.Round((double)((kt.VerbrauchAktuell + 50 / 3) / 10)));
                    return val * 10;
                }
                else if (kt.Preis == 5)
                {
                    val = Convert.ToInt32(Math.Round(((double)(kt.VerbrauchAktuell + 50 / 2) / 10)));
                    return val * 10;
                }
                else
                {
                    return (int)(kt.Diskontmenge);
                }
        }
    }
}
