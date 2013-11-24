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

    }
}
