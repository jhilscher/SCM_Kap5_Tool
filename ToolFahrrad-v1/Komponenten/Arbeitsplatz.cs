using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolFahrrad_v1.Komponenten
{
    class Arbeitsplatz
    {
        protected int nummer;
        protected int anzSchichten = 1;
        protected int anzUeberMin = 0;
        int warteschlangenZeit = 0;
        /// <summary>
        /// benötigte Zeit zur hersetllung von  teil mit der Key nummer
        /// </summary>
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
