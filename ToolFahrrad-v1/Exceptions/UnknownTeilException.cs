using System;
using System.Windows.Forms;
using ToolFahrrad_v1.Properties;

namespace ToolFahrrad_v1.Exceptions
{
    /** Exception is thrown when Teil could not be recognized */
    class UnknownTeilException : Exception
    {
        // Class member
        // Constructor
        public UnknownTeilException(int nummer_param)
        {
            Nummer = nummer_param;

            MessageBox.Show("Kaufteil " + nummer_param + " wurde nicht gefunden", Resources.Fahrrad_XmlOeffnen_Fehlermeldung,
                                           MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }
        // Getter / Setter
        public int Nummer { get; set; }
    }
}
