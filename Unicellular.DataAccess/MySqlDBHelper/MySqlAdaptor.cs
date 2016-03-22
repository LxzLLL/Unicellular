//using MySql.Data.MySqlClient;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.Common;
//using System.Linq;
//using System.Text;

//namespace Unicellular.DataAccess.MySqlDBHelper
//{
//    public class MySqlAdaptor : IDBHelper
//    {
//        private static string _ConnectionStringKey = "DefaultConnection";

//        public MySqlAdaptor(string connKey)
//        {
//            _ConnectionStringKey = connKey;
//        }


//        /// <summary>
//        /// 取得数据库连接
//        /// </summary>
//        /// <param name="DBKey">数据库连接主键</param>
//        /// <returns></returns>
//        public static MySqlConnection GetConnByKey(string connectionStringKey)
//        {
//            ConnectionStringSettings css = ConfigurationManager.ConnectionStrings[connectionStringKey];
//            string constr = css.ConnectionString;
//            MySqlConnection con = new MySqlConnection(constr);
//            return con;
//        }

//        #region 事务



//        /// <summary>
//        /// 开始一个事务
//        /// </summary>
//        public DbTransaction BeginTractionand()
//        {
//            MySqlConnection con = GetConnByKey(_ConnectionStringKey);
//            DbTransaction transaction = MySqlHelper.BeginTransaction(con);
//            return transaction;
//        }

//        /// <summary>
//        /// 开始一个事务
//        /// </summary>
//        public DbTransaction BeginTractionand(string connKey)
//        {
//            MySqlConnection con = GetConnByKey(connKey);
//            DbTransaction transaction = MySqlHelper.BeginTransaction(con);
//            return transaction;
//        }

//        /// <summary>
//        /// 回滚事务
//        /// </summary>
//        public void RollbackTractionand(DbTransaction dbTransaction)
//        {
//            MySqlHelper.endTransactionRollback(dbTransaction);
//        }

//        /// <summary>
//        /// 结束并确认事务
//        /// </summary>
//        public void CommitTractionand(DbTransaction dbTransaction)
//        {
//            MySqlHelper.endTransactionCommit(dbTransaction);
//        }
//        #endregion

//        #region DataSet


//        /// <summary>
//        /// 执行sql语句,ExecuteDataSet 返回DataSet
//        /// </summary>
//        /// <param name="commandText">sql语句</param>
//        public DataSet ExecuteDataSet(string commandText, CommandType commandType)
//        {
//            MySqlConnection con =GetConnByKey(_ConnectionStringKey);
//            DataSet ds = MySqlHelper.ExecuteDataset(con, commandType, commandText);
//            return ds;
//        }

//        /// <summary>
//        /// 执行sql语句,ExecuteDataSet 返回DataSet
//        /// </summary>
//        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
//        /// <param name="commandText">sql语句</param>
//        public DataSet ExecuteDataSet(string connKey, string commandText, CommandType commandType)
//        {
//            MySqlConnection con = GetConnByKey(connKey);
//            DataSet ds = MySqlHelper.ExecuteDataset(con, commandType, commandText);
//            return ds;
//        }

//        /// <summary>
//        /// 执行sql语句,ExecuteDataSet 返回DataSet
//        /// </summary>
//        /// <param name="commandText">sql语句</param>
//        /// <param name="parameterValues">参数</param>
//        public DataSet ExecuteDataSet(string commandText, CommandType commandType, params DbParameter[] parameterValues)
//        {
//            MySqlConnection con = GetConnByKey(_ConnectionStringKey);
//            DataSet ds = MySqlHelper.ExecuteDataset(con, commandType, commandText, parameterValues);
//            return ds;
//        }

//        /// <summary>
//        /// 执行sql语句,ExecuteDataSet 返回DataSet
//        /// </summary>
//        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
//        /// <param name="commandText">sql语句</param>
//        /// <param name="parameterValues">参数</param>
//        public DataSet ExecuteDataSet(string connKey, string commandText, CommandType commandType, params DbParameter[] parameterValues)
//        {
//            MySqlConnection con = GetConnByKey(connKey);
//            DataSet ds = MySqlHelper.ExecuteDataset(con, commandType, commandText, parameterValues);
//            return ds;
//        }


