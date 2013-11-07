using System;
using System.Net;
using System.IO;
using System.Xml;

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
			html = RemoveUnusedCrap(html);
			
		}
		catch (Exception e) 
		{
			Console.WriteLine(e);
		}
	}

	public static String GetUrl(Boolean ok, String url, String path, CookieContainer container) {
		url += path;
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
		request.CookieContainer = container;
		request.Method = WebRequestMethods.Http.Get;

		var response = request.GetResponse();
		StreamReader reader = new StreamReader(response.GetResponseStream());
		String body = reader.ReadToEnd();
		
		return body;
	}

	public static CookieContainer Authenticate(String url, CookieContainer container) {
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
		return container;//.Add(response.Cookies);
	}

	public static String RemoveUnusedCrap(String html) {
		Console.WriteLine(html);
		return "";
	}
}
