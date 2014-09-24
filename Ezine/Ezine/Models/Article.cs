using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ezine.Models
{
    /// <summary>
    /// 文章信息
    /// </summary>
    public class Article
    {
        public int Id { get; set; }

        /// <summary>
        /// 章节Id
        /// </summary>
        public int EzineId { get; set; }
        
        /// <summary>
        /// 章节Id
        /// </summary>
        public int SectionId { get; set; }
        
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 赞同、反对数量
        /// </summary>
        public int Agrees { get; set; }

        /// <summary>
        /// 图片  多张
        /// </summary>
        public string AttachmentId { get; set; }
        
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime AddDate { get; set; }

    }
}