using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ezine.Abstract;
using Ezine.Models;

namespace Ezine.Areas.Manage.Controllers
{
    public class SectionController : Controller
    {
        private IEzine ezineRepository;
        private ISection sectionRepository;
        public SectionController(IEzine ezine, ISection section)
        {
            ezineRepository = ezine;
            sectionRepository = section;
        }
        // GET: Manage/Section
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加目录
        /// </summary>
        /// <returns></returns>
        public ActionResult AddSections(int id)
        {
            var ezine = ezineRepository.GetEzine(id);
            return View(ezine);
        }

        /// <summary>
        /// 添加目录
        /// </summary>
        /// <returns></returns>
        public JsonResult Save(Section model)
        {
            return Json("test");
        }
    }
}