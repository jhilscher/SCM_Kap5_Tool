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
        private int verwendungProdukt1;
        private int verwendungProdukt2;
        private int verwendungProdukt3;
        // Constructor
        public KTeil(int nummer, string bez) : base(nummer, bez)
        {}
        // Getter / Setter
        public double Bestellkosten
        {
            get { return bestellkosten; }
            set { bestellkosten = value; }
        }
        public double Preis
        {
            get { return this.preis; }
            set { this.preis = value; }
        }
        public double Lieferdauer
        {
            get { return this.lieferdauer; }
            set { this.lieferdauer = value; }
        }
        public double AbweichungLieferdauer
        {
            get { return abweichungLieferdauer; }
            set { abweichungLieferdauer = value; }
        }
        public int DiskontMenge
        {
            get { return diskontMenge; }
            set { diskontMenge = value; }
        }
        public int LagerZugang
        {
            get { return lagerZugang; }
            set { lagerZugang = value; }
        }
        public int PeriodeBestellung
        {
            get { return periodeBestellung; }
            set { periodeBestellung = value; }
        }
        // Function shows where KTeil is used
        public List<ETeil> IstTeilVon
        {
            get
            {
                if (istTeil == null)
                {
                    List<ETeil> res = new List<ETeil>();
                    foreach (ETeil teil in DataContainer.Instance.ListeETeile)
                    {
                        if (teil.Nummer == 17)
                        {}
                        if (teil.Zusammensetzung.ContainsKey(this))
                        {
                            res.Add(teil);
                        }
                    }
                    this.istTeil = res;
                }
                return this.istTeil;
            }
        }
        public int BruttoBedarfPer0
        {
            get { return bruttoBedarfPer0; }
            set { bruttoBedarfPer0 = value; }
        }
        public int BruttoBedarfPer1
        {
            get { return bruttoBedarfPer1; }
            set { bruttoBedarfPer1 = value; }
        }
        public int BruttoBedarfPer2
        {
            get { return bruttoBedarfPer2; }
            set { bruttoBedarfPer2 = value; }
        }
        public int BruttoBedarfPer3
        {
            get { return bruttoBedarfPer3; }
            set { bruttoBedarfPer3 = value; }
        }
        public int BestandPer1
        {
            get { return bestandPer1; }
            set { bestandPer1 = value; }
        }
        public int BestandPer2
        {
            get { return bestandPer2; }
            set { bestandPer2 = value; }
        }
        public int BestandPer3
        {
            get { return bestandPer3; }
            set { bestandPer3 = value; }
        }
        public int BestandPer4
        {
            get { return bestandPer4; }
            set { bestandPer4 = value; }
        }
        public int VerwendungProdukt1
        {
            get { return verwendungProdukt1; }
            set { verwendungProdukt1 = value; }
        }
        public int VerwendungProdukt2
        {
            get { return verwendungProdukt2; }
            set { verwendungProdukt2 = value; }
        }
        public int VerwendungProdukt3
        {
            get { return verwendungProdukt3; }
            set { verwendungProdukt3 = value; }
        }
        // Public function to initialize BruttoBedarf
        public void initBruttoBedarf(int index, int prodMengeAkt)
        {
            if (index == 1)
            {
                bruttoBedarfPer0 = prodMengeAkt * verwendungProdukt1;
            }
            else if (index == 2)
            {
                bruttoBedarfPer0 = prodMengeAkt * verwendungProdukt2;
            }
            else if (index == 3)
            {
                bruttoBedarfPer0 = prodMengeAkt * verwendungProdukt3;
            }
        }
        // Public function to calculate forecast consumption for next 3 periods
        public void berechnungVerbrauchPrognose(double verwendeAbweichung)
        {
            // Future period 1
/*            verbProg1MA = Convert.ToInt32(
                lagerstand + erwarteteBestellung - vertriebAktuell * (lieferdauer + abweichungLieferdauer));
            verbProg1OA = Convert.ToInt32(
                lagerstand + erwarteteBestellung - vertriebAktuell * lieferdauer);
            // Future period 2
            verbProg2MA = Convert.ToInt32(
                lagerstand + erwarteteBestellung - (
                    vertriebAktuell + (vertriebAktuell + verbrauchPrognose1 + verbrauchPrognose2) / 3) *
                    (lieferdauer + abweichungLieferdauer));
            verbProg2OA = Convert.ToInt32(
                lagerstand + erwarteteBestellung - (
                    vertriebAktuell + (vertriebAktuell + verbrauchPrognose1 + verbrauchPrognose2) / 3) *
                    lieferdauer);
            // Future period 3
            verbProg3MA = Convert.ToInt32(
                lagerstand + erwarteteBestellung - (
                    vertriebAktuell + (
                        vertriebAktuell + verbrauchPrognose1 + verbrauchPrognose2 + verbrauchPrognose3) / 4) *
                        (lieferdauer + abweichungLieferdauer));
            verbProg3OA = Convert.ToInt32(
                lagerstand + erwarteteBestellung - (
                    vertriebAktuell + (
                        vertriebAktuell + verbrauchPrognose1 + verbrauchPrognose2 + verbrauchPrognose3) / 4) *
                        lieferdauer);*/
        }
        // Equals function
        public bool Equals(KTeil kt)
        {
            if (nr == kt.Nummer)
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
