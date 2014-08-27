using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ezine.Models;

namespace Ezine.Abstract
{
    /// <summary>
    /// 章节目录信息
    /// </summary>
    public interface ISection
    {
        IList<Section> GetAlListByEzineId(int ezineId);
    }
}
