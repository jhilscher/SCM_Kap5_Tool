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
        private int bruttoBedarfAkt;
        private int bruttoBedarfP1;
        private int bruttoBedarfP2;
        private int bruttoBedarfP3;
        private int bestandP1;
        private int bestandP2;
        private int bestandP3;
        private int bestandP4;
        private int verwendungP1;
        private int verwendungP2;
        private int verwendungP3;
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
        public int BruttoBedarfAkt
        {
            get { return bruttoBedarfAkt; }
            set { bruttoBedarfAkt = value; }
        }
        public int BruttoBedarfP1
        {
            get { return bruttoBedarfP1; }
            set { bruttoBedarfP1 = value; }
        }
        public int BruttoBedarfP2
        {
            get { return bruttoBedarfP2; }
            set { bruttoBedarfP2 = value; }
        }
        public int BruttoBedarfP3
        {
            get { return bruttoBedarfP3; }
            set { bruttoBedarfP3 = value; }
        }
        public int BestandP1
        {
            get { return bestandP1; }
            set { bestandP1 = value; }
        }
        public int BestandP2
        {
            get { return bestandP2; }
            set { bestandP2 = value; }
        }
        public int BestandP3
        {
            get { return bestandP3; }
            set { bestandP3 = value; }
        }
        public int BestandP4
        {
            get { return bestandP4; }
            set { bestandP4 = value; }
        }
        public int VerwendungP1
        {
            get { return verwendungP1; }
            set { verwendungP1 = value; }
        }
        public int VerwendungP2
        {
            get { return verwendungP2; }
            set { verwendungP2 = value; }
        }
        public int VerwendungP3
        {
            get { return verwendungP3; }
            set { verwendungP3 = value; }
        }
        // Public function to initialize BruttoBedarf
        public void initBruttoBedarf(int index, int prodMengeAkt)
        {
            if (index == 1)
            {
                bruttoBedarfAkt = prodMengeAkt * verwendungP1;
            }
            else if (index == 2)
            {
                bruttoBedarfAkt = prodMengeAkt * verwendungP2;
            }
            else if (index == 3)
            {
                bruttoBedarfAkt = prodMengeAkt * verwendungP3;
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
