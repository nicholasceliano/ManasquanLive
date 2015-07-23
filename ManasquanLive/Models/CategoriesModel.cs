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
    public class CategoriesListModel
    {
        public List<CategoryModel> Categories { get; set; }
    }

    public class CategoryModel
    {
        public int ID { get; set; }
        public string Cat { get; set; }
    }
}