using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using ToolFahrrad_v1.Design.Partial;

namespace ToolFahrrad_v1.Design
{
    /// <summary>
    /// Christians partial.
    /// </summary>
    partial class Fahrrad
    {

        public static Boolean authenticated = false;
        public static MatchCollection formdata;

        public void Get_Market_Place(Credentials credentials)
        {
            lbl_in_progress.Visible = true;
            String url = "http://www.iwi.hs-karlsruhe.de/scs/";
            try
            {
                ///
                /// dirty hack um zu vermeiden, dass die Methode 2mal aufgerufen wird.
                ///
                if (this.MarketPlaceGrid.Rows.Count > 1)
                {
                    //return;
                }
                ///
                /// ist ein bisschen kompliziert, die Marketplace-Seite anzeigen zu
                /// lassen ohne 500 zu bekommen. Der Weg der funktioniert ist:
                /// start -> market/market_set.html -> authenticate:market/ -> market/marketinfo.jsp
                ///
                CookieContainer container = new CookieContainer();
                GetUrl(false, url, "start", container);
                GetUrl(false, url, "market/market_set.html", container);

                ///
                /// 
                ///
                Authenticate(credentials.username, credentials.password, url + "market/", container);
                String html = GetUrl(true, url, "market/marketinfo.jsp", container);
                String[] parsed = RemoveUnusedCrap(html);

                ///
                /// parsing 
                ///
                List<Gebot> offers = ParseOffer(parsed[1]);
                List<Gesuch> requests = ParseRequest(parsed[2]);
                List<Gebot> ownOffers = ParseOffer(parsed[3]);
                List<Gesuch> ownRequests = ParseRequest(parsed[4]);
                int lengthMarket = this.MarketPlaceGrid.Rows.Count - 1;
                for (int i = 0; i < lengthMarket; i++)
                {
                    this.MarketPlaceGrid.Rows.RemoveAt(0);
                }
                int lengthGesuche = this.dta_Gesuche.Rows.Count - 1;
                for (int i = 0; i < lengthGesuche; i++)
                {
                    this.dta_Gesuche.Rows.RemoveAt(0);
                }
                int lengthEAngebote = this.dta_e_Angebote.Rows.Count - 1;
                for (int i = 0; i < lengthEAngebote; i++)
                {
                    this.dta_e_Angebote.Rows.RemoveAt(0);
                }
                int lengthEGesuche = this.dta_e_Gesuche.Rows.Count - 1;
                for (int i = 0; i < lengthEGesuche; i++)
                {
                    this.dta_e_Gesuche.Rows.RemoveAt(0);
                }
                
                foreach (Gebot offer in offers)
                    this.MarketPlaceGrid.Rows.Add();

                foreach (Gesuch req in requests)
                    this.dta_Gesuche.Rows.Add();

                foreach (Gebot o_req in ownOffers)
                    this.dta_e_Angebote.Rows.Add();

                foreach (Gesuch o_ges in ownRequests)
                    this.dta_e_Gesuche.Rows.Add();

                for (int i = 0; i < offers.Count; i++)
                {
                    this.MarketPlaceGrid.Rows[i].Cells[0].Value = offers[i].seller;
                    this.MarketPlaceGrid.Rows[i].Cells[1].Value = offers[i].article;
                    this.MarketPlaceGrid.Rows[i].Cells[2].Value = offers[i].quantity;
                    this.MarketPlaceGrid.Rows[i].Cells[3].Value = offers[i].price;
                    this.MarketPlaceGrid.Rows[i].Cells[5].Value = offers[i].formfields;
                }

                for (int i = 0; i < requests.Count; i++)
                {
                    this.dta_Gesuche.Rows[i].Cells[0].Value = requests[i].requestor;
                    this.dta_Gesuche.Rows[i].Cells[1].Value = requests[i].article;
                    this.dta_Gesuche.Rows[i].Cells[2].Value = requests[i].quantity;
                    this.dta_Gesuche.Rows[i].Cells[3].Value = requests[i].price;
                    this.dta_Gesuche.Rows[i].Cells[5].Value = requests[i].formfields;
                }

                for (int i = 0; i < ownOffers.Count; i++)
                {
                    this.dta_e_Angebote.Rows[i].Cells[0].Value = ownOffers[i].seller;
                    this.dta_e_Angebote.Rows[i].Cells[1].Value = ownOffers[i].article;
                    this.dta_e_Angebote.Rows[i].Cells[2].Value = ownOffers[i].quantity;
                    this.dta_e_Angebote.Rows[i].Cells[3].Value = ownOffers[i].price;
                    this.dta_e_Angebote.Rows[i].Cells[5].Value = ownOffers[i].formfields;
                }

                for (int i = 0; i < ownRequests.Count; i++)
                {
                    this.dta_e_Gesuche.Rows[i].Cells[0].Value = ownRequests[i].requestor;
                    this.dta_e_Gesuche.Rows[i].Cells[1].Value = ownRequests[i].article;
                    this.dta_e_Gesuche.Rows[i].Cells[2].Value = ownRequests[i].quantity;
                    this.dta_e_Gesuche.Rows[i].Cells[3].Value = ownRequests[i].price;
                    this.dta_e_Gesuche.Rows[i].Cells[5].Value = ownRequests[i].formfields;
                }

                GetUrl(false, url, "logout", container);
                lbl_in_progress.Visible = false;

                panel_password.Visible = false;
                panel_password2.Visible = false;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("oops, da ging wohl was schief: " + e.Message);
                lbl_in_progress.Visible = false;
            }


        }

