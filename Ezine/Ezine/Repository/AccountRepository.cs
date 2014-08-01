using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ezine.Abstract;
using Ezine.Areas.Manage.Models;
using Ezine.Infrastructure;

namespace Ezine.Repository
{
    public class AccountRepository : IAccount
    {
        EFDbContext db = new EFDbContext();

        public Account UserLogin(string userName, string pwd)
        {
            var account = from u in db.Accounts
                          where
                              u.LoginName.Equals(userName.Trim()) && u.Password.Equals(pwd.Trim())
                          select u;

            return account.FirstOrDefault<Account>();
        }
    }
}