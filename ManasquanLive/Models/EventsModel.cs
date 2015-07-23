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
    public class EventListModel
    {
        public List<EventModel> Events { get; set; }
    }

    public class EventModel
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Location { get; set; }
        public string Time { get; set; }
    }
}