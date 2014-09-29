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
                            EzineId = a.EzineId,
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

        /// <summary>
        /// 文章，章节，期刊信息
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public EditArticle GetArticleById(int articleId)
        {
            var query = from a in db.Articles
                        join s in db.Sections on a.EzineId equals s.EzineId
                        join e in db.EzineInfos on s.EzineId equals e.Id
                        select new EditArticle
                        {
                            EzineId = e.Id,
                            EzineName = e.Name,
                            SectionName = (db.Sections.Where(c => c.Id == a.SectionId).First().Name),
                            Sections = (db.Sections.Where(c => c.EzineId == e.Id).ToList<Section>()),
                            Title = a.Title,
                            Author = a.Author,
                            Contents = a.Contents,
                            Source = a.Source,
                            Agrees = a.Agrees
                        };

            return query.First();
        }

        /// <summary>
        /// 章节所属的文章列表
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public IList<ArticleList> ListBySectionId(int ezineId)
        {

            var query = from s in db.Sections
                        join a in db.Articles on s.Id equals a.SectionId
                        join e in db.EzineInfos on s.EzineId equals e.Id
                        where s.EzineId == ezineId
                        select
                        new ArticleList
                        {
                            EzineName = e.Name,
                            ArticleId = a.Id,
                            SectionId = s.Id,
                            SectionName = s.Name,
                            Articels = (db.Articles.Where(t => t.SectionId == s.Id).ToList<Article>())
                        };

            return query.ToList<ArticleList>()
                .Distinct<ArticleList>(new ArticleSectionComparer())
                .ToList<ArticleList>();
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

    /// <summary>
    /// 去重复
    /// </summary>
    class ArticleSectionComparer : EqualityComparer<ArticleList>
    {
        public override bool Equals(ArticleList x, ArticleList y)
        {
            return x.SectionId == y.SectionId;
        }
        public override int GetHashCode(ArticleList obj)
        {
            return obj.SectionId.GetHashCode();
        }
    }
}