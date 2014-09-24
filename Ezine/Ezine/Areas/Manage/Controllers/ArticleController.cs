using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ezine.Abstract;
using Ezine.Models;
using Ezine.ViewModels;

namespace Ezine.Areas.Manage.Controllers
{
    public class ArticleController : Controller
    {
        private IEzine ezineRepository;
        private ISection sectionRepository;
        private IArticle articleRepository;
        public ArticleController(IEzine ezine, ISection section, IArticle article)
        {
            ezineRepository = ezine;
            sectionRepository = section;
            articleRepository = article;
        }
        // GET: Manage/Article
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 添加新文章
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add(int id)
        {
            var ezine = ezineRepository.GetEzine(id);

            return View(ezine);
        }

        /// <summary>
        /// 添加新文章
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]  
        public ActionResult Add(Article endity)
        {
            endity.AddDate = DateTime.Now;

            var ezine = articleRepository.AddArticle(endity);
            if (ezine)
            {
                return View("Index");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ArticleList(int id)
        {
            var list = articleRepository.ListByEzineId(id);

            return View(list);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var ezine = articleRepository.GetArticle(id);
            
            return View(ezine);
        }
    }
}