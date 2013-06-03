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
            numericUpDown1.Value = instance.ErsteSchicht;
            numericUpDown2.Value = instance.ZweiteSchicht;
            diskGrenze.Value = Convert.ToDecimal(instance.DiskountGrenze);
            mengeGrenze.Value = Convert.ToDecimal(instance.GrenzeMenge);
            trackBar1.Value = (int)instance.VerwendeDiskount / 10;
            trackBarAbweichung.Value = (int)instance.VerwendeAbweichung / 10;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            instance.VerwendeAbweichung = trackBarAbweichung.Value * 10;
            panel1.Visible = true;
        }

        private void trackBarAbweichung_Scroll(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void btn_schicht_save_Click(object sender, EventArgs e)
        {
            instance.ErsteSchicht = (int)numericUpDown1.Value;
            instance.ZweiteSchicht = (int)numericUpDown2.Value;

            panel2.Visible = true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Change();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Change();
        }

        private void Change()
        {
            panel2.Visible = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e) {
            panel1.Visible = false;
        }

        private void diskGrenze_ValueChanged(object sender, EventArgs e) {
            if (diskGrenze.Value >= mengeGrenze.Value) {
                diskGrenze.Value = mengeGrenze.Value - 1;
            }
        }

        private void mengeGrenze_ValueChanged(object sender, EventArgs e) {
            if (diskGrenze.Value >= mengeGrenze.Value) {
                mengeGrenze.Value = diskGrenze.Value + 1;
            }
        }

        private void diskSpeichern_Click(object sender, EventArgs e) {
            instance.DiskountGrenze = Convert.ToDouble(diskGrenze.Value);
            instance.GrenzeMenge = Convert.ToDouble(mengeGrenze.Value);
            instance.VerwendeDiskount = trackBar1.Value * 10;
            panel3.Visible = true;
        }
    }
}
