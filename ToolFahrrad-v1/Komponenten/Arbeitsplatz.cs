using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFahrrad_v1
{
    class Arbeitsplatz
    {
        // Private Felder
        protected int nummer;
        protected int anzSchichten = 1;
        public int Schichten
        {
            get { return this.anzSchichten; }
            set { this.anzSchichten = value; }
        }
        protected int anzUeberMin = 0;
        public int UeberMin
        {
            get { return this.anzUeberMin; }
            set { this.anzUeberMin = value; }
        }
        int warteschlangenZeit = 0;
        // Benoetigte Zeit zur Hersetllung von Teil mit der Key nummer
        private Dictionary<int, int> werkZeit;
        public Dictionary<int, int> WerkZeitJeStk
        {
            get { return this.werkZeit; }
        }
        protected Dictionary<int, int> ruestzeit;
        private int anzRuestung = 0;
        public int AnzRuestung
        {
            set { this.anzRuestung = value; }
        }

        private Dictionary<int, int> naechsterSchritt = new Dictionary<int, int>();// als Dictionary umschreiben


        public Arbeitsplatz(int nr)
        {
            this.nummer = nr;
            this.ruestzeit = new Dictionary<int, int>();
            this.werkZeit = new Dictionary<int, int>();
        }

        public void AddRuestzeit(int teil, int zeit)
        {
            if (zeit < 0)
            {
                throw new InvalidValueException(zeit.ToString(), "Ruestzeit am Arbeitsplatz " + this.nummer);
            }
            if ((this.ruestzeit.ContainsKey(teil) && this.ruestzeit[teil] == 0) || !this.ruestzeit.ContainsKey(teil))
            {
                this.ruestzeit[teil] = zeit;
                if (!this.werkZeit.ContainsKey(teil))
                {
                    this.werkZeit[teil] = 0;
                }
            }
            if (!this.ruestzeit.ContainsKey(teil) && this.ruestzeit[teil] != 0)
            {
                throw new InvalidValueException(string.Format("Am Arbeitsplatz {0} ist bereits eine Rüstzeit für das Teil {1} hinterlegt", this.nummer, teil));
            }
        }

        /// <summary>
        /// Zur verfuegung stehende zeit je Woche
        /// </summary>
        /// <value>The zu verfuegung stehende zeit.</value>
        public int ZuVerfuegungStehendeZeit
        {
            get
            {
                return this.anzSchichten * 2400 + anzUeberMin * 5;
            }
        }

        public int BenoetigteZeit
        {
            get
            {
                DataContainer data = DataContainer.Instance;
                int res = 0;
                foreach (KeyValuePair<int, int> kvp in this.werkZeit)
                {
                    res += kvp.Value * (data.GetTeil(kvp.Key) as ETeil).Produktionsmenge;
                }
                res += this.warteschlangenZeit;
                return res;
            }
        }

        public double Ruestzeit
        {
            get
            {
                double sum = 0;
                foreach (KeyValuePair<int, int> kvp in this.ruestzeit)
                {
                    sum += kvp.Value;
                }
                return (sum / this.ruestzeit.Count) * this.anzRuestung;
            }
        }

        /// <summary>
        /// Fügt die Ueberstunden in minuten pro Tag ein
        /// falls es eine neue Schicht eröffnet werden muss wird das gemacht falls max Kapa erreicht wird false zurückgegeben
        /// </summary>
        /// <param name="min">Minuten pro Tag.</param>
        /// <returns></returns>
        public bool AddUeberMinute(int min)
        {
            if (this.anzUeberMin + min < 240)
            {
                this.anzUeberMin += min;
                return true;
            }
            else
            {
                if (this.AddnewSchicht())
                {
                    this.anzUeberMin = 0;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// fügt eine neue schicht hinzu gibt false zurück falls es nicht mehr möglich ist
        /// </summary>
        /// <returns></returns>
        public bool AddnewSchicht()
        {
            if (this.anzSchichten < 2)
            {
                this.anzSchichten++;
                return true;
            }
            return false;
        }

        public int Nummer
        {
            get
            {
                return this.nummer;
            }
        }


        /// <summary>
        /// gibt die Liste aller Teile die an diesem Arbeitsplatz hergestellt werden
        /// </summary>
        /// <returns></returns>
        public List<ETeil> HergestelteTeile
        {
            get
            {
                List<ETeil> list = new List<ETeil>();
                DataContainer cont = DataContainer.Instance;
                foreach (KeyValuePair<int, int> res in this.werkZeit)
                {
                    list.Add(cont.GetTeil(res.Key) as ETeil);
                }
                return list;

            }
        }

        public void AddWerkzeit(int teil, int zeit)
        {
            if (zeit < 0)
            {
                throw new InvalidValueException(zeit.ToString(), "Werkzeit am Arbeitsplatz " + this.nummer);
            }
            if (!this.naechsterSchritt.ContainsKey(teil))
            {
                this.naechsterSchritt[teil] = -1;
            }

            if ((this.werkZeit.ContainsKey(teil) && this.werkZeit[teil] == 0) || !this.werkZeit.ContainsKey(teil))
            {
                this.werkZeit[teil] = zeit;
                if (!this.ruestzeit.ContainsKey(teil))
                {
                    this.ruestzeit[teil] = 0;
                }

                if (!(DataContainer.Instance.GetTeil(teil) as ETeil).BenutzteArbeitsplaetze.Contains(this))
                {
                    (DataContainer.Instance.GetTeil(teil) as ETeil).AddArbeitsplatz(this.nummer);
                }
            }
            if (!this.werkZeit.ContainsKey(teil) && this.werkZeit[teil] != 0 && this.werkZeit[teil] != zeit)
            {
                throw new InvalidValueException(string.Format("Am Arbeitsplatz {0} ist bereits eine Werkzeit für das Teil {1} hinterlegt", this.nummer, teil));
            }
        }
    }
}
