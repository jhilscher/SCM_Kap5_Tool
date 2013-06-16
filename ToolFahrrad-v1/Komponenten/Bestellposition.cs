namespace ToolFahrrad_v1.Komponenten
{
    /** Class describes position of ordered KTeile */
    public class Bestellposition
    {
        // Class members
        // Constructor
        public Bestellposition(KTeil ktParam, int mengeParam, bool eilParam)
        {
            Kaufteil = ktParam;
            Menge = mengeParam;
            Eil = eilParam;
        }
        // Getter / Setter
        public KTeil Kaufteil { get; set; }
        public int Menge { get; set; }
        public bool Eil { get; set; }

        public int OutputEil
        {
            get {
                return Eil ? 4 : 5;
            }
        }
    }
}
