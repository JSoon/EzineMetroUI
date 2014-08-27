using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ezine.Areas.Manage.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Manage/Article
        public ActionResult Index()
        {
            return View();
        }
    }
}