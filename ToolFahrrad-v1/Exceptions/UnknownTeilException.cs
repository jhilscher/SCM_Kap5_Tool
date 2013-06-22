using System;
using System.Windows.Forms;
using ToolFahrrad_v1.Properties;

namespace ToolFahrrad_v1.Exceptions
{
    /** Exception is thrown when Teil could not be recognized */
    class UnknownTeilException : Exception
    {
        private string message;
        public UnknownTeilException(int nummerParam){

            message = string.Format("{0} wurde im System nicht gefunden", nummerParam);

        }

        public override string Message {
            get {
                return message;
            }
        }
    }
}
