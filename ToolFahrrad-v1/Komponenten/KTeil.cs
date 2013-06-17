using System.Collections.Generic;
using System.Linq;

namespace ToolFahrrad_v1.Komponenten
{
    /** This class is sub class from Teil and describes a buyed Teil */
    public class KTeil : Teil
    {
        // Class members
        private readonly List<List<int>> _offeneBestellungen;
        public List<List<int>> OffeneBestellungen {
            get { return _offeneBestellungen; }
        }
        // ToDo speichern in welcher periode bestellt bei offenen bestellungen
        private List<ETeil> _istTeil;
        // Constructor
        public KTeil(int nummer, string bez)
            : base(nummer, bez) {
            _offeneBestellungen = new List<List<int>>();
        }
        // Getter / Setter
        public double Bestellkosten { get; set; }

        public double Preis { get; set; }

        public double Lieferdauer { get; set; }

        public double AbweichungLieferdauer { get; set; }

        public int DiskontMenge { get; set; }

        public int LagerZugang { get; set; }

        public int PeriodeBestellung { get; set; }

        public void AddOffeneBestellung(int periode, int mode, int menge) {
            var neueOffeneBestellung = new List<int> {periode, mode, menge};
            _offeneBestellungen.Add(neueOffeneBestellung);
        }
        public void RemoveOffeneBestellung(int periode, int mode, int menge) {
            var alteOffeneBestellung = new List<int> {periode, mode, menge};
            _offeneBestellungen.Remove(alteOffeneBestellung);
        }
        // Function shows where KTeil is used
        public List<ETeil> IstTeilVon {
            get {
                if (_istTeil == null) {
                    var res = new List<ETeil>();
                    foreach (ETeil teil in DataContainer.Instance.ListeETeile) {
                        if (teil.Nummer == 17) { }
                        if (teil.Zusammensetzung.ContainsKey(this)) {
                            res.Add(teil);
                        }
                    }
                    _istTeil = res;
                }
                return _istTeil;
            }
        }

        public int BruttoBedarfPer0 { get; set; }

        public int BruttoBedarfPer1 { get; set; }

        public int BruttoBedarfPer2 { get; set; }

        public int BruttoBedarfPer3 { get; set; }

        public int BestandPer1 { get; set; }

        public int BestandPer2 { get; set; }

        public int BestandPer3 { get; set; }

        public int BestandPer4 { get; set; }

        // Public function to initialize BruttoBedarf
        public void InitBruttoBedarf(int index, ETeil teil, int menge) {
            if (index == 1) { // +  alle KDH
                BruttoBedarf(teil, menge);
            }
            else if (index == 2 && !teil.Verwendung.Contains("KDH")) {
                BruttoBedarf(teil, menge);
            }
            else if (index == 3 && !teil.Verwendung.Contains("KDH")) {
                BruttoBedarf(teil, menge);
            }
        }
        // Set Bruttobedarf, when periode0 check that Produktionsmenge is > 0
        private void BruttoBedarf(ETeil teil, int menge) {
            int pm = 0;
            if (!teil.Verwendung.Contains("KDH")) {
                if (teil.ProduktionsMengePer0 > 0) {
                    BruttoBedarfPer0 += teil.ProduktionsMengePer0 * menge;
                }
            }
            else
            {
                pm += teil.KdhProduktionsmenge.Sum(pair => pair.Value);
                BruttoBedarfPer0 += pm * menge;
            }
            BruttoBedarfPer1 += teil.VerbrauchPer1 * menge;
            BruttoBedarfPer2 += teil.VerbrauchPer2 * menge;
            BruttoBedarfPer3 += teil.VerbrauchPer3 * menge;
        }
        // Public function to calculate forecast consumption for next 3 periods
        public void BerechnungVerbrauchPrognose(int aktPeriode, double verwendeAbweichung) {
            // Future period: 0+1
            BestandPer1 = Lagerstand + EingetroffeneBestellmenge(aktPeriode, verwendeAbweichung) - BruttoBedarfPer0;
            // Future peroid: 0+2
            BestandPer2 = BestandPer1 + EingetroffeneBestellmenge(aktPeriode + 1, verwendeAbweichung) - BruttoBedarfPer1;
            // Future peroid: 0+3
            BestandPer3 = BestandPer2 + EingetroffeneBestellmenge(aktPeriode + 2, verwendeAbweichung) - BruttoBedarfPer2;
            // Future peroid: 0+4
            BestandPer4 = BestandPer3 + EingetroffeneBestellmenge(aktPeriode + 3, verwendeAbweichung) - BruttoBedarfPer3;
        }
        // Private function to calculate period when ordered KTeil will arrive
        private int EingetroffeneBestellmenge(int aktPeriode, double abweichung) {
            int menge = 0;
            if (_offeneBestellungen.Count() != 0) {
                foreach (List<int> ob in _offeneBestellungen) {
                    double zeitpunktEintreffen = 0;
                    if (ob[1] == 5) {
                        zeitpunktEintreffen = ob[0] + Lieferdauer + AbweichungLieferdauer * abweichung / 100;
                    }
                    else if (ob[1] == 4) {
                        zeitpunktEintreffen = ob[0] + Lieferdauer / 2;
                    }
                    // Check if KTeil arrive at actual period
                    if (zeitpunktEintreffen >= aktPeriode && zeitpunktEintreffen < (aktPeriode + 0.8)) {
                        menge += ob[2];
                    }
                    // Check if KTeil arrived at last period last day, and count value to next period
                    else if (zeitpunktEintreffen < aktPeriode &&
                        ((aktPeriode - zeitpunktEintreffen) > 0 && (aktPeriode - zeitpunktEintreffen) <= 0.2)) {
                        menge += ob[2];
                    }
                }
            }
            return menge;
        }
        // Equals function
        public bool Equals(KTeil kt)
        {
            return Nummer == kt.Nummer;
        }
    }
}
