using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolFahrrad_v1
{
    class Produktionsplanung
    {
        DataContainer cont;
        bool aufgeöst = false;

        public Produktionsplanung()
        {
            this.cont = DataContainer.Instance;
        }

        /// <summary>
        /// löst die Prognosen für Produkte berechnet die benötigten E und Kteile
        /// </summary>
        public void Aufloesen()
        {
            if (this.cont == null)
            {
                this.cont = DataContainer.Instance;
            }
            this.RekursAufloesen(this.cont.GetTeil(1) as ETeil);
            this.RekursAufloesen(this.cont.GetTeil(2) as ETeil);
            this.RekursAufloesen(this.cont.GetTeil(3) as ETeil);
            aufgeöst = true;

        }

        private void KategorieBestimmen()
        {
            (cont.GetTeil(1) as ETeil).Kategorie = 1;
            (cont.GetTeil(2) as ETeil).Kategorie = 1;
            (cont.GetTeil(3) as ETeil).Kategorie = 1;
            foreach (ETeil teil in cont.ListeETeile)
            {
                if (teil.Kategorie == 0)
                {
                    if (teil.IstTeilVon.Contains(cont.GetTeil(1) as ETeil) || teil.IstTeilVon.Contains(cont.GetTeil(2) as ETeil) || teil.IstTeilVon.Contains(cont.GetTeil(3) as ETeil))
                    {
                        teil.Kategorie = 2;
                    }
                    else
                    {
                        teil.Kategorie = 3;
                    }
                }
            }

        }//KategorieBestimmen


        public void Planen()
        {
            if (this.cont == null)
            {
                this.cont = DataContainer.Instance;
            }
            this.KategorieBestimmen();

            if (!aufgeöst)
            {
                this.Aufloesen();
            }
            this.PrimaereProduktionsplanung();
            //  do
            //  { }
            //  while (!this.AnpassungMenge());
            if (cont.UeberstundenErlaubt)
            {
                this.SetUeberstunde();
            }
            else
            {
                this.AnpassungMengeAnZeit();
            }
            Dictionary<int, int[]> tmp = new Dictionary<int, int[]>();
            foreach (Arbeitsplatz arbPl in this.cont.ArbeitsplatzList)
            {
                this.ReihenfolgeAnpassung(arbPl, ref tmp);
            }
            this.ReihenfolgePart2(tmp);
        }


        /// <summary>
        /// Rekursive Prozedur zum iterieren über die Zusammensetzung der Teile
        /// </summary>
        /// <param name="teil">The teil.</param>
        private void RekursAufloesen(ETeil teil)
        {
            foreach (KeyValuePair<Teil, int> kvp in teil.Zusammensetzung)
            {
                kvp.Key.VerbrauchPrognose1 += kvp.Value * teil.VerbrauchPrognose1;
                kvp.Key.VerbrauchPrognose2 += kvp.Value * teil.VerbrauchPrognose2;

                if (kvp.Key is ETeil)
                {

                    this.RekursAufloesen(kvp.Key as ETeil);
                }

            }
            return;
        }

        private void PrimaereProduktionsplanung()
        {
            foreach (ETeil et in this.cont.ListeETeile)
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

        /*    private void PrimäreProduktionsplanung()
            {
                foreach (ETeil et in this.cont.ETeilList)
                {
                    if (et.Kategorie==1)
                    {
                        et.Produktionsmenge = et.VerbrauchAktuel;
                        foreach (KeyValuePair<Teil, int> teil in et.Zusammensetzung)
                        {
                            if (teil.Key is ETeil && !teil.Key.Verwendung.Equals("KDH"))
                            {
                                (teil.Key as ETeil).Produktionsmenge += teil.Key.VerbrauchPrognose1;
                            }
                            else
                            {
                                teil.Key.VerbrauchAktuel += teil.Value*teil.Key.VerbrauchPrognose1;
                            }

                            if (teil.Key is ETeil && teil.Key.Verwendung.Equals("KDH"))
                            {
                                if (teil.Key.Lagerstand + (teil.Key as ETeil).Produktionsmenge < teil.Key.VerbrauchAktuel + teil.Key.VerbrauchPrognose1)
                                {
                                    this.cont.Sonderproduktion = true;
                                    (teil.Key as ETeil).Produktionsmenge = teil.Key.VerbrauchPrognose1 + teil.Key.VerbrauchPrognose2;
                                }
                            }
                        }
                    }//if
                    else
                    {
                        foreach (KeyValuePair<Teil, int> teil in et.Zusammensetzung)
                        {
                            if (teil.Key is ETeil && !teil.Key.Verwendung.Equals("KDH"))
                            {
                                (teil.Key as ETeil).Produktionsmenge = teil.Key.VerbrauchPrognose2;
                            }
                            else
                            {
                                teil.Key.VerbrauchAktuel += teil.Value*teil.Key.VerbrauchPrognose1;
                            }

                            if (teil.Key is ETeil && teil.Key.Verwendung.Equals("KDH"))
                            {
                                if (teil.Key.Lagerstand + (teil.Key as ETeil).Produktionsmenge < teil.Key.VerbrauchAktuel + teil.Key.VerbrauchPrognose1)
                                {
                                    this.cont.Sonderproduktion = true;
                                    (teil.Key as ETeil).Produktionsmenge = teil.Key.VerbrauchPrognose1 + teil.Key.VerbrauchPrognose2;
                                }
                            }
                        }
                    }//else  

                    et.Produktionsmenge += et.Pufferwert;
                }//foreach Eteil
            }//Primäre Produktionsplanung */



        /// <summary>
        /// Anpassung der Menge.
        /// </summary>
        /// <returns></returns>
        private bool AnpassungMenge()
        {
            int count = 0;
            foreach (KTeil kteil in this.cont.ListeKTeile)
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
                                if (arpl.ZuVerfuegungStehendeZeit - arpl.BenoetigteZeit < time)
                                {
                                    time = arpl.ZuVerfuegungStehendeZeit - arpl.BenoetigteZeit;
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
                        (this.cont.GetTeil(nrToChange) as ETeil).Produktionsmenge = (this.cont.GetTeil(nrToChange) as ETeil).Produktionsmenge - 10;
                        return false;
                    }
                }//if
            }//foreach

            count = 0;
            foreach (ETeil eteil in this.cont.ListeETeile)
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
                            if (arpl.ZuVerfuegungStehendeZeit - arpl.BenoetigteZeit < time)
                            {
                                time = arpl.ZuVerfuegungStehendeZeit - arpl.BenoetigteZeit;
                                nrToChange = et.Nummer;
                            }
                        }//foreach Arbeitsplatz
                    }//foreach Eteil
                    if (nrToChange != 0)
                    {
                        (this.cont.GetTeil(nrToChange) as ETeil).Produktionsmenge = (this.cont.GetTeil(nrToChange) as ETeil).Produktionsmenge - 10;
                        return false;
                    }
                }//if
            }
            return true;
        }// AnpassungMenge

        /// <summary>
        /// Setzt die benötigten Überstunden und doppelschichten 
        /// </summary>
        /// <returns></returns>
        private bool SetUeberstunde()
        {
            foreach (Arbeitsplatz arbl in this.cont.ArbeitsplatzList)
            {
                if (arbl.BenoetigteZeit + arbl.Ruestzeit > arbl.ZuVerfuegungStehendeZeit)
                {
                    arbl.AddUeberMinute(Convert.ToInt32(arbl.BenoetigteZeit + arbl.Ruestzeit - arbl.ZuVerfuegungStehendeZeit) / 5);
                }
                // TODO Anpassung um die Zeit besser auszunutzen
            }

            return true;
        }

        /// <summary>
        /// findet die Reihenfolge für einen Aebreitsplatz
        /// </summary>
        /// <param name="arb">Arbeitsplatz objekt</param>
        /// <param name="tmp">temporäres Dictionary</param>
        private void ReihenfolgeAnpassung(Arbeitsplatz arb, ref Dictionary<int, int[]> tmp)
        {
            int lastPos;
            int counter = 0;
            bool lastposChgd;
            bool notInserted;
            if (!tmp.ContainsKey(arb.Nummer))
            {
                tmp[arb.Nummer] = new int[arb.HergestelteTeile.Count];
                lastPos = arb.HergestelteTeile.Count - 1;
                foreach (ETeil hergestellt in arb.HergestelteTeile) // alle Teile die an diesem Arbeitsplatz hergestellt werden
                {
                    lastposChgd = false;
                    notInserted = true;
                    foreach (KeyValuePair<Teil, int> bestandTeil in hergestellt.Zusammensetzung) //die Teile aus dennen sich die hergestellten Teile Zusammensetzen
                    {
                        if (bestandTeil.Key is ETeil)
                        {
                            if (bestandTeil.Key.Lagerstand - bestandTeil.Key.VerbrauchAktuell < 0) // Falls Verbrauch höher Lagerstand dieses Teil am Arbeitsplatz bevorzugen
                            {
                                tmp[arb.Nummer][lastPos] = hergestellt.Nummer;
                                lastposChgd = true;
                                notInserted = false;
                            }
                            if (bestandTeil.Key.Lagerstand - bestandTeil.Key.VerbrauchAktuell >= 0) // Falls Verbrauch kleiner/gleich Lagerstand
                            {
                                bool präferenz = false;
                                foreach (ETeil nachfolger in (bestandTeil.Key as ETeil).IstTeilVon) // Teile die von ersten Teil abhängig sind prüfen
                                {
                                    if (nachfolger.Lagerstand - nachfolger.VerbrauchAktuell < 0)
                                    {
                                        präferenz = true; // Diese Teile bevorzugen da nicht mehr vorhanden
                                        break;
                                    }
                                }
                                if (präferenz)
                                {
                                    int c = tmp[arb.Nummer][0];
                                    tmp[arb.Nummer][0] = hergestellt.Nummer;
                                    tmp[arb.Nummer][counter] = c;
                                    notInserted = false;

                                }
                                else
                                {
                                    tmp[arb.Nummer][counter] = hergestellt.Nummer;
                                    notInserted = false;

                                }
                            }
                        }
                    }//foreach KeyValuePair<Teil,int>
                    if (notInserted)
                    {
                        tmp[arb.Nummer][counter] = hergestellt.Nummer;
                    }
                    if (lastposChgd)
                    {
                        lastPos--;
                    }
                    else
                    {
                        counter++;
                    }
                }//foreach Eteil
            }//if
        }

        private void AnpassungMengeAnZeit()
        {
            double diff = 0;
            bool changed = false;
            int count = 0;
            int sumProd = 0;
            double zwischenWert = 0;
            int val;

            foreach (Arbeitsplatz arbl in this.cont.ArbeitsplatzList)
            {
                sumProd = 0;
                diff = arbl.ZuVerfuegungStehendeZeit - arbl.BenoetigteZeit - arbl.Ruestzeit;
                if (diff < 0)
                {
                    foreach (ETeil teil in arbl.HergestelteTeile)
                    {
                        sumProd += teil.Produktionsmenge;
                    }

                    foreach (ETeil teil in arbl.HergestelteTeile)
                    {
                        zwischenWert = Convert.ToInt32((-diff / teil.Produktionsmenge) * sumProd);
                        val = Convert.ToInt32(Math.Round(zwischenWert / arbl.WerkZeitJeStk[teil.Nummer]));
                        if (val < teil.Produktionsmenge)
                        {
                            teil.Produktionsmenge -= val;
                        }
                        else
                        {

                        }
                    }
                }
            }

        }

        /// <summary>
        /// wenn ein Wert bereits eingereiht ist in die Reihenfolge dann wird die Position zurückgegeben ansonsten -1
        /// </summary>
        /// <param name="nr">The nr.</param>
        /// <returns></returns>
        private int ReihenfolgeContains(int nr)
        {
            int pos = 0;
            foreach (int i in cont.Reihenfolge)
            {
                if (i == nr)
                {
                    return pos;
                }
                pos++;
            }
            return -1;
        }

        private void ReihenfolgePart2(Dictionary<int, int[]> tmp)
        {
            int counter = 0;
            int oldPos = 0;

            cont.Reihenfolge = new int[cont.ListeETeile.Count];
            foreach (KeyValuePair<int, int[]> abPl in tmp)
            {
                foreach (int teilNr in abPl.Value)
                {
                    if ((this.cont.GetTeil(teilNr) as ETeil).BenutzteArbeitsplaetze.Count == 1)
                    {
                        cont.Reihenfolge[counter] = teilNr;
                        counter++;
                    }
                    else
                    {
                        oldPos = this.ReihenfolgeContains(teilNr);
                        if (oldPos == -1)
                        {
                            cont.Reihenfolge[counter] = teilNr;
                            counter++;
                        }

                        //TODO: anpassen in der Reihenfolge
                    }
                }
            }
        }
    }
}
