using System;
using System.Collections.Generic;

using DapperExtensions;
using Unicellular.Web.Entity.Ecommerce;
using Unicellular.Web.DAO.Ecommerce;
using Unicellular.Web.Entity;
using XLToolLibrary.Utilities;
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

        /// <summary>
        /// 根据ID查找对象
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public T_EC_KeyWords Get( string ID )
        {
            T_EC_KeyWords keyword = new T_EC_KeyWords();
            try
            {
                keyword = dao.GetById<T_EC_KeyWords>( ID );
            }
            catch ( Exception ex )
            {
                throw ex;
            }
            return keyword;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public MsgEntity Add( T_EC_KeyWords keyword )
        {
            MsgEntity me = new MsgEntity();
            if ( keyword == null || string.IsNullOrEmpty( keyword.KEY_WORD ))
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "名称不能为空";
                return me;
            }
            //查找关键字是否有相同值（不同plate_type的name不能相同）
            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.KEY_WORD, Operator.Eq, keyword.KEY_WORD ) );
            pg.Predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.PLAT_TYPE, Operator.Eq, keyword.PLAT_TYPE ) );
            int count = dao.Count<T_EC_KeyWords>(pg);
            if ( count > 0 )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "同一平台名称重复";
                return me;
            }
            dynamic result = null;
            try
            {
                result = dao.Insert<T_EC_KeyWords>( keyword );
            }
            catch ( Exception ex )
            {
                throw ex;
            }
            if ( result != null )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Success;
                me.MsgDes = MsgEntity.MsgCodeEnum.Success.GetDescription();
            }
            return me;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public MsgEntity Edit( T_EC_KeyWords keyword )
        {
            MsgEntity me = new MsgEntity();
            if ( keyword == null || string.IsNullOrEmpty( keyword.KEY_WORD ) )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "名称不能为空";
                return me;
            }
            //查找关键字是否有相同值（不同id的dict_code不能相同）
            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.KEY_WORD, Operator.Eq, keyword.KEY_WORD ) );
            pg.Predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.PLAT_TYPE, Operator.Eq, keyword.PLAT_TYPE ) );
            pg.Predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.ID, Operator.Eq, keyword.ID,true ) );
            int count = dao.Count<T_EC_KeyWords>(pg);
            if ( count > 0 )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "同一平台名称重复";
                return me;
            }
            bool result = false;
            try
            {
                result = dao.Update<T_EC_KeyWords>( keyword );
                me.MsgCode = MsgEntity.MsgCodeEnum.Success;
                me.MsgDes = "编辑成功";
            }
            catch ( Exception ex )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = ex.Message;
            }
            return me;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public MsgEntity Delete( string ID )
        {
            MsgEntity me = new MsgEntity();
            try
            {
                dao.Delete<T_EC_KeyWords>( ID );
                me.MsgCode = MsgEntity.MsgCodeEnum.Success;
                me.MsgDes = "删除成功";
            }
            catch ( Exception ex )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = ex.Message;
            }
            return me;
        }


        #endregion
    }
}
