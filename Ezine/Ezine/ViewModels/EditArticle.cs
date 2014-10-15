using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ezine.Models;

namespace Ezine.ViewModels
{
    /// <summary>
    /// 后台编辑文章信息
    /// </summary>
    public class EditArticle
    {
        public int ArticleId { get; set; }
        public int EzineId { get; set; }

        public string EzineName { get; set; }
        public string SectionName { get; set; }
        public IList<Section> Sections { get; set; }

        public string Title { get; set; }
        public int SectionId { get; set; }

        public string Author { get; set; }

        public string Contents { get; set; }

        public string Source { get; set; }

        public DateTime AddDate { get; set; }
        /// <summary>
        /// 赞同、反对数量
        /// </summary>
        public int Agrees { get; set; }
    }
}