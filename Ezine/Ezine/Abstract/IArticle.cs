using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ezine.Models;
using Ezine.ViewModels;

namespace Ezine.Abstract
{
    /// <summary>
    /// 文章信息
    /// </summary>
    public interface IArticle
    {

        /// <summary>
        /// 查询文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Article GetArticle(int id);

        /// <summary>
        /// 添加新文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddArticle(Article model);

        /// <summary>
        /// 所在杂志的所有文章列表
        /// </summary>
        /// <param name="ezineId">杂志Id</param>
        /// <returns></returns>
        IList<ArticleViewModel> ListByEzineId(int ezineId);
        /// <summary>
        /// 章节所属的文章列表
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        IList<ArticleList> ListBySectionId(int sectionId);

        EditArticle GetArticleById(int articleId);

    }
}
