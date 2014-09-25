using Ezine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ezine.Controllers
{
    public class HomeController : Controller
    {
        private IEzine repository;
        public HomeController(IEzine ezineRepository)
        {
            repository = ezineRepository;
        }
        /// <summary>
        /// 获取所有的期刊
        /// </summary>
        /// <returns></returns>
        public JsonResult EzineList()
        {
            var query = repository.GetAllList();
            return Json(query.ToList());
        }

        public ActionResult Index()
        {
            var ezines = repository.GetAllList();
            return View(ezines);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}