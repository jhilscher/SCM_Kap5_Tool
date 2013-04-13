using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFahrrad_v1
{
    /** This class is sub class from Teil and describes a produced Teil */
    class ETeil : Teil
    {
        // Class members
        int produktion = 0;
        int in_warteschlange = 0;
        int kategorie = 0;
        Dictionary<Teil, int> zusammensetzung;
        Dictionary<int, int> pos;
        List<int> benutzte_arbeitsplaetze;
        List<ETeil> istTeil = null;
        // Constructor
        public ETeil(int nummer, string bez) : base(nummer, bez)
        {
            this.zusammensetzung = new Dictionary<Teil, int>();
            this.pos = new Dictionary<int, int>();
            this.benutzte_arbeitsplaetze = new List<int>();
        }
        // Getter / Setter
        public int Produktionsmenge
        {
            get { return produktion; }
            set
            {
                this.produktion = value;
                foreach (KeyValuePair<Teil, int> kvp in zusammensetzung)
                {
                    kvp.Key.VerbrauchAktuell = kvp.Value * produktion;
                }
            }
        }
        public int InWartschlange
        {
            get { return in_warteschlange; }
            set { in_warteschlange = value; }
        }
        public int Kategorie
        {
            get { return kategorie; }
            set { kategorie = value; }
        }
        public Dictionary<Teil, int> Zusammensetzung
        {
            get { return zusammensetzung; }
        }
        // Position and Menge in Reihenfolgenplanung
        public Dictionary<int, int> Position
        {
            get { return this.pos; }
        }
        // Used stations
        public List<Arbeitsplatz> BenutzteArbeitsplaetze
        {
            get
            {
                List<Arbeitsplatz> res = new List<Arbeitsplatz>();
                foreach (int arpl in benutzte_arbeitsplaetze)
                {
                    res.Add(DataContainer.Instance.GetArbeitsplatz(arpl));
                }
                return res;
            }
        }
        public List<ETeil> IstTeilVon
        {
            get
            {
                if (this.istTeil == null)
                {
                    List<ETeil> res = new List<ETeil>();
                    foreach (ETeil et in DataContainer.Instance.ETeilList)
                    {
                        if (et.Zusammensetzung.ContainsKey(et))
                        {
                            res.Add(et);
                        }
                    }
                    this.istTeil = res;
                }
                return this.istTeil;
            }
        }
        // Functions
        public void AddArbeitsplatz(int nr)
        {
            this.benutzte_arbeitsplaetze.Add(nr);
        }
        public void AddBestandteil(Teil t, int menge)
        {
            zusammensetzung[t] = menge;
        }
        public void AddBestandteil(int t, int menge)
        {
            DataContainer container = DataContainer.Instance;
            zusammensetzung[container.GetTeil(t)] = menge;
        }
        // Equals function
        public bool Equals(ETeil et)
        {
            if (nr == et.Nummer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
