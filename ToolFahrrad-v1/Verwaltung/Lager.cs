using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolFahrrad_v1
{
    public class Lager
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int amount;
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        private int startamount;
        public int Startamount
        {
            get { return startamount; }
            set { startamount = value; }
        }

        private double pct;
        public double Pct
        {
            get { return pct; }
            set { pct = value; }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        private double stockvalue;
        public double Stockvalue
        {
            get { return stockvalue; }
            set { stockvalue = value; }
        }

        private double totalstockvalue;
        public double Totalstockvalue
        {
            get { return totalstockvalue; }
            set { totalstockvalue = value; }
        }

        public Lager()
        {

        }
    }
}
