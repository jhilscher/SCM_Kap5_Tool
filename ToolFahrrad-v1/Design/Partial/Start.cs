using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ToolFahrrad_v1.Komponenten;
using ToolFahrrad_v1.Properties;
using ToolFahrrad_v1.Verwaltung;
using ToolFahrrad_v1.Windows;
using ToolFahrrad_v1.XML;

namespace ToolFahrrad_v1.Design
{
    /// <summary>
    /// Partial from Pierre
    /// </summary>
    partial class Fahrrad
    {
        private void start_prognose_numeric_periodeN_ValueChanged(object sender, EventArgs e)
        {
            start_prognose_textbox_periodeN.Text = Convert.ToString(upDownAW1.Value + upDownAW2.Value + upDownAW3.Value);
        }

        private void start_prognose_numeric_periodeN1_ValueChanged(object sender, EventArgs e)
        {
            start_prognose_textbox_periodeN1.Text = Convert.ToString(upDownP11.Value + upDownP12.Value + upDownP13.Value);
        }

        private void start_prognose_numeric_periodeN2_ValueChanged(object sender, EventArgs e)
        {
            start_prognose_textbox_periodeN2.Text = Convert.ToString(upDownP21.Value + upDownP22.Value + upDownP23.Value);
        }

        private void start_prognose_numeric_periodeN3_ValueChanged(object sender, EventArgs e)
        {
            start_prognose_textbox_periodeN3.Text = Convert.ToString(upDownP31.Value + upDownP32.Value + upDownP33.Value);
        }

        private void start_prognose_numeric_puffer_ValueChanged(object sender, EventArgs e)
        {
            start_prognose_textbox_puffer.Text = Convert.ToString(pufferP1.Value + pufferP2.Value + pufferP3.Value);
        }
    }
}
