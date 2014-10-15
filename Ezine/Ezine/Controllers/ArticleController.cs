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
        /// 章节下属的文章列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult ListArticleBySection(int id)
        {
            var list = repository.ListBySectionId(id);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}