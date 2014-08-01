using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ezine.Models
{
    /// <summary>
    /// 杂志信息
    /// </summary>
    public class EzineInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime PublishDate { get; set; }
        
        /// <summary>
        /// 封面图片
        /// </summary>
        public int AttachmentId { get; set; }
    }
}