//        #endregion

//        #region ExecuteNonQuery


//        /// <summary>
//        /// 执行sql语句,返回影响的行数
//        /// </summary>
//        /// <param name="commandText">sql语句</param>
//        public int ExecuteNonQuery(string commandText, CommandType commandType)
//        {
//            MySqlConnection con = GetConnByKey(_ConnectionStringKey);
//            int result = MySqlHelper.ExecuteNonQuery(con, commandType, commandText);
//            return result;
//        }

//        /// <summary>
//        /// 执行sql语句,返回影响的行数
//        /// </summary>
//        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
//        /// <param name="commandText">sql语句</param>
//        public int ExecuteNonQuery(string connKey, string commandText, CommandType commandType)
//        {
//            MySqlConnection con = GetConnByKey(connKey);
//            int result = MySqlHelper.ExecuteNonQuery(con, commandType, commandText);
//            return result;
//        }




//        /// <summary>
//        /// 执行sql语句,返回影响的行数
//        /// </summary>
//        /// <param name="trans">事务对象</param>
//        /// <param name="commandText">sql语句</param>
//        public int ExecuteNonQuery(DbTransaction trans, string commandText, CommandType commandType)
//        {
//            int result = MySqlHelper.ExecuteNonQuery(trans, commandType, commandText);
//            return result;
//        }

//        /// <summary>
//        /// 执行sql语句,返回影响的行数
//        /// </summary>
//        /// <param name="commandText">sql语句</param>
//        /// <param name="parameterValues">参数</param>
//        public int ExecuteNonQuery(string commandText, CommandType commandType, params DbParameter[] parameterValues)
//        {
//            MySqlConnection con = GetConnByKey(_ConnectionStringKey);
//            int result = MySqlHelper.ExecuteNonQuery(con, commandType, commandText, parameterValues);
//            return result;
//        }

//        /// <summary>
//        /// 执行sql语句,返回影响的行数
//        /// </summary>
//        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
//        /// <param name="commandText">sql语句</param>
//        /// <param name="parameterValues">参数</param>
//        public int ExecuteNonQuery(string connKey, string commandText, CommandType commandType, params DbParameter[] parameterValues) 
//        {
//            MySqlConnection con = GetConnByKey(connKey);
//            int result = MySqlHelper.ExecuteNonQuery(con, commandType, commandText, parameterValues);
//            return result;
//        }

//        /// <summary>
//        /// 执行sql语句,返回影响的行数
//        /// </summary>
//        /// <param name="trans">事务对象</param>
//        /// <param name="commandText">sql语句</param>
//        /// <param name="parameterValues">参数</param>
//        public int ExecuteNonQuery(DbTransaction trans, string commandText, CommandType commandType, params DbParameter[] parameterValues)
//        {
//            int result = MySqlHelper.ExecuteNonQuery(trans, commandType, commandText, parameterValues);
//            return result;
//        }


//        #endregion

//        #region IDataReader

//        /// <summary>
//        /// 执行sql语句,ExecuteReader 返回IDataReader
//        /// </summary>   
//        /// <param name="commandText">sql语句</param>
//        public IDataReader ExecuteReader(string commandText, CommandType commandType)
//        {
//            MySqlConnection con = GetConnByKey(_ConnectionStringKey);
//            IDataReader dr = MySqlHelper.ExecuteReader(con, commandType, commandText);
//            return dr;
//        }

//        /// <summary>
//        /// 执行sql语句,ExecuteReader 返回IDataReader
//        /// </summary> 
//        /// <param name="commandText">sql语句</param>
//        /// <param name="parameterValues">参数</param>
//        public IDataReader ExecuteReader(string commandText, CommandType commandType, params DbParameter[] parameterValues)
//        {
//            MySqlConnection con = GetConnByKey(_ConnectionStringKey);
//            IDataReader dr = MySqlHelper.ExecuteReader(con, commandType, commandText, parameterValues);
//            return dr;
//        }

