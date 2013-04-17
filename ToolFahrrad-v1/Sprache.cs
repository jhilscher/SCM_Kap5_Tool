using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace ToolFahrrad_v1
{
    public partial class Sprache : Form
    {
        private string checkSprache;

        public string GetCheck
        {
            get { return checkSprache; }
        }


        public Sprache()
        {
            InitializeComponent();
        }

        private void spracheOK_Click(object sender, EventArgs e)
        {
            if (en.Checked == true)
                checkSprache = "EN";
            else
                checkSprache = "DE";
            this.Close();
        }
    }
}
