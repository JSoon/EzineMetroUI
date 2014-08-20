using Ezine.Abstract;
using Ezine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ezine.Repository
{
    public class AttachmentRepository:IAttachment
    {
        EFDbContext db = new EFDbContext();
        public Models.Attachment AttachmentById(int Id)
        {
            return db.Attachments.Find(Id);
        }
    }
}