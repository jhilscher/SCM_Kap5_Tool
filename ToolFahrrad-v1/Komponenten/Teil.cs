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
        protected string verwendung;
        protected int pufferwert;
        protected int verbrauchAktuell;
        protected int verbrauchPrognose1;
        protected int verbrauchPrognose2;
        // Constructor
        public Teil(int nummer, string bez)
        {
            // Except nr and b
            this.nr = nummer;
            this.bezeichnung = bez;
            this.lagerstand = 0;
            this.pufferwert = 0;
            this.verbrauchAktuell = 0;
            this.verbrauchPrognose1 = 0;
            this.verbrauchPrognose2 = 0;
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
                    throw new InputException("Bei dem Teil " + this.nr + " ist eine nicht zulässige Verwendung eingegeben (" + value + ")");
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
            get{ return verbrauchAktuell; }
            set{ verbrauchAktuell = value; }
        }
        public int VerbrauchPrognose1
        {
            get{ return verbrauchPrognose1; }
            set{ verbrauchPrognose1 = value; }
        }
        public int VerbrauchPrognose2
        {
            get{ return verbrauchPrognose2; }
            set{ verbrauchPrognose2 = value; }
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
