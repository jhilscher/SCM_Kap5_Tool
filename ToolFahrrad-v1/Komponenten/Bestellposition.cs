using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFahrrad_v1
{
    /** Class describes position of ordered KTeile */
    class Bestellposition
    {
        // Class members
        private KTeil kaufteil;
        private int menge;
        private bool eil;
        // Constructor
        public Bestellposition(KTeil kt_param, int menge_param, bool eil_param)
        {
            this.kaufteil = kt_param;
            this.menge = menge_param;
            this.eil = eil_param;
        }
        // Getter / Setter
        public KTeil Kaufteil
        {
            get { return this.kaufteil; }
            set { this.kaufteil = value; }
        }
        public int Menge
        {
            get { return this.menge; }
            set { this.menge = value; }
        }
        public bool Eil
        {
            get { return this.eil; }
            set { this.eil = value; }
        }
        public int OutputEil
        {
            get
            {
                if (this.eil)
                {
                    return 4;
                }
                else
                {
                    return 5;
                }
            }
        }
    }
}
