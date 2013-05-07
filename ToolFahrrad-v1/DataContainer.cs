using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace ToolFahrrad_v1
{
    class DataContainer
    {
        // Class members
        private static DataContainer instance = new DataContainer();
        private List<Bestellposition> listeBestellungen;
        private Dictionary<int, Teil> listeTeile;
        private Dictionary<int, Arbeitsplatz> listeArbeitsplaetze;
        private int[] listeReihenfolge;
        private bool sonderProduktion = false;
        private bool pufferGeaendert = false;
        private bool ueberstundenErlaubt = true;
        private Produktionsplanung pp;
        private string openFile;
        private string saveFile;
        private int ersteSchicht = 3600;
        private int zweiteSchicht = 6000;
        public int ZweiteSchicht
        {
            get { return zweiteSchicht; }
            set { zweiteSchicht = value; }
        }
        public int ErsteSchicht
        {
            get { return ersteSchicht; }
            set { ersteSchicht = value; }
        }
        // Constructor
        private DataContainer()
        {
            listeBestellungen = new List<Bestellposition>();
            listeTeile = new Dictionary<int, Teil>();
            listeArbeitsplaetze = new Dictionary<int, Arbeitsplatz>();
            openFile = Application.StartupPath + "//output.xml";
            saveFile = Application.StartupPath + "//input.xml";
        }
        // Getter for instance of DataContainer
        public static DataContainer Instance
        {
            get { return instance; }
        }
        // Getter / Setter for Reihenfolge of production
        public int[] Reihenfolge
        {
            get { return listeReihenfolge; }
            set { listeReihenfolge = value; }
        }
        // Getter of list all KTeil
        public List<KTeil> ListeKTeile
        {
            get
            {
                List<KTeil> res = new List<KTeil>();
                foreach (KeyValuePair<int, Teil> kvp in listeTeile)
                {
                    if (kvp.Value is KTeil)
                    {
                        res.Add(kvp.Value as KTeil);
                    }
                }
                return res;
            }
        }
        // Getter of list all ETeil
        public List<ETeil> ListeETeile
        {
            get
            {
                List<ETeil> res = new List<ETeil>();
                foreach (KeyValuePair<int, Teil> kvp in listeTeile)
                {
                    if (kvp.Value is ETeil)
                    {
                        res.Add(kvp.Value as ETeil);
                    }
                }
                return res;
            }
        }
        // Getter of all Bestellpositionen
        public List<Bestellposition> Bestellungen
        {
            get { return listeBestellungen; }
        }
        // Path of input file
        public string OpenFile
        {
            get { return openFile; }
            set { openFile = value; }
        }
        // Path of output file
        public string SaveFile
        {
            get { return saveFile; }
            set
            {
                saveFile = value;
                saveFile += @"\\Input.xml";
            }
        }
        // Getter / Setter bool flag sonderproduktion
        public bool Sonderproduktion
        {
            get { return sonderProduktion; }
            set { sonderProduktion = value; }
        }
        // Getter / Setter bool flag ueberstunden
        public bool UeberstundenErlaubt
        {
            get { return ueberstundenErlaubt; }
            set { ueberstundenErlaubt = value; }
        }

                // Getter for Teil with given number
        public Teil GetTeil(int nr)
        {
            if (listeTeile.ContainsKey(nr))
            {
                return listeTeile[nr];
            }
            else
            {
                throw new UnknownTeilException(nr);
            }
        }
        // Setter of Puffer for Teil
        public void SetPuffer(int nr, int wert)
        {
            if (listeTeile.ContainsKey(nr) == false)
            {
                throw new UnknownTeilException(nr);
            }
            if (wert < 0)
            {
                throw new InputException("Puffer darf nicht negativ sein!");
            }
            if (listeTeile[nr].Pufferwert < 0)
            {
                listeTeile[nr].Pufferwert = wert;
            }
            if (listeTeile[nr].Pufferwert != wert)
            {
                listeTeile[nr].Pufferwert = wert;
                pufferGeaendert = true;
            }
        }
        // Getter data table of KTeil
        public DataTable DataTableKTeil
        {
            get
            {
                DataTable table = new DataTable();
                table.Columns.Add(new DataColumn("KTeil"));
                table.Columns.Add(new DataColumn("Anzahl"));
                table.Columns.Add(new DataColumn("Typ"));
                foreach (Bestellposition pos in listeBestellungen)
                {
                    DataRow row = table.NewRow();
                    row["KTeil"] = pos.Kaufteil.Nummer;
                    row["Anzahl"] = pos.Menge;
                    if (pos.Eil)
                    {
                        row["Typ"] = "Eil";
                    }
                    else
                    {
                        row["Typ"] = "Normal";
                    }
                    table.Rows.Add(row);
                }
                return table;
            }
        }
        // Reload of data table Bestellung
        public void ReloadDataTableKTeil(DataTable table)
        {
            listeBestellungen.Clear();
            foreach (DataRow row in table.Rows)
            {
                Teil teil = listeTeile[Convert.ToInt32(row["KTeil"])];
                int menge = Convert.ToInt32(row["Anzahl"]);
                if (row["Typ"].ToString().Equals("Eil"))
                {
                    listeBestellungen.Add(new Bestellposition(teil as KTeil, menge, true));
                }
                else
                {
                    listeBestellungen.Add(new Bestellposition(teil as KTeil, menge, false));
                }
            }
        }
        // Getter for data table of Produktion
        public DataTable DataTableProduktion
        {
            get
            {
                DataTable table = new DataTable();
                table.Columns.Add(new DataColumn("Teil"));
                table.Columns.Add(new DataColumn("Menge"));
                foreach (int pos in listeReihenfolge)
                {
                    DataRow row = table.NewRow();
                    row["Teil"] = pos;
                    row["Menge"] = (listeTeile[pos] as ETeil).ProduktionsMengePer0;
                    table.Rows.Add(row);
                }
                return table;
            }
        }
        // Reload of data table Produktion
        public void ReloadDataTableProduktion(DataTable table)
        {
            listeReihenfolge = new int[table.Rows.Count];
            int count = 0;
            foreach (DataRow row in table.Rows)
            {
                listeReihenfolge[count] = Convert.ToInt32(row[0]);
                count++;
                if ((listeTeile[Convert.ToInt32(row[0])] as ETeil).ProduktionsMengePer0 < Convert.ToInt32(row[1]))
                {
                    pp.Nachpruefen(listeTeile[Convert.ToInt32(row[0])], Convert.ToInt32(row[1]));
                }
                else
                {
                    (listeTeile[Convert.ToInt32(row[0])] as ETeil).ProduktionsMengePer0 = Convert.ToInt32(row[1]);
                }
            }
        }
        // Getter for data table of Arbeitsplatz
        public DataTable DataTableArbeitsPlatz
        {
            get
            {
                DataTable table = new DataTable();
                table.Columns.Add(new DataColumn("Arbeitsplatz"));
                table.Columns.Add(new DataColumn("Schichten"));
                table.Columns.Add(new DataColumn("Minuten"));
                foreach (KeyValuePair<int, Arbeitsplatz> platz in listeArbeitsplaetze)
                {
                    DataRow row = table.NewRow();
                    row["Arbeitsplatz"] = platz.Key;
                    row["Schichten"] = platz.Value.AnzSchichten;
                    row["Minuten"] = platz.Value.UeberMin;
                    table.Rows.Add(row);
                }
                return table;
            }
        }
        // Reload of data table Arbeitsplatz
        public void ReloadDataTableArbeitsPlatz(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                listeArbeitsplaetze[Convert.ToInt32(row[0])].AnzSchichten = Convert.ToInt32(row[1]);
                listeArbeitsplaetze[Convert.ToInt32(row[0])].UeberMin = Convert.ToInt32(row[2]);
            }
        }
        // Add new Bestellposition to bestellungen
        public void AddBestellposition(Bestellposition bpos)
        {
            listeBestellungen.Add(bpos);
        }
        // Add new Teil to bestellungen (as Bestellposition)
        public void AddBestellposition(int kteilNr, int menge, bool eil)
        {
            if (listeTeile.ContainsKey(kteilNr) == false)
            {
                throw new UnknownTeilException(kteilNr);
            }
            if ((listeTeile[kteilNr] is KTeil) == false)
            {
                throw new InputException(string.Format("Teil mit Nr.:{0} ist kein KTeil!", kteilNr));
            }
            listeBestellungen.Add(new Bestellposition(listeTeile[kteilNr] as KTeil, menge, eil));
        }
        // Setter for Preis
        public void SetPreis(Dictionary<int, double> kostenDict)
        {
            foreach (KeyValuePair<int, double> kostKvp in kostenDict)
            {
                if (listeTeile.ContainsKey(kostKvp.Key) == false)
                {
                    throw new UnknownTeilException(kostKvp.Key);
                }
                if ((listeTeile[kostKvp.Key] is KTeil) == false)
                {
                    throw new InputException(string.Format("Teil mit Nr.:{0} ist kein KTeil!", kostKvp.Key));
                }
                (listeTeile[kostKvp.Key] as KTeil).Preis = kostKvp.Value;
            }
        }
        // Add new KTeil to member liste_teile
        public void AddKTeil(KTeil teil)
        {
            if (listeTeile.ContainsKey(teil.Nummer) == false)
            {
                listeTeile[teil.Nummer] = teil;
            }
            else
            {
                throw new InputException(string.Format("Teil mit Nr.:{0} bereits verwendet!", teil.Nummer));
            }
        }
        // Create new KTeil and add this to member liste_teile
        public void NewTeil(int nr, string bez, double p, double bk, double ld, double abw, int dm, int bs, string vw, int TVP1, int TVP2, int TVP3)
        {
            if (listeTeile.ContainsKey(nr) == false)
            {
                KTeil kt = new KTeil(nr, bez);
                kt.Preis = p;
                kt.Bestellkosten = bk;
                kt.Lieferdauer = ld;
                kt.AbweichungLieferdauer = abw;
                kt.DiskontMenge = dm;
                kt.Lagerstand = bs;
                kt.Verwendung = vw;
                kt.VerwendungProdukt1 = TVP1;
                kt.VerwendungProdukt2 = TVP2;
                kt.VerwendungProdukt3 = TVP3;
                listeTeile[nr] = kt;
            }
            else
            {
                throw new InputException(string.Format("Teil mit Nr.:{0} bereits vorhanden!", nr));
            }
        }
        // Add new ETeil to member liste_teile
        public void AddETeil(ETeil teil)
        {
            if (listeTeile.ContainsKey(teil.Nummer) == false)
            {
                listeTeile[teil.Nummer] = teil;
            }
            else
            {
                throw new InputException(string.Format("Teil mit Nr.:{0} bereits verwendet!", teil.Nummer));
            }
        }
        // Create new ETeil and add this to member liste_teile
        public void NewTeil(int nr, string bez, int bs, string vw)
        {
            if (listeTeile.ContainsKey(nr) == false)
            {
                ETeil et = new ETeil(nr, bez);
                et.Lagerstand = bs;
                et.Verwendung = vw;
                listeTeile[nr] = et;
            }
            else
            {
                throw new InputException(string.Format("Teil mit Nr.:{0} bereits vorhanden!", nr));
            }
        }
        // Getter for Arbeitsplatz with given number
        public Arbeitsplatz GetArbeitsplatz(int nr)
        {
            return listeArbeitsplaetze[nr];
        }
        // Getter for member liste_arbeitsplaetze
        public List<Arbeitsplatz> ArbeitsplatzList
        {
            get
            {
                List<Arbeitsplatz> ap_liste = new List<Arbeitsplatz>();
                foreach (KeyValuePair<int, Arbeitsplatz> kvp in listeArbeitsplaetze)
                {
                    ap_liste.Add(kvp.Value);
                }
                return ap_liste;
            }
        }
        // Setter for Arbeitsplatz in member liste_arbeitsplaetze
        public void NeuArbeitsplatz(Arbeitsplatz ap)
        {
            if (listeArbeitsplaetze.ContainsKey(ap.GetNummerArbeitsplatz) == false)
            {
                listeArbeitsplaetze[ap.GetNummerArbeitsplatz] = ap;
            }
            else
            {
                throw new Exception(string.Format("Arbeitsplatz mit der Nr.:{0} bereits vorhanden!", ap.GetNummerArbeitsplatz));
            }
        }
        // Perform optimization
        public void Optimieren()
        {
            pp = new Produktionsplanung();
            pp.Aufloesen();
            pp.Planen();
            Bestellverwaltung bv = new Bestellverwaltung();
            bv.erzeugeBestellListe();
        }
        // Reset of Arbeitsplatz
        public void Reset()
        {
            foreach (KeyValuePair<int, Arbeitsplatz> ap in listeArbeitsplaetze)
            {
                ap.Value.UeberMin = 0;
                ap.Value.AnzSchichten = 1;
            }
            foreach (ETeil et in ListeETeile)
            {
                et.ProduktionsMengePer0 = 0;
                et.VertriebPer0 = 0;
                et.VerbrauchPer1 = 0;
                et.VerbrauchPer2 = 0;
                et.VerbrauchPer3 = 0;
            }
        }
    }
}
