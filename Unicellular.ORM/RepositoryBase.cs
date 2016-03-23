using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using DapperExtensions;
using Unicellular.DataAccess;

/// <summary>
/// IDataRepository的实现类，数据层继承Unicellular.ORM中的 RepositoryBase类
/// </summary>
namespace Unicellular.ORM
{
    public class RepositoryBase : RepositoryServiceBase, IDataRepository
    {
        public RepositoryBase()
        {
        }

        public new void SetDBSession( IDBSession dbSession )
        {
            base.SetDBSession( dbSession );
        }


        public RepositoryBase( IDBSession dbSession )
            : base( dbSession )
        {
        }



        /// <summary>
        /// 根据条件筛选出数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public IEnumerable<T> Get<T>( string sql, dynamic param = null, bool buffered = true ) where T : class
        {

            return DBSession.Connection.Query<T>( sql, param as object, DBSession.Transaction, buffered );
        }

        /// <summary>
        /// 根据条件筛选数据集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> Get( string sql, dynamic param = null, bool buffered = true )
        {
            return DBSession.Connection.Query( sql, param as object, DBSession.Transaction, buffered );
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="allRowsCount"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="allRowsCountSql"></param>
        /// <param name="allRowsCountParam"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public IEnumerable<T> GetPage<T>( int pageIndex, int pageSize, out long allRowsCount, string sql, dynamic param = null, string allRowsCountSql = null, dynamic allRowsCountParam = null, bool buffered = true ) where T : class
        {
            IEnumerable<T> entityList = DBSession.Connection.GetPage<T>(pageIndex, pageSize, out allRowsCount, sql, param as object, allRowsCountSql, null, null, buffered, databaseType: DBSession.DatabaseType);
            return entityList;
        }

        /// <summary>
        /// 根据表达式筛选
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<TReturn> Get<TFirst, TSecond, TReturn>( string sql, Func<TFirst, TSecond, TReturn> map,
            dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id",
            int? commandTimeout = null )
        {
            return DBSession.Connection.Query( sql, map, param as object, transaction, buffered, splitOn );
        }

        /// <summary>
        /// 根据表达式筛选
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<TReturn> Get<TFirst, TSecond, TThird, TReturn>( string sql, Func<TFirst, TSecond, TThird, TReturn> map,
            dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id",
            int? commandTimeout = null )
        {
            return DBSession.Connection.Query( sql, map, param as object, transaction, buffered, splitOn );
        }

        /// <summary>
        /// 获取多实体集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public SqlMapper.GridReader GetMultiple( string sql, dynamic param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null )
        {
            return DBSession.Connection.QueryMultiple( sql, param as object, transaction, commandTimeout, commandType );
        }

        /// <summary>
        /// 执行sql操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Execute( string sql, dynamic param = null, IDbTransaction transaction = null )
        {
            return DBSession.Connection.Execute( sql, param as object, transaction );
        }
    }
}
