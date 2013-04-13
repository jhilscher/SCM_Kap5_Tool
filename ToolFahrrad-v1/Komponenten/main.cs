using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ToolFahrrad_v1
{
    class Laden
    {
        [STAThread]
        public static void Main()
        {
            InitialisierungClass init = new InitialisierungClass();
            init.Initialisieren();
            Application.EnableVisualStyles();
            Application.Run(new scsim());
        }
    }
}