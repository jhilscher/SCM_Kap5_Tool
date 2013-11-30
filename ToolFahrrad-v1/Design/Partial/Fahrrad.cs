using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using ToolFahrrad_v1.Komponenten;
using ToolFahrrad_v1.Windows;
using ToolFahrrad_v1.Design;
using ToolFahrrad_v1.Design.Partial;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using ToolFahrrad_v1.Properties;

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
            this.Select_Marketplace();
        }

        public void Select_Marketplace()
        {
            Credentials credentials = new Credentials();
            Boolean exists = File.Exists(this.path + @"\userdata");
            if (File.Exists(this.path + @"\userdata"))
            {
                FileStream eing = new FileStream(this.path + @"\userdata", FileMode.Open);
                BinaryFormatter format = new BinaryFormatter();
                
                credentials = format.Deserialize(eing) as Credentials;
                eing.Close();
                this.Get_Market_Place(credentials);
            }
            tabs.SelectedTab = tab_marktplatz;
        }

        public Credentials LoadCredentials()
        {
            Credentials credentials = new Credentials("","");
            Boolean exists = File.Exists(this.path + @"\userdata");
            if (File.Exists(this.path + @"\userdata"))
            {
                FileStream eing = new FileStream(this.path + @"\userdata", FileMode.Open);
                BinaryFormatter format = new BinaryFormatter();

                credentials = format.Deserialize(eing) as Credentials;
                eing.Close();
            }
            if (credentials.username.Equals(""))
            {
                credentials = new Credentials(txt_benutzername.Text, txt_passwort.Text);
            }
            return credentials;
        }


        /// <summary>
        /// 8. Button links: Statistik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_nav_8_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = tab_statistik;

           


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


        private void button_nav_9_Click(object sender, EventArgs e)
        {
            var einstellungen = new Einstellungen();
            tabs.SelectedTab = tab_einstellungen;
        }


        /// <summary>
        /// Klick auf Aktionsbutton einer Bestellung aus dem DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReiheLoeschenBestellung(object sender, DataGridViewCellEventArgs e) {



            if (dataGridViewBestellung.Rows.Count < 2)
                return;

            int i = e.RowIndex;

            var row = dataGridViewBestellung.Rows[i];

            if (row.Cells[6].Value == "Hinzufügen" || row.Cells[6].Value == "Add") {
                ReiheHinzu(row);
                return;
            }

            int artNr = Convert.ToInt32(row.Cells[0].Value);

            // haut die reihe weg
            dataGridViewBestellung.Rows.Remove(row);

            // reset menge auf null up in here
            var bp = _bv.BvPositionen;
            foreach (var a in bp) {

                if (a.Kaufteil.Nummer == artNr) {

                    a.Menge = 0;

                }

            }
        }

        private void AddRowToBestellungen(object sender, DataGridViewRowEventArgs e)
        {

            DataGridViewRow row = e.Row;

            row.Cells[6].Value = _culInfo.Contains("de") ? "Hinzufügen" : "Add";
            

        }

        private void ReiheHinzu(DataGridViewRow newRow) {
        try
            {
                if (dataGridViewBestellung.AllowUserToAddRows)
                {
                    dataGridViewBestellung.AllowUserToAddRows = false;
                    kNr.ReadOnly = true;
                }
                

                
                 _bp = new List<Bestellposition>();

                 foreach (DataGridViewRow row in dataGridViewBestellung.Rows)
                 {
                     var check2 = (DataGridViewCheckBoxCell)row.Cells[5];
                     bool c = check2.Value != null;

                     var value = row.Cells[0].Value;
                     if (value != null)
                     {
                         var teil = _instance.GetTeil(Convert.ToInt32(value.ToString())) as KTeil;
                         if (teil != null)
                         {
                             if (row.Cells[4].Value != null)
                             {

                                 var bbp = new Bestellposition(teil, Convert.ToInt32(row.Cells[4].Value.ToString()),
                                                               c);
                                 _bp.Add(bbp);

                             }
                             else
                                 MessageBox.Show(
                                     Resources.Fahrrad_pictureBox3_Click_Kaufteil_N + value +
                                     Resources.Fahrrad_pictureBox3_Click_, Resources.Fahrrad_XmlOeffnen_Fehlermeldung,
                                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                         }
                         else
                             MessageBox.Show(
                                 Resources.Fahrrad_pictureBox3_Click_Kaufteil_N + value +
                                 Resources.Fahrrad_pictureBox3_Click_1, Resources.Fahrrad_XmlOeffnen_Fehlermeldung,
                                 MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                     }
                     else
                     {
                         MessageBox.Show(Resources.Fahrrad_pictureBox3_Click_Kaufteil_kann_nicht_NULL_sein,
                                         Resources.Fahrrad_XmlOeffnen_Fehlermeldung,
                                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                                         MessageBoxDefaultButton.Button1);
                     }

                 }

                newRow.Cells[6].Value = _culInfo.Contains("de") ? "Löschen" : "Delete";

                _bestellungUpdate = true;
                _bv.SetBvPositionen(_bp);
                Information();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            // fuer update
            _dvUpdate = true;
            _bestellungUpdate = true;
            _bv.SetDvPositionen(_dirv);
            Information();
        }

    }
}
