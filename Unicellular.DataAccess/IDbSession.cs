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

        public string ConnKey { get; set; }

        public Database(IDbConnection connection)
        {
            Connection = connection;
        }


        public Database(DatabaseType dbType, string connKey)
        {
            DatabaseType = dbType;
            ConnKey = connKey;
            Connection = SqlConnectionFactory.CreateSqlConnection(dbType, connKey);
        }

    }


    /// <summary>
    /// 数据连接事务的Session接口
    /// </summary>
    public interface IDBSession : IDisposable
    {
        string ConnKey { get; }
        DatabaseType DatabaseType { get; }
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        IDbTransaction Begin(IsolationLevel isolation = IsolationLevel.ReadCommitted);
        void Commit();
        void Rollback();
    }
}
