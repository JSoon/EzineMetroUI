using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ezine.Abstract;
using Ezine.Infrastructure;
using Ezine.Models;
using Ezine.ViewModels;

namespace Ezine.Repository
{
    /// <summary>
    /// 文章仓储
    /// </summary>
    public class ArticleRepository : IArticle
    {
        EFDbContext db = new EFDbContext();

        /// <summary>
        /// 添加新文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddArticle(Article model)
        {
            db.Articles.Add(model);

            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="ezineId"></param>
        /// <returns></returns>
        public IList<ArticleViewModel> ListByEzineId(int ezineId)
        {
            var query = from a in db.Articles
                        join e in
                            db.EzineInfos on a.EzineId equals e.Id
                        where a.EzineId == ezineId
                        select
                        new ArticleViewModel
                        {
                            EzineId=a.EzineId,
                            ArticleId = a.Id,
                            EzineName = e.Name,
                            Title = a.Title,
                            Author = a.Author,
                            AddDate = a.AddDate,
                            Agrees = a.Agrees
                        };

            var list = query.ToList<ArticleViewModel>()
                .Distinct<ArticleViewModel>(new ArticleComparer())
                .ToList<ArticleViewModel>();
            return list;
        }
        /// <summary>
        /// 查询文章信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Article GetArticle(int id)
        {
            var query = db.Articles.Where(a => a.Id == id);

            return query.FirstOrDefault();
        }
    }

    /// <summary>
    /// 去重复
    /// </summary>
    class ArticleComparer : EqualityComparer<ArticleViewModel>
    {
        public override bool Equals(ArticleViewModel x, ArticleViewModel y)
        {
            return x.Title == y.Title;
        }
        public override int GetHashCode(ArticleViewModel obj)
        {
            return obj.Title.GetHashCode();
        }
    }
}