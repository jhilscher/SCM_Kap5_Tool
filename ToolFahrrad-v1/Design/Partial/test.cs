using System;
using System.Net;
using System.IO;

public class WebsiteChecker 
{
	public static void Main()
	{
		String url = "http://www.iwi.hs-karlsruhe.de/scs/";
		try 
		{

			CookieContainer container = new CookieContainer();
			container = GetUrl(false, url, "start", container);
			container = GetUrl(false, url, "market/market_set.html", container);
			container = Authenticate(url + "market/", container);
			GetUrl(true, url, "market/marketinfo.jsp", container);
			
		}
		catch (Exception e) 
		{
			Console.WriteLine(e);
		}
	}

	public static CookieContainer GetUrl(Boolean ok, String url, String path, CookieContainer container) {
		url += path;
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
		request.CookieContainer = container;
		request.Method = WebRequestMethods.Http.Get;

		var response = request.GetResponse();
		StreamReader reader = new StreamReader(response.GetResponseStream());
		String body = reader.ReadToEnd();
		if (ok) 
			Console.WriteLine(body);
		return container;
//		Console.WriteLine(body);
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

		var response = (HttpWebResponse)authRequest.GetResponse();
		StreamReader reader = new StreamReader(response.GetResponseStream());
		String body = reader.ReadToEnd();
		return container;//.Add(response.Cookies);
	}
}
