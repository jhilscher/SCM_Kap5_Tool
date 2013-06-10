using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace ToolFahrrad_v1
{
    public class XMLDatei
    {
        public string period = "";

        private static DataContainer dc = DataContainer.Instance;
        public XMLDatei() {

        }

        public bool ReadDatei(string pfad) {
            bool res = false;
            XmlNodeList items;
            DataContainer dc = DataContainer.Instance;
            string zeile = string.Empty;
            string xmlText = string.Empty;
            int zahl = 1;
            //xml saubern und in xmlText speichern
            using (StreamReader sr = new StreamReader(pfad, Encoding.UTF8)) {
                while ((zeile = sr.ReadLine()) != null) {
                    zeile = zeile.Replace("- <", "<");
                    if (zeile.Contains("results"))
                        res = true;
                    xmlText += zeile;
                }
                if (res == false)
                    return res;
            }
            // xmlLoad
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlText);

            //Period
            items = doc.GetElementsByTagName("results");
            foreach (XmlNode node in items) {
                period = node.Attributes[2].Value;
            }

            //LagerBestand
            items = doc.GetElementsByTagName("warehousestock");
            foreach (XmlNode node in items) {
                foreach (XmlNode attr in node.ChildNodes) {
                    if (attr.Name == "article") {
                        dc.GetTeil(zahl).Lagerstand = Convert.ToInt32(attr.Attributes[1].Value);
                        dc.GetTeil(zahl).Verhaeltnis = Convert.ToDouble(attr.Attributes[3].Value);
                    }
                    ++zahl;
                }
            }

            //LagerZugang
            items = doc.GetElementsByTagName("futureinwardstockmovement");
            foreach (XmlNode node in items) {
                foreach (XmlNode attr in node.ChildNodes) {
                    if (attr.Name == "order") {
                        foreach (var a in dc.ListeKTeile) {
                            if (Convert.ToInt32(attr.Attributes[3].Value).Equals(a.Nummer)) {
                                a.AddOffeneBestellung(Convert.ToInt32(attr.Attributes[0].Value), Convert.ToInt32(attr.Attributes[2].Value), Convert.ToInt32(attr.Attributes[4].Value));
                            }
                        }
                    }
                }
            }

            //Warteliste
            items = doc.GetElementsByTagName("waitinglistworkstations");
            foreach (XmlNode node in items) {
                foreach (XmlNode attr in node.ChildNodes) {
                    if (attr.Name == "workplace") {
                        foreach (XmlNode attr2 in attr) {
                            if (attr2.Name == "waitinglist") {
                                foreach (var a in dc.ListeETeile) {
                                    if (Convert.ToInt32(attr2.Attributes[4].Value).Equals(a.Nummer)) {
                                        a.InWartschlange = Convert.ToInt32(attr2.Attributes[5].Value);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //in der Bearbeitung
            items = doc.GetElementsByTagName("ordersinwork");
            foreach (XmlNode node in items) {
                foreach (XmlNode attr in node.ChildNodes) {
                    if (attr.Name == "workplace") {
                        foreach (var a in dc.ListeETeile) {
                            if (Convert.ToInt32(attr.Attributes[4].Value).Equals(a.Nummer)) {
                                a.InBearbeitung = Convert.ToInt32(attr.Attributes[5].Value);
                            }
                        }
                    }
                }
            }


            //Arbeitsplatz
            items = doc.GetElementsByTagName("idletimecosts");
            foreach (XmlNode node in items) {
                foreach (XmlNode attr in node.ChildNodes) {
                    if (attr.Name == "workplace") {
                        dc.GetArbeitsplatz(Convert.ToInt32(attr.Attributes[0].Value)).RuestungVorPeriode = Convert.ToInt32(attr.Attributes[1].Value);
                        dc.GetArbeitsplatz(Convert.ToInt32(attr.Attributes[0].Value)).Leerzeit = Convert.ToInt32(attr.Attributes[2].Value);

                        //dc.GetTeil(zahl).Lagerstand = Convert.ToInt32(attr.Attributes[1].Value);
                        //dc.GetTeil(zahl).Verhaeltnis = Convert.ToDouble(attr.Attributes[3].Value);
                    }
                    ++zahl;
                }
            }
            return res;
        }
        /// WriteInput Methode
        /// schreibst die input.xml zum Einlesen in SCSIM
        public static void WriteInput() {

            //Dateianfang
            WriteFile("<PeriodInput>");
            // WriteFile("<qualitycontrol type=\"no\" losequantity=\"0\" delay=\"0\" />");
            WriteFile("<SalesWishes>");
            WriteFile("<SalesWish>");
            WriteFile("<SalesItemInternalNumber>\"1\"</SalesItemInternalNumber>");
            WriteFile("<SalesQuantity>" + (dc.GetTeil(1) as ETeil).ProduktionsMengePer0 + "</SalesQuantity>");
            WriteFile("<DirectSaleQuantity>0</DirectSaleQuantity>");
            WriteFile("<DirectSalePrice>0.0</DirectSalePrice>");
            WriteFile("<DirectSalePenalty>0.0</DirectSalePenalty>");
            WriteFile("</SalesWish>");
            WriteFile("<SalesWish>");
            WriteFile("<SalesItemInternalNumber>2</SalesItemInternalNumber>");
            WriteFile("<SalesQuantity>" + (dc.GetTeil(2) as ETeil).ProduktionsMengePer0 + "</SalesQuantity>");
            WriteFile("<DirectSaleQuantity>0</DirectSaleQuantity>");
            WriteFile("<DirectSalePrice>0.0</DirectSalePrice>");
            WriteFile("<DirectSalePenalty>0.0</DirectSalePenalty>");
            WriteFile("</SalesWish>");
            WriteFile("<SalesWish>");
            WriteFile("<SalesItemInternalNumber>2</SalesItemInternalNumber>");
            WriteFile("<SalesQuantity>" + (dc.GetTeil(3) as ETeil).ProduktionsMengePer0 + "</SalesQuantity>");
            WriteFile("<DirectSaleQuantity>0</DirectSaleQuantity>");
            WriteFile("<DirectSalePrice>0.0</DirectSalePrice>");
            WriteFile("<DirectSalePenalty>0.0</DirectSalePenalty>");
            WriteFile("</SalesWish>");
            WriteFile("</SalesWishes>");
            //Bestellungen
            WriteFile("<ItemOrders>");
            foreach (Bestellposition bp in dc.Bestellungen) {
                WriteFile("<ItemOrder>");
                WriteFile("<ItemInternalNumber>" + bp.Kaufteil.Nummer + "</ItemInternalNumber>");
                WriteFile("<Quantity>" + bp.Menge + "</Quantity>");
                WriteFile("<Supplier>" + bp.OutputEil + "</Supplier>");
                WriteFile("</ItemOrder>");
            }
            WriteFile("</ItemOrders>");
            //Produktionsaufträge
            WriteFile("<ProductionOrders>");

            foreach (int z in dc.Reihenfolge) {
                WriteFile("<ProductionOrder>");
                WriteFile("<ItemInternalNumber>" + z + "</ItemInternalNumber>");
                WriteFile("<Quantity>" + (dc.GetTeil(z) as ETeil).ProduktionsMengePer0 + "</Quantity>");
                WriteFile("</ProductionOrder>");
            }
            WriteFile("</ProductionOrders>");
            //Workingtimelist
            WriteFile("<WorkplaceShifts>");
            for (int i = 1; i <= 15; i++) {
                if (i == 5) {
                    i++;
                }
                WriteFile("<WorkplaceShift>");
                WriteFile("<WorkplaceName>" + i + "</WorkplaceName>");
                WriteFile("<Shifts>" + dc.GetArbeitsplatz(i).AnzSchichten + "</Shifts>");
                WriteFile("<OvertimeInMinutes>" + dc.GetArbeitsplatz(i).UeberMin + "</OvertimeInMinutes>");
                WriteFile("</WorkplaceShift>");
            }
            WriteFile("</WorkplaceShifts>");
            WriteFile("</PeriodInput>");

        }


        private static void WriteFile(string Inhalt) {



            /*
            StreamWriter datei;
            datei = File.AppendText(DataContainer.Instance.SaveFile);
            datei.WriteLine(Inhalt);
            datei.Close();
             */
        }


        internal void WriteDatei(string fileName) {
            StreamWriter sw = File.CreateText(fileName);
            sw.WriteLine("<input>");
            sw.WriteLine("<qualitycontrol type=\"no\" losequantity=\"0\" delay=\"0\"/>");
            //Vertriebswunsch
            sw.WriteLine("<sellwish>");
            for (int i = 1; i < 4; ++i) {
                sw.WriteLine("<item article=\"" + i + "\" quantity=\"" + dc.GetTeil(i).VerbrauchPer1 + "\"/>");
            }
            sw.WriteLine("</sellwish>");

            //Direktverkauf
            sw.WriteLine("<selldirect>");
            if (dc.DVerkauf.Count > 0) {
                foreach (DvPosition dv in dc.DVerkauf) {
                    sw.WriteLine("<item article=\"" + dv.DvTeilNr + "\" quantity=\"" + dv.DvMenge + "\" price=\"" +dv.DvPreis + "\" penalty=\"" + dv.DvStrafe + "\"/>");
                }
            }
            else {
                for (int i = 1; i < 4; ++i) {
                    sw.WriteLine("<item article=\"" + i + "\" quantity=\"0\" price=\"0.0\" penalty=\"0.0\"/>");
                }                
            }
            sw.WriteLine("</selldirect>");
            
            sw.WriteLine("</input>");

            sw.Flush();
            sw.Close();

        }
    }
}
