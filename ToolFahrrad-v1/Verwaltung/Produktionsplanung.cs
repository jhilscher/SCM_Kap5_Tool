using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFahrrad_v1
{
    public class Produktionsplanung
    {
        // Class members
        DataContainer dc;
        bool aufgeloest = false;
        // Constructor
        public Produktionsplanung()
        {
            dc = DataContainer.Instance;
        }
        // Aufloesung Prognosen für Produkte, berechnet die benoetigten E- und KTeile
        public void Aufloesen()
        {
            if (dc == null)
            {
                dc = DataContainer.Instance;
            }
            RekursAufloesen(dc.GetTeil(1) as ETeil);
            RekursAufloesen(dc.GetTeil(2) as ETeil);
            RekursAufloesen(dc.GetTeil(3) as ETeil);
            aufgeloest = true;
        }
        private void KategorieBestimmen()
        {
            (dc.GetTeil(1) as ETeil).Kategorie = 1;
            (dc.GetTeil(2) as ETeil).Kategorie = 1;
            (dc.GetTeil(3) as ETeil).Kategorie = 1;
            foreach (ETeil teil in dc.ListeETeile)
            {
                if (teil.Kategorie == 0)
                {
                    if (teil.IstTeilVon.Contains(dc.GetTeil(1) as ETeil) || teil.IstTeilVon.Contains(dc.GetTeil(2) as ETeil) || teil.IstTeilVon.Contains(dc.GetTeil(3) as ETeil))
                    {
                        teil.Kategorie = 2;
                    }
                    else
                    {
                        teil.Kategorie = 3;
                    }
                }
            }

        }
        public void Planen()
        {
            if (dc == null)
            {
                dc = DataContainer.Instance;
            }
            KategorieBestimmen();
            if (aufgeloest == false)
            {
                Aufloesen();
            }
            PrimaereProduktionsplanung();
            if (dc.UeberstundenErlaubt)
            {
                SetUeberstunde();
            }
            else
            {
                AnpassungMengeAnZeit();
            }
            Dictionary<int, int[]> tmp = new Dictionary<int, int[]>();
            foreach (Arbeitsplatz arbPl in dc.ArbeitsplatzList)
            {
                ReihenfolgeAnpassung(arbPl, ref tmp);
            }
            ReihenfolgePart2(tmp);
        }
        // Rekursive Prozedur zum iterieren über die Zusammensetzung der Teile
        private void RekursAufloesen(ETeil teil)
        {
            foreach (KeyValuePair<Teil, int> kvp in teil.Zusammensetzung)
            {
                kvp.Key.VerbrauchPrognose1 += kvp.Value * teil.VerbrauchPrognose1;
                kvp.Key.VerbrauchPrognose2 += kvp.Value * teil.VerbrauchPrognose2;
                if (kvp.Key is ETeil)
                {
                    RekursAufloesen(kvp.Key as ETeil);
                }
            }
            return;
        }
        // ---
        private void PrimaereProduktionsplanung()
        {
            foreach (ETeil et in dc.ListeETeile)
            {
                if (et.Kategorie == 1)
                {
                    if (et.Lagerstand - et.Pufferwert < et.VerbrauchAktuell)
                    {
                        et.Produktionsmenge = et.VerbrauchAktuell - et.Lagerstand + et.Pufferwert;
                    }
                }
                else if (et.Kategorie == 2)
                {
                    if (et.Nummer == 26)
                    {
                        if (et.Lagerstand < et.VerbrauchAktuell + et.VerbrauchPrognose1)
                        {
                            et.Produktionsmenge = et.VerbrauchPrognose2;
                        }
                    }
                    else
                    {
                        if (et.Lagerstand - et.Pufferwert < et.VerbrauchPrognose1)
                        {
                            et.Produktionsmenge = et.VerbrauchPrognose1 - et.Lagerstand + et.Pufferwert;
                        }
                    }

                }
                else if (et.Kategorie == 3)
                {
                    if (et.Lagerstand - et.Pufferwert < et.VerbrauchPrognose2)
                    {
                        et.Produktionsmenge = et.VerbrauchPrognose2 - et.Lagerstand + et.Pufferwert;
                    }
                }
            }
        }
        // ---
        public String Nachpruefen(Teil teil, int mengeNeu)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<Teil, int> kvp in (teil as ETeil).Zusammensetzung)
            {
                if (kvp.Key is KTeil)
                {
                    if (kvp.Key.Lagerstand + (kvp.Key as KTeil).ErwarteteBestellung < kvp.Value * mengeNeu)
                    {
                        sb.Append(string.Format("Nicht genug Kaufteile (nr. {0}) um die manuell gesetzte Menge({1}) an  Teil {2} herzustellen", kvp.Key.Nummer, mengeNeu, teil.Nummer));
                        sb.Append(Environment.NewLine);
                    }
                }
            }
            return sb.ToString();
        }
        // Anpassung der Menge
        private bool AnpassungMenge()
        {
            int count = 0;
            foreach (KTeil kteil in dc.ListeKTeile)
            {
                count++;
                //wenn kteile nicht ausrechen muss von irgendeinem Teil weniger 
                if (kteil.Lagerstand < kteil.VerbrauchAktuell)
                {
                    int nrToChange = 0;
                    double time = 4800;
                    foreach (ETeil et in kteil.IstTeilVon)
                    {
                        if (et.Produktionsmenge > 0)
                        {
                            foreach (Arbeitsplatz arpl in et.BenutzteArbeitsplaetze)
                            {
                                if (arpl.GetVerfuegbareZeit - arpl.GetBenoetigteZeit < time)
                                {
                                    time = arpl.GetVerfuegbareZeit - arpl.GetBenoetigteZeit;
                                    nrToChange = et.Nummer;
                                }
                            }
                        }
                        else
                        {
                            time = 4800;
                        }
                    }
                    if (nrToChange > 0)
                    {
                        (dc.GetTeil(nrToChange) as ETeil).Produktionsmenge = (dc.GetTeil(nrToChange) as ETeil).Produktionsmenge - 10;
                        return false;
                    }
                }
            }
            count = 0;
            foreach (ETeil eteil in dc.ListeETeile)
            {
                count++;
                if (eteil.Lagerstand + eteil.Produktionsmenge + eteil.InWartschlange < eteil.VerbrauchAktuell)
                {
                    int nrToChange = 0;
                    int time = 2400;
                    foreach (ETeil et in eteil.IstTeilVon)
                    {
                        foreach (Arbeitsplatz arpl in et.BenutzteArbeitsplaetze)
                        {
                            if (arpl.GetVerfuegbareZeit - arpl.GetBenoetigteZeit < time)
                            {
                                time = arpl.GetVerfuegbareZeit - arpl.GetBenoetigteZeit;
                                nrToChange = et.Nummer;
                            }
                        }
                    }
                    if (nrToChange != 0)
                    {
                        (dc.GetTeil(nrToChange) as ETeil).Produktionsmenge = (dc.GetTeil(nrToChange) as ETeil).Produktionsmenge - 10;
                        return false;
                    }
                }
            }
            return true;
        }
        // Setzt die benoetigten Ueberstunden und doppelschichten um
        private bool SetUeberstunde()
        {
            foreach (Arbeitsplatz arbl in dc.ArbeitsplatzList)
            {
                if (arbl.GetBenoetigteZeit + arbl.GetRuestZeit > arbl.GetVerfuegbareZeit)
                {
                    arbl.AddUeberMinute(Convert.ToInt32(arbl.GetBenoetigteZeit + arbl.GetRuestZeit - arbl.GetVerfuegbareZeit) / 5);
                }
            }
            return true;
        }
        // Findet die Reihenfolge für einen Aebreitsplatz
        private void ReihenfolgeAnpassung(Arbeitsplatz ap, ref Dictionary<int, int[]> tmp)
        {
            int lastPos;
            int counter = 0;
            bool lastposChgd;
            bool notInserted;
            if (!tmp.ContainsKey(ap.GetNummerArbeitsplatz))
            {
                tmp[ap.GetNummerArbeitsplatz] = new int[ap.GetHergestellteTeile.Count];
                lastPos = ap.GetHergestellteTeile.Count - 1;
                foreach (ETeil hergestellt in ap.GetHergestellteTeile) // alle Teile die an diesem Arbeitsplatz hergestellt werden
                {
                    lastposChgd = false;
                    notInserted = true;
                    foreach (KeyValuePair<Teil, int> bestandTeil in hergestellt.Zusammensetzung) //die Teile aus dennen sich die hergestellten Teile Zusammensetzen
                    {
                        if (bestandTeil.Key is ETeil)
                        {
                            if (bestandTeil.Key.Lagerstand - bestandTeil.Key.VerbrauchAktuell < 0) // Falls Verbrauch höher Lagerstand dieses Teil am Arbeitsplatz bevorzugen
                            {
                                tmp[ap.GetNummerArbeitsplatz][lastPos] = hergestellt.Nummer;
                                lastposChgd = true;
                                notInserted = false;
                            }
                            if (bestandTeil.Key.Lagerstand - bestandTeil.Key.VerbrauchAktuell >= 0) // Falls Verbrauch kleiner/gleich Lagerstand
                            {
                                bool praeferenz = false;
                                foreach (ETeil nachfolger in (bestandTeil.Key as ETeil).IstTeilVon) // Teile die von ersten Teil abhängig sind prüfen
                                {
                                    if (nachfolger.Lagerstand - nachfolger.VerbrauchAktuell < 0)
                                    {
                                        praeferenz = true; // Diese Teile bevorzugen da nicht mehr vorhanden
                                        break;
                                    }
                                }
                                if (praeferenz)
                                {
                                    int c = tmp[ap.GetNummerArbeitsplatz][0];
                                    tmp[ap.GetNummerArbeitsplatz][0] = hergestellt.Nummer;
                                    tmp[ap.GetNummerArbeitsplatz][counter] = c;
                                    notInserted = false;
                                }
                                else
                                {
                                    tmp[ap.GetNummerArbeitsplatz][counter] = hergestellt.Nummer;
                                    notInserted = false;
                                }
                            }
                        }
                    }
                    if (notInserted)
                    {
                        tmp[ap.GetNummerArbeitsplatz][counter] = hergestellt.Nummer;
                    }
                    if (lastposChgd)
                    {
                        lastPos--;
                    }
                    else
                    {
                        counter++;
                    }
                }
            }
        }
        // ---
        private void AnpassungMengeAnZeit()
        {
            double diff = 0;
            bool changed = false;
            int count = 0;
            int sumProd = 0;
            double zwischenWert = 0;
            int val;
            foreach (Arbeitsplatz ap in dc.ArbeitsplatzList)
            {
                sumProd = 0;
                diff = ap.GetVerfuegbareZeit - ap.GetBenoetigteZeit - ap.GetRuestZeit;
                if (diff < 0)
                {
                    foreach (ETeil teil in ap.GetHergestellteTeile)
                    {
                        sumProd += teil.Produktionsmenge;
                    }
                    foreach (ETeil teil in ap.GetHergestellteTeile)
                    {
                        zwischenWert = Convert.ToInt32((-diff / teil.Produktionsmenge) * sumProd);
                        val = Convert.ToInt32(Math.Round(zwischenWert / ap.WerkZeitJeStk[teil.Nummer]));
                        if (val < teil.Produktionsmenge)
                        {
                            teil.Produktionsmenge -= val;
                        }
                    }
                }
            }
        }
        // Wenn ein Wert bereits eingereiht ist in die Reihenfolge dann wird die Position zurueckgegeben ansonsten -1
        private int ReihenfolgeContains(int nr)
        {
            int pos = 0;
            foreach (int i in dc.Reihenfolge)
            {
                if (i == nr)
                {
                    return pos;
                }
                pos++;
            }
            return -1;
        }
        // ---
        private void ReihenfolgePart2(Dictionary<int, int[]> tmp)
        {
            int counter = 0;
            int oldPos = 0;
            dc.Reihenfolge = new int[dc.ListeETeile.Count];
            foreach (KeyValuePair<int, int[]> ap in tmp)
            {
                foreach (int teilNr in ap.Value)
                {
                    if ((dc.GetTeil(teilNr) as ETeil).BenutzteArbeitsplaetze.Count == 1)
                    {
                        dc.Reihenfolge[counter] = teilNr;
                        counter++;
                    }
                    else
                    {
                        oldPos = ReihenfolgeContains(teilNr);
                        if (oldPos == -1)
                        {
                            dc.Reihenfolge[counter] = teilNr;
                            counter++;
                        }
                    }
                }
            }
        }
    }
}
