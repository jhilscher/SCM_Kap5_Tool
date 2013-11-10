using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using ToolFahrrad_v1.Komponenten;

namespace ToolFahrrad_v1.Design
{
    /// <summary>
    /// Joerg's partial.
    /// </summary>
    partial class Fahrrad
    {




        /// <summary>
        /// 1. Button links
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nav_button_1_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = tab_xml;
        }

        /// <summary>
        /// 2. Button links
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_nav_2_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = tab_xml;
        }

        /// <summary>
        /// 3. Button links
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_nav_3_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = tab_produktion;
        }

        /// <summary>
        /// 4. Button links
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_nav_4_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = tab_arbeitzeit;
        }

        /// <summary>
        /// 5. Button links
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_nav_5_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = tab_bestellverwaltung;
        }
        
        /// <summary>
        /// 6. Button links
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_nav_6_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 8. Button links: Statistik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_nav_8_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = tab_statistik;

            this.chart_statistik.ChartAreas.First().AxisX.Interval = 1;

            /// 
            /// Bestellungen Anzahl
            /// 
            Series series1 = this.chart_statistik.Series.FirstOrDefault(x => x.Name == "Bestellungen");

            var bestellSumme = 0.00;

            // baue datapoints zusammen
            var listBv = _bv.BvPositionen.Select<Bestellposition, DataPoint>(x =>
            {
                var dp = new DataPoint(x.Kaufteil.Nummer/2, x.Menge);
                dp.AxisLabel = x.Kaufteil.ToString();
                bestellSumme += x.Kaufteil.Preis;
                return dp;
            }).ToList();

            // hinzu zur serie!
            listBv.ForEach(x => series1.Points.Add(x));


            ///
            /// Produktion Anzahl
            /// 

            Series series2 = this.chart_statistik.Series.FirstOrDefault(x => x.Name == "Produktion");

            // baue datapoints zusammen
            var listPv = _pp.ProdListe.Select(x =>
            {
                var dp = new DataPoint(x.Key/2, x.Value);
                dp.AxisLabel = x.Key.ToString();
                return dp;
            }).ToList();

            // hinzu zur serie!
            listPv.ForEach(x => series2.Points.Add(x));

            
        }
                

        /// <summary>
        /// 7. Button links
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_nav_7_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = tab_beenden;
            string xmlText = _xml.ToString();
            this.TextBox_xmlOutput.Text = xmlText;

            /*XmlDocument reader = new XmlDocument();
            reader.LoadXml(xmlText);

            foreach (XmlNode node in reader.ChildNodes)
            {

                foreach (XmlNode subnode in node.ChildNodes)
                {

                    switch (subnode.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.
                            this.TextBox_xmlOutput.SelectionColor = System.Drawing.Color.Blue;
                            this.TextBox_xmlOutput.AppendText("<");
                            this.TextBox_xmlOutput.SelectionColor = System.Drawing.Color.Brown;
                            this.TextBox_xmlOutput.AppendText(subnode.Name);
                            this.TextBox_xmlOutput.SelectionColor = System.Drawing.Color.Blue;
                            this.TextBox_xmlOutput.AppendText(">");
                            break;
                        case XmlNodeType.Text: //Display the text in each element.
                            this.TextBox_xmlOutput.SelectionColor = System.Drawing.Color.Black;
                            this.TextBox_xmlOutput.AppendText(subnode.Value);
                            break;
                        case XmlNodeType.EndElement: //Display the end of the element.
                            this.TextBox_xmlOutput.SelectionColor = System.Drawing.Color.Blue;
                            this.TextBox_xmlOutput.AppendText("</");
                            this.TextBox_xmlOutput.SelectionColor = System.Drawing.Color.Brown;
                            this.TextBox_xmlOutput.AppendText(subnode.Name);
                            this.TextBox_xmlOutput.SelectionColor = System.Drawing.Color.Blue;
                            this.TextBox_xmlOutput.AppendText(">");
                            this.TextBox_xmlOutput.AppendText("\n");
                            break;
                            case XmlNodeType.Attribute
                    }
                }
            }
            
            */
            //TextBox_xmlOutput.Text = xmlText;
            //TextBox_xmlOutput.Refresh();
        }

        /// <summary>
        /// NEU: export ueber button auf xml-preview seite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_exportXml_Click(object sender, EventArgs e)
        {
            XmlExport();
        }

    }
}
