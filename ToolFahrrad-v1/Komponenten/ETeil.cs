using System.Collections.Generic;
using System.Linq;

namespace ToolFahrrad_v1.Komponenten
{
    /** This class is sub class from Teil and describes a produced Teil */
    public class ETeil : Teil
    {
        // Class members
        private int _produktionsMenge;
        private int _inWarteschlange;
        private int _inBearbeitung;
        readonly Dictionary<Teil, int> _zusammensetzung;
        readonly Dictionary<int, int> _position;
        readonly List<int> _benutzteArbeitsplaetze;
        List<ETeil> _istTeil;
        // Constructor
        public ETeil(int nummer, string bez)
            : base(nummer, bez)
        {
            IstEndProdukt = false;
            KdhUpdate = false;
            Wert = 0.0;
            Puffer = -1;
            _zusammensetzung = new Dictionary<Teil, int>();
            _position = new Dictionary<int, int>();
            _benutzteArbeitsplaetze = new List<int>();
            KDHaufNULL();
            KdhProduktionsmenge = new Dictionary<string, int>();
        }
        public void KDHaufNULL(){
        var array = new[]{-1,-1,-1};
        var array2 = new[] { 0, 0, 0 };
            KdhPuffer = new Dictionary<int,int[]>();
            KdhVaterInWarteschlange = new Dictionary<int, int[]>();
            KdhPuffer.Add(26, array);
            KdhPuffer.Add(16, array);
            KdhPuffer.Add(17, array);
            KdhVaterInWarteschlange.Add(26, array2);
            KdhVaterInWarteschlange.Add(16, array2);
            KdhVaterInWarteschlange.Add(17, array2);
        }
        // Getter / Setter
        public double Wert { get; set; }
        public bool KdhUpdate { get; set; }
        public int Puffer { get; set; }
        public int VaterInWarteschlange { get; set; }
        public bool IstEndProdukt { get; set; }
        public Dictionary<int, int[]> KdhPuffer { get; set; }
        public Dictionary<string, int> KdhProduktionsmenge { get; set; }
        public Dictionary<int, int[]> KdhVaterInWarteschlange { get; set; }

        public int ProduktionsMengePer0
        {
            get { return _produktionsMenge; }
            set
            {
                _produktionsMenge = value;
                foreach (KeyValuePair<Teil, int> kvp in _zusammensetzung)
                {
                    kvp.Key.VertriebPer0 = kvp.Value * _produktionsMenge;
                }
            }
        }
        public int InWartschlange
        {
            get { return _inWarteschlange; }
            set
            {
                if (_inWarteschlange != 0)
                {
                    _inWarteschlange += value;
                }
                else
                {
                    _inWarteschlange = value;
                }
            }
        }
        public int InBearbeitung
        {
            get { return _inBearbeitung; }
            set
            {
                if (_inBearbeitung != 0)
                {
                    _inBearbeitung += value;
                }
                else
                {
                    _inBearbeitung = value;
                }
            }
        }
        public Dictionary<Teil, int> Zusammensetzung
        {
            get { return _zusammensetzung; }
        }
        // Position and Menge in Reihenfolgenplanung
        public Dictionary<int, int> Position
        {
            get { return _position; }
        }
        // Used stations
        public List<Arbeitsplatz> BenutzteArbeitsplaetze
        {
            get
            {
                return _benutzteArbeitsplaetze.Select(arpl => DataContainer.Instance.GetArbeitsplatz(arpl)).ToList();
            }
        }
        public List<ETeil> IstTeilVon
        {
            get
            {
                if (_istTeil == null)
                {
                    var res = DataContainer.Instance.ListeETeile.Where(et => et.Zusammensetzung.ContainsKey(et)).ToList();
                    _istTeil = res;
                }
                return _istTeil;
            }
        }
        public void SetProduktionsMenge(int index, ETeil vaterTeil)
        {
            if (Aufgeloest == false)
            {
                if (IstEndProdukt)
                {
                    _produktionsMenge = VertriebPer0 + Puffer - Lagerstand - _inWarteschlange - _inBearbeitung;
                }
                else
                {
                    // Set members
                    VertriebPer0 = vaterTeil.ProduktionsMengePer0 > 0 ? vaterTeil.ProduktionsMengePer0 : 0;
                    // Calculation
                    if (Verwendung.Contains("KDH") == false)
                    {
                        VaterInWarteschlange = vaterTeil.InWartschlange;
                        if (Puffer == -1)
                        {
                            Puffer = vaterTeil.Puffer;
                        }
                        _produktionsMenge = VertriebPer0 + VaterInWarteschlange + Puffer - Lagerstand - _inWarteschlange - _inBearbeitung;
                        Aufgeloest = true;
                    }
                    else
                    {
                        var pufTemp = 0;
                        foreach (KeyValuePair<int, int[]> pair in KdhPuffer.Where(pair => pair.Key.Equals(Nummer)))
                        {
                            if (pair.Value[index - 1] == -1) {
                                pair.Value[index - 1] = vaterTeil.Puffer;
                            }
                            pufTemp = pair.Value[index - 1];
                            KdhVaterInWarteschlange[Nummer][index - 1] = vaterTeil.InWartschlange;
                        }
                        int pmTemp;
                        if (index == 1) {
                            pmTemp = VertriebPer0 + KdhVaterInWarteschlange[Nummer][index - 1] + pufTemp - Lagerstand - _inWarteschlange - _inBearbeitung;
                        }
                        else {
                            pmTemp = VertriebPer0 + KdhVaterInWarteschlange[Nummer][index - 1] + pufTemp;
                        }
                        KdhProduktionsmenge.Add(index + "-" + Nummer, pmTemp);

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
            Aufgeloest = false;
            // puffer
            if (member == 0)
            {
                if (Verwendung.Contains("KDH") && p.Equals(0))
                    Puffer = value;
                else if (_zusammensetzung.Count() != 0 && DataContainer.Instance.BerechneKindTeil)
                {
                    if (!Verwendung.Contains("KDH"))
                        Puffer = value;
                    foreach (var et in from kvp in _zusammensetzung where kvp.Key is ETeil select kvp.Key as ETeil)
                    {
                        et.FeldGeandert(member, value, 1);
                    }
                }
            }
        }
        public void AddArbeitsplatz(int nummerNr)
        {
            _benutzteArbeitsplaetze.Add(nummerNr);
        }
        public void AddBestandteil(Teil t, int menge)
        {
            _zusammensetzung[t] = menge;
        }
        public void AddBestandteil(int t, int menge)
        {
            var dc = DataContainer.Instance;
            _zusammensetzung[dc.GetTeil(t)] = menge;
        }
        // Equals function
        public bool Equals(ETeil et)
        {
            return Nummer == et.Nummer;
        }
    }
}
