using System;
using System.Collections.Generic;
using System.Linq;

using Unicellular.Web.DAO;
using Unicellular.Web.Entity;
using Unicellular.Web.Entity.Ecommerce;
using DapperExtensions;
using XLToolLibrary.Utilities;
namespace Unicellular.Web.BLL.Ecommerce
{
    public class CategoryService
    {
        private readonly DAOBase dao;

        public CategoryService()
        {
            dao = new DAOBase();
        }
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public List<T_EC_KWCategory> GetList()
        {
            List<T_EC_KWCategory> categorys = new List<T_EC_KWCategory>();
            try
            {
                categorys = dao.GetAll<T_EC_KWCategory>().ToList();
            }
            catch ( Exception ex )
            {
                throw ex;
            }
            return categorys;
        }

        /// <summary>
        /// 根据ID查找对象
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public T_EC_KWCategory Get( string ID )
        {
            T_EC_KWCategory category = new T_EC_KWCategory();
            try
            {
                category = dao.GetById<T_EC_KWCategory>( ID );
            }
            catch ( Exception ex )
            {
                throw ex;
            }
            return category;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public MsgEntity Add( T_EC_KWCategory category )
        {
            MsgEntity me = new MsgEntity();
            if ( category == null || string.IsNullOrEmpty( category.CATEGORY_CODE ) || string.IsNullOrEmpty( category.CATEGORY_NAME ) )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "编码或名称不能为空";
                return me;
            }
            //查找关键字是否有相同值
            int count = dao.Count<T_EC_KWCategory>(Predicates.Field<T_EC_KWCategory>(f=>f.CATEGORY_CODE,Operator.Eq,category.CATEGORY_CODE));
            if ( count > 0 )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "编码重复";
                return me;
            }
            dynamic result = null;
            try
            {
                result = dao.Insert<T_EC_KWCategory>( category );
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
        public MsgEntity Edit( T_EC_KWCategory category )
        {
            MsgEntity me = new MsgEntity();
            if ( category == null || string.IsNullOrEmpty( category.CATEGORY_CODE ) || string.IsNullOrEmpty( category.CATEGORY_NAME ) )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "编码或名称不能为空";
                return me;
            }
            //查找关键字是否有相同值（不同id的dict_code不能相同）
            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add( Predicates.Field<T_EC_KWCategory>( f => f.CATEGORY_CODE, Operator.Eq, category.CATEGORY_CODE ) );
            pg.Predicates.Add( Predicates.Field<T_EC_KWCategory>( f => f.ID, Operator.Eq, category.ID, true ) );
            int count = dao.Count<T_EC_KWCategory>(pg);
            if ( count > 0 )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "编码重复";
                return me;
            }
            bool result = false;
            try
            {
                result = dao.Update<T_EC_KWCategory>( category );
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
                dao.Delete<T_EC_KWCategory>( ID );
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
    }
}
