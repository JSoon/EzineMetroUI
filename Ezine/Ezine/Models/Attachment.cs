using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ezine.Models
{
    /// <summary>
    /// 附件信息
    /// </summary>
    public class Attachment
    {
        public int Id { get; set; }
        public string FileType { get; set; }
        public string FileSzie { get; set; }
        public string FilePath { get; set; }
        public string NewFileName { get; set; }
        public string OldFileName { get; set; }
        public DateTime AddTime { get; set; }
    }
}