using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
//using System.Data.OracleClient;
using Oracle.DataAccess.Client;
using System.Linq;
using System.Text;

namespace Unicellular.DataAccess.OracleDBHelper
{
    public class OracleAdaptor : IDBHelper
    {


  private static string _ConnectionStringKey = "DefaultConnection";

  public OracleAdaptor(string connKey)
        {
            _ConnectionStringKey = connKey;
        }


        /// <summary>
        /// 取得数据库连接
        /// </summary>
        /// <param name="DBKey">数据库连接主键</param>
        /// <returns></returns>
        public static OracleConnection GetConnByKey(string connectionStringKey)
        {
            ConnectionStringSettings css = ConfigurationManager.ConnectionStrings[connectionStringKey];
            string constr = css.ConnectionString;
            OracleConnection con = new OracleConnection(constr);
            return con;
        }

        #region 事务



        /// <summary>
        /// 开始一个事务
        /// </summary>
        public DbTransaction BeginTractionand()
        {
            OracleConnection con = GetConnByKey(_ConnectionStringKey);
            DbTransaction transaction = OracleHelper.BeginTransaction(con);
            return transaction;
        }

        /// <summary>
        /// 开始一个事务
        /// </summary>
        public DbTransaction BeginTractionand(string connKey)
        {
            OracleConnection con = GetConnByKey(connKey);
            DbTransaction transaction = OracleHelper.BeginTransaction(con);
            return transaction;
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTractionand(DbTransaction dbTransaction)
        {
            OracleHelper.endTransactionRollback(dbTransaction);
        }

        /// <summary>
        /// 结束并确认事务
        /// </summary>
        public void CommitTractionand(DbTransaction dbTransaction)
        {
            OracleHelper.endTransactionCommit(dbTransaction);
        }
        #endregion

        #region DataSet


        /// <summary>
        /// 执行sql语句,ExecuteDataSet 返回DataSet
        /// </summary>
        /// <param name="commandText">sql语句</param>
        public DataSet ExecuteDataSet(string commandText, CommandType commandType)
        {
            OracleConnection con =GetConnByKey(_ConnectionStringKey);
            DataSet ds = OracleHelper.ExecuteDataset(con, commandType, commandText);
            return ds;
        }

        /// <summary>
        /// 执行sql语句,ExecuteDataSet 返回DataSet
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
        /// <param name="commandText">sql语句</param>
        public DataSet ExecuteDataSet(string connKey, string commandText, CommandType commandType)
        {
            OracleConnection con = GetConnByKey(connKey);
            DataSet ds = OracleHelper.ExecuteDataset(con, commandType, commandText);
            return ds;
        }

        /// <summary>
        /// 执行sql语句,ExecuteDataSet 返回DataSet
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public DataSet ExecuteDataSet(string commandText, CommandType commandType, params DbParameter[] parameterValues)
        {
            OracleConnection con = GetConnByKey(_ConnectionStringKey);
            DataSet ds = OracleHelper.ExecuteDataset(con, commandType, commandText, parameterValues);
            return ds;
        }

        /// <summary>
        /// 执行sql语句,ExecuteDataSet 返回DataSet
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public DataSet ExecuteDataSet(string connKey, string commandText, CommandType commandType, params DbParameter[] parameterValues)
        {
            OracleConnection con = GetConnByKey(connKey);
            DataSet ds = OracleHelper.ExecuteDataset(con, commandType, commandText, parameterValues);
            return ds;
        }


        #endregion

        #region ExecuteNonQuery


        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="commandText">sql语句</param>
        public int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            OracleConnection con = GetConnByKey(_ConnectionStringKey);
            int result = OracleHelper.ExecuteNonQuery(con, commandType, commandText);
            return result;
        }

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
        /// <param name="commandText">sql语句</param>
        public int ExecuteNonQuery(string connKey, string commandText, CommandType commandType)
        {
            OracleConnection con = GetConnByKey(connKey);
            int result = OracleHelper.ExecuteNonQuery(con, commandType, commandText);
            return result;
        }




        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="commandText">sql语句</param>
        public int ExecuteNonQuery(DbTransaction trans, string commandText, CommandType commandType)
        {
            int result = OracleHelper.ExecuteNonQuery(trans, commandType, commandText);
            return result;
        }

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public int ExecuteNonQuery(string commandText, CommandType commandType, params DbParameter[] parameterValues)
        {
            OracleConnection con = GetConnByKey(_ConnectionStringKey);
            int result = OracleHelper.ExecuteNonQuery(con, commandType, commandText, parameterValues);
            return result;
        }

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public int ExecuteNonQuery(string connKey, string commandText, CommandType commandType, params DbParameter[] parameterValues) 
        {
            OracleConnection con = GetConnByKey(connKey);
            int result = OracleHelper.ExecuteNonQuery(con, commandType, commandText, parameterValues);
            return result;
        }

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public int ExecuteNonQuery(DbTransaction trans, string commandText, CommandType commandType, params DbParameter[] parameterValues)
        {
            int result = OracleHelper.ExecuteNonQuery(trans, commandType, commandText, parameterValues);
            return result;
        }


