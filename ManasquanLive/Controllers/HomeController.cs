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
using System.Diagnostics;

namespace ManasquanLive.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.News = GetNews();
            ViewBag.Locations = GetLocations();
            ViewBag.Events = GetEvents();

            return View();
        }

        private string GetNews()
        {
            //Maybe load this into memory? Not sure how to do this. Something about Global.asax?
            List<NewsModel> newsList = new List<NewsModel>();
            newsList = StarNewsGroupNews(GetGoogleNews());        
            newsList.Sort(new Comparison<NewsModel>((x, y) => DateTime.Compare(y.Date , x.Date)));
    
            return Newtonsoft.Json.JsonConvert.SerializeObject(newsList);
        }

        private string GetLocations()
        {
            List<LocationsModel> businessLocations = GetBusinessLocations();
            return Newtonsoft.Json.JsonConvert.SerializeObject(businessLocations);
        }

        private string GetEvents()
        {
            //Need to store in database
            //Need to add Location and Time
            //Need to build in ability to add custom events/send email to admin
            string events = "{ \"Events\" : [{ \"Date\": \"July 19, 2015\", \"Title\": \"Manasquan One Mile Swim\", \"Desc\": \"8 a.m. This event benefits the Manasquan High School Swim Team. Sign up at Ocean Avenue 6:00 am.\" } ,{ \"Date\": \"July 21, 2015\", \"Title\": \"Inlet Celebration\", \"Desc\": \"- Presented by the Manasquan Tourism Commission - Sponsored by the Manasquan Elks. Along the Manasquan Inlet - 5:30 p.m.- 9 p.m. Fun for the entire family, Food, Music Celebration, Arts and Crafts and Car Show.(Rain Date 7/ 22 / 15).\" } ,{ \"Date\": \"July 23, 2015\", \"Title\": \"Band Concert- 'WILLIE LYNCH'\", \"Desc\": \"Main Beach - 7:30 p.m.\" } ,{ \"Date\": \"July 28, 2015\", \"Title\": \"Manasquan Volunteer Engine Co. #2 Firemen's Fair\", \"Desc\": \"FUN FOR THE ENTIRE FAMILY!! Games, rides, food concessions & much, much more. 6 p.m. - 11 p.m. - Mallard Park.\"  } ,{ \"Date\": \"August 1, 2015\", \"Title\": \"Manasquan Volunteer Engine Co. #2 Firemen's Fair\", \"Desc\": \"FUN FOR THE ENTIRE FAMILY!! Games, rides, food concessions & much, much more. 6 p.m. - 11 p.m. - Mallard Park.\" } ,{ \"Date\": \"August 2, 2015\", \"Title\": \"Manasquan Seniors Festival/Craft Show\", \"Desc\": \"Squan Plaza - 8 a.m. Rain Date 8/9/15).\" } ,{ \"Date\": \"August 4, 2015\", \"Title\": \"Manasquan 'Elks Day at the Beach'\",\"Desc\": \"Held at the Elks Beach at Ocean Ave. A spirited and fun-filled gathering for challenged children and their counsellors. Call 732-223-2534 for information.\" },{ \"Date\": \"August 6, 2015\", \"Title\": \"Band Concert - 'THE KOOTZ'\", \"Desc\": \"Main Beach - 7:30 p.m.\" },{ \"Date\": \"August 7, 2015\", \"Title\": \"Manasquan Chamber of Commerce Side-walk Sale\", \"Desc\": \"Main Street - 10 a.m. – 5 p.m.\" },{ \"Date\": \"August 8, 2015\", \"Title\": \"Manasquan Chamber of Commerce Side-walk Sale\", \"Desc\": \"Main Street - 10 a.m. – 5 p.m.\" },{ \"Date\": \"August 8, 2015\", \"Title\": \"Big Sea Day Celebration\", \"Desc\": \"A fun-filled annual tradition. Activities include sand castle contest, surfing, body-surfing, pie eating contest and fishing contests. Exhibition of local artwork and much, much more.(Rain date 8/9/15).\" },{ \"Date\": \"August 8, 2015\", \"Title\": \"Sand Castle Tournament\", \"Desc\": \"Sponsored BY Manasquan Tourism Commission in conjunction with Big Sea Day Celebration. Rec Beach. (Rain date 8/9/15).\" },{ \"Date\": \"August 10, 2015\", \"Title\": \"Manasquan Lifeguard Relays\", \"Desc\": \"Main Beach - 6 p.m.\" },{ \"Date\": \"August 13, 2015\", \"Title\": \"Band Concert - 'REGGAE PLUS'\", \"Desc\": \"Main Beach - 7 p.m.\" },{ \"Date\": \"August 20, 2015\", \"Title\": \"Band Concert- DON’T KNOW JACK\", \"Desc\": \"Main Beach - 7:30 p.m.\" },{ \"Date\": \"August 30, 2015\", \"Title\": \"Clambake\", \"Desc\": \"- Presented by the Manasquan Tourism Commission - Sponsored by Squan Beach House. 2 p.m. - Mallard Park. Advanced Tickets Required.\" },{ \"Date\": \"September 5, 2015\", \"Title\": \"Labor Day Celebration\", \"Desc\": \"- Presented by the Manasquan Tourism Commission - Sponsored by The Osprey • Concert - 'MOONLIGHTING'  - 7:30 p.m. - Main Beach. • Gala fireworks display to follow - Main Beach. (Rain Date 9/ 6 / 15).\" },{ \"Date\": \"September 12, 2015\", \"Title\": \"Manasquan Board Riders Classic Surf Competition\", \"Desc\": \"Inlet Beach. (Storm Date 9/13/15).\" },{ \"Date\": \"September 26, 2015\", \"Title\": \"Manasquan Chamber of Commerce Arts and Crafts Festival\", \"Desc\": \"- Squan Plaza - 10 a.m. – 4 p.m. (Rain Date 9/27/15).\" },{ \"Date\": \"October 11, 2015\", \"Title\": \"Manasquan Chamber of Commerce Wine Festival\", \"Desc\": \"Squan Plaza Noon – 4 p.m.\" },{ \"Date\": \"November 21, 2015\", \"Title\": \"Annual Turkey Run\",  \"Desc\": \"Five Mile Run and One Mile Run. Stockton Beach. Parking Lot at the Little League Field.\" },{ \"Date\": \"December 4, 2015\", \"Title\": \"Manasquan Chamber of Commerce Tree Lighting and Candy Cane Hunt\", \"Desc\": \"Squan Plaza - 6 p.m. (Rain Date 12/11/15).\" },{ \"Date\": \"December 12, 2015\", \"Title\": \"Manasquan Chamber of Commerce Open House Weekend\", \"Desc\": \"Main Street Business District.\" } ,{ \"Date\": \"December 13, 2015\", \"Title\": \"Christmas in Manasquan\", \"Desc\": \"- Sponsored by the Manasquan Tourism Commission Come and share the holiday spirit. Carollers, Hay rides with Santa and Mrs.Claus.Main Street Business District.\" } ,{ \"Date\": \"December 13, 2015\", \"Title\": \"Manasquan Chamber of Commerce Open House Weekend\", \"Desc\": \"Main Street Business District.\" }]}";
            EventListModel eventsList = Newtonsoft.Json.JsonConvert.DeserializeObject<EventListModel>(events);
            eventsList.Events.Sort(new Comparison<EventModel>((x, y) => DateTime.Compare(x.Date, y.Date)));

            return Newtonsoft.Json.JsonConvert.SerializeObject(eventsList);
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

        private List<LocationsModel> GetBusinessLocations()
        {
            List<LocationsModel> locationsList = new List<LocationsModel>();
            string businessLocations = Regex.Replace(WebRequestData("http://www.manasquanchamber.org/member-directory.html"), @"\t|\n|\r", "").Replace("</td><tr>", "</td></tr><tr>");
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(HttpUtility.HtmlDecode(businessLocations));

            HtmlNode listOfLocations = html.DocumentNode.SelectNodes("//table[contains(@class,'sortable')]")[0];
            foreach (HtmlNode tr in listOfLocations.SelectNodes("tr"))
            {
                if (tr.FirstChild.Name == "td")
                {
                    LocationsModel loc = new LocationsModel();
                    foreach (HtmlNode td in tr.ChildNodes)
                    {
                        if (td == tr.FirstChild)
                        {
                            int brCt = 0;
                            foreach (HtmlNode node in td.ChildNodes)
                            {
                                if (node.Name == "br")
                                {
                                    string val = node.PreviousSibling.InnerText;
                                    switch (brCt)
                                    {
                                        case 0:
                                            loc.BusinessName = val;
                                            break;
                                        case 1:
                                            loc.Address = val;
                                            break;
                                        case 2:
                                            loc.Telephone = val;
                                            break;
                                        case 3:
                                            loc.Email = val;
                                            break;
                                        case 4:
                                            loc.Website = val;
                                            break;
                                        case 5:
                                            loc.Description = node.NextSibling == null ? string.Empty : node.NextSibling.InnerText.Replace('"','\'');
                                            break;
                                        default:
                                            break;
                                    }
                                    brCt++;
                                }
                            }
                        }
                        else
                        {
                            loc.Categories = td.InnerText.Split(',').Select(x => x.Trim()).ToArray();
                        }
                    }
                    locationsList.Add(loc);
                }
            }
            return locationsList;
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