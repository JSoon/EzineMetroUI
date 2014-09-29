using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ezine.Models;

namespace Ezine.ViewModels
{
    public class ArticleList
    {
        public string EzineName { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int ArticleId { get; set; }
        public IList<Article> Articels { get; set; }

    }

}