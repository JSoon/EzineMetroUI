using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ezine.Areas.Manage.Models;

namespace Ezine.Abstract
{
    public interface IAccount
    {
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        Account UserLogin(string userName, string pwd);
    }
}