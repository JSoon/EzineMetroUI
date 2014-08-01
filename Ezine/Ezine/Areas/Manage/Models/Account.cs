using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ezine.Areas.Manage.Models
{
    /// <summary>
    /// 管理账户
    /// </summary>
    public class Account
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
}