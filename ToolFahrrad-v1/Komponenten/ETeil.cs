using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolFahrrad_v1.Komponenten;

namespace ToolFahrrad_v1
{
    /** This class is sub class from Teil and describes a produced Teil */
    public class ETeil : Teil
    {
        // Class members
        private double wert;
        private int puffer;
        private bool kdhUpdate = false;
        private int produktionsMenge = 0;
        private int vaterInWarteschlange = 0;
        private int inWarteschlange = 0;
        private int inBearbeitung = 0;
        private bool istEndProdukt = false;
        private Dictionary<int, int[]> kdhPuffer;
        private Dictionary<string, int> kdhProduktionsmenge;
        private Dictionary<int, int[]> kdhVaterInWarteschlange;
        Dictionary<Teil, int> zusammensetzung;
        Dictionary<int, int> position;
        List<int> benutzteArbeitsplaetze;
        List<ETeil> istTeil = null;
        // Constructor
        public ETeil(int nummer, string bez)
            : base(nummer, bez)
        {
            wert = 0.0;
            puffer = -1;
            zusammensetzung = new Dictionary<Teil, int>();
            position = new Dictionary<int, int>();
            benutzteArbeitsplaetze = new List<int>();
            KDHaufNULL();
            kdhProduktionsmenge = new Dictionary<string, int>();
        }
        public void KDHaufNULL(){
        int[] array = new int[]{-1,-1,-1};
        int[] array2 = new int[] { 0, 0, 0 };
            kdhPuffer = new Dictionary<int,int[]>();
            kdhVaterInWarteschlange = new Dictionary<int, int[]>();
            kdhPuffer.Add(26, array);
            kdhPuffer.Add(16, array);
            kdhPuffer.Add(17, array);
            kdhVaterInWarteschlange.Add(26, array2);
            kdhVaterInWarteschlange.Add(16, array2);
            kdhVaterInWarteschlange.Add(17, array2);
        }
        // Getter / Setter
        public double Wert
        {
            get { return wert; }
            set { wert = value; }
        }
        public bool KdhUpdate
        {
            get { return kdhUpdate; }
            set { kdhUpdate = value; }
        }
        public int Puffer
        {
            get { return puffer; }
            set { puffer = value; }
        }
        public int VaterInWarteschlange
        {
            get { return vaterInWarteschlange; }
            set { vaterInWarteschlange = value; }
        }
        public bool IstEndProdukt
        {
            get { return istEndProdukt; }
            set { istEndProdukt = value; }
        }
        public Dictionary<int, int[]> KdhPuffer
        {
            get { return kdhPuffer; }
            set { kdhPuffer = value; }
        }
        public Dictionary<string, int> KdhProduktionsmenge
        {
            get { return kdhProduktionsmenge; }
            set { kdhProduktionsmenge = value; }
        }
        public Dictionary<int, int[]> KdhVaterInWarteschlange
        {
            get { return kdhVaterInWarteschlange; }
            set { kdhVaterInWarteschlange = value; }
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
            set
            {
                if (inWarteschlange != 0)
                {
                    inWarteschlange += value;
                }
                else
                {
                    inWarteschlange = value;
                }
            }
        }
        public int InBearbeitung
        {
            get { return inBearbeitung; }
            set
            {
                if (inBearbeitung != 0)
                {
                    inBearbeitung += value;
                }
                else
                {
                    inBearbeitung = value;
                }
            }
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
                    if (vaterTeil.ProduktionsMengePer0 > 0)
                        vertriebPer0 = vaterTeil.ProduktionsMengePer0;
                    else
                        vertriebPer0 = 0;
                    // Calculation
                    if (Verwendung.Contains("KDH") == false)
                    {
                        vaterInWarteschlange = vaterTeil.InWartschlange;
                        if (puffer == -1)
                        {
                            puffer = vaterTeil.Puffer;
                        }
                        produktionsMenge = vertriebPer0 + vaterInWarteschlange + Puffer - lagerstand - inWarteschlange - inBearbeitung;
                        Aufgeloest = true;
                    }
                    else
                    {
                        int pufTemp = 0;
                        foreach (KeyValuePair<int, int[]> pair in kdhPuffer) {
                            if (pair.Key.Equals(nr)) {
                                if (pair.Value[index - 1] == -1) {
                                    pair.Value[index - 1] = vaterTeil.Puffer;
                                }
                                pufTemp = pair.Value[index - 1];
                                kdhVaterInWarteschlange[nr][index - 1] = vaterTeil.InWartschlange;
                            }
                        }
                        int pmTemp = 0;
                        if (index == 1) {
                            pmTemp = vertriebPer0 + kdhVaterInWarteschlange[nr][index - 1] + pufTemp - lagerstand - inWarteschlange - inBearbeitung;
                        }
                        else {
                            pmTemp = vertriebPer0 + kdhVaterInWarteschlange[nr][index - 1] + pufTemp;
                        }
                        kdhProduktionsmenge.Add(index.ToString() + "-" + nr.ToString(), pmTemp);

                        //if (index == 1 && puffer != -1) {
                        //    kdhUpdate = true;
                        //    produktionsMenge = 0;
                        //}
                        //if (puffer == -1)
                        //{
                        //    puffer = 0;
                        //    produktionsMenge = 0;
                        //}
                        //if (kdhUpdate == false)
                        //    puffer += vaterTeil.Puffer;
                        //if (index == 1) {
                        //    produktionsMenge += vertriebPer0 + vaterInWarteschlange + Puffer - lagerstand - inWarteschlange - inBearbeitung;
                        //    Aufgeloest = false;
                        //}
                        //else
                        //{
                        //    produktionsMenge += vertriebPer0 + Puffer - lagerstand - inWarteschlange - inBearbeitung;
                        //    Aufgeloest = true;
                        //}
                    }
                }
            }
        }
        // Public function to change members puffer (0)
        public void FeldGeandert(int member, int value, int p)
        {
            aufgeloest = false;
            // puffer
            if (member == 0)
            {
                if (Verwendung.Contains("KDH") && p.Equals(0))
                    puffer = value;
                else if (zusammensetzung.Count() != 0 && DataContainer.Instance.BerechneKindTeil == true)
                {
                    if (!Verwendung.Contains("KDH"))
                        puffer = value;
                    foreach (KeyValuePair<Teil, int> kvp in zusammensetzung)
                    {
                        if (kvp.Key is ETeil)
                        {
                            ETeil et = kvp.Key as ETeil;
                            et.FeldGeandert(member, value, 1);
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