//        /// <summary>
//        /// 执行sql语句,ExecuteReader 返回IDataReader
//        /// </summary>
//        /// <param name="connectionStringKey">数据库连接字符串的Key</param>        
//        /// <param name="commandText">sql语句</param>
//        public IDataReader ExecuteReader(string connKey, string commandText, CommandType commandType)
//        {
//            MySqlConnection con = GetConnByKey(connKey);
//            IDataReader dr = MySqlHelper.ExecuteReader(con, commandType, commandText);
//            return dr;
//        }

//        /// <summary>
//        /// 执行sql语句,ExecuteReader 返回IDataReader
//        /// </summary>
//        /// <param name="connectionStringKey">数据库连接字符串的Key</param>        
//        /// <param name="commandText">sql语句</param>
//        /// <param name="parameterValues">参数</param>
//        public IDataReader ExecuteReader(string connKey, string commandText, CommandType commandType, params DbParameter[] parameterValues)
//        {
//            MySqlConnection con = GetConnByKey(connKey);
//            IDataReader dr = MySqlHelper.ExecuteReader(con, commandType, commandText, parameterValues);
//            return dr;
//        }



//        #endregion

//        #region ExecuteScalar
//        /// <summary>
//        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
//        /// </summary>
//        /// <param name="commandText">sql语句</param>
//        public object ExecuteScalar(string commandText, CommandType commandType)
//        {
//            MySqlConnection con = GetConnByKey(_ConnectionStringKey);
//            object result = MySqlHelper.ExecuteScalar(con, commandType, commandText);
//            return result;
//        }


//        /// <summary>
//        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
//        /// </summary>
//        /// <param name="commandText">sql语句</param>
//        /// <param name="parameterValues">参数</param>
//        public object ExecuteScalar(string commandText, CommandType commandType, params DbParameter[] parameterValues)
//        {
//            MySqlConnection con = GetConnByKey(_ConnectionStringKey);
//            object result = MySqlHelper.ExecuteScalar(con, commandType, commandText, parameterValues);
//            return result;
//        }

//        /// <summary>
//        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
//        /// </summary>
//        /// <param name="trans">事务</param>
//        /// <param name="commandText">sql语句</param>
//        public object ExecuteScalar(DbTransaction trans, string commandText, CommandType commandType)
//        {
//            object result = MySqlHelper.ExecuteScalar(trans, commandType, commandText);
//            return result;
//        }

//        /// <summary>
//        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
//        /// </summary>
//        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
//        /// <param name="commandText">sql语句</param>
//        public object ExecuteScalar(string connKey, string commandText, CommandType commandType)
//        {
//            MySqlConnection con = GetConnByKey(connKey);
//            object result = MySqlHelper.ExecuteScalar(con, commandType, commandText);
//            return result;
//        }


//        /// <summary>
//        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
//        /// </summary>
//        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
//        /// <param name="commandText">sql语句</param>
//        /// <param name="parameterValues">参数</param>
//        public object ExecuteScalar(string connKey, string commandText, CommandType commandType, params DbParameter[] parameterValues)
//        {
//            MySqlConnection con = GetConnByKey(connKey);
//            object result = MySqlHelper.ExecuteScalar(con, commandType, commandText, parameterValues);
//            return result;
//        }

//        /// <summary>
//        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
//        /// </summary>
//        /// <param name="trans">事务param>
//        /// <param name="commandText">sql语句</param>
//        /// <param name="parameterValues">参数</param>
//        public object ExecuteScalar(DbTransaction trans, string commandText, CommandType commandType, params DbParameter[] parameterValues)
//        {
//            object result = MySqlHelper.ExecuteScalar(trans, commandType, commandText, parameterValues);
//            return result;
//        }

//        #endregion

//        /// <summary>
//        /// 生成分页SQL语句
//        /// </summary>
//        /// <param name="pageIndex">page索引</param>
//        /// <param name="pageSize">page大小</param>
//        /// <param name="selectSql">查询语句</param>
//        /// <param name="SqlCount">查询总数的语句</param>
//        /// <param name="orderBy">排序</param>
//        /// <returns></returns>
//        public string GetPagingSql(int pageIndex, int pageSize, string selectSql, string SqlCount, string orderBy)
//        {
//            return PageHelper.GetPagingSql(pageIndex, pageSize, selectSql, SqlCount, orderBy);
//        }


//    }

//}