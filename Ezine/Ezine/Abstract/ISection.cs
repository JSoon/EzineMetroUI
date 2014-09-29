using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ezine.Models;
using Ezine.ViewModels;

namespace Ezine.Abstract
{
    /// <summary>
    /// 章节目录信息
    /// </summary>
    public interface ISection
    {
        IList<Section> GetAlListByEzineId(int ezineId);

        /// <summary>
        /// 保存每个杂志的多个章节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Save(Section model);

        IList<SectionViewModel> GetList(int id);

        bool EditSection(IList<Section> models);
    }
}
