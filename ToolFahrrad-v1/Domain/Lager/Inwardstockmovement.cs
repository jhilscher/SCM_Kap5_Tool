using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolFahrrad_v1
{
    public class Inwardstockmovement
    {
        private int orderperiod;
        public int Orderperiod
        {
            get { return orderperiod; }
            set { orderperiod = value; }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int mode;
        public int Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        private int article;
        public int Article
        {
            get { return article; }
            set { article = value; }
        }

        private int amount;
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        private int time;
        public int Time
        {
            get { return time; }
            set { time = value; }
        }

        private double materialcosts;
        public double Materialcosts
        {
            get { return materialcosts; }
            set { materialcosts = value; }
        }

        private double ordercosts;
        public double Ordercosts
        {
            get { return ordercosts; }
            set { ordercosts = value; }
        }

        private double entirecosts;
        public double Entirecosts
        {
            get { return entirecosts; }
            set { entirecosts = value; }
        }

        private double piececosts;
        public double Piececosts
        {
            get { return piececosts; }
            set { piececosts = value; }
        }

    }
}
