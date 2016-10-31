using System;
using System.Data;

namespace Unicellular.DataAccess
{
    /// <summary>
    /// 数据库连接事务的Session对象
    /// </summary>
    public class DBSessionBase : IDBSession 
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private readonly DatabaseType _databaseType;
        private readonly string _connKey;


        public DBSessionBase( IDatabase Database )
        {
            _connection = Database.Connection;
            _databaseType = Database.DatabaseType;
            _connKey = Database.ConnKey;
        }

        public DatabaseType DatabaseType
        {
            get { return _databaseType; }
        }

        public string ConnKey
        {
            get { return _connKey; }
        }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public IDbConnection Connection
        {
            get { return _connection; }
        }

        /// <summary>
        /// 数据库事务对象
        /// </summary>
        public IDbTransaction Transaction
        {
            get { return _transaction; }
        }

       

        /// <summary>
        /// 开启会话
        /// </summary>
        /// <param name="isolation"></param>
        /// <returns></returns>
        public IDbTransaction Begin(IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction(isolation);
            return _transaction;
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        public void Commit()
        {
            _transaction.Commit();
            _transaction = null;
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void Rollback()
        {
            _transaction.Rollback();
            _transaction = null;
        }

        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                    _transaction = null;
                }
                _connection.Close();
                _connection = null;
            }
            GC.SuppressFinalize(this);
        }


    }
}
