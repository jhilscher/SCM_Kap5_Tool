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
        public static void Get_Market_Place()
        {
            String url = "http://www.iwi.hs-karlsruhe.de/scs/";
            try
            {
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
                Authenticate("kap5", "sperling", url + "market/", container);
                String html = GetUrl(true, url, "market/marketinfo.jsp", container);
                String[] parsed = RemoveUnusedCrap(html);

                ///
                /// parsing 
                ///
                List<Gebot> offers = ParseOffer(parsed[1]);
                List<Gesuch> requests = ParseRequest(parsed[2]);
                List<Gebot> ownOffers = ParseOffer(parsed[3]);
                List<Gesuch> ownRequests = ParseRequest(parsed[4]);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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

            var response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            String body = reader.ReadToEnd();

            return body;
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

            authRequest.GetResponse();
            return container;
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

            /// 
            /// ersetzt den restlichen html-code mit nichts
            ///
            Regex reg = new Regex("(\\<)(.*?)(\\>)\\s*");
            xml = reg.Replace(xml, "");

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
                String[] singleLine = line.Split(new Char[] { '|' });
                if (singleLine.Length > 1)
                {
                    ret.Add(new Gebot(singleLine[1], singleLine[2], singleLine[3], singleLine[0]));
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
                String[] singleLine = line.Split(new Char[] { '|' });
                if (singleLine.Length > 1)
                {
                    ret.Add(new Gesuch(singleLine[1], singleLine[2], singleLine[3], singleLine[0]));
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
