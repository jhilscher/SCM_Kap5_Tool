using System;
using System.Windows.Forms;
using ToolFahrrad_v1.Design;
using ToolFahrrad_v1.XML;

namespace ToolFahrrad_v1
{
    static class StartTool
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var init = new Initialisierung();
            init.Initialisieren();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Fahrrad());
        }
    }
}
