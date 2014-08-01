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
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public ActionResult userLogin(string userName, string pwd)
        {
            var account = repository.UserLogin(userName, pwd);
            
            return View(account);
        }

    }
}
