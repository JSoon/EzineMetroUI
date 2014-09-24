using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                         join f in db.Attachments on e.AttachmentId equals f.Id into ef
                         from ff in ef.DefaultIfEmpty()
                         select new EzineViewModel
                         {
                             EzineId = e.Id,
                             Name = e.Name,
                             PublishDate = e.PublishDate,
                             FilePath = ff.FilePath,
                             OldFileName = ff.OldFileName
                         };
            return ezines;
        }


        public int Add(EzineInfo model)
        {
            var ezine = db.EzineInfos.Add(model);
            int row = db.SaveChanges();
            return row;
        }

        /// <summary>
        /// 删除杂志信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var ezine = db.EzineInfos.Remove(db.EzineInfos.Where(e => e.Id == id).FirstOrDefault());
            int row = db.SaveChanges();
            return row > 0;
        }

        /// <summary>
        /// 查询杂志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EzineInfo GetEzine(int id)
        {
            var model = db.EzineInfos.Where(e => e.Id == id).FirstOrDefault();
            return model;
        }

        /// <summary>
        /// 编辑杂志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(EzineInfo model)
        {
            db.EzineInfos.Attach(model);
            db.Entry<EzineInfo>(model).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }
    }
}