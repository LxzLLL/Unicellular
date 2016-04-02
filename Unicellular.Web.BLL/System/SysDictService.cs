using System;
using System.Collections.Generic;
using System.Linq;

using Unicellular.Web.DAO;
using Unicellular.Web.Entity;
using Unicellular.Web.Entity.System;
using DapperExtensions;
using XLToolLibrary.Utilities;

namespace Unicellular.Web.BLL.System
{
    /// <summary>
    /// 系统字典处理
    /// </summary>
    public class SysDictService
    {
        private DAOBase dao;

        public SysDictService()
        {
            dao = new DAOBase();
        }

        #region 字典

        /// <summary>
        /// 根据id获取字典
        /// </summary>
        /// <param name="dictId"></param>
        /// <returns></returns>
        public T_Sys_Dict GetDict(string dictId)
        {
            return dao.GetById<T_Sys_Dict>( dictId );
        }


        /// <summary>
        /// 获取全部dict列表
        /// </summary>
        /// <returns></returns>
        public List<T_Sys_Dict> GetDictAll()
        {
            List < T_Sys_Dict > dicts = dao.GetList<T_Sys_Dict>().ToList();
            return dicts == null ? new List<T_Sys_Dict>() : dicts;
        }

        public int GetDictCount()
        {
            int iCount = dao.Count<T_Sys_Dict>(null);
            return iCount;
        }
        /// <summary>
        /// 根据前台传递的参数，对"字典"数据进行分页，搜索
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="number">返回的total总数</param>
        /// <param name="sort">排序字段</param>
        /// <param name="sortOrder">排序标识asc或desc</param>
        /// <param name="search">搜索的字符串</param>
        /// <returns></returns>
        public List<T_Sys_Dict> GetDictPages(int pageNumber,int pageSize, out long number, string sort = null, string sortOrder = null, string search = null )
        {
            //List < T_Sys_Dict > dicts =dao.GetPageList<T_Sys_Dict>(pageNumber,pageSize,out number).ToList();
            //创建谓词
            IFieldPredicate predicate = null;
            if (!string.IsNullOrEmpty( search ) )
            {
                predicate = Predicates.Field<T_Sys_Dict>( f => f.DICT_NAME, Operator.Like, "%" + search + "%" );
            }
            IList<ISort> sorts = new List<ISort>();
            //创建排序
            if ( sort == null )
            {
                sorts.Add( new Sort { PropertyName = "DICT_CODE", Ascending = sortOrder == "asc" ? true : false } );
            }
            else
            {
                sorts.Add( new Sort { PropertyName = sort, Ascending = sortOrder=="asc"?true:false } );
            }
            List < T_Sys_Dict > dicts =dao.GetPageList<T_Sys_Dict>(pageNumber,pageSize,out number,predicate,sorts).ToList();
            return dicts;
        }

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public MsgEntity AddDict( T_Sys_Dict dict )
        {
            MsgEntity me = new MsgEntity();
            if ( dict == null || string.IsNullOrEmpty( dict.DICT_CODE ) )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "字典编码不能为空";
                return me;
            }
            //查找关键字是否有相同值
            int count = dao.Count<T_Sys_Dict>(Predicates.Field<T_Sys_Dict>(f=>f.DICT_CODE,Operator.Eq,dict.DICT_CODE));
            if ( count > 0 )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "字典编码重复";
                return me;
            }
            dynamic result = dao.Insert<T_Sys_Dict>( dict );
            if(result!=null)
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Success;
                me.MsgDes = MsgEntity.MsgCodeEnum.Success.GetDescription();
            }
            return me;
        }


        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public MsgEntity EditDict( T_Sys_Dict dict )
        {
            MsgEntity me = new MsgEntity();
            if ( dict == null || string.IsNullOrEmpty( dict.DICT_CODE ) )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "字典编码不能为空";
                return me;
            }
            //查找关键字是否有相同值（不同id的dict_code不能相同）
            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add( Predicates.Field<T_Sys_Dict>( f => f.DICT_CODE, Operator.Eq, dict.DICT_CODE ) );
            pg.Predicates.Add( Predicates.Field<T_Sys_Dict>( f => f.ID, Operator.Eq, dict.ID, true ) );
            int count = dao.Count<T_Sys_Dict>(pg);
            if ( count > 0 )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "字典编码重复";
                return me;
            }
            bool result = false;
            try
            {
                result = dao.Update<T_Sys_Dict>( dict );
                me.MsgCode = MsgEntity.MsgCodeEnum.Success;
                me.MsgDes = "字典编辑成功";
            }
            catch(Exception ex )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = ex.Message;
            }
            return me;
        }

        /// <summary>
        /// 根据ID删除字典
        /// </summary>
        /// <param name="dictId"></param>
        /// <returns></returns>
        public MsgEntity DelDict(string dictId )
        {
            MsgEntity me = new MsgEntity();
            if ( string.IsNullOrEmpty( dictId ))
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "字典ID不存在";
                return me;
            }
            try
            {
                dao.Delete<T_Sys_Dict>( dictId );
                me.MsgCode = MsgEntity.MsgCodeEnum.Success;
                me.MsgDes = "字典删除成功";
            }
            catch ( Exception ex )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = ex.Message;
            }
            return me;   
        }

        #endregion

        #region 字典项
        /// <summary>
        /// 根据前台传递的参数，对"字典项"数据进行分页，搜索
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="number">返回的total总数</param>
        /// <param name="sort">排序字段</param>
        /// <param name="sortOrder">排序标识asc或desc</param>
        /// <param name="search">搜索的字符串</param>
        /// <returns></returns>
        public List<T_Sys_DictItem> GetDictItemPages( int pageNumber, int pageSize, out long number, string sort = null, string sortOrder = null, string search = null, string dictId = null )
        {
            //创建谓词
            //IFieldPredicate predicate = null;
            PredicateGroup pg = null;
            List < IPredicate >  predicates= new List<IPredicate>();
            //PredicateGroup pg = new PredicateGroup {Operator=GroupOperator.And,Predicates=new List<IPredicate>() };
            if ( !string.IsNullOrEmpty( search ) )
            {
                predicates.Add(Predicates.Field<T_Sys_DictItem>( f => f.DI_NAME, Operator.Like, "%" + search + "%" ));
            }
            if( !string.IsNullOrEmpty( dictId ) )
            {
                predicates.Add( Predicates.Field<T_Sys_DictItem>( f => f.DICT_ID, Operator.Eq, dictId ) );
            }
            if ( predicates.Count > 0 )
            {
                pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = predicates };
            }
            IList<ISort> sorts = new List<ISort>();
            //创建排序
            if ( sort == null )
            {
                sorts.Add( new Sort { PropertyName = "DICT_ID", Ascending = sortOrder == "asc" ? true : false } );
                sorts.Add( new Sort { PropertyName = "DI_CODE", Ascending = sortOrder == "asc" ? true : false } );
            }
            else
            {
                sorts.Add( new Sort { PropertyName = sort, Ascending = sortOrder == "asc" ? true : false } );
            }
            List < T_Sys_DictItem > dictitems =dao.GetPageList<T_Sys_DictItem>(pageNumber,pageSize,out number,pg,sorts).ToList();
            return dictitems;
        }

        /// <summary>
        /// 获取全部字典项列表
        /// </summary>
        /// <returns></returns>
        //public List<T_Sys_DictItem> GetDictItem()
        //{
        //    List < T_Sys_DictItem > dictitems = dao.GetList<T_Sys_DictItem>().ToList();
        //    return dictitems == null ? new List<T_Sys_DictItem>() : dictitems;
        //}


        /// <summary>
        /// 根据id获取字典
        /// </summary>
        /// <param name="dictId"></param>
        /// <returns></returns>
        public T_Sys_DictItem GetDictItem( string dictId )
        {
            return dao.GetById<T_Sys_DictItem>( dictId );
        }

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public MsgEntity AddDictItem( T_Sys_DictItem dictItem )
        {
            MsgEntity me = new MsgEntity();
            if ( dictItem == null || string.IsNullOrEmpty( dictItem.DI_CODE ) )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "字典项编码不能为空";
                return me;
            }
            //查找关键字是否有相同值
            int count = dao.Count<T_Sys_DictItem>(Predicates.Field<T_Sys_DictItem>(f=>f.DI_CODE,Operator.Eq,dictItem.DI_CODE));
            if ( count > 0 )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "字典项编码重复";
                return me;
            }
            dynamic result = dao.Insert<T_Sys_DictItem>( dictItem );
            if ( result != null )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Success;
                me.MsgDes = MsgEntity.MsgCodeEnum.Success.GetDescription();
            }
            return me;
        }


        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public MsgEntity EditDictItem( T_Sys_DictItem dictItem )
        {
            MsgEntity me = new MsgEntity();
            if ( dictItem == null || string.IsNullOrEmpty( dictItem.DI_CODE ) )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "字典项编码不能为空";
                return me;
            }
            //查找关键字是否有相同值（不同id的dict_code不能相同）
            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add( Predicates.Field<T_Sys_DictItem>( f => f.DI_CODE, Operator.Eq, dictItem.DI_CODE ) );
            pg.Predicates.Add( Predicates.Field<T_Sys_DictItem>( f => f.ID, Operator.Eq, dictItem.ID, true ) );
            int count = dao.Count<T_Sys_DictItem>(pg);
            if ( count > 0 )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "字典项编码重复";
                return me;
            }
            bool result = false;
            try
            {
                result = dao.Update<T_Sys_DictItem>( dictItem );
                me.MsgCode = MsgEntity.MsgCodeEnum.Success;
                me.MsgDes = "字典项编辑成功";
            }
            catch ( Exception ex )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = ex.Message;
            }
            return me;
        }

        /// <summary>
        /// 根据ID删除字典
        /// </summary>
        /// <param name="dictId"></param>
        /// <returns></returns>
        public MsgEntity DelDictItem( string dictItemId )
        {
            MsgEntity me = new MsgEntity();
            if ( string.IsNullOrEmpty( dictItemId ) )
            {
                me.MsgCode = MsgEntity.MsgCodeEnum.Failure;
                me.MsgDes = "字典项ID不存在";
                return me;
            }
            try
            {
                dao.Delete<T_Sys_DictItem>( dictItemId );
                me.MsgCode = MsgEntity.MsgCodeEnum.Success;
                me.MsgDes = "字典项删除成功";
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
