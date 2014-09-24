using Ezine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ezine.ViewModels;

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

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Edit(EzineInfo model);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        EzineInfo GetEzine(int id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
    }
}