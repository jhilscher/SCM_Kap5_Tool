using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFahrrad_v1.Komponenten
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
        public string Bezeichnung
        {
            get { return this.bezeichnung; }
            set { this.bezeichnung = value; }
        }
        public int Lagerstand
        {
            get{ return lagerstand; }
            set{ lagerstand = value; }
        }
        public string Verwendung
        {
            get { return this.verwendung; }
            set
            {
                if (value == "K" || value == "D" || value == "H" || value == "KDH")
                {
                    verwendung = value;
                }
                else
                {
                    throw new InputException("Bei dem Teil {0} ist eine nicht zulässige Verwendung eingegeben ({1})", this.nr, value);
                }
            }
        }
        public int Pufferwert
        {
            get { return this.pufferwert; }
            set { this.pufferwert = value; }
        }
        public int VerbrauchAktuell
        {
            get{ return this.verbrauchAktuell; }
            set{ this.verbrauchAktuell = value; }
        }
        public int VerbrauchPrognose1
        {
            get{ return this.verbrauchPrognose1; }
            set{ this.verbrauchPrognose1 = value; }
        }
        public int VerbrauchPrognose2
        {
            get{ return this.verbrauchPrognose2; }
            set{ this.verbrauchPrognose2 = value; }
        }
        // Method generates hashcode of class member nr
        public int GetHashcode()
        {
            return this.nr.GetHashCode();
        }
        // Method compares two objects from type Teil and returns bool value
        public bool Equals(Teil k)
        {
            if (this.nr == k.nr)
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
