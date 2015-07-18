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
using System.Web.Script.Serialization;

namespace ManasquanLive.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.News = GetNews();
            return View();
        }

        private string GetNews()
        {
            //Maybe load this into memory? Not sure how to do this. Something about Global.asax?
            List<NewsModel> newsList = new List<NewsModel>();
            newsList = StarNewsGroupNews(GetGoogleNews());        
            newsList.Sort(new Comparison<NewsModel>((x, y) => DateTime.Compare(y.Date , x.Date)));
    
            return Newtonsoft.Json.JsonConvert.SerializeObject(newsList); ;
        }

        private List<NewsModel> GetGoogleNews()
        {
            List<NewsModel> newsList = new List<NewsModel>();
            string news = WebRequestData("https://news.google.com/news?q=manasquan&output=rss");

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(HttpUtility.HtmlDecode(news));

            HtmlNode listOfNewsEvents = html.DocumentNode.ChildNodes["rss"].ChildNodes["channel"];
            foreach (HtmlNode item in listOfNewsEvents.ChildNodes)
            {
                if (item.Name == "item")
                {
                    string headline = string.Empty;
                    DateTime date = new DateTime();
                    string provider = string.Empty;
                    string url = string.Empty;
                    foreach (HtmlNode attr in item.ChildNodes)
                    {
                        string attrText = attr.InnerText;
                        if (attr.Name == "title")
                        {
                            string[] splits = attrText.Split('-');
                            for (int i = 0; i < splits.Length - 1; i++)
                            {
                                if (i == 0)
                                    headline = splits[i];
                                else
                                    headline = headline + "-" + splits[i];
                            }
                            provider = splits[splits.Length - 1].Trim();
                        }
                        else if (attr.Name == "pubdate") 
                        {
                            date = DateTime.Parse(attrText);
                        }
                        else if (attr.Name == "link")
                        {
                            url = attr.NextSibling.InnerText;
                        }
                    }
                    NewsModel newsArticle = new NewsModel();
                    newsArticle.Headline = headline;
                    newsArticle.Date = date;
                    newsArticle.Provider = provider;
                    newsArticle.URL = url;

                    newsList.Add(newsArticle);
                }
            }

            return newsList;
        }

        private List<NewsModel> StarNewsGroupNews(List<NewsModel> newsList)
        {
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
                    newsArticle.Provider = "Star News Group";
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