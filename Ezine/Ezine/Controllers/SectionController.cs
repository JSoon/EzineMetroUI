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
        private ISection repository;

        public SectionController(ISection sectionRepository)
        {
            repository = sectionRepository;
        }

        // GET: Section
        public ActionResult Index(int Id)
        {
            var sections = repository.GetAlListByEzineId(Id);

            return View(sections);
        }
     
    }
}