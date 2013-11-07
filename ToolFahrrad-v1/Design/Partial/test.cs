using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class WebsiteChecker 
{
	public static void Main()
	{
		String url = "http://www.iwi.hs-karlsruhe.de/scs/";
		try 
		{

			CookieContainer container = new CookieContainer();
			GetUrl(false, url, "start", container);
			GetUrl(false, url, "market/market_set.html", container);
			Authenticate(url + "market/", container);
			String html = GetUrl(true, url, "market/marketinfo.jsp", container);
			String[] parsed = RemoveUnusedCrap(html);
			
			List<Gebot> offers = ParseOffer(parsed[1]);	
			List<Gesuch> requests = ParseRequest(parsed[2]);
			List<Gebot> ownOffers = ParseOffer(parsed[2]);
			List<Gesuch> ownRequests = ParseRequest(parsed[4]);

		}
		catch (Exception e) 
		{
			Console.WriteLine(e);
		}
	}

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

	public static CookieContainer Authenticate(String url, CookieContainer container) 
	{
		String username = "kap5";
		String password = "sperling";
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

	public static String[] RemoveUnusedCrap(String html) 
	{
		List<String> plain = new List<String>();
		String[] splitted = html.Split(new Char[] { '\n' });
		Boolean inTable = false;
		for ( int i = 0; i < splitted.Length; i++) 
		{
			if (!splitted[i].Trim().StartsWith("<") || splitted[i].Trim().StartsWith("<table width"))
			{
				continue;
			}
			if (splitted[i].Trim().StartsWith("<table")) 
			{
				inTable = true;
			}
			if (inTable) 
			{
				if (splitted[i].Trim().StartsWith("</table")) 
				{
					inTable = false;
				}
				plain.Add(splitted[i].Trim().Replace("<tr>", "").Replace("</tr>", "\n").Replace("</td>", "|").Replace("<td align=\"center\">", "").Replace("</th>", ""));
			}
			
		}
		String xml = "";
		for(int i = 0; i < plain.Count; i++) 
		{
			if (!plain[i].Equals("\n")) 
			{
				if ( !plain[i].Equals(""))
				{
					xml += plain[i];
				}
			}
		}

		Regex reg = new Regex("(\\<)(.*?)(\\>)\\s*");
		xml = reg.Replace(xml, "");
		
		String[] data = xml.Split(new Char[] {';'});
		return data;
	}

	public static List<Gebot> ParseOffer(String str)
	{
		List<Gebot> ret = new List<Gebot>();
		String[] splitted = str.Split(new Char[] { '\n' });
		
		foreach(String line in splitted)
		{
			String[] singleLine = line.Split(new Char[] { '|' });
			if (singleLine.Length > 1)
			{
				ret.Add(new Gebot(singleLine[1], singleLine[2], singleLine[3], singleLine[0]));
			}
		}

		foreach(Gebot ges in ret)
		{
			Console.WriteLine(ges);
		}
		return ret;
	}
	
	public static List<Gesuch> ParseRequest(String str)
	{
		List<Gesuch> ret = new List<Gesuch>();
		String[] splitted = str.Split(new Char[] { '\n' });
		
		foreach(String line in splitted)
		{
			String[] singleLine = line.Split(new Char[] { '|' });
			if (singleLine.Length > 1)
			{
				ret.Add(new Gesuch(singleLine[1], singleLine[2], singleLine[3], singleLine[0]));
			}
		}

		foreach(Gesuch ges in ret)
		{
			Console.WriteLine(ges);
		}
		return ret;
	}
}

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

public class Gebot : Angebot
{
	public String seller;	

	public Gebot(String nArticle, String nQuantity, String nPrice, String nSeller) : base(nArticle, nQuantity, nPrice) 
	{
		this.seller = nSeller;
	}

	public override String ToString() 
	{
		return String.Format("Verkäufer {0} verkauft ", this.seller) + "" + base.ToString();
	}
}

public class Gesuch : Angebot
{
	public String requestor;	

	public Gesuch(String nArticle, String nQuantity, String nPrice, String nRequestor) : base(nArticle, nQuantity, nPrice) 
	{
		this.requestor = nRequestor;
	}

	public override String ToString() 
	{
		return String.Format("Kunde {0} sucht ", this.requestor) + "" + base.ToString();
	}
	
}
