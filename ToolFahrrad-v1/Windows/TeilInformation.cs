using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolFahrrad_v1
{
    public partial class TeilInformation : Form
    {
        private int _nummer;
        DataContainer dc = DataContainer.Instance;
        public TeilInformation(int nummer)
        {
            this._nummer = nummer;
            InitializeComponent();
            label2.Text = Convert.ToString(_nummer);
        }

        internal void GetTeilvonETeilMitMenge()
        {
            ausgabe.Text = "Lieferdauer: " + (dc.GetTeil(_nummer) as KTeil).Lieferdauer + " (" + (dc.GetTeil(_nummer) as KTeil).AbweichungLieferdauer + ")\n" +
                            "Diskontmenge: " + (dc.GetTeil(_nummer) as KTeil).DiskontMenge + "\n" +
                            "Bestellkosten: " + (dc.GetTeil(_nummer) as KTeil).Bestellkosten + "\n\n";
            List<ETeil> list = (dc.GetTeil(_nummer) as KTeil).IstTeilVon;
            foreach (ETeil e in list)
            {
                foreach (KeyValuePair<Teil, int> kvp in e.Zusammensetzung)
                {
                    if (kvp.Key.Nummer == _nummer)
                    {
                        
                        ausgabe.Text += "wird in Teil " + e.Nummer + "  " + kvp.Value + " mal verwendet\n";
                    }
                }
            }
        }
    }
}
