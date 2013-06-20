using System;

namespace ToolFahrrad_v1.Exceptions
{
    /** Exception is thrown when input is undefined */
    class InputException : Exception
    {
        // Class members
        private readonly string message;
        // Constructor
        public InputException(string msg)
        {
            message = msg;
        }
        // Function
        public override string Message
        {
            get { return message; }
        }
    }
}
