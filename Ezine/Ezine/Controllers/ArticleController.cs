using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ezine.Abstract;
using Ezine.Models;

namespace Ezine.Controllers
{
    public class ArticleController : Controller
    {
        private IArticle repository;

        public ArticleController(IArticle articleRepository)
        {
            repository = articleRepository;
        }
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 文章信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Articel(int id)
        {
            var model = repository.GetArticle(id);
            ViewBag.Artcile = model;

            return View(model);
        }

        /// <summary>
        /// 模板一
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult Temp3(Article entity)
        {
            entity.Contents = CharContent(entity.Contents);
            return View(entity);
        }

        /// <summary>
        /// 章节下属的文章列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult ListArticleBySection(int id)
        {
            var list = repository.ListBySectionId(id);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 拆分字符串
        /// </summary>
        /// <param name="articleContents"></param>
        /// <returns></returns>
        public string CharContent(string articleContents)
        {
            string content = articleContents;
            int len = articleContents.Length;

            string str1 = articleContents.Substring(0, len / 3);
            string str2 = articleContents.Substring(len / 3, len / 3 * 2);
            string str3 = articleContents.Substring(len / 3 * 2, len);

            return str1 + "@" + str2 + "@" + str3;
        }
    }
}