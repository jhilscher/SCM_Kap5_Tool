using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolFahrrad_v1.Design.Partial
{
    ///
    /// Abgeleitete Klasse von Angebot: Gesuch
    ///
    public class Gesuch : Angebot
    {
        public String requestor;

        public Gesuch(String nArticle, String nQuantity, String nPrice, String nRequestor)
            : base(nArticle, nQuantity, nPrice)
        {
            this.requestor = nRequestor;
        }

        public override String ToString()
        {
            return String.Format("Kunde {0} sucht ", this.requestor) + "" + base.ToString();
        }

    }

}