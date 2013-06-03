using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFahrrad_v1
{
    /** This class is sub class from Teil and describes a buyed Teil */
    public class KTeil : Teil
    {
        // Class members
        private double bestellkosten;
        private double preis;
        private double lieferdauer;
        private double abweichungLieferdauer;
        private int diskontMenge;
        private int lagerZugang;
        private List<List<int>> offeneBestellungen;
        public List<List<int>> OffeneBestellungen {
            get { return offeneBestellungen; }
        }
        // ToDo speichern in welcher periode bestellt bei offenen bestellungen
        private int periodeBestellung;
        private List<ETeil> istTeil = null;
        private int bruttoBedarfPer0;
        private int bruttoBedarfPer1;
        private int bruttoBedarfPer2;
        private int bruttoBedarfPer3;
        private int bestandPer1;
        private int bestandPer2;
        private int bestandPer3;
        private int bestandPer4;
        // Constructor
        public KTeil(int nummer, string bez)
            : base(nummer, bez) {
            offeneBestellungen = new List<List<int>>();
        }
        // Getter / Setter
        public double Bestellkosten {
            get { return bestellkosten; }
            set { bestellkosten = value; }
        }
        public double Preis {
            get { return this.preis; }
            set { this.preis = value; }
        }
        public double Lieferdauer {
            get { return this.lieferdauer; }
            set { this.lieferdauer = value; }
        }
        public double AbweichungLieferdauer {
            get { return abweichungLieferdauer; }
            set { abweichungLieferdauer = value; }
        }
        public int DiskontMenge {
            get { return diskontMenge; }
            set { diskontMenge = value; }
        }
        public int LagerZugang {
            get { return lagerZugang; }
            set { lagerZugang = value; }
        }
        public int PeriodeBestellung {
            get { return periodeBestellung; }
            set { periodeBestellung = value; }
        }
        public void AddOffeneBestellung(int periode, int mode, int menge) {
            List<int> neueOffeneBestellung = new List<int>();
            neueOffeneBestellung.Add(periode);
            neueOffeneBestellung.Add(mode);
            neueOffeneBestellung.Add(menge);
            offeneBestellungen.Add(neueOffeneBestellung);
        }
        public void RemoveOffeneBestellung(int periode, int mode, int menge) {
            List<int> alteOffeneBestellung = new List<int>();
            alteOffeneBestellung.Add(periode);
            alteOffeneBestellung.Add(mode);
            alteOffeneBestellung.Add(menge);
            offeneBestellungen.Remove(alteOffeneBestellung);
        }
        // Function shows where KTeil is used
        public List<ETeil> IstTeilVon {
            get {
                if (istTeil == null) {
                    List<ETeil> res = new List<ETeil>();
                    foreach (ETeil teil in DataContainer.Instance.ListeETeile) {
                        if (teil.Nummer == 17) { }
                        if (teil.Zusammensetzung.ContainsKey(this)) {
                            res.Add(teil);
                        }
                    }
                    this.istTeil = res;
                }
                return this.istTeil;
            }
        }
        public int BruttoBedarfPer0 {
            get { return bruttoBedarfPer0; }
            set { bruttoBedarfPer0 = value; }
        }
        public int BruttoBedarfPer1 {
            get { return bruttoBedarfPer1; }
            set { bruttoBedarfPer1 = value; }
        }
        public int BruttoBedarfPer2 {
            get { return bruttoBedarfPer2; }
            set { bruttoBedarfPer2 = value; }
        }
        public int BruttoBedarfPer3 {
            get { return bruttoBedarfPer3; }
            set { bruttoBedarfPer3 = value; }
        }
        public int BestandPer1 {
            get { return bestandPer1; }
            set { bestandPer1 = value; }
        }
        public int BestandPer2 {
            get { return bestandPer2; }
            set { bestandPer2 = value; }
        }
        public int BestandPer3 {
            get { return bestandPer3; }
            set { bestandPer3 = value; }
        }
        public int BestandPer4 {
            get { return bestandPer4; }
            set { bestandPer4 = value; }
        }
        // Public function to initialize BruttoBedarf
        public void initBruttoBedarf(int index, ETeil teil, int menge) {
            if (index == 1) { // +  alle KDH
                bruttoBedarf(teil, menge);
            }
            else if (index == 2 && !teil.Verwendung.Contains("KDH")) {
                bruttoBedarf(teil, menge);
            }
            else if (index == 3 && !teil.Verwendung.Contains("KDH")) {
                bruttoBedarf(teil, menge);
            }
        }
        // Set Bruttobedarf, when periode0 check that Produktionsmenge is > 0
        private void bruttoBedarf(ETeil teil, int menge) {
            int a = teil.Nummer;
            int pm = 0;
            if (!teil.Verwendung.Contains("KDH")) {
                if (teil.ProduktionsMengePer0 > 0) {
                    bruttoBedarfPer0 += teil.ProduktionsMengePer0 * menge;
                }
            }
            else {
                foreach (KeyValuePair<string, int> pair in teil.KdhProduktionsmenge) {
                    pm += pair.Value;
                }
                bruttoBedarfPer0 += pm * menge;
            }
            bruttoBedarfPer1 += teil.VerbrauchPer1 * menge;
            bruttoBedarfPer2 += teil.VerbrauchPer2 * menge;
            bruttoBedarfPer3 += teil.VerbrauchPer3 * menge;
        }
        // Public function to calculate forecast consumption for next 3 periods
        public void berechnungVerbrauchPrognose(int aktPeriode, double verwendeAbweichung) {
            // Future period: 0+1
            bestandPer1 = lagerstand + eingetroffeneBestellmenge(aktPeriode, verwendeAbweichung) - bruttoBedarfPer0;
            // Future peroid: 0+2
            bestandPer2 = bestandPer1 + eingetroffeneBestellmenge(aktPeriode + 1, verwendeAbweichung) - bruttoBedarfPer1;
            // Future peroid: 0+3
            bestandPer3 = bestandPer2 + eingetroffeneBestellmenge(aktPeriode + 2, verwendeAbweichung) - bruttoBedarfPer2;
            // Future peroid: 0+4
            bestandPer4 = bestandPer3 + eingetroffeneBestellmenge(aktPeriode + 3, verwendeAbweichung) - bruttoBedarfPer3;
        }
        // Private function to calculate period when ordered KTeil will arrive
        private int eingetroffeneBestellmenge(int aktPeriode, double abweichung) {
            int menge = 0;
            if (offeneBestellungen.Count() != 0) {
                foreach (List<int> ob in offeneBestellungen) {
                    double zeitpunktEintreffen = 0;
                    if (ob[1] == 5) {
                        zeitpunktEintreffen = (double)(ob[0] + lieferdauer + abweichungLieferdauer * abweichung / 100);
                    }
                    else if (ob[1] == 4) {
                        zeitpunktEintreffen = (double)(ob[0] + lieferdauer / 2);
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
        public bool Equals(KTeil kt) {
            if (nr == kt.Nummer) {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
