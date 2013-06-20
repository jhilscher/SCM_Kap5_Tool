using System;

namespace ToolFahrrad_v1.Exceptions
{
    class UnknownFileException : Exception
    {
        private readonly string message;

        public UnknownFileException(string msg)
        {
            message = msg;
        }

        public override string Message
        {
            get
            {
                return message;
            }
        }

    }
}
