using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Unicellular.DataAccess;

/// <summary>
/// 提供数据层使用，继承了IDataServiceRepository，支持传入sql
/// </summary>
namespace Unicellular.ORM
{
    public interface IDataRepository : IDataServiceRepository
    {
        IDBSession DBSession { get; }

        IEnumerable<T> Get<T>( string sql, dynamic param = null, bool buffered = true ) where T : class;
        IEnumerable<dynamic> Get( string sql, dynamic param = null, bool buffered = true );
        IEnumerable<TReturn> Get<TFirst, TSecond, TReturn>( string sql, Func<TFirst, TSecond, TReturn> map,
            dynamic param = null, IDbTransaction transaction = null, bool buffered = true,
            string splitOn = "Id", int? commandTimeout = null );

        IEnumerable<TReturn> Get<TFirst, TSecond, TThird, TReturn>( string sql, Func<TFirst, TSecond, TThird, TReturn> map,
           dynamic param = null, IDbTransaction transaction = null, bool buffered = true,
           string splitOn = "Id", int? commandTimeout = null );

        SqlMapper.GridReader GetMultiple( string sql, dynamic param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null );


        IEnumerable<T> GetPage<T>( int pageIndex, int pageSize, out long allRowsCount, string sql, dynamic param = null, string allRowsCountSql = null, dynamic allRowsCountParam = null, bool buffered = true ) where T : class;

        Int32 Execute( string sql, dynamic param = null, IDbTransaction transaction = null );
    }
}