        ///
        /// <param>Boolean ok</param>not needed anymore... 
        /// <param>String url</param>url to connect to
        /// <param>String path</param>path to file
        /// <param>CookieContainer container</param>to place sessioncookies
        /// <summary>Gets html code of web-page</summary>
        ///
        public static String GetUrl(Boolean ok, String url, String path, CookieContainer container)
        {
            url += path;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = container;
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                var response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                String body = reader.ReadToEnd();
                response.Close();
                return body;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                return "";
            }

        }

        ///
        /// <param>String username</param> username for login
        /// <param>String password</param> password for login
        /// <param>String url</param> url to connect to
        /// <param>CookieContainer container</param> to place sessioncookies
        /// <summary>authenticates the j_security_check and keeps session</summary>
        ///
        public static CookieContainer Authenticate(String username, String password, String url, CookieContainer container)
        {
            url += String.Format("j_security_check?j_username={0}&j_password={1}",
                    username, password);
            HttpWebRequest authRequest = (HttpWebRequest)WebRequest.Create(url);
            authRequest.CookieContainer = container;
            authRequest.Method = WebRequestMethods.Http.Post;
            authRequest.ContentType = "application/x-www-form-urlencoded";
            authRequest.Timeout = 360000;

            WebResponse response = authRequest.GetResponse();
            response.Close();
            return container;
        }

        /// <summary>
        /// sends post to /scs/market
        /// </summary>
        /// <param name="formfields">List with all params for the post data</param>
        /// <returns></returns>
        public static CookieContainer DoPost(List<FormField> formfields, Credentials credentials)
        {
            String url = "http://www.iwi.hs-karlsruhe.de/scs/";
            String postPath = "market";
            String param = "?";
            foreach (FormField field in formfields)
            {
                param += String.Format("{0}={1}&", field.name, field.value);
            }
            param = param.Remove(param.Length - 1);

            ///
            /// ist ein bisschen kompliziert, die Marketplace-Seite anzeigen zu
            /// lassen ohne 500 zu bekommen. Der Weg der funktioniert ist:
            /// start -> market/market_set.html -> authenticate:market/ -> market/marketinfo.jsp
            ///
            CookieContainer container = new CookieContainer();
            GetUrl(false, url, "start", container);
            GetUrl(false, url, "market/market_set.html", container);

            ///
            /// 
            ///
            Authenticate(credentials.username, credentials.password, url + "market/", container);

            String postUrl = url + postPath + param;

            HttpWebRequest authRequest = (HttpWebRequest)WebRequest.Create(postUrl);
            authRequest.CookieContainer = container;
            authRequest.Method = WebRequestMethods.Http.Post;
            authRequest.ContentType = "application/x-www-form-urlencoded";
            authRequest.Timeout = 360000;

            WebResponse response = authRequest.GetResponse();
            response.Close();
            
            return new CookieContainer();
        }

