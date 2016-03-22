using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Unicellular.DataAccess;

namespace Unicellular.DataAccess.SqlDBHelper
{
    /// <summary>
    /// 数据库访问适配器
    /// </summary>
    public class DBAdaptor : IDBHelper
    {
        private static string _ConnectionStringKey = "DefaultConnection";

        public DBAdaptor(string connKey)
        {
            _ConnectionStringKey = connKey;
        }


        /// <summary>
        /// 取得数据库连接
        /// </summary>
        /// <param name="DBKey">数据库连接主键</param>
        /// <returns></returns>
        public static SqlConnection GetConnByKey(string connectionStringKey)
        {
            ConnectionStringSettings css = ConfigurationManager.ConnectionStrings[connectionStringKey];
            string constr = css.ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            return con;
        }

        #region 事务



        /// <summary>
        /// 开始一个事务
        /// </summary>
        public DbTransaction BeginTractionand()
        {
            SqlConnection con = GetConnByKey(_ConnectionStringKey);
            DbTransaction transaction = SQLHelper.BeginTransaction(con);
            return transaction;
        }

        /// <summary>
        /// 开始一个事务
        /// </summary>
        public DbTransaction BeginTractionand(string connKey)
        {
            SqlConnection con = GetConnByKey(connKey);
            DbTransaction transaction = SQLHelper.BeginTransaction(con);
            return transaction;
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTractionand(DbTransaction dbTransaction)
        {
            SQLHelper.endTransactionRollback(dbTransaction);
        }

        /// <summary>
        /// 结束并确认事务
        /// </summary>
        public void CommitTractionand(DbTransaction dbTransaction)
        {
            SQLHelper.endTransactionCommit(dbTransaction);
        }
        #endregion

        #region DataSet


        /// <summary>
        /// 执行sql语句,ExecuteDataSet 返回DataSet
        /// </summary>
        /// <param name="commandText">sql语句</param>
        public DataSet ExecuteDataSet(string commandText, CommandType commandType)
        {
            SqlConnection con =GetConnByKey(_ConnectionStringKey);
            DataSet ds = SQLHelper.ExecuteDataset(con, commandType, commandText);
            return ds;
        }

        /// <summary>
        /// 执行sql语句,ExecuteDataSet 返回DataSet
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
        /// <param name="commandText">sql语句</param>
        public DataSet ExecuteDataSet(string connKey, string commandText, CommandType commandType)
        {
            SqlConnection con = GetConnByKey(connKey);
            DataSet ds = SQLHelper.ExecuteDataset(con, commandType, commandText);
            return ds;
        }

        /// <summary>
        /// 执行sql语句,ExecuteDataSet 返回DataSet
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public DataSet ExecuteDataSet(string commandText, CommandType commandType, params DbParameter[] parameterValues)
        {
            SqlConnection con = GetConnByKey(_ConnectionStringKey);
            DataSet ds = SQLHelper.ExecuteDataset(con, commandType, commandText, parameterValues);
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
            SqlConnection con = GetConnByKey(connKey);
            DataSet ds = SQLHelper.ExecuteDataset(con, commandType, commandText, parameterValues);
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
            SqlConnection con = GetConnByKey(_ConnectionStringKey);
            int result = SQLHelper.ExecuteNonQuery(con, commandType, commandText);
            return result;
        }

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
        /// <param name="commandText">sql语句</param>
        public int ExecuteNonQuery(string connKey, string commandText, CommandType commandType)
        {
            SqlConnection con = GetConnByKey(connKey);
            int result = SQLHelper.ExecuteNonQuery(con, commandType, commandText);
            return result;
        }




        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="commandText">sql语句</param>
        public int ExecuteNonQuery(DbTransaction trans, string commandText, CommandType commandType)
        {
            int result = SQLHelper.ExecuteNonQuery(trans, commandType, commandText);
            return result;
        }

        /// <summary>
        /// 执行sql语句,返回影响的行数
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public int ExecuteNonQuery(string commandText, CommandType commandType, params DbParameter[] parameterValues)
        {
            SqlConnection con = GetConnByKey(_ConnectionStringKey);
            int result = SQLHelper.ExecuteNonQuery(con, commandType, commandText, parameterValues);
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
            SqlConnection con = GetConnByKey(connKey);
            int result = SQLHelper.ExecuteNonQuery(con, commandType, commandText, parameterValues);
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
            int result = SQLHelper.ExecuteNonQuery(trans, commandType, commandText, parameterValues);
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
            SqlConnection con = GetConnByKey(_ConnectionStringKey);
            IDataReader dr = SQLHelper.ExecuteReader(con, commandType, commandText);
            return dr;
        }

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary> 
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public IDataReader ExecuteReader(string commandText, CommandType commandType, params DbParameter[] parameterValues)
        {
            SqlConnection con = GetConnByKey(_ConnectionStringKey);
            IDataReader dr = SQLHelper.ExecuteReader(con, commandType, commandText, parameterValues);
            return dr;
        }

        /// <summary>
        /// 执行sql语句,ExecuteReader 返回IDataReader
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>        
        /// <param name="commandText">sql语句</param>
        public IDataReader ExecuteReader(string connKey, string commandText, CommandType commandType)
        {
            SqlConnection con = GetConnByKey(connKey);
            IDataReader dr = SQLHelper.ExecuteReader(con, commandType, commandText);
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
            SqlConnection con = GetConnByKey(connKey);
            IDataReader dr = SQLHelper.ExecuteReader(con, commandType, commandText, parameterValues);
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
            SqlConnection con = GetConnByKey(_ConnectionStringKey);
            object result = SQLHelper.ExecuteScalar(con, commandType, commandText);
            return result;
        }


        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        public object ExecuteScalar(string commandText, CommandType commandType, params DbParameter[] parameterValues)
        {
            SqlConnection con = GetConnByKey(_ConnectionStringKey);
            object result = SQLHelper.ExecuteScalar(con, commandType, commandText, parameterValues);
            return result;
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="commandText">sql语句</param>
        public object ExecuteScalar(DbTransaction trans, string commandText, CommandType commandType)
        {
            object result = SQLHelper.ExecuteScalar(trans, commandType, commandText);
            return result;
        }

        /// <summary>
        /// 执行sql语句,ExecuteScalar 返回第一行第一列的值
        /// </summary>
        /// <param name="connectionStringKey">数据库连接字符串的Key</param>
        /// <param name="commandText">sql语句</param>
        public object ExecuteScalar(string connKey, string commandText, CommandType commandType)
        {
            SqlConnection con = GetConnByKey(connKey);
            object result = SQLHelper.ExecuteScalar(con, commandType, commandText);
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
            SqlConnection con = GetConnByKey(connKey);
            object result = SQLHelper.ExecuteScalar(con, commandType, commandText, parameterValues);
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
            object result = SQLHelper.ExecuteScalar(trans, commandType, commandText, parameterValues);
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
            return PageHelper.GetPagingSql(pageIndex, pageSize, selectSql, sqlCount, orderBy);
        }


    }

}