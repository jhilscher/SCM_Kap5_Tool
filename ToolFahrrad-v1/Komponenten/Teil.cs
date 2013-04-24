using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFahrrad_v1
{
    /*Abstract class Teil as basic class for inheritance */
    public abstract class Teil
    {
        // Class members
        protected int nr;
        protected string bezeichnung;
        protected int lagerstand;
        private double verhaeltnis;       
        private double preis;      
        protected string verwendung;
        protected int pufferwert;
        protected int verbrauch_aktuell;
        protected int verbrauch_prognose1;
        protected int verbrauch_prognose2;
        // Constructor
        public Teil(int nummer, string bez)
        {
            // Except nr and b
            this.nr = nummer;
            this.bezeichnung = bez;
            this.lagerstand = 0;
            this.verhaeltnis = 0.0;
            this.preis = 0.0;
            this.pufferwert = 0;
            this.verbrauch_aktuell = 0;
            this.verbrauch_prognose1 = 0;
            this.verbrauch_prognose2 = 0;
        }
        // Getter / Setter
        public int Nummer
        {
            get { return nr; }
        }
        public string Bezeichnung
        {
            get { return bezeichnung; }
            set { bezeichnung = value; }
        }
        public int Lagerstand
        {
            get{ return lagerstand; }
            set{ lagerstand = value; }
        }
        public double Verhaeltnis
        {
            get { return verhaeltnis; }
            set { verhaeltnis = value; }
        }
        public double Preis
        {
            get { return preis; }
            set { preis = value; }
        }
        public string Verwendung
        {
            get { return verwendung; }
            set
            {
                if (value == "K" || value == "D" || value == "H" || value == "KDH")
                {
                    verwendung = value;
                }
                else
                {
                    throw new InputException("Bei dem Teil " + this.nr + " ist eine nicht zulaessige Verwendung eingegeben (" + value + ")");
                }
            }
        }
        public int Pufferwert
        {
            get { return pufferwert; }
            set { pufferwert = value; }
        }
        public int VerbrauchAktuell
        {
            get{ return verbrauch_aktuell; }
            set{ verbrauch_aktuell = value; }
        }
        public int VerbrauchPrognose1
        {
            get{ return verbrauch_prognose1; }
            set{ verbrauch_prognose1 = value; }
        }
        public int VerbrauchPrognose2
        {
            get{ return verbrauch_prognose2; }
            set{ verbrauch_prognose2 = value; }
        }
        // Method generates hashcode of class member nr
        public int GetHashcode()
        {
            return nr.GetHashCode();
        }
        // Method compares two objects from type Teil and returns bool value
        public bool Equals(Teil k)
        {
            if (nr == k.nr)
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
