using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolFahrrad_v1
{
    /** Class decribes position of parts for direct distribution (Direktverkauf)*/
    public class DvPosition
    {
        // Class members
        int dvTeilNr;
        int dvMenge;
        double dvPreis;
        double dvStrafe;
        // Constructor
        public DvPosition(int teilNr_param, int menge_param, double preis_param, double strafe_param)
        {
            dvTeilNr = teilNr_param;
            dvMenge = menge_param;
            dvPreis = preis_param;
            dvStrafe = strafe_param;
        }
        // Getter/Setter
        public int DvTeilNr
        {
            get { return dvTeilNr; }
            set { dvTeilNr = value; }
        }
        public int DvMenge
        {
            get { return dvMenge; }
            set { dvMenge = value; }
        }
        public double DvPreis
        {
            get { return dvPreis; }
            set { dvPreis = value; }
        }
        public double DvStrafe
        {
            get { return dvStrafe; }
            set { dvStrafe = value; }
        }
    }
}
