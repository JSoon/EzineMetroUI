using Ezine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ezine.Controllers
{
    public class SectionController : Controller
    {
        private ISection sectionRepository;
        private IArticle articleRepository;
        public SectionController(ISection sectionRepository, IArticle articleRepository)
        {
            this.sectionRepository = sectionRepository;
            this.articleRepository = articleRepository;
        }

        // GET: Section
        public ActionResult Index(int Id)
        {

            var list = articleRepository.ListBySectionId(Id);

            //return Json(list, JsonRequestBehavior.AllowGet);

            //var sections = repository.GetAlListByEzineId(Id);

            return View(list);
        }
     
    }
}