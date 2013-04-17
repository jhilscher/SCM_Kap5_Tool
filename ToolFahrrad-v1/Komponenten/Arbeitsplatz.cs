using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFahrrad_v1
{
    public class Arbeitsplatz
    {
        // Class members
        protected int nr;
        protected int anz_schichten = 1;
        protected int anz_uebermin = 0;
        private int anz_ruestung = 0;
        int warteschlangen_zeit = 0;
        // Needed time to produce Teil with given number
        private Dictionary<int, int> werk_zeiten;
        protected Dictionary<int, int> ruest_zeiten;
        private Dictionary<int, int> naechster_schritt;
        // Constructor
        public Arbeitsplatz(int nr_neu)
        {
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
        public int AnzRuestung
        {
            set { anz_ruestung = value; }
        }
        public Dictionary<int, int> WerkZeitJeStk
        {
            get { return werk_zeiten; }
        }
        public double GetRuestZeit
        {
            get
            {
                double sum = 0;
                foreach (KeyValuePair<int, int> kvp in ruest_zeiten)
                {
                    sum += kvp.Value;
                }
                return (sum / ruest_zeiten.Count) * anz_ruestung;
            }
        }
        public int GetBenoetigteZeit
        {
            get
            {
                DataContainer data = DataContainer.Instance;
                int res = 0;
                foreach (KeyValuePair<int, int> kvp in werk_zeiten)
                {
                    res += kvp.Value * (data.GetTeil(kvp.Key) as ETeil).Produktionsmenge;
                }
                res += warteschlangen_zeit;
                return res;
            }
        }
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
