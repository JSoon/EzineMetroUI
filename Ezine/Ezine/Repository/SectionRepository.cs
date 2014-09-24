using Ezine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ezine.Models;
using Ezine.Infrastructure;
using System.Data.Entity;

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


        /// <summary>
        /// 保存每个杂志的多个章节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Save(Section model)
        {
            string[] arrName = model.Name.TrimEnd(',').Split(',');

            foreach (var item in arrName)
            {
                var entity = new Section();
                entity.EzineId = model.EzineId;
                entity.Name = item.ToString();

                db.Sections.Add(entity);
            }
            return db.SaveChanges() > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<SectionViewModel> GetList(int id)
        {
            var query = from e in db.EzineInfos
                        join s in db.Sections on e.Id equals s.EzineId
                        where e.Id == id
                        select
                        new SectionViewModel
                        {
                            EzineId = e.Id,
                            SectionId = s.Id,
                            EzineName = e.Name,
                            SectionName = s.Name,
                            PublishDate = e.PublishDate
                        };

            return query.ToList<SectionViewModel>();
        }

        /// <summary>
        /// 保存编辑的信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditSection(IList<Section> models)
        {
            foreach (var item in models)
            {
                db.Sections.Attach(item);
                db.Entry<Section>(item).State = EntityState.Modified;
            }

            return db.SaveChanges() > 0;
        }
    }
}