using System.Collections.Generic;
using System.Linq;
using ToolFahrrad_v1.Komponenten;

namespace ToolFahrrad_v1.Verwaltung
{
    public class Produktionsplanung
    {
        // Class members
        private DataContainer _dc;
        private int _aktPeriode;
        private Dictionary<int, int> _prodListe;
        // Constructor
        public Produktionsplanung()
        {
            _dc = DataContainer.Instance;
            _prodListe = new Dictionary<int,int>();
        }
        // Getter / Setter
        public int AktPeriode
        {
            set { _aktPeriode = value; }
        }
        public Dictionary<int, int> ProdListe
        {
            get { return _prodListe; }
            set
            {
                _prodListe.Clear();
                _prodListe = value;
            }
        }
        // Aufloesung der Teile, setzen der Ueberstunden, anpassen Reihenfolge
        // ------------------------------------------------------------------------------------------------------------
        // Aufloesung Prognosen fuer Produkte, berechnet benoetigte E- und KTeile
        public void Aufloesen()
        {
            if (_dc == null)
            {
                _dc = DataContainer.Instance;
            }
            for (int index = 1; index < 4; index++)
            {
                RekursAufloesenETeile(index, null, _dc.GetTeil(index) as ETeil);
            }
                       // Aufloesung der KTeile
            // Prüfung, ob Bruttobedarf schon gerechnet wurde
            if ((_dc.GetTeil(21) as KTeil).BruttoBedarfPer0 != 0 || (_dc.GetTeil(21) as KTeil).BruttoBedarfPer1 != 0 ||
                (_dc.GetTeil(21) as KTeil).BruttoBedarfPer2 != 0 || (_dc.GetTeil(21) as KTeil).BruttoBedarfPer3 != 0)
            {
                foreach (KTeil k in _dc.ListeKTeile)
                {
                    k.BruttoBedarfPer0 = 0;
                    k.BruttoBedarfPer1 = 0;
                    k.BruttoBedarfPer2 = 0;
                    k.BruttoBedarfPer3 = 0;
                }
            }
            for (int index = 1; index < 4; index++)
            {
                RekursAufloesenKTeile(index, null, _dc.GetTeil(index) as ETeil);
            }
        }
        // Rekursive Prozedur zum Iterieren ueber die Zusammensetzung der Teile
        private void RekursAufloesenETeile(int index, ETeil vaterTeil, ETeil kindTeil)
        {
            kindTeil.SetProduktionsMenge(index, vaterTeil);
            if (kindTeil.Zusammensetzung.Count() != 0)
            {
                foreach (KeyValuePair<Teil, int> kvp in kindTeil.Zusammensetzung)
                {
                    if (kvp.Key is ETeil)
                    {
                        RekursAufloesenETeile(index, kindTeil, kvp.Key as ETeil);
                    }
                }
            }
        }
        public void RekursAufloesenKTeile(int index, ETeil vaterTeil, ETeil kindTeil)
        {
            if (vaterTeil != null)
            {
                kindTeil.VerbrauchPer1 = vaterTeil.VerbrauchPer1;
                kindTeil.VerbrauchPer2 = vaterTeil.VerbrauchPer2;
                kindTeil.VerbrauchPer3 = vaterTeil.VerbrauchPer3;
            }
            foreach (KeyValuePair<Teil, int> kvp in kindTeil.Zusammensetzung)
            {
                if (kvp.Key is KTeil)
                {
                    var kt = kvp.Key as KTeil;
                    kt.InitBruttoBedarf(index, kindTeil, kvp.Value);
                    kt.BerechnungVerbrauchPrognose(_aktPeriode, _dc.VerwendeAbweichung);
                }
                else
                {
                    RekursAufloesenKTeile(index, kindTeil, kvp.Key as ETeil);
                }
            }
        }
        // Optimierung der Auftragsreihenfolge
        public void OptimiereProdListe()
        {
            _prodListe.Clear();
            // Optimierung der Reihenfolge

            foreach (var et1 in _dc.ListeETeile.Where(et1 => et1.InWartschlange > 0 && et1.IstEndProdukt == false))
            {
                if (et1.ProduktionsMengePer0 > 0 && !et1.Verwendung.Equals("KDH"))
                {
                    _prodListe.Add(et1.Nummer, et1.ProduktionsMengePer0);
                }
                else if (et1.Verwendung.Equals("KDH"))
                {
                    var pr = et1.KdhProduktionsmenge.Sum(pair => pair.Value);
                    if(pr > 0)
                        _prodListe.Add(et1.Nummer, et1.KdhProduktionsmenge.Sum(pair => pair.Value));
                }
            }
            foreach (var et2 in _dc.ListeETeile.Where(et2 => et2.InWartschlange == 0 && et2.IstEndProdukt == false))
            {
                if (et2.ProduktionsMengePer0 > 0 && !et2.Verwendung.Equals("KDH")) {
                    _prodListe.Add(et2.Nummer, et2.ProduktionsMengePer0);
                }
                else if (et2.Verwendung.Equals("KDH")) {
                    var pr = et2.KdhProduktionsmenge.Sum(pair => pair.Value);
                    if (pr > 0)
                        _prodListe.Add(et2.Nummer, et2.KdhProduktionsmenge.Sum(pair => pair.Value));
                }
            }
            foreach (ETeil et3 in _dc.ListeETeile.Where(et3 => et3.ProduktionsMengePer0 > 0 && et3.IstEndProdukt))
            {
                _prodListe.Add(et3.Nummer, et3.ProduktionsMengePer0);
            }
        }
        public void LoadProdListeInDc()
        {
            _dc.ListeProduktion = _prodListe;
        }
    }
}
