using ToolFahrrad_v1.Exceptions;

namespace ToolFahrrad_v1.Komponenten
{
    /*Abstract class Teil as basic class for inheritance */
    public abstract class Teil
    {
        // Class members
        private string _verwendung;
        // Constructor
        protected Teil(int nummer, string bez)
        {
            Aufgeloest = false;
            Nummer = nummer;
            Bezeichnung = bez;
            Lagerstand = 0;
            Verhaeltnis = 0.0;
            VertriebPer0 = 0;
            VerbrauchPer1 = 0;
            VerbrauchPer2 = 0;
            VerbrauchPer3 = 0;
        }
        // Getter / Setter
        public bool Aufgeloest { get; set; }
        public int Nummer { get; protected set; }
        public string Bezeichnung { get; set; }
        public int Lagerstand { get; set; }
        public double Verhaeltnis { get; set; }
        public int VertriebPer0 { get; set; }
        public int VerbrauchPer1 { get; set; }
        public int VerbrauchPer2 { get; set; }
        public int VerbrauchPer3 { get; set; }

        public string Verwendung
        {
            get { return _verwendung; }
            set
            {
                if (value == "K" || value == "D" || value == "H" || value == "KDH")
                {
                    _verwendung = value;
                }
                else
                {
                    throw new InputException("Bei dem Teil " + Nummer +
                                             " ist eine nicht zulaessige Verwendung eingegeben (" + value + ")");
                }
            }
        }

        

        // Method generates hashcode of class member nr
        public int GetHashcode()
        {
            return Nummer.GetHashCode();
        }
        // Method compares two objects from type Teil and returns bool value
        public bool Equals(Teil k)
        {
            return Nummer == k.Nummer;
        }

        public override string ToString()
        {
            return Nummer.ToString();
        }
    }
}
