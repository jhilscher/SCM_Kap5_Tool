using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolFahrrad_v1.Design.Partial
{
    ///
    /// Oberklasse für Angebote auf dem Marktplatz
    ///
    public class Angebot
    {
        public String article;
        public String quantity;
        public String price;

        public Angebot(String nArticle, String nQuantity, String nPrice)
        {
            this.article = nArticle;
            this.quantity = nQuantity;
            this.price = nPrice;
        }

        public override String ToString()
        {
            return String.Format("Artikel: {0}, mit der Menge {1} und dem Preis {2}!", this.article, this.quantity, this.price);
        }
    }
}
