using Ezine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ezine.Repository
{
    public class SectionRepository : ISection
    {
        private ISection repository;
        /// <summary>
        /// 获取杂志的目录根据杂志Id
        /// </summary>
        /// <param name="ezineId"></param>
        /// <returns></returns>
        public IList<ISection> GetAlListByEzineId(int ezineId)
        {
            throw new NotImplementedException();
        }
    }
}