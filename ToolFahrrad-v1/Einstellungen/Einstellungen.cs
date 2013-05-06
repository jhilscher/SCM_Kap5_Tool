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
    public partial class Einstellungen : Form
    {
        Bestellverwaltung bv = new Bestellverwaltung();

        public Einstellungen()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            bv.VerwendeAbweichung = trackBarAbweichung.Value * 10;
        }
    }
}
