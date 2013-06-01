﻿using System;
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
    public partial class TeilInformation : Form
    {
        private int _nummer;
        DataContainer dc = DataContainer.Instance;
        public TeilInformation(string name, int nummer) {
            InitializeComponent();
            this._nummer = nummer;
            if (name.Equals("kteil")) {
                label1.Text = "Kaufteil N " + _nummer;
            }
            else if (name.Equals("arbeitsplatz")) {
                label1.Text = "Arbeitsplatz N " + _nummer;
            }


        }

        internal void GetTeilvonETeilMitMenge() {
            ausgabe.Text = "Lieferdauer: " + (dc.GetTeil(_nummer) as KTeil).Lieferdauer + " (" + (dc.GetTeil(_nummer) as KTeil).AbweichungLieferdauer + ")\n" +
                            "Diskontmenge: " + (dc.GetTeil(_nummer) as KTeil).DiskontMenge + "\n" +
                            "Bestellkosten: " + (dc.GetTeil(_nummer) as KTeil).Bestellkosten + "\n\n";
            List<ETeil> list = (dc.GetTeil(_nummer) as KTeil).IstTeilVon;
            foreach (ETeil e in list) {
                foreach (KeyValuePair<Teil, int> kvp in e.Zusammensetzung) {
                    if (kvp.Key.Nummer == _nummer) {

                        ausgabe.Text += "wird in Teil " + e.Nummer + "  " + kvp.Value + " mal verwendet\n";
                    }
                }
            }
        }

        internal void GetZeitInformation() {
            ausgabe2.Text = "Rüstungszeit: \n";
            int sum = 0;
            int val = 0;
            int prMenge = 0;
            foreach (KeyValuePair<int, int> kvp in dc.GetArbeitsplatz(_nummer).Ruest_zeiten) {
                if (!(dc.GetTeil(kvp.Key) as ETeil).Verwendung.Contains("KDH")) {
                    if ((dc.GetTeil(kvp.Key) as ETeil).ProduktionsMengePer0 >= 0)
                        val = kvp.Value;
                    else
                        val = 0;
                }
                else {
                    foreach (KeyValuePair<string, int> pair in (dc.GetTeil(kvp.Key) as ETeil).KdhProduktionsmenge) {
                        prMenge += pair.Value;
                        if (prMenge >= 0)
                            val = kvp.Value;
                        else
                            val = 0;
                    }
                }
                ausgabe2.Text += "Teil " + kvp.Key + ": " + val + " min. \n";
                sum += val;
            }
            ausgabe2.Text += "Summe: " + sum + "\n\n";
            
            //
            ausgabe.Text += "Kapazitätsbedarf: \n";
            sum = 0;
            prMenge = 0;
            foreach (KeyValuePair<int, int> kvp in dc.GetArbeitsplatz(_nummer).Werk_zeiten) {
                {
                    if (!(dc.GetTeil(kvp.Key) as ETeil).Verwendung.Contains("KDH")) {
                        prMenge = (dc.GetTeil(kvp.Key) as ETeil).ProduktionsMengePer0;
                    }
                    else {
                        foreach (KeyValuePair<string, int> pair in (dc.GetTeil(kvp.Key) as ETeil).KdhProduktionsmenge) {
                            prMenge += pair.Value;
                        }
                    }
                    if (prMenge < 0)
                        prMenge = 0;
                    ausgabe.Text += "Teil " + kvp.Key + ": " + kvp.Value + " * " + prMenge + " = " + kvp.Value * prMenge + " min.\n";
                    sum += kvp.Value * prMenge;
                    prMenge = 0;
                }
            }
            ausgabe.Text += "Summe: " + sum + "\n\n";
        }
    }
}
