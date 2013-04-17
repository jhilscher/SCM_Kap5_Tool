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
        private List<Bestellposition> liste_bestellungen;
        private Dictionary<int, Teil> liste_teile;
        private Dictionary<int, Arbeitsplatz> liste_arbeitsplaetze;
        private int[] liste_reihenfolge;
        private bool sonderproduktion = false;
        private bool puffer_geandert = false;
        private bool ueberstunden_erlaubt = true;
        private Produktionsplanung pp;
        private string openFile;
        private string saveFile;
        private int[] arrayAktuelleWoche;
        public int[] ArrayAktuelleWoche
        {
            get { return arrayAktuelleWoche; }
            set { this.arrayAktuelleWoche = value; }
        }
        private int[,] arrayPrognose;
        public int[,] ArrayPrognose
        {
            get { return arrayPrognose; }
            set { arrayPrognose = value; }
        }
        /// // Constructor
        private DataContainer()
        {
            this.liste_bestellungen = new List<Bestellposition>();
            this.liste_teile = new Dictionary<int, Teil>();
            this.liste_arbeitsplaetze = new Dictionary<int, Arbeitsplatz>();
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
            get { return this.liste_reihenfolge; }
            set { this.liste_reihenfolge = value; }
        }
        // Getter of list all KTeil
        public List<KTeil> ListeKTeile
        {
            get
            {
                List<KTeil> res = new List<KTeil>();
                foreach (KeyValuePair<int, Teil> kvp in this.liste_teile)
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
                foreach (KeyValuePair<int, Teil> kvp in this.liste_teile)
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
            get { return this.liste_bestellungen; }
        }
        // Path of input file
        public string OpenFile
        {
            get { return this.openFile; }
            set { this.openFile = value; }
        }
        // Path of output file
        public string SaveFile
        {
            get { return this.saveFile; }
            set
            {
                this.saveFile = value;
                this.saveFile += @"\\Input.xml";
            }
        }
        // Getter / Setter bool flag sonderproduktion
        public bool Sonderproduktion
        {
            get { return this.sonderproduktion; }
            set { this.sonderproduktion = value; }
        }
        // Getter / Setter bool flag ueberstunden
        public bool UeberstundenErlaubt
        {
            get { return this.ueberstunden_erlaubt; }
            set { this.ueberstunden_erlaubt = value; }
        }
        // Getter for Teil with given number
        public Teil GetTeil(int nr)
        {
            if (this.liste_teile.ContainsKey(nr))
            {
                return this.liste_teile[nr];
            }
            else
            {
                throw new UnknownTeilException(nr);
            }
        }
        // Setter of Puffer for Teil
        public void SetPuffer(int nr, int wert)
        {
            if (this.liste_teile.ContainsKey(nr) == false)
            {
                throw new UnknownTeilException(nr);
            }
            if (wert < 0)
            {
                throw new InputException("Puffer darf nicht negativ sein!");
            }
            if (this.liste_teile[nr].Pufferwert < 0)
            {
                this.liste_teile[nr].Pufferwert = wert;
            }
            if (this.liste_teile[nr].Pufferwert != wert)
            {
                this.liste_teile[nr].Pufferwert = wert;
                this.puffer_geandert = true;
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
                foreach (Bestellposition pos in this.liste_bestellungen)
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
            this.liste_bestellungen.Clear();
            foreach (DataRow row in table.Rows)
            {
                Teil teil = this.liste_teile[Convert.ToInt32(row["KTeil"])];
                int menge = Convert.ToInt32(row["Anzahl"]);
                if (row["Typ"].ToString().Equals("Eil"))
                {
                    this.liste_bestellungen.Add(new Bestellposition(teil as KTeil, menge, true));
                }
                else
                {
                    this.liste_bestellungen.Add(new Bestellposition(teil as KTeil, menge, false));
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
                foreach (int pos in this.liste_reihenfolge)
                {
                    DataRow row = table.NewRow();
                    row["Teil"] = pos;
                    row["Menge"] = (this.liste_teile[pos] as ETeil).Produktionsmenge;
                    table.Rows.Add(row);
                }
                return table;
            }
        }
        // Reload of data table Produktion
        public void ReloadDataTableProduktion(DataTable table)
        {
            this.liste_reihenfolge = new int[table.Rows.Count];
            int count = 0;
            foreach (DataRow row in table.Rows)
            {
                this.liste_reihenfolge[count] = Convert.ToInt32(row[0]);
                count++;
                if ((this.liste_teile[Convert.ToInt32(row[0])] as ETeil).Produktionsmenge < Convert.ToInt32(row[1]))
                {
                    pp.Nachpruefen(this.liste_teile[Convert.ToInt32(row[0])], Convert.ToInt32(row[1]));
                }
                else
                {
                    (this.liste_teile[Convert.ToInt32(row[0])] as ETeil).Produktionsmenge = Convert.ToInt32(row[1]);
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
                foreach (KeyValuePair<int, Arbeitsplatz> platz in this.liste_arbeitsplaetze)
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
                this.liste_arbeitsplaetze[Convert.ToInt32(row[0])].AnzSchichten = Convert.ToInt32(row[1]);
                this.liste_arbeitsplaetze[Convert.ToInt32(row[0])].UeberMin = Convert.ToInt32(row[2]);
            }
        }
        // Add new Bestellposition to bestellungen
        public void AddBestellposition(Bestellposition bpos)
        {
            this.liste_bestellungen.Add(bpos);
        }
        // Add new Teil to bestellungen (as Bestellposition)
        public void AddBestellposition(int kteilNr, int menge, bool eil)
        {
            if (this.liste_teile.ContainsKey(kteilNr) == false)
            {
                throw new UnknownTeilException(kteilNr);
            }
            if ((this.liste_teile[kteilNr] is KTeil) == false)
            {
                throw new InputException(string.Format("Teil mit Nr.:{0} ist kein KTeil!", kteilNr));
            }
            this.liste_bestellungen.Add(new Bestellposition(this.liste_teile[kteilNr] as KTeil, menge, eil));
        }
        // Setter for Preis
        public void SetPreis(Dictionary<int, double> kostenDict)
        {
            foreach (KeyValuePair<int, double> kostKvp in kostenDict)
            {
                if (this.liste_teile.ContainsKey(kostKvp.Key) == false)
                {
                    throw new UnknownTeilException(kostKvp.Key);
                }
                if ((this.liste_teile[kostKvp.Key] is KTeil) == false)
                {
                    throw new InputException(string.Format("Teil mit Nr.:{0} ist kein KTeil!", kostKvp.Key));
                }
                (this.liste_teile[kostKvp.Key] as KTeil).Preis = kostKvp.Value;
            }
        }
        // Add new KTeil to member liste_teile
        public void AddKTeil(KTeil teil)
        {
            if (this.liste_teile.ContainsKey(teil.Nummer) == false)
            {
                this.liste_teile[teil.Nummer] = teil;
            }
            else
            {
                throw new InputException(string.Format("Teil mit Nr.:{0} bereits verwendet!", teil.Nummer));
            }
        }
        // Create new KTeil and add this to member liste_teile
        public void NewTeil(int nr, string bez, double p, double bk, double ld, double abw, int dm, int bs, string vw)
        {
            if (this.liste_teile.ContainsKey(nr) == false)
            {
                KTeil kt = new KTeil(nr, bez);
                kt.Preis = p;
                kt.Bestellkosten = bk;
                kt.Lieferdauer = ld;
                kt.Abweichung_lieferdauer = abw;
                kt.Diskontmenge = dm;
                kt.Lagerstand = bs;
                kt.Verwendung = vw;
                this.liste_teile[nr] = kt;
            }
            else
            {
                throw new InputException(string.Format("Teil mit Nr.:{0} bereits vorhanden!", nr));
            }
        }
        // Add new ETeil to member liste_teile
        public void AddETeil(ETeil teil)
        {
            if (this.liste_teile.ContainsKey(teil.Nummer) == false)
            {
                this.liste_teile[teil.Nummer] = teil;
            }
            else
            {
                throw new InputException(string.Format("Teil mit Nr.:{0} bereits verwendet!", teil.Nummer));
            }
        }
        // Create new ETeil and add this to member liste_teile
        public void NewTeil(int nr, string bez, int bs, string vw)
        {
            if (this.liste_teile.ContainsKey(nr) == false)
            {
                ETeil et = new ETeil(nr, bez);
                et.Lagerstand = bs;
                et.Verwendung = vw;
                this.liste_teile[nr] = et;
            }
            else
            {
                throw new InputException(string.Format("Teil mit Nr.:{0} bereits vorhanden!", nr));
            }
        }
        // Getter for Arbeitsplatz with given number
        public Arbeitsplatz GetArbeitsplatz(int nr)
        {
            return this.liste_arbeitsplaetze[nr];
        }
        // Getter for member liste_arbeitsplaetze
        public List<Arbeitsplatz> ArbeitsplatzList
        {
            get
            {
                List<Arbeitsplatz> ap_liste = new List<Arbeitsplatz>();
                foreach (KeyValuePair<int, Arbeitsplatz> kvp in this.liste_arbeitsplaetze)
                {
                    ap_liste.Add(kvp.Value);
                }
                return ap_liste;
            }
        }
        // Setter for Arbeitsplatz in member liste_arbeitsplaetze
        public void NeuArbeitsplatz(Arbeitsplatz ap)
        {
            if (this.liste_arbeitsplaetze.ContainsKey(ap.GetNummerArbeitsplatz) == false)
            {
                this.liste_arbeitsplaetze[ap.GetNummerArbeitsplatz] = ap;
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
            bv.erzeugen_liste_bestellungen();
        }
        // Reset of Arbeitsplatz
        public void Reset()
        {
            foreach (KeyValuePair<int, Arbeitsplatz> ap in this.liste_arbeitsplaetze)
            {
                ap.Value.UeberMin = 0;
                ap.Value.AnzSchichten = 1;
            }
            foreach (ETeil et in this.ListeETeile)
            {
                et.Produktionsmenge = 0;
                et.VerbrauchAktuell = 0;
                et.VerbrauchPrognose1 = 0;
                et.VerbrauchPrognose2 = 0;
            }
        }
    }
}
