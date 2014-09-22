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
            var result = sectionRepository.Save(model);

            return Json(result);
        }

        /// <summary>
        /// 编辑章节
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var result = sectionRepository.GetList(id);

            return View(result);
        }

        /// <summary>
        /// 编辑章节
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(SectionViewModel viewmodel)
        {
            IList<Section> list = new List<Section>();
            Section section = null;
            string[] arrSection = Request.Params["SectionName"].Split(',');
            string[] arrSectionId = Request.Params["SectionId"].Split(',');

            for (int i = 0; i < arrSection.Length; i++)
            {
                section = new Section();
                section.EzineId = viewmodel.EzineId;
                section.Id = viewmodel.SectionId;
                section.Id = int.Parse(arrSectionId[i]);
                section.Name = arrSection[i].ToString();

                list.Add(section);
            }

            var result = sectionRepository.EditSection(list);
            if (result)
            {
                return RedirectToAction("Index", "Ezine");
            }
            else
            {
                return View();
            }
        }
    }
}