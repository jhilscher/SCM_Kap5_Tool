namespace ToolFahrrad_v1.Komponenten
{
    /** Class decribes position of parts for direct distribution (Direktverkauf)*/
    public class DvPosition
    {
        // Class members
        // Constructor
        public DvPosition(int teilNrParam, int mengeParam, double preisParam, double strafeParam)
        {
            DvTeilNr = teilNrParam;
            DvMenge = mengeParam;
            DvPreis = preisParam;
            DvStrafe = strafeParam;
        }
        // Getter/Setter
        public int DvTeilNr { get; set; }
        public int DvMenge { get; set; }
        public double DvPreis { get; set; }
        public double DvStrafe { get; set; }
    }
}
