using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolFahrrad_v1.Design.Partial
{
    public class FormField
    {
        public String value;
        public String name;

        public FormField(String nValue, String nName)
        {
            this.value = nValue;
            this.name = nName;
        }
    }
}
