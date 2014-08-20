using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ezine.Models
{
    public class EzineViewModel
    {
        public int EzineId { get; set; }
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }
        public string FilePath { get; set; }
        public string OldFileName { get; set; }
    }
}