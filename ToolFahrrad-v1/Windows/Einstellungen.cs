using System;
using System.Windows.Forms;

namespace ToolFahrrad_v1.Windows
{
    public partial class Einstellungen : Form
    {
        DataContainer _instance = DataContainer.Instance;

        public Einstellungen()
        {
            InitializeComponent();
            numericUpDown1.Value = _instance.ErsteSchicht;
            numericUpDown2.Value = _instance.ZweiteSchicht;
            diskGrenze.Value = Convert.ToDecimal(_instance.DiskountGrenze);
            mengeGrenze.Value = Convert.ToDecimal(_instance.GrenzeMenge);
            trackBar1.Value = (int)_instance.VerwendeDiskount / 10;
            trackBarAbweichung.Value = (int)_instance.VerwendeAbweichung / 10;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            _instance.VerwendeAbweichung = trackBarAbweichung.Value * 10;
            panel1.Visible = true;
        }

        private void trackBarAbweichung_Scroll(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void btn_schicht_save_Click(object sender, EventArgs e)
        {
            _instance.ErsteSchicht = (int)numericUpDown1.Value;
            _instance.ZweiteSchicht = (int)numericUpDown2.Value;

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
            _instance.DiskountGrenze = Convert.ToDouble(diskGrenze.Value);
            _instance.GrenzeMenge = Convert.ToDouble(mengeGrenze.Value);
            _instance.VerwendeDiskount = trackBar1.Value * 10;
            panel3.Visible = true;
        }

        private void tab_abweichung_Click(object sender, EventArgs e)
        {

        }
    }
}
