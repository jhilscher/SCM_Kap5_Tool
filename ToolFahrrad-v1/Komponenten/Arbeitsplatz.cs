using System.Collections.Generic;
using System.Linq;

namespace ToolFahrrad_v1.Komponenten
{
    public class Arbeitsplatz
    {
        DataContainer _dc;
        // Class members
        public int ZeitErsteSchicht = 2400;
        public int ZeitZweiteSchicht = 4800;
        public int Limit = 7200;

        protected int anz_schichten = 1;
        protected int anz_uebermin = 0;
        public int RuestNew { get; set; }

        public bool Geaendert { get; set; }

        public int RuestungCustom { get; set; }

        int _warteschlangenZeit;

        public int Leerzeit { get; set; }

        public Arbeitsplatz()
        {
            Leerzeit = 0;
            RuestungCustom = 0;
            RuestNew = 0;
        }

        // Needed time to produce Teil with given number
        public Dictionary<int, int> WerkZeiten { get; private set; }

        public Dictionary<int, int> RuestZeiten { get; private set; }

        private Dictionary<int, int> _naechsterSchritt;
        // Constructor
        public Arbeitsplatz(int nrNeu) {
            Leerzeit = 0;
            RuestungCustom = 0;
            RuestNew = 0;
            Geaendert = false;
            GetNummerArbeitsplatz = nrNeu;
            RuestZeiten = new Dictionary<int, int>();
            WerkZeiten = new Dictionary<int, int>();
            _naechsterSchritt = new Dictionary<int, int>();
        }
        // Getter / Setter
        public int GetNummerArbeitsplatz { get; protected set; }

        public int RuestungVorPeriode { get; set; }

        public Dictionary<int, int> WerkZeitJeStk
        {
            get { return WerkZeiten; }
            set { WerkZeiten = value; }
        }

        /// <summary>
        /// Kapazitätsplan
        /// </summary>
        public int GetRuestZeit {
            get {
                RuestNew = 0;
                _dc = DataContainer.Instance;
                var sum = 0;
                foreach (KeyValuePair<int, int> kvp in RuestZeiten) {
                    sum += kvp.Value;
                    if (((ETeil) _dc.GetTeil(kvp.Key)).ProduktionsMengePer0 > 0)
                        ++RuestNew;
                }
                return sum;

                //ruestungCustom = (newRuest + ruestungVorPeriode) / 2;
                //if (ruestungCustom < newRuest)
                //    ruestungCustom = newRuest;

            }
        }

        //public void CustomRuestungGeaendert(int ruest) {
        //    if (geaendert == false) {
        //        ruestungCustom = (ruestNew + ruestungVorPeriode) / 2;
        //        if (ruestungCustom < ruestNew)
        //            ruestungCustom = ruestNew;
        //        geaendert = true;
        //    }
        //    else {
        //        if (ruest != -1)
        //            ruestungCustom = ruest;
        //    }
        //}

        /// <summary>
        /// Kapazitätsplan:
        /// Kapazitätbedarf
        /// </summary>
        public int GetBenoetigteZeit {
            get {
                _dc = DataContainer.Instance;
                int result = 0;
                foreach (KeyValuePair<int, int> kvp in WerkZeiten) {
                    int prMenge = ((ETeil) _dc.GetTeil(kvp.Key)).ProduktionsMengePer0;
                    if (prMenge < 0)
                        prMenge = 0;
                    result += kvp.Value * prMenge;
                }
                result += _warteschlangenZeit;
                return result;
            }
        }

        /// <summary>
        /// In Initialisierung
        /// </summary>
        /// <param name="teil">NR</param>
        /// <param name="zeit">ZEIT</param>
        public void AddWerkzeit(int teil, int zeit) {
            if (zeit < 0) {
                throw new InvalidValueException(zeit.ToString(), string.Format("Werkzeit am Arbeitsplatz {0}", GetNummerArbeitsplatz));
            }
            if (_naechsterSchritt.ContainsKey(teil) == false) {
                _naechsterSchritt[teil] = -1;
            }
            if ((WerkZeiten.ContainsKey(teil) && WerkZeiten[teil] == 0) || WerkZeiten.ContainsKey(teil) == false) {
                WerkZeiten[teil] = zeit;
                if (WerkZeiten.ContainsKey(teil) == false) {
                    RuestZeiten[teil] = 0;
                }
                if (((ETeil) DataContainer.Instance.GetTeil(teil)).BenutzteArbeitsplaetze.Contains(this) == false) {
                    ((ETeil) DataContainer.Instance.GetTeil(teil)).AddArbeitsplatz(GetNummerArbeitsplatz);
                }
            }
            if (WerkZeiten.ContainsKey(teil) == false && WerkZeiten[teil] != 0 && WerkZeiten[teil] != zeit) {
                throw new InvalidValueException(string.Format("Am Arbeitsplatz {0} ist bereits eine Werkzeit für das Teil {1} hinterlegt!", GetNummerArbeitsplatz, teil));
            }
        }


        public void AddRuestzeit(int teil, int zeit) {
            if (zeit < 0) {
                throw new InvalidValueException(zeit.ToString(), string.Format("Ruestzeit am Arbeitsplatz {0}", GetNummerArbeitsplatz));
            }
            if ((RuestZeiten.ContainsKey(teil) && RuestZeiten[teil] == 0) || RuestZeiten.ContainsKey(teil) == false) {
                RuestZeiten[teil] = zeit;
                if (WerkZeiten.ContainsKey(teil) == false) {
                    WerkZeiten[teil] = 0;
                }
            }
            if (RuestZeiten.ContainsKey(teil) == false && RuestZeiten[teil] != 0) {
                throw new InvalidValueException(string.Format("Am Arbeitsplatz {0} ist bereits eine Rüstzeit für das Teil {1} hinterlegt!", GetNummerArbeitsplatz, teil));
            }
        }
        // Add Teil in queue with given number of Teil, amount and current place
        public void AddWarteschlange(int nr, int menge, bool aktPlatz) {
            if (aktPlatz) {
                ((ETeil) DataContainer.Instance.GetTeil(nr)).InWartschlange += menge;
            }
            _warteschlangenZeit += menge * WerkZeiten[nr];
            if (_naechsterSchritt[nr] != -1) {
                DataContainer.Instance.GetArbeitsplatz(_naechsterSchritt[nr]).AddWarteschlange(nr, menge, false);
            }
        }
        public int UeberMin {
            get { return anz_uebermin; }
            set { anz_uebermin = value; }
        }
        // Adds "Ueberstunden" minutes per day
        public bool AddUeberMinute(int min) {
            if (anz_uebermin + min < 240) {
                anz_uebermin += min;
                return true;
            }
            if (AddSchicht()) {
                anz_uebermin = 0;
                return true;
            }
            return false;
        }
        public int AnzSchichten {
            get { return anz_schichten; }
            set { anz_schichten = value; }
        }
        public bool AddSchicht() {
            if (anz_schichten < 2) {
                anz_schichten++;
                return true;
            }
            return false;
        }
        public int GetVerfuegbareZeit {
            get { return anz_schichten * 2400 + anz_uebermin * 5; }
        }
        public List<ETeil> GetHergestellteTeile {
            get {
                DataContainer dc = DataContainer.Instance;
                return WerkZeiten.Select(res => dc.GetTeil(res.Key) as ETeil).ToList();
            }
        }
        public Dictionary<int, int> NaechsterArbeitsplatz {
            get { return _naechsterSchritt; }
            set { _naechsterSchritt = value; }
        }
    }
}
