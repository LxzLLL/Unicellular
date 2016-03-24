using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
//using Oracle.DataAccess.Client;
using System.Data.OracleClient;

namespace Unicellular.DataAccess.OracleDBHelper
{
    public class OracleAdaptor : IDBAdaptor
    {
        private OracleConnection _connection;

        public OracleAdaptor( IDbConnection connection )
        {
            _connection = (OracleConnection)connection;
        }

        #region 事务
        /// <summary>
        /// 开始一个事务
        /// </summary>
        public DbTransaction BeginTractionand()
        {
            DbTransaction transaction = OracleHelper.BeginTransaction(this._connection);
            return transaction;
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTractionand( DbTransaction dbTransaction )
        {
            OracleHelper.endTransactionRollback( (OracleTransaction)dbTransaction );
        }

        /// <summary>
        /// 结束并确认事务
        /// </summary>
        public void CommitTractionand( DbTransaction dbTransaction )
        {
            OracleHelper.endTransactionCommit( ( OracleTransaction )dbTransaction );
        }
        #endregion

        #region DataSet
        /// <summary>
        /// 执行sql语句,ExecuteDataSet 返回DataSet
        /// </summary>
        /// <param name="commandText">sql语句</param>
        public DataSet ExecuteDataSet( string commandText, CommandType commandType )
        {
            DataSet ds = OracleHelper.ExecuteDataset(this._connection, commandType, commandText);
            return ds;
        }

        /// <summary>
        /// 执行sql语句,ExecuteDataSet 返回DataSet
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public DataSet ExecuteDataSet( string commandText, CommandType commandType, params DbParameter[ ] parameterValues )
        {
            DataSet ds = OracleHelper.ExecuteDataset(this._connection, commandType, commandText, (OracleParameter[])parameterValues);
            return ds;
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="commandText">sql语句</param>
        public int ExecuteNonQuery( string commandText, CommandType commandType )
        {
            int result = OracleHelper.ExecuteNonQuery(this._connection, commandType, commandText);
            return result;
        }

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="commandText">sql语句</param>
        public int ExecuteNonQuery( DbTransaction trans, string commandText, CommandType commandType )
        {
            int result = OracleHelper.ExecuteNonQuery((OracleTransaction)trans, commandType, commandText);
            return result;
        }

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public int ExecuteNonQuery( string commandText, CommandType commandType, params DbParameter[ ] parameterValues )
        {
            int result = OracleHelper.ExecuteNonQuery(this._connection, commandType, commandText, (OracleParameter[])parameterValues);
            return result;
        }

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public int ExecuteNonQuery( DbTransaction trans, string commandText, CommandType commandType, params DbParameter[ ] parameterValues )
        {
            int result = OracleHelper.ExecuteNonQuery((OracleTransaction)trans, commandType, commandText, (OracleParameter[])parameterValues);
            return result;
        }
        #endregion

        #region IDataReader
        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>   
        /// <param name="commandText">sql语句</param>
        public IDataReader ExecuteReader( string commandText, CommandType commandType )
        {
            IDataReader dr = OracleHelper.ExecuteReader(this._connection, commandType, commandText);
            return dr;
        }

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary> 
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public IDataReader ExecuteReader( string commandText, CommandType commandType, params DbParameter[ ] parameterValues )
        {
            IDataReader dr = OracleHelper.ExecuteReader(this._connection, commandType, commandText, (OracleParameter[])parameterValues);
            return dr;
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        public object ExecuteScalar( string commandText, CommandType commandType )
        {
            object result = OracleHelper.ExecuteScalar(this._connection, commandType, commandText);
            return result;
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public object ExecuteScalar( string commandText, CommandType commandType, params DbParameter[ ] parameterValues )
        {
            object result = OracleHelper.ExecuteScalar(this._connection, commandType, commandText, (OracleParameter[])parameterValues);
            return result;
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="commandText">sql语句</param>
        public object ExecuteScalar( DbTransaction trans, string commandText, CommandType commandType )
        {
            object result = OracleHelper.ExecuteScalar(trans, commandType, commandText);
            return result;
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务param>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public object ExecuteScalar( DbTransaction trans, string commandText, CommandType commandType, params DbParameter[ ] parameterValues )
        {
            object result = OracleHelper.ExecuteScalar((OracleTransaction)trans, commandType, commandText, (OracleParameter[])parameterValues);
            return result;
        }

        #endregion

        /// <summary>
        /// 生成分页SQL语句
        /// </summary>
        /// <param name="pageIndex">page索引</param>
        /// <param name="pageSize">page大小</param>
        /// <param name="selectSql">查询语句</param>
        /// <param name="sqlCount">查询总数的语句</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public string GetPagingSql( int pageIndex, int pageSize, string selectSql, string sqlCount, string orderBy )
        {
            return PageHelper.GetOraclePagingSql( pageIndex, pageSize, selectSql, sqlCount, orderBy );
        }


    }

}