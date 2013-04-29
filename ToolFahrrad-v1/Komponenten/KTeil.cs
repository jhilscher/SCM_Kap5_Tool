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
        private int erwarteteBestellung;
        private double preis;
        private double lieferdauer;
        private double abweichungLieferdauer;
        private int diskontMenge;
        private int lagerZugang;
        private List<ETeil> istTeil = null;
        // Members for consumption forecast at period 1,2,3; MA=Mit Abweichung; OA=Ohne Abweichung
        private int verbProg1MA;
        private int verbProg1OA;
        private int verbProg2MA;
        private int verbProg2OA;
        private int verbProg3MA;
        private int verbProg3OA;
        // Constructor
        public KTeil(int nummer, string bez) : base(nummer, bez)
        {}
        // Getter / Setter
        public double Bestellkosten
        {
            get { return bestellkosten; }
            set { bestellkosten = value; }
        }
        public int ErwarteteBestellung
        {
            get { return erwarteteBestellung; }
            set { erwarteteBestellung = value; }
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
        public int VerbrauchPrognose1MA
        {
            get { return verbProg1MA; }
            set { verbProg1MA = value; }
        }
        public int VerbrauchPrognose1OA
        {
            get { return verbProg1OA; }
            set { verbProg1OA = value; }
        }
        public int VerbrauchPrognose2MA
        {
            get { return verbProg2MA; }
            set { verbProg2MA = value; }
        }
        public int VerbrauchPrognose2OA
        {
            get { return verbProg2OA; }
            set { verbProg2OA = value; }
        }
        public int VerbrauchPrognose3MA
        {
            get { return verbProg3MA; }
            set { verbProg3MA = value; }
        }
        public int VerbrauchPrognose3OA
        {
            get { return verbProg3OA; }
            set { verbProg3OA = value; }
        }
        // Public function to calculate forecast consumption for next 3 periods
        public void berechnungVerbrauchPrognose()
        {
            // Future period 1
            verbProg1MA = Convert.ToInt32(
                lagerstand + erwarteteBestellung - verbrauchAktuell * (lieferdauer + abweichungLieferdauer));
            verbProg1OA = Convert.ToInt32(
                lagerstand + erwarteteBestellung - verbrauchAktuell * lieferdauer);
            // Future period 2
            verbProg2MA = Convert.ToInt32(
                lagerstand + erwarteteBestellung - (
                    verbrauchAktuell + (verbrauchAktuell + verbrauchPrognose1 + verbrauchPrognose2) / 3) *
                    (lieferdauer + abweichungLieferdauer));
            verbProg2OA = Convert.ToInt32(
                lagerstand + erwarteteBestellung - (
                    verbrauchAktuell + (verbrauchAktuell + verbrauchPrognose1 + verbrauchPrognose2) / 3) *
                    lieferdauer);
            // Future period 3
            verbProg3MA = Convert.ToInt32(
                lagerstand + erwarteteBestellung - (
                    verbrauchAktuell + (
                        verbrauchAktuell + verbrauchPrognose1 + verbrauchPrognose2 + verbrauchPrognose3) / 4) *
                        (lieferdauer + abweichungLieferdauer));
            verbProg3OA = Convert.ToInt32(
                lagerstand + erwarteteBestellung - (
                    verbrauchAktuell + (
                        verbrauchAktuell + verbrauchPrognose1 + verbrauchPrognose2 + verbrauchPrognose3) / 4) *
                        lieferdauer);
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
