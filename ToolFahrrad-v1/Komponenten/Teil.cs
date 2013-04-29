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
        protected string verwendung;
        protected int pufferwert;
        protected int vertriebAktuell;
        protected int verbrauchPrognose1;
        protected int verbrauchPrognose2;
        protected int verbrauchPrognose3;
                // Constructor
        public Teil(int nummer, string bez)
        {
            nr = nummer;
            bezeichnung = bez;
            lagerstand = 0;
            verhaeltnis = 0.0;
            pufferwert = 0;
            vertriebAktuell = 0;
            verbrauchPrognose1 = 0;
            verbrauchPrognose2 = 0;
            verbrauchPrognose3 = 0;
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
                    throw new InputException("Bei dem Teil " + this.nr +
                                             " ist eine nicht zulaessige Verwendung eingegeben (" + value + ")");
                }
            }
        }
        public int Pufferwert
        {
            get { return pufferwert; }
            set { pufferwert = value; }
        }
        public int VertriebAktuell
        {
            get{ return vertriebAktuell; }
            set{ vertriebAktuell = value; }
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
        public int VerbrauchPrognose3
        {
            get { return verbrauchPrognose3; }
            set { verbrauchPrognose3 = value; }
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
