using Ezine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ezine.Abstract
{
    /// <summary>
    /// 杂志信息
    /// </summary>
    public interface IEzine
    {
        /// <summary>
        /// 获取所有的杂志信息
        /// </summary>
        /// <returns></returns>
        IQueryable<EzineViewModel> GetAllList();
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Add(EzineInfo model);
    }
}