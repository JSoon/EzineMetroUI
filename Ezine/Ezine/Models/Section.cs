using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ezine.Models
{
    /// <summary>
    /// 章节
    /// </summary>
    public class Section
    {
        public int Id { get; set; }
        public int EzineId { get; set; }
        public string Name { get; set; }
    }
}