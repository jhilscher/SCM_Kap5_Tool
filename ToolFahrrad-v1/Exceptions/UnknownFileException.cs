using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolFahrrad_v1
{
    class UnknownFileException : Exception
    {
        private string message;

        public UnknownFileException(string msg)
        {
            this.message = msg;
        }

        public override string Message
        {
            get
            {
                return this.message;
            }
        }

    }
}
