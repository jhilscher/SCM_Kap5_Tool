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
        protected bool aufgeloest;
        protected int nr;
        protected string bezeichnung;
        protected int lagerstand;
        private double verhaeltnis;
        protected string verwendung;
        protected int vertriebPer0;
        protected int verbrauchPer1;
        protected int verbrauchPer2;
        protected int verbrauchPer3;
        // Constructor
        public Teil(int nummer, string bez)
        {
            aufgeloest = false;
            nr = nummer;
            bezeichnung = bez;
            lagerstand = 0;
            verhaeltnis = 0.0;
            vertriebPer0 = 0;
            verbrauchPer1 = 0;
            verbrauchPer2 = 0;
            verbrauchPer3 = 0;
        }
        // Getter / Setter
        protected bool Aufgeloest
        {
            get { return aufgeloest; }
            set { aufgeloest = value; }
        }
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
        public int VertriebPer0
        {
            get{ return vertriebPer0; }
            set{ vertriebPer0 = value; }
        }
        public int VerbrauchPer1
        {
            get{ return verbrauchPer1; }
            set{ verbrauchPer1 = value; }
        }
        public int VerbrauchPer2
        {
            get{ return verbrauchPer2; }
            set{ verbrauchPer2 = value; }
        }
        public int VerbrauchPer3
        {
            get { return verbrauchPer3; }
            set { verbrauchPer3 = value; }
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
