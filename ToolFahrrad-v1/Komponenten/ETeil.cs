using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFahrrad_v1
{
    /** This class is sub class from Teil and describes a produced Teil */
    public class ETeil : Teil
    {
        // Class members
        private int produktionsMenge = 0;
        private int inWarteschlange = 0;
        private int inBearbeitung = 0;
        private bool istEndProdukt = false;
        public bool IstEndProdukt
        {
            get { return istEndProdukt; }
            set { istEndProdukt = value; }
        }
        Dictionary<Teil, int> zusammensetzung;
        Dictionary<int, int> position;
        List<int> benutzteArbeitsplaetze;
        List<ETeil> istTeil = null;
        // Constructor
        public ETeil(int nummer, string bez) : base(nummer, bez)
        {
            zusammensetzung = new Dictionary<Teil, int>();
            position = new Dictionary<int, int>();
            benutzteArbeitsplaetze = new List<int>();
        }
        // Getter / Setter
        public int ProduktionsMenge
        {
            get { return produktionsMenge; }
            set
            {
                this.produktionsMenge = value;
                foreach (KeyValuePair<Teil, int> kvp in zusammensetzung)
                {
                    kvp.Key.VertriebAktuell = kvp.Value * produktionsMenge;
                }
            }
        }
        public int InWartschlange
        {
            get { return inWarteschlange; }
            set { inWarteschlange = value; }
        }
        public int InBearbeitung
        {
            get { return inBearbeitung; }
            set { inBearbeitung = value; }
        }
        public Dictionary<Teil, int> Zusammensetzung
        {
            get { return zusammensetzung; }
        }
        // Position and Menge in Reihenfolgenplanung
        public Dictionary<int, int> Position
        {
            get { return position; }
        }
        // Used stations
        public List<Arbeitsplatz> BenutzteArbeitsplaetze
        {
            get
            {
                List<Arbeitsplatz> res = new List<Arbeitsplatz>();
                foreach (int arpl in benutzteArbeitsplaetze)
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
                    foreach (ETeil et in DataContainer.Instance.ListeETeile)
                    {
                        if (et.Zusammensetzung.ContainsKey(et))
                        {
                            res.Add(et);
                        }
                    }
                    istTeil = res;
                }
                return istTeil;
            }
        }
        /// <summary>
        /// Functions
        /// </summary>
        /// <param name="index"></param>
        /// <param name="vTeil"></param>
 
        public void SetProduktionsMenge(int index, ETeil vTeil)
        {
            if (istEndProdukt == true)
            {
                produktionsMenge = vertriebAktuell + pufferwert - lagerstand - inWarteschlange - inBearbeitung;
            }
            else
            {
                // Set members
                vertriebAktuell = vTeil.ProduktionsMenge + vTeil.InWartschlange;
                pufferwert = vTeil.Pufferwert;
                // Calculation
                //TODO: das muss man ändern
                if (Verwendung.Contains("KDH") == false || (Verwendung.Contains("KDH") == true && index == 1))
                {
                    produktionsMenge = vertriebAktuell + pufferwert - lagerstand - inWarteschlange - inBearbeitung;
                }
            }
        }
        public void AddArbeitsplatz(int nr)
        {
            benutzteArbeitsplaetze.Add(nr);
        }
        public void AddBestandteil(Teil t, int menge)
        {
            zusammensetzung[t] = menge;
        }
        public void AddBestandteil(int t, int menge)
        {
            DataContainer dc = DataContainer.Instance;
            zusammensetzung[dc.GetTeil(t)] = menge;
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
