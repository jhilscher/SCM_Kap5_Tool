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

        private void start_prognose_button_save_Click(object sender, EventArgs e)
        {
            _instance.GetTeil(1).VertriebPer0 = Convert.ToInt32(start_prognose_numeric_periodeN_P1.Value);
            _instance.GetTeil(2).VertriebPer0 = Convert.ToInt32(start_prognose_numeric_periodeN_P2.Value);
            _instance.GetTeil(3).VertriebPer0 = Convert.ToInt32(start_prognose_numeric_periodeN_P3.Value);

            (_instance.GetTeil(1) as ETeil).Puffer = Convert.ToInt32(start_prognose_numeric_puffer_P1.Value);
            (_instance.GetTeil(2) as ETeil).Puffer = Convert.ToInt32(start_prognose_numeric_puffer_P2.Value);
            (_instance.GetTeil(3) as ETeil).Puffer = Convert.ToInt32(start_prognose_numeric_puffer_P3.Value);

            _instance.GetTeil(1).VerbrauchPer1 = Convert.ToInt32(start_prognose_numeric_periodeN1_P1.Value);
            _instance.GetTeil(1).VerbrauchPer2 = Convert.ToInt32(start_prognose_numeric_periodeN2_P1.Value);
            _instance.GetTeil(1).VerbrauchPer3 = Convert.ToInt32(start_prognose_numeric_periodeN3_P1.Value);

            _instance.GetTeil(2).VerbrauchPer1 = Convert.ToInt32(start_prognose_numeric_periodeN1_P2.Value);
            _instance.GetTeil(2).VerbrauchPer2 = Convert.ToInt32(start_prognose_numeric_periodeN2_P2.Value);
            _instance.GetTeil(2).VerbrauchPer3 = Convert.ToInt32(start_prognose_numeric_periodeN3_P2.Value);

            _instance.GetTeil(3).VerbrauchPer1 = Convert.ToInt32(start_prognose_numeric_periodeN1_P3.Value);
            _instance.GetTeil(3).VerbrauchPer2 = Convert.ToInt32(start_prognose_numeric_periodeN2_P3.Value);
            _instance.GetTeil(3).VerbrauchPer3 = Convert.ToInt32(start_prognose_numeric_periodeN2_P3.Value);

            bildSpeichOk.Visible = true;
            panelXML.Visible = true;
            _okPrognose = true;
            start_prognose_label_successInfo.ForeColor = System.Drawing.Color.LimeGreen;
            if (_okXml) {
                toolAusfueren.Visible = true;
            }

            GetInfo(_culInfo.Contains("de") ? "Prognose wurde gespeichert" : "Forecast has been saved");
        }

        private void start_prognose_numeric_periodeN_ValueChanged(object sender, EventArgs e)
        {
            start_prognose_textbox_periodeN.Text = Convert.ToString(start_prognose_numeric_periodeN_P1.Value + start_prognose_numeric_periodeN_P2.Value + start_prognose_numeric_periodeN_P3.Value);
        }

        private void start_prognose_numeric_periodeN1_ValueChanged(object sender, EventArgs e)
        {
            start_prognose_textbox_periodeN1.Text = Convert.ToString(start_prognose_numeric_periodeN1_P1.Value + start_prognose_numeric_periodeN1_P2.Value + start_prognose_numeric_periodeN1_P3.Value);
        }

        private void start_prognose_numeric_periodeN2_ValueChanged(object sender, EventArgs e)
        {
            start_prognose_textbox_periodeN2.Text = Convert.ToString(start_prognose_numeric_periodeN2_P1.Value + start_prognose_numeric_periodeN2_P2.Value + start_prognose_numeric_periodeN2_P3.Value);
        }

        private void start_prognose_numeric_periodeN3_ValueChanged(object sender, EventArgs e)
        {
            start_prognose_textbox_periodeN3.Text = Convert.ToString(start_prognose_numeric_periodeN3_P1.Value + start_prognose_numeric_periodeN3_P2.Value + start_prognose_numeric_periodeN3_P3.Value);
        }

        private void start_prognose_numeric_puffer_ValueChanged(object sender, EventArgs e)
        {
            start_prognose_textbox_puffer.Text = Convert.ToString(start_prognose_numeric_puffer_P1.Value + start_prognose_numeric_puffer_P2.Value + start_prognose_numeric_puffer_P3.Value);
        }
    }
}
