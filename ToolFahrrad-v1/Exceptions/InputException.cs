using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolFahrrad_v1
{
    /** Exception is thrown when input is undefined */
    class InputException : Exception
    {
        // Class members
        private string message;
        // Constructor
        public InputException(string msg)
        {
            this.message = msg;
        }
        // Function
        public override string Message
        {
            get { return this.message; }
        }
    }
}