        ///
        /// <param>String html</param>
        /// <summary>removes unuseful crap html code and keeps the important data</summary>
        ///
        public static String[] RemoveUnusedCrap(String html)
        {
            List<String> plain = new List<String>();

            ///
            /// html zeilenweise splitten
            ///
            String[] splitted = html.Split(new Char[] { '\n' });

            ///
            /// true, if code is between <table>... </table>
            ///
            Boolean inTable = false;
            ///
            /// für jede zeile tue..
            ///
            for (int i = 0; i < splitted.Length; i++)
            {
                ///
                /// falls Zeile ohne whitespaces nicht mit mit < oder \<table width > beginnt
                /// und überspringe Schleifendurchgang
                ///
                if (!splitted[i].Trim().StartsWith("<") || splitted[i].Trim().StartsWith("<table width"))
                {
                    continue;
                }

                ///
                /// falls Zeile nur mit <table > beginnt, setze inTable auf true
                ///
                if (splitted[i].Trim().StartsWith("<table"))
                {
                    inTable = true;
                }

                ///
                /// falls inTable dann
                ///
                if (inTable)
                {
                    ///
                    /// prüfe, ob Ende der Tabelle erreicht ist, falls ja setze inTable auf false
                    ///
                    if (splitted[i].Trim().StartsWith("</table"))
                    {
                        inTable = false;
                    }

                    ///
                    /// füge Zeile zu plain hinzu, und entferne crappy fucking unnötigen html-code
                    ///
                    plain.Add(splitted[i].Trim().Replace("<tr>", "")
                            .Replace("</tr>", "\n")
                            .Replace("</td>", "|")
                            .Replace("<td align=\"center\">", "")
                            .Replace("</th>", ""));
                }

            }

            ///
            /// gehe plain durch, und füge jede zeile, die nicht nur aus \n oder "" besteht
            ///
            String xml = "";
            for (int i = 0; i < plain.Count; i++)
            {
                if (!plain[i].Equals("\n"))
                {
                    if (!plain[i].Equals(""))
                    {
                        xml += plain[i];
                    }
                }
            }

            //Regex reg_form = new Regex(@"value=""(\w*)"" name=""(\w*)""");
            //formdata = reg_form.Matches(xml);


            /// 
            /// ersetzt den restlichen html-code mit nichts
            ///
            //Regex reg = new Regex("(\\<)(.*?)(\\>)\\s*");
            //xml = reg.Replace(xml, "");

            String[] data = xml.Split(new Char[] { ';' });
            return data;
        }

        ///
        /// <param>String str</param> dieses format: seller|article|quantity|price|andererscheiß\n
        /// 					     seller|....
        /// <summary>parst Gebote</summary>
        ///
        public static List<Gebot> ParseOffer(String str)
        {
            List<Gebot> ret = new List<Gebot>();
            ///
            /// zeilenweise splitten
            ///
            String[] splitted = str.Split(new Char[] { '\n' });

            foreach (String line in splitted)
            {
                Regex reg_form = new Regex(@"value=""(\w*)"" name=""(\w*)""");
                formdata = reg_form.Matches(line);
                List<FormField> fields = new List<FormField>();
                for (int i = 0; i < formdata.Count; i++)
                {
                    fields.Add(new FormField(formdata[i].Groups[1].ToString(), formdata[i].Groups[2].ToString()));
                }
                //System.Windows.Forms.MessageBox.Show(formdata[0].Groups[0].ToString() + " " + /* value */formdata[0].Groups[1].ToString() + " " + /*name*/formdata[0].Groups[2].ToString());;
                Regex reg = new Regex("(\\<)(.*?)(\\>)\\s*");
                String nline = reg.Replace(line, "");
                String[] singleLine = nline.Split(new Char[] { '|' });
                if (singleLine.Length > 1)
                {
                    ret.Add(new Gebot(singleLine[1], singleLine[2], singleLine[3], singleLine[0], fields));
                }
            }

            foreach (Gebot ges in ret)
            {
                Console.WriteLine(ges);
            }
            return ret;
        }

        public static List<Gesuch> ParseRequest(String str)
        {
            List<Gesuch> ret = new List<Gesuch>();
            String[] splitted = str.Split(new Char[] { '\n' });

            foreach (String line in splitted)
            {
                Regex reg_form = new Regex(@"value=""(\w*)"" name=""(\w*)""");
                formdata = reg_form.Matches(line);
                List<FormField> fields = new List<FormField>();
                for (int i = 0; i < formdata.Count; i++)
                {
                    fields.Add(new FormField(formdata[i].Groups[1].ToString(), formdata[i].Groups[2].ToString()));
                }
                Regex reg = new Regex("(\\<)(.*?)(\\>)\\s*");
                String nline = reg.Replace(line, "");
                String[] singleLine = nline.Split(new Char[] { '|' });
                if (singleLine.Length > 1)
                {
                    ret.Add(new Gesuch(singleLine[1], singleLine[2], singleLine[3], singleLine[0], fields));
                }
            }

            foreach (Gesuch ges in ret)
            {
                Console.WriteLine(ges);
            }
            return ret;
        }
    }
}
