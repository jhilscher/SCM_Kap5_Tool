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
        private int erwartete_bestellung;
        private double preis;
        private double lieferdauer;
        private double abweichung_lieferdauer;
        private int diskontmenge;
        private int lagerZugang;
        public int LagerZugang
        {
            get { return lagerZugang; }
            set { lagerZugang = value; }
        }
        private List<ETeil> istTeil = null;
        // Members for Verbrauch Prognose 1&2; MA=Mittlere Abweichung; OA=Obere Abweichung
        private int verbProg1MA;
        private int verbProg1OA;
        private int verbProg2MA;
        private int verbProg2OA;
        // Constructor
        public KTeil(int nummer, string bez)
            : base(nummer, bez)
        { }
        // Getter / Setter
        public double Bestellkosten
        {
            get { return bestellkosten; }
            set { bestellkosten = value; }
        }
        public int ErwarteteBestellung
        {
            get { return erwartete_bestellung; }
            set { erwartete_bestellung = value; }
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
        public double Abweichung_lieferdauer
        {
            get { return abweichung_lieferdauer; }
            set { abweichung_lieferdauer = value; }
        }
        public int Diskontmenge
        {
            get { return diskontmenge; }
            set { diskontmenge = value; }
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
                        { }
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
        public void berechnung_verbrauch_prognose()
        {
            verbProg1MA = Convert.ToInt32(
                lagerstand + erwartete_bestellung - verbrauch_aktuell * (
                    lieferdauer + abweichung_lieferdauer));
            verbProg1OA = Convert.ToInt32(
                lagerstand + erwartete_bestellung - verbrauch_aktuell * (lieferdauer));
            verbProg2MA = Convert.ToInt32(
                lagerstand + erwartete_bestellung - (
                    verbrauch_aktuell + (
                        verbrauch_aktuell + verbrauch_prognose1 + verbrauch_prognose2) / 3) * (lieferdauer + abweichung_lieferdauer));
            verbProg2OA = Convert.ToInt32(
                lagerstand + erwartete_bestellung - (
                    verbrauch_aktuell + (
                        verbrauch_aktuell + verbrauch_prognose1 + verbrauch_prognose2) / 3) * (lieferdauer));
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
