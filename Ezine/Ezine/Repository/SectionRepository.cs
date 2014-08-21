using Ezine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ezine.Models;
using Ezine.Infrastructure;

namespace Ezine.Repository
{
    public class SectionRepository : ISection
    {
        EFDbContext db = new EFDbContext();
        /// <summary>
        /// 获取杂志的目录根据杂志Id
        /// </summary>
        /// <param name="ezineId"></param>
        /// <returns></returns>
        public IList<Section> GetAlListByEzineId(int ezineId)
        {
            var sections = from s in db.Sections
                           where s.Id == ezineId
                           select s;

            return sections.ToList<Section>();
        }

    }
}