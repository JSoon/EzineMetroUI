using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ezine.Abstract;
using Ezine.Models;

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
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(EzineInfo model)
        {
            int result = repository.Add(model);

            return RedirectToAction("Index");
        }
        /// <summary>
        /// 所有的杂志信息列表
        /// </summary>
        /// <returns></returns>
        public JsonResult EzineList()
        {
            var ezine = repository.GetAllList();
            return Json(ezine, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ezine = repository.GetEzine(id);
            return View(ezine);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(EzineInfo model)
        {
            var result = repository.Edit(model);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        /// <summary>
        /// 删除杂志封面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Delete(int id)
        {
            return Json(repository.Delete(id), JsonRequestBehavior.AllowGet);
        }
    }
}