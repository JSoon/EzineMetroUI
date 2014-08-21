using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ezine.Abstract;

namespace Ezine.Areas.Manage.Controllers
{
    public class EzineController : Controller
    {
        private IEzine repository;
        public EzineController(IEzine ezineRepository)
        {
            repository = ezineRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 所有的杂志信息列表
        /// </summary>
        /// <returns></returns>
        public JsonResult EzineList()
        {
            var ezine = repository.GetAllList();
            return Json(ezine,JsonRequestBehavior.AllowGet);
        }
    }
}