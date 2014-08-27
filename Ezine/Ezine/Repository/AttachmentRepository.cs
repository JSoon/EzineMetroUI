using Ezine.Abstract;
using Ezine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ezine.Models;

namespace Ezine.Repository
{
    public class AttachmentRepository : IAttachment
    {
        EFDbContext db = new EFDbContext();
        public Attachment AttachmentById(int Id)
        {
            return db.Attachments.Find(Id);
        }

        /// <summary>
        /// 添加附件到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddFile(Attachment model)
        {
            var file = db.Attachments.Add(model);
            db.SaveChanges();
            int row = 0;
            if (file != null)
            {
                row = file.Id;
            }
            return row;
        }
    }
}