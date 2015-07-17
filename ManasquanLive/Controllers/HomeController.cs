using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ManasquanLive.Models;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace ManasquanLive.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.News = GetNews();
            return View();
        }

        private string GetNews(){
            string gg = GetGoogleNews();
            List<NewsModel> aa = StarNewsGroupNews();

            //need to combine both lists and sort by date 
            //then conver to json and return 

            return Newtonsoft.Json.JsonConvert.SerializeObject(aa);
        }


        private string GetGoogleNews()
        {
            string news = WebRequestData("https://news.google.com/news?q=manasquan&output=rss");
            //need to modify news

            return "";

        }

        private List<NewsModel> StarNewsGroupNews()
        {
            List<NewsModel> newsList = new List<NewsModel>();
            string baseURL = "http://starnewsgroup.com";
            string news = WebRequestData(baseURL + "/town.html?town=Manasquan");
            
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(HttpUtility.HtmlDecode(news));

            HtmlNode listOfNewsEvents = html.DocumentNode.SelectNodes("//div[contains(@class,'content')]")[0].ChildNodes["ul"];
            foreach (HtmlNode li in listOfNewsEvents.ChildNodes)
            {
                HtmlNode linkNode = li.ChildNodes["a"];
                if (linkNode != null)
                {
                    string href = linkNode.Attributes["href"].Value;
                    string dateText = href.Substring(href.LastIndexOf(@"/") - 8, 8);
                    string innerText = Regex.Replace(li.InnerText, @"^[^\r]*", "");
                    innerText = Regex.Replace(innerText, @"\t|\n|\r", "");

                    NewsModel newsArticle = new NewsModel();
                    newsArticle.Headline = innerText;
                    newsArticle.Date = DateTime.ParseExact(dateText, "MM.dd.yy", System.Globalization.CultureInfo.InvariantCulture);
                    newsArticle.Provider = "CoastStar";
                    newsArticle.URL = baseURL + href;

                    newsList.Add(newsArticle);
                }
            }

            return newsList;
        }


        private string WebRequestData(string url)
        {
            string data = "";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse webResp = (HttpWebResponse)webRequest.GetResponse();

            if (webResp.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = webResp.GetResponseStream();
                StreamReader readStream = null;
                readStream = new StreamReader(receiveStream);
                data = readStream.ReadToEnd();
                webResp.Close();
                readStream.Close();
            }

            return data;
        }
    }
}