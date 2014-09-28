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

            int imges = model.AttachmentId.Split(',').Length;
            if (imges == 1)
            {
                return RedirectToAction("");
            }
            else if (imges == 2)
            {
                return RedirectToAction("");
            }
            else if (imges == 3)
            {
                return RedirectToAction("ListArticleBySection");
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// 模板一
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult Temp1(Article entity)
        {
            string content = entity.Contents;
            int arrLen = 3;
            int len = entity.Contents.Length;
            if (len % 3 != 0)
            {
                arrLen = 4;
            }
            string[] array = new string[arrLen];

            return View(entity);
        }
        /// <summary>
        /// 模板一
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult Temp2(Article entity)
        {
            return View(entity);
        }
        /// <summary>
        /// 模板一
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult Temp3(Article entity)
        {
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
    }
}