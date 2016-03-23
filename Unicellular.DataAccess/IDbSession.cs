using System;
using System.Data;

namespace Unicellular.DataAccess
{

    /// <summary>
    /// 数据库接口
    /// </summary>
    public interface IDatabase
    {
        IDbConnection Connection { get; }

        DatabaseType DatabaseType { get; }

        string ConnKey { get; }
    }

    /// <summary>
    /// 数据库类对象
    /// </summary>
    public class Database : IDatabase
    {
        public IDbConnection Connection { get; private set; }

        public DatabaseType DatabaseType { get; private set; }

        public string ConnKey { get; private set; }

        public Database(DatabaseType dbType, string connKey)
        {
            DatabaseType = dbType;
            ConnKey = connKey;
            Connection = SqlConnectionFactory.CreateSqlConnection(dbType, connKey);
        }

    }


    /// <summary>
    /// 数据连接事务的Session接口，针对DapperExtension
    /// </summary>
    public interface IDBSession : IDisposable
    {
        /// <summary>
        /// 链接字符串key值
        /// </summary>
        string ConnKey { get; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        DatabaseType DatabaseType { get; }
        /// <summary>
        /// 数据库链接
        /// </summary>
        IDbConnection Connection { get; }
        /// <summary>
        /// 数据库事务
        /// </summary>
        IDbTransaction Transaction { get; }
        /// <summary>
        /// 事务开始
        /// </summary>
        /// <param name="isolation">事务级别</param>
        /// <returns></returns>
        IDbTransaction Begin(IsolationLevel isolation = IsolationLevel.ReadCommitted);
        /// <summary>
        /// 事务提交
        /// </summary>
        void Commit();
        /// <summary>
        /// 事务回滚
        /// </summary>
        void Rollback();
    }
}
