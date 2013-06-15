using System;
using System.Collections.Generic;
using System.Linq;

namespace ToolFahrrad_v1
{
    class DataContainer
    {
        // Class members
        static DataContainer _instance = new DataContainer();
        private readonly List<Bestellposition> _listeBestellungen;
        private readonly List<DvPosition> _listeDVerkauf;
        private readonly Dictionary<int, Teil> _listeTeile;
        private readonly Dictionary<int, Arbeitsplatz> _listeArbeitsplaetze;
        private int _ersteSchicht = 3600;
        private int _zweiteSchicht = 6000;
        private double _verwendeAbweichung = 0.5;
        private double _verwendeDiskount = 0.5;
        // Getter / Setter
        public List<int[]> ApKapazitaet { get; set; }

        public double DiskountGrenze { get; set; }

        public double GrenzeMenge { get; set; }

        public double VerwendeAbweichung
        {
            get { return _verwendeAbweichung * 100; }
            set { _verwendeAbweichung = value / 100; }
        }
        public double VerwendeDiskount
        {
            get { return _verwendeDiskount * 100; }
            set { _verwendeDiskount = value / 100; }
        }
        public int ZweiteSchicht
        {
            get { return _zweiteSchicht; }
            set { _zweiteSchicht = value; }
        }
        public int ErsteSchicht
        {
            get { return _ersteSchicht; }
            set { _ersteSchicht = value; }
        }
        // Constructor
        private DataContainer()
        {
            GrenzeMenge = 10;
            DiskountGrenze = 5;
            ApKapazitaet = new List<int[]>();
            BerechneKindTeil = true;
            _listeBestellungen = new List<Bestellposition>();
            _listeDVerkauf = new List<DvPosition>();
            _listeTeile = new Dictionary<int, Teil>();
            _listeArbeitsplaetze = new Dictionary<int, Arbeitsplatz>();
        }

        public bool BerechneKindTeil { get; private set; }

        // Getter for _instance of DataContainer
        public static DataContainer Instance
        {
            get { return _instance; }
        }
        // Getter / Setter for Reihenfolge of production
        public int[] Reihenfolge { get; set; }

        // Getter of list all KTeil
        public IEnumerable<KTeil> ListeKTeile
        {
            get
            {
                return (from kvp in _listeTeile where kvp.Value is KTeil select kvp.Value as KTeil).ToList();
            }
        }
        // Getter of list all ETeil
        public List<ETeil> ListeETeile
        {
            get
            {
                return (from kvp in _listeTeile where kvp.Value is ETeil select kvp.Value as ETeil).ToList();
            }
        }
        // Getter of all Bestellpositionen
        public List<Bestellposition> Bestellungen
        {
            get { return _listeBestellungen; }
            set
            {
                _listeBestellungen.Clear();
                foreach (Bestellposition bp in value)
                {
                    _listeBestellungen.Add(new Bestellposition(bp.Kaufteil, bp.Menge, bp.Eil));
                }
            }
        }
        // Getter of list direct distribution (Direktverkauf)
        public List<DvPosition> DVerkauf
        {
            get { return _listeDVerkauf; }
            set
            {
                _listeDVerkauf.Clear();
                foreach (DvPosition dvp in value)
                {
                    _listeDVerkauf.Add(new DvPosition(dvp.DvTeilNr, dvp.DvMenge, dvp.DvPreis, dvp.DvStrafe));
                }
            }
        }

        // Getter for Teil with given number
        public Teil GetTeil(int nr)
        {
            if (_listeTeile.ContainsKey(nr))
            {
                return _listeTeile[nr];
            }
            throw new UnknownTeilException(nr);
        }

        public void NewTeil(int nr, string bez, double p, double bk, double ld, double abw, int dm, int bs, string vw)
        {
            if (_listeTeile.ContainsKey(nr) == false)
            {
                var kt = new KTeil(nr, bez)
                    {
                        Preis = p,
                        Bestellkosten = bk,
                        Lieferdauer = ld,
                        AbweichungLieferdauer = abw,
                        DiskontMenge = dm,
                        Lagerstand = bs,
                        Verwendung = vw
                    };
                _listeTeile[nr] = kt;
            }
            else
            {
                throw new InputException(string.Format("Teil mit Nr.:{0} bereits vorhanden!", nr));
            }
        }
        // Create new ETeil and add this to member liste_teile
        public void NewTeil(int nr, string bez, int bs, string vw)
        {
            if (_listeTeile.ContainsKey(nr) == false)
            {
                var et = new ETeil(nr, bez) {Lagerstand = bs, Verwendung = vw};
                _listeTeile[nr] = et;
            }
            else
            {
                throw new InputException(string.Format("Teil mit Nr.:{0} bereits vorhanden!", nr));
            }
        }
        // Getter for Arbeitsplatz with given number
        public Arbeitsplatz GetArbeitsplatz(int nr)
        {
            return _listeArbeitsplaetze[nr];
        }
        // Getter for member liste_arbeitsplaetze
        public IEnumerable<Arbeitsplatz> ArbeitsplatzList
        {
            get
            {
                return _listeArbeitsplaetze.Select(kvp => kvp.Value).ToList();
            }
        }
        // Setter for Arbeitsplatz in member liste_arbeitsplaetze
        public void NeuArbeitsplatz(Arbeitsplatz ap)
        {
            if (_listeArbeitsplaetze.ContainsKey(ap.GetNummerArbeitsplatz) == false)
            {
                _listeArbeitsplaetze[ap.GetNummerArbeitsplatz] = ap;
            }
            else
            {
                throw new Exception(string.Format("Arbeitsplatz mit der Nr.:{0} bereits vorhanden!", ap.GetNummerArbeitsplatz));
            }
        }
    }
}
