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
        private int puffer;
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
            puffer = -1;
            zusammensetzung = new Dictionary<Teil, int>();
            position = new Dictionary<int, int>();
            benutzteArbeitsplaetze = new List<int>();
        }
        // Getter / Setter
        public int Puffer
        {
            get { return puffer; }
            set { puffer = value; }
        }
        public int ProduktionsMengePer0
        {
            get { return produktionsMenge; }
            set
            {
                produktionsMenge = value;
                foreach (KeyValuePair<Teil, int> kvp in zusammensetzung)
                {
                    kvp.Key.VertriebPer0 = kvp.Value * produktionsMenge;
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
        public void SetProduktionsMenge(int index, ETeil vaterTeil)
        {
            if (Aufgeloest == false)
            {
                if (istEndProdukt == true)
                {
                    produktionsMenge = vertriebPer0 + Puffer - lagerstand - inWarteschlange - inBearbeitung;
                }
                else
                {
                    // Set members
                    vertriebPer0 = vaterTeil.ProduktionsMengePer0 + vaterTeil.InWartschlange;
                    if (puffer == -1)
                    {
                        puffer = vaterTeil.Puffer;
                    }
                    // Calculation
                    if (Verwendung.Contains("KDH") == false)
                    {
                        produktionsMenge = vertriebPer0 + Puffer - lagerstand - inWarteschlange - inBearbeitung;
                    }
                    else
                    {
                        produktionsMenge += (vertriebPer0 + Puffer - lagerstand - inWarteschlange - inBearbeitung) / 3;
                    }
                }
                Aufgeloest = true;
            }
        }
        // Public function to change members puffer (0) or produktionsMenge (1)
        public void FeldGeandert(int member, int value)
        {
            if (aufgeloest == true)
            {
                aufgeloest = false;
                // puffer
                if (member == 0)
                {
                    puffer = value;
                    if (zusammensetzung.Count() != 0 && DataContainer.Instance.BerechneKindTeil == true)
                    {
                        foreach (KeyValuePair<Teil, int> kvp in zusammensetzung)
                        {
                            if (kvp.Key is ETeil)
                            {
                                ETeil et = kvp.Key as ETeil;
                                et.FeldGeandert(member, value);
                            }
                        }
                    }
                }
                // produktionsMenge
                else if (member == 1)
                {
                    produktionsMenge = value;
                    if (zusammensetzung.Count() != 0 && DataContainer.Instance.BerechneKindTeil == true)
                    {
                        foreach (KeyValuePair<Teil, int> kvp in zusammensetzung)
                        {
                            if (kvp.Key is ETeil)
                            {
                                ETeil et = kvp.Key as ETeil;
                                et.FeldGeandert(member, value);
                            }
                        }
                    }
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
