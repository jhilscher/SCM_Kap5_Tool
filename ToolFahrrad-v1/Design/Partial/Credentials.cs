using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ToolFahrrad_v1.Design.Partial
{
    [Serializable()]
    public class Credentials
    {
        public String username;
        public String password;
        public String path;

        public Credentials(String nUsername, String nPassword)
        {
            this.username = nUsername;
            this.password = nPassword;
           
        }
        public Credentials()
        { }
    }
}
