using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFahrrad_v1
{
    /** Exception is thrown when Teil could not be recognized */
    class UnknownTeilException : Exception
    {
        // Class member
        int nr;
        // Constructor
        public UnknownTeilException(int nummer_param)
        {
            this.nr = nummer_param;
        }
        // Getter / Setter
        public int Nummer
        {
            get { return nr; }
            set { nr = value; }
        }
    }
}
