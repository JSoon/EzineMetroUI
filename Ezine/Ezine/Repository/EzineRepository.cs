using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ezine.Abstract;
using Ezine.Infrastructure;
using Ezine.Models;

namespace Ezine.Repository
{
    public class EzineRepository : IEzine
    {
        EFDbContext db = new EFDbContext();

        /// <summary>
        /// 获取所有的杂志信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<EzineViewModel> GetAllList()
        {
            var ezines = from e in db.EzineInfos
                         join f in db.Attachments on e.AttachmentId equals f.Id
                         select new EzineViewModel
                         {
                             EzineId = e.Id,
                             Name = e.Name,
                             PublishDate = e.PublishDate,
                             FilePath = f.FilePath,
                             OldFileName = f.OldFileName
                         };
            return ezines;
        }
    }
}