using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;

namespace ManasquanLive.Models
{
    public class NewsModel
    {
        public string Headline { get; set; }
        public DateTime Date { get; set; }
        public string URL { get; set; }
        public string Provider { get; set; }
    }
}