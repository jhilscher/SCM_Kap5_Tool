﻿using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using ToolFahrrad_v1.Komponenten;

namespace ToolFahrrad_v1.XML
{
    public class XmlDatei
    {
        public string Period = "";

        private static DataContainer _dc = DataContainer.Instance;

        public bool ReadDatei(string pfad) {
            var res = false;
            var dc = DataContainer.Instance;
            var xmlText = string.Empty;
            var zahl = 1;
            //xml saubern und in xmlText speichern
            using (var sr = new StreamReader(pfad, Encoding.UTF8)) {
                string zeile;
                while ((zeile = sr.ReadLine()) != null) {
                    var r = new Regex(@"^\s+");
                    zeile = r.Replace(zeile, @"");
                    zeile = zeile.Replace("\r\n", string.Empty);
                    zeile = zeile.Replace("- <", "<");
                    if (zeile.Contains("results"))
                        res = true;
                    xmlText += zeile;
                }
                if (res == false)
                    return res;
            }
            // xmlLoad
            var doc = new XmlDocument();
            doc.LoadXml(xmlText);

            //Period
            XmlNodeList items = doc.GetElementsByTagName("results");
            foreach (XmlNode node in items) {
                Period = node.Attributes[2].Value;
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
                    }
                    ++zahl;
                }
            }
            return res;
        }
  
        internal void WriteDatei(string fileName) {
            StreamWriter sw = File.CreateText(fileName);
            sw.WriteLine("<input>");
            sw.WriteLine("<qualitycontrol type=\"no\" losequantity=\"0\" delay=\"0\"/>");
            //Vertriebswunsch
            sw.WriteLine("<sellwish>");
            for (int i = 1; i < 4; ++i) {
                sw.WriteLine("<item article=\"" + i + "\" quantity=\"" + _dc.GetTeil(i).VerbrauchPer1 + "\"/>");
            }
            sw.WriteLine("</sellwish>");

            //Direktverkauf
            sw.WriteLine("<selldirect>");
            if (_dc.DVerkauf.Count > 0) {
                foreach (DvPosition dv in _dc.DVerkauf) {
                    sw.WriteLine("<item article=\"" + dv.DvTeilNr + "\" quantity=\"" + dv.DvMenge + "\" price=\"" +dv.DvPreis + "\" penalty=\"" + dv.DvStrafe + "\"/>");
                }
            }
            else {
                for (int i = 1; i < 4; ++i) {
                    sw.WriteLine("<item article=\"" + i + "\" quantity=\"0\" price=\"0.0\" penalty=\"0.0\"/>");
                }                
            }
            sw.WriteLine("</selldirect>");

            //Einkaufsaufträge
            sw.WriteLine("<orderlist>");
            foreach (Bestellposition bp in _dc.Bestellungen) {
                sw.WriteLine("<order article=\"" + bp.Kaufteil.Nummer + "\" quantity=\"" + bp.Menge + "\" modus=\"" + bp.OutputEil + "\"/>");
            }
            sw.WriteLine("</orderlist>");

            //Produktionsaufträge
            sw.WriteLine("<productionlist>");
            //<production article="9" quantity="140" />
            foreach (var pl in _dc.ListeProduktion)
            {
                sw.WriteLine("<production article=\"" + pl.Key + "\" quantity=\"" + pl.Value + "\"/>");
            }
            sw.WriteLine("</productionlist>");

            //Produktionskapaziläten
            sw.WriteLine("<workingtimelist>");
            foreach (int[] a in _dc.ApKapazitaet) {
                sw.WriteLine("<workingtime station=\"" + a[0] + "\" shift=\"" + a[1] + "\" overtime=\"" + (a[2] / 5) + "\"/>");  
            }
            sw.WriteLine("</workingtimelist>");

            sw.WriteLine("</input>");

            sw.Flush();
            sw.Close();

        }

        /// <summary>
        /// To Display the content in the app.
        /// </summary>
        /// <returns>String mit schoenem xml</returns>
        internal string ToString()
        {
            // einrueken
            string tab = "     ";

            StringBuilder sw = new StringBuilder();
            sw.Append("<input>").AppendLine();
            sw.Append("<qualitycontrol type=\"no\" losequantity=\"0\" delay=\"0\"/>").AppendLine();
            //Vertriebswunsch
            sw.Append("<sellwish>").AppendLine();
            for (int i = 1; i < 4; ++i)
            {
                sw.Append(tab);
                sw.Append("<item article=\"" + i + "\" quantity=\"" + _dc.GetTeil(i).VerbrauchPer1 + "\"/>").AppendLine();
            }
            sw.Append("</sellwish>").AppendLine();

            //Direktverkauf
            sw.Append("<selldirect>").AppendLine();
            if (_dc.DVerkauf.Count > 0)
            {
                foreach (DvPosition dv in _dc.DVerkauf)
                {
                    sw.Append(tab);
                    sw.Append("<item article=\"" + dv.DvTeilNr + "\" quantity=\"" + dv.DvMenge + "\" price=\"" + dv.DvPreis + "\" penalty=\"" + dv.DvStrafe + "\"/>");
                    sw.AppendLine();
                }
            }
            else
            {
                for (int i = 1; i < 4; ++i)
                {
                    sw.Append(tab);
                    sw.Append("<item article=\"" + i + "\" quantity=\"0\" price=\"0.0\" penalty=\"0.0\"/>");
                    sw.AppendLine();
                }
            }
            sw.Append("</selldirect>").AppendLine();

            //Einkaufsaufträge
            sw.Append("<orderlist>").AppendLine();
            foreach (Bestellposition bp in _dc.Bestellungen)
            {
                sw.Append(tab);
                sw.Append("<order article=\"" + bp.Kaufteil.Nummer + "\" quantity=\"" + bp.Menge + "\" modus=\"" + bp.OutputEil + "\"/>");
                sw.AppendLine();
            }
            sw.Append("</orderlist>").AppendLine();

            //Produktionsaufträge
            sw.Append("<productionlist>").AppendLine();
            //<production article="9" quantity="140" />
            foreach (var pl in _dc.ListeProduktion)
            {
                sw.Append(tab);
                sw.Append("<production article=\"" + pl.Key + "\" quantity=\"" + pl.Value + "\"/>").AppendLine();
            }
            sw.Append("</productionlist>").AppendLine();

            //Produktionskapaziläten
            sw.Append("<workingtimelist>").AppendLine();
            foreach (int[] a in _dc.ApKapazitaet)
            {
                sw.Append(tab);
                sw.Append("<workingtime station=\"" + a[0] + "\" shift=\"" + a[1] + "\" overtime=\"" + (a[2] / 5) + "\"/>").AppendLine();
            }
            sw.Append("</workingtimelist>").AppendLine();

            sw.Append("</input>").AppendLine();

            return sw.ToString();
        }

    }
}
