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
        DataContainer instance = DataContainer.Instance;
        Arbeitsplatz ap = new Arbeitsplatz();

        public Einstellungen()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            bv.VerwendeAbweichung = trackBarAbweichung.Value * 10;
            panel1.Visible = true;
        }

        private void trackBarAbweichung_Scroll(object sender, EventArgs e)
        {
            lbl_text.Text = "Abweichung = " + trackBarAbweichung.Value.ToString() +"0%";
            panel1.Visible = false;
        }

        private void btn_schicht_save_Click(object sender, EventArgs e)
        {
            ap.ErsteSchicht = (int)numericUpDown1.Value;
            ap.ZweiteSchicht = (int)numericUpDown2.Value;
        }
    }
}
