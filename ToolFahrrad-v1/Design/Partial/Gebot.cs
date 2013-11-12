using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolFahrrad_v1.Design.Partial
{
    ///
    /// Abgeleitete Klasse von Angebot: Gebot
    ///
    public class Gebot : Angebot
    {
        public String seller;

        public Gebot(String nArticle, String nQuantity, String nPrice, String nSeller)
            : base(nArticle, nQuantity, nPrice)
        {
            this.seller = nSeller;
        }

        public override String ToString()
        {
            return String.Format("VerkÃ¤ufer {0} verkauft ", this.seller) + "" + base.ToString();
        }
    }
}