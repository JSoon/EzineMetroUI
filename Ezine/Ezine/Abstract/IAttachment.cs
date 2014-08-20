using Ezine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezine.Abstract
{
    /// <summary>
    /// 附件信息
    /// </summary>
    public interface IAttachment
    {
        Attachment AttachmentById(int Id);
    }
}
