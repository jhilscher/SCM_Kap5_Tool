using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFahrrad_v1
{
    /** Class describes position of ordered KTeile */
    public class Bestellposition
    {
        // Class members
        private KTeil kt;
        private int menge;
        private bool eil;
        // Constructor
        public Bestellposition(KTeil kt_param, int menge_param, bool eil_param)
        {
            kt = kt_param;
            menge = menge_param;
            eil = eil_param;
        }
        // Getter / Setter
        public KTeil Kaufteil
        {
            get { return kt; }
            set { kt = value; }
        }
        public int Menge
        {
            get { return menge; }
            set { menge = value; }
        }
        public bool Eil
        {
            get { return eil; }
            set { eil = value; }
        }
        public int OutputEil
        {
            get
            {
                if (eil == true)
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
