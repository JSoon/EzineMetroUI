using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Ezine.Areas.Manage.Models;
using Ezine.Models;

namespace Ezine.Infrastructure
{
    public class EFDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<EzineInfo> EzineInfos { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
    }
}