using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ezine.Models
{
    public class SectionViewModel
    {
        public int EzineId { get; set; }
        public int SectionId { get; set; }
        public string EzineName { get; set; }
        public string SectionName { get; set; }
        public DateTime PublishDate { get; set; }
    }
}