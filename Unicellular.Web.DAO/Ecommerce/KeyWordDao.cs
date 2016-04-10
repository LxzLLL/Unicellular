using System;
using System.Collections.Generic;
using System.Linq;

using DapperExtensions;
using Unicellular.Web.Entity.Ecommerce;
using Unicellular.Web.Entity.System;

namespace Unicellular.Web.DAO.Ecommerce
{
    public class KeyWordDao : DAOBase
    {
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
            string orderby = string.Empty;
            if(sort!=null && sortOrder != null )
            {
                orderby = "ORDER BY "+ sort+" "+ sortOrder;
            }

            string query = @"SELECT  tkeywords.*, tPlatType.DI_NAME AS PLAT_TYPE_NAME
                                        FROM T_EC_KeyWords AS tkeywords
                                        LEFT JOIN T_Sys_DictItem AS tPlatType 
                                        ON tkeywords.PLAT_TYPE=tPlatType.ID
                                        {0}
                                        ";
            query = string.Format( query, orderby );
            var list = this.GetPage<T_EC_KeyWords>(pageNumber,pageSize,out number,query ).ToList();
            return list;
            ////创建谓词
            ////IFieldPredicate predicate = null;
            //PredicateGroup pg = PredicateFactory(keywordEntity);

            //IList<ISort> sorts = new List<ISort>();
            ////创建排序
            //if ( sort == null )
            //{
            //    sorts.Add( new Sort { PropertyName = "PLAT_TYPE", Ascending = sortOrder == "asc" ? true : false } );
            //    sorts.Add( new Sort { PropertyName = "GOODS_TYPE", Ascending = sortOrder == "asc" ? true : false } );
            //}
            //else
            //{
            //    sorts.Add( new Sort { PropertyName = sort, Ascending = sortOrder == "asc" ? true : false } );
            //}
            //List < T_Sys_DictItem > dictitems =this.GetPageList<T_Sys_DictItem>(pageNumber,pageSize,out number,pg,sorts).ToList();
            //return dictitems;
        }
        #endregion

        #region 谓词组装（全AND，如需其他形式，自行定义）
        /// <summary>
        /// 谓词组装，全部字段以And组装
        /// </summary>
        /// <param name="keywordEntity">对象</param>
        /// <returns></returns>
        public PredicateGroup PredicateFactory( T_EC_KeyWords keywordEntity )
        {
            PredicateGroup predicateGroup = null;
            if ( keywordEntity == null )
            {
                return predicateGroup;
            }
            List < IPredicate >  predicates= new List<IPredicate>();
            //ID
            if ( !string.IsNullOrEmpty( keywordEntity.ID ) && !string.IsNullOrEmpty( keywordEntity.ID.Trim() ) )
            {
                predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.ID, Operator.Eq, keywordEntity.ID ) );
            }
            //PLAT_TYPE
            if ( !string.IsNullOrEmpty( keywordEntity.PLAT_TYPE ) && !string.IsNullOrEmpty( keywordEntity.PLAT_TYPE.Trim() ) )
            {
                predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.PLAT_TYPE, Operator.Eq, keywordEntity.PLAT_TYPE ) );
            }
            //KEYWORD_TYPE
            if ( !string.IsNullOrEmpty( keywordEntity.KEYWORD_TYPE ) && !string.IsNullOrEmpty( keywordEntity.KEYWORD_TYPE.Trim() ) )
            {
                predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.KEYWORD_TYPE, Operator.Eq, keywordEntity.KEYWORD_TYPE ) );
            }
            //GOODS_TYPE
            if ( !string.IsNullOrEmpty( keywordEntity.GOODS_TYPE ) && !string.IsNullOrEmpty( keywordEntity.GOODS_TYPE.Trim() ) )
            {
                predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.GOODS_TYPE, Operator.Eq, keywordEntity.GOODS_TYPE ) );
            }
            //KEY_WORD
            if ( !string.IsNullOrEmpty( keywordEntity.KEY_WORD ) && !string.IsNullOrEmpty( keywordEntity.KEY_WORD.Trim() ) )
            {
                predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.KEY_WORD, Operator.Eq, keywordEntity.KEY_WORD ) );
            }
            //KW_CN
            if ( !string.IsNullOrEmpty( keywordEntity.KW_CN ) && !string.IsNullOrEmpty( keywordEntity.KW_CN.Trim() ) )
            {
                predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.KW_CN, Operator.Eq, keywordEntity.KW_CN ) );
            }
            //KW_DES
            if ( !string.IsNullOrEmpty( keywordEntity.KW_DES ) && !string.IsNullOrEmpty( keywordEntity.KW_DES.Trim() ) )
            {
                predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.KW_DES, Operator.Eq, keywordEntity.KW_DES ) );
            }
            //KW_VOLUME
            if ( keywordEntity.KW_VOLUME != null )
            {
                predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.KW_VOLUME, Operator.Eq, keywordEntity.KW_VOLUME ) );
            }
            //C_DATA_TIME
            if ( keywordEntity.C_DATA_TIME != null )
            {
                predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.C_DATA_TIME, Operator.Eq, keywordEntity.C_DATA_TIME ) );
            }
            //C_DATA_UID
            if ( !string.IsNullOrEmpty( keywordEntity.C_DATA_UID ) && !string.IsNullOrEmpty( keywordEntity.C_DATA_UID.Trim() ) )
            {
                predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.C_DATA_UID, Operator.Eq, keywordEntity.C_DATA_UID ) );
            }
            //U_DATA_TIME
            if ( keywordEntity.U_DATA_TIME != null )
            {
                predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.U_DATA_TIME, Operator.Eq, keywordEntity.U_DATA_TIME ) );
            }
            //U_DATA_UID
            if ( !string.IsNullOrEmpty( keywordEntity.U_DATA_UID ) && !string.IsNullOrEmpty( keywordEntity.U_DATA_UID.Trim() ) )
            {
                predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.U_DATA_UID, Operator.Eq, keywordEntity.U_DATA_UID ) );
            }

            //RESERVE1
            if ( !string.IsNullOrEmpty( keywordEntity.RESERVE1 ) && !string.IsNullOrEmpty( keywordEntity.RESERVE1.Trim() ) )
            {
                predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.RESERVE1, Operator.Eq, keywordEntity.RESERVE1 ) );
            }
            //RESERVE2
            if ( !string.IsNullOrEmpty( keywordEntity.RESERVE2 ) && !string.IsNullOrEmpty( keywordEntity.RESERVE2.Trim() ) )
            {
                predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.RESERVE2, Operator.Eq, keywordEntity.RESERVE2 ) );
            }
            //RESERVE3
            if ( !string.IsNullOrEmpty( keywordEntity.RESERVE3 ) && !string.IsNullOrEmpty( keywordEntity.RESERVE3.Trim() ) )
            {
                predicates.Add( Predicates.Field<T_EC_KeyWords>( f => f.RESERVE3, Operator.Eq, keywordEntity.RESERVE3 ) );
            }

            predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = predicates };
            return predicateGroup;
        }
        #endregion
    }
}