        #endregion

        #region IDataReader

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>   
        /// <param name="commandText">sql语句</param>
        public IDataReader ExecuteReader(string commandText, CommandType commandType)
        {
            OracleConnection con = GetConnByKey(_ConnectionStringKey);
            IDataReader dr = OracleHelper.ExecuteReader(con, commandType, commandText);
            return dr;
        }

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary> 
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public IDataReader ExecuteReader(string commandText, CommandType commandType, params DbParameter[] parameterValues)
        {
            OracleConnection con = GetConnByKey(_ConnectionStringKey);
            IDataReader dr = OracleHelper.ExecuteReader(con, commandType, commandText, parameterValues);
            return dr;
        }

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>        
        /// <param name="commandText">sql语句</param>
        public IDataReader ExecuteReader(string connKey, string commandText, CommandType commandType)
        {
            OracleConnection con = GetConnByKey(connKey);
            IDataReader dr = OracleHelper.ExecuteReader(con, commandType, commandText);
            return dr;
        }

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>        
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public IDataReader ExecuteReader(string connKey, string commandText, CommandType commandType, params DbParameter[] parameterValues)
        {
            OracleConnection con = GetConnByKey(connKey);
            IDataReader dr = OracleHelper.ExecuteReader(con, commandType, commandText, parameterValues);
            return dr;
        }



        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        public object ExecuteScalar(string commandText, CommandType commandType)
        {
            OracleConnection con = GetConnByKey(_ConnectionStringKey);
            object result = OracleHelper.ExecuteScalar(con, commandType, commandText);
            return result;
        }


        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public object ExecuteScalar(string commandText, CommandType commandType, params DbParameter[] parameterValues)
        {
            OracleConnection con = GetConnByKey(_ConnectionStringKey);
            object result = OracleHelper.ExecuteScalar(con, commandType, commandText, parameterValues);
            return result;
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="commandText">sql语句</param>
        public object ExecuteScalar(DbTransaction trans, string commandText, CommandType commandType)
        {
            object result = OracleHelper.ExecuteScalar(trans, commandType, commandText);
            return result;
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
        /// <param name="commandText">sql语句</param>
        public object ExecuteScalar(string connKey, string commandText, CommandType commandType)
        {
            OracleConnection con = GetConnByKey(connKey);
            object result = OracleHelper.ExecuteScalar(con, commandType, commandText);
            return result;
        }


        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public object ExecuteScalar(string connKey, string commandText, CommandType commandType, params DbParameter[] parameterValues)
        {
            OracleConnection con = GetConnByKey(connKey);
            object result = OracleHelper.ExecuteScalar(con, commandType, commandText, parameterValues);
            return result;
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务param>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public object ExecuteScalar(DbTransaction trans, string commandText, CommandType commandType, params DbParameter[] parameterValues)
        {
            object result = OracleHelper.ExecuteScalar(trans, commandType, commandText, parameterValues);
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
        public string GetPagingSql(int pageIndex, int pageSize, string selectSql, string sqlCount, string orderBy)
        {
            return PageHelper.GetOraclePagingSql(pageIndex, pageSize, selectSql, sqlCount, orderBy);
        }


    }

}