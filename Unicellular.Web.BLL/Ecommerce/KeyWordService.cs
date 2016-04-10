using System;
using System.Collections.Generic;

using DapperExtensions;
using Unicellular.Web.Entity.Ecommerce;
using Unicellular.Web.DAO.Ecommerce;

namespace Unicellular.Web.BLL.Ecommerce
{
    /// <summary>
    /// 关键词
    /// </summary>
    public class KeyWordService
    {
        private readonly KeyWordDao dao;

        public KeyWordService()
        {
            dao = new KeyWordDao();
        }

        #region 关键词列表
        /// <summary>
        /// 根据前台传递的参数，对"字典项"数据进行分页，搜索
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="number">返回的total总数</param>
        /// <param name="sort">排序字段</param>
        /// <param name="sortOrder">排序标识asc或desc</param>
        /// <returns></returns>
        public List<T_EC_KeyWords> GetKeyWordsPages( int pageNumber, int pageSize, out long number, string sort = null, string sortOrder = null, T_EC_KeyWords keywordEntity = null )
        {
            return dao.GetKeyWordsPages( pageNumber, pageSize, out number, sort, sortOrder, keywordEntity );
        }
        #endregion
    }
}
