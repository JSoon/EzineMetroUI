using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ezine.Abstract;

namespace Ezine.Areas.Manage.Controllers
{
    public class AccountController : Controller
    {
        private IAccount repository;
        public AccountController(IAccount accountRepository)
        {
            repository = accountRepository;
        }
        // GET: Manage/Account
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public JsonResult UserLogin(string userName, string userPwd)
        {
            var account = repository.UserLogin(userName, userPwd);
            string falg = "";
            if (account != null)
            {
                falg = "success";
            }
            else
            {
                falg = "false";
            }
            return Json(falg);
        }

        /// <summary>
        /// 管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

    }
}
