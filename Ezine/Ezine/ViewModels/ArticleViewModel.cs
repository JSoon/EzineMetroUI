using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ezine.Models;

namespace Ezine.ViewModels
{
    public class ArticleViewModel 
    {
        public int ArticleId { get; set; }

        public int EzineId { get; set; }

        public string EzineName { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int Agrees { get; set; }

        public DateTime AddDate { get; set; }
        
    }
}