using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFahrrad_v1
{
    public class Arbeitsplatz
    {
        DataContainer dc;
        // Class members
        protected int nr;
        public int zeit = 2400;
        protected int anz_schichten = 1;
        protected int anz_uebermin = 0;
        private int ruestungVorPeriode = 0;
        private int ruestNew = 0;
        public int RuestNew
        {
            get { return ruestNew; }
            set { ruestNew = value; }
        }
        private int ruestungCustom = 0; //Mittelwert
        private bool geaendert;

        public bool Geaendert
        {
            get { return geaendert; }
            set { geaendert = value; }
        }
        public int RuestungCustom
        {
            get { return ruestungCustom; }
        }
        private int leerzeit = 0;
        int warteschlangen_zeit = 0;

        public int Leerzeit
        {
            get { return leerzeit; }
            set { leerzeit = value; }
        }
        public Arbeitsplatz()
        {

        }

        // Needed time to produce Teil with given number
        private Dictionary<int, int> werk_zeiten;
        protected Dictionary<int, int> ruest_zeiten;
        private Dictionary<int, int> naechster_schritt;
        // Constructor
        public Arbeitsplatz(int nr_neu)
        {
            this.geaendert = false;
            this.nr = nr_neu;
            this.ruest_zeiten = new Dictionary<int, int>();
            this.werk_zeiten = new Dictionary<int, int>();
            this.naechster_schritt = new Dictionary<int, int>();
        }
        // Getter / Setter
        public int GetNummerArbeitsplatz
        {
            get { return nr; }
        }
        public int RuestungVorPeriode
        {
            get { return ruestungVorPeriode; }
            set { ruestungVorPeriode = value; }
        }
        public Dictionary<int, int> WerkZeitJeStk
        {
            get { return werk_zeiten; }
        }

        /// <summary>
        /// Kapazitätsplan
        /// </summary>
        public double GetRuestZeit
        {            
            get
            {
                if (geaendert == true)
                    this.ruestNew = 0;

                dc = DataContainer.Instance;
                double sum = 0;
                foreach (KeyValuePair<int, int> kvp in ruest_zeiten)
                {
                    sum += kvp.Value;
                    if ((dc.GetTeil(kvp.Key) as ETeil).ProduktionsMengePer0 > 0)
                        ++this.ruestNew;
                }
                return sum / ruest_zeiten.Count();

                //ruestungCustom = (newRuest + ruestungVorPeriode) / 2;
                //if (ruestungCustom < newRuest)
                //    ruestungCustom = newRuest;

            }
        }

        public void CustomRuestungGeaendert(int ruest)
        {
            if (geaendert == false)
            {
                this.ruestungCustom = (ruestNew + ruestungVorPeriode) / 2;
                if (ruestungCustom < ruestNew)
                    ruestungCustom = ruestNew;
                geaendert = true;
            }
            else 
            {
                if(ruest != -1)
                    this.ruestungCustom = ruest;
            }
        }

        /// <summary>
        /// Kapazitätsplan:
        /// Kapazitätbedarf
        /// </summary>
        public int GetBenoetigteZeit
        {
            get
            {
                dc = DataContainer.Instance;
                int result = 0;
                foreach (KeyValuePair<int, int> kvp in werk_zeiten)
                {
                    int prMenge = (dc.GetTeil(kvp.Key) as ETeil).ProduktionsMengePer0;
                    if (prMenge < 0)
                        prMenge = 0;
                    result += kvp.Value * prMenge;
                }
                result += warteschlangen_zeit;
                return result;
            }
        }

        /// <summary>
        /// In Initialisierung
        /// </summary>
        /// <param name="teil">NR</param>
        /// <param name="zeit">ZEIT</param>
        public void AddWerkzeit(int teil, int zeit)
        {
            if (zeit < 0)
            {
                throw new InvalidValueException(zeit.ToString(), string.Format("Werkzeit am Arbeitsplatz {0}", nr));
            }
            if (naechster_schritt.ContainsKey(teil) == false)
            {
                naechster_schritt[teil] = -1;
            }
            if ((werk_zeiten.ContainsKey(teil) && werk_zeiten[teil] == 0) || werk_zeiten.ContainsKey(teil) == false)
            {
                werk_zeiten[teil] = zeit;
                if (werk_zeiten.ContainsKey(teil) == false)
                {
                    ruest_zeiten[teil] = 0;
                }
                if ((DataContainer.Instance.GetTeil(teil) as ETeil).BenutzteArbeitsplaetze.Contains(this) == false)
                {
                    (DataContainer.Instance.GetTeil(teil) as ETeil).AddArbeitsplatz(nr);
                }
            }
            if (werk_zeiten.ContainsKey(teil) == false && werk_zeiten[teil] != 0 && werk_zeiten[teil] != zeit)
            {
                throw new InvalidValueException(string.Format("Am Arbeitsplatz {0} ist bereits eine Werkzeit für das Teil {1} hinterlegt!", nr, teil));
            }
        }


        public void AddRuestzeit(int teil, int zeit)
        {
            if (zeit < 0)
            {
                throw new InvalidValueException(zeit.ToString(), string.Format("Ruestzeit am Arbeitsplatz {0}", nr));
            }
            if ((ruest_zeiten.ContainsKey(teil) && ruest_zeiten[teil] == 0) || ruest_zeiten.ContainsKey(teil) == false)
            {
                ruest_zeiten[teil] = zeit;
                if (werk_zeiten.ContainsKey(teil) == false)
                {
                    werk_zeiten[teil] = 0;
                }
            }
            if (ruest_zeiten.ContainsKey(teil) == false && this.ruest_zeiten[teil] != 0)
            {
                throw new InvalidValueException(string.Format("Am Arbeitsplatz {0} ist bereits eine Rüstzeit für das Teil {1} hinterlegt!", nr, teil));
            }
        }
        // Add Teil in queue with given number of Teil, amount and current place
        public void AddWarteschlange(int nr, int menge, bool akt_platz)
        {
            if (akt_platz == true)
            {
                (DataContainer.Instance.GetTeil(nr) as ETeil).InWartschlange += menge;
            }
            this.warteschlangen_zeit += menge * werk_zeiten[nr];
            if (naechster_schritt[nr] != -1)
            {
                DataContainer.Instance.GetArbeitsplatz(naechster_schritt[nr]).AddWarteschlange(nr, menge, false);
            }
        }
        public int UeberMin
        {
            get { return anz_uebermin; }
            set { anz_uebermin = value; }
        }
        // Adds "Ueberstunden" minutes per day
        public bool AddUeberMinute(int min)
        {
            if (anz_uebermin + min < 240)
            {
                anz_uebermin += min;
                return true;
            }
            else
            {
                if (AddSchicht())
                {
                    anz_uebermin = 0;
                    return true;
                }
            }
            return false;
        }
        public int AnzSchichten
        {
            get { return anz_schichten; }
            set { anz_schichten = value; }
        }
        public bool AddSchicht()
        {
            if (anz_schichten < 2)
            {
                anz_schichten++;
                return true;
            }
            return false;
        }
        public int GetVerfuegbareZeit
        {
            get { return anz_schichten * 2400 + anz_uebermin * 5; }
        }
        public List<ETeil> GetHergestellteTeile
        {
            get
            {
                List<ETeil> list = new List<ETeil>();
                DataContainer dc = DataContainer.Instance;
                foreach (KeyValuePair<int, int> res in werk_zeiten)
                {
                    list.Add(dc.GetTeil(res.Key) as ETeil);
                }
                return list;
            }
        }
        public Dictionary<int, int> NaechsterArbeitsplatz
        {
            get { return naechster_schritt; }
            set { naechster_schritt = value; }
        }
    }
}
