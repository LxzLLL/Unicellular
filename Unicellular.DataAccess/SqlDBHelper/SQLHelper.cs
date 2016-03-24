//////////////////////////////////////////////////////////////////////////////////////////////////
//目的：封装数据库的基本操作
//方法： static SqlConnection GetDataCon　根据数据库名称返回连接　　
//　　　 static SqlTransaction BeginTransaction　开始对应数据库的事务，返回事务实例
//　　　 static int ExecuteNonQuery　执行ＳＱＬ语句或者存储过程 ,不返回参数
//       static DataSet ExecuteDataset 执行ＳＱＬ语句或者存储过程，返回dataset
//       static SqlDataReader ExecuteReader 执行ＳＱＬ语句或者存储过程，返回SqlDataReader
//       static object ExecuteScalar 执行ＳＱＬ语句或者存储过程，返回object
//  
/////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Data;
using System.Data.SqlClient;

using System.Data.Common;

namespace Unicellular.DataAccess.SqlDBHelper
{
    /// <summary>
    /// 封装数据库的基本操作
    /// </summary>
    /// <remarks>    
    public class SqlHelper
    {
        #region 私有方法和工具
        /// <summary>
        /// sql准备
        /// </summary>
        /// <param name="command">Sql命令</param>
        /// <param name="connection">sql连接</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandType">类型，文本还是存储过程</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandParameters">参数</param>
        /// <param name="mustCloseConnection">是否要关闭</param>
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            // 打开连接
            if ( connection.State != ConnectionState.Open )
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }
            command.Connection = connection;
            command.CommandText = commandText;
            // 如果存在事务，则绑定事务
            if ( transaction != null )
            {
                command.Transaction = transaction;
            }
            command.CommandType = commandType;
            // 如果存在参数，则绑定参数
            if ( commandParameters != null )
            {
                AttachParameters( command, commandParameters );
            }
            return;
        }

        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="command">sql命令</param>
        /// <param name="commandParameters">命令参数</param>
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }
        #endregion

        #region transaction 事务处理

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="Iso">指定连接的事务锁定行为</param>
        /// <returns>当前事务</returns>  
        public static SqlTransaction BeginTransaction(SqlConnection conn, IsolationLevel Iso)
        {
            conn.Open();
            return conn.BeginTransaction(Iso);
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <returns>当前事务</returns>
        public static SqlTransaction BeginTransaction(SqlConnection conn)
        {
            conn.Open();
            return conn.BeginTransaction();
        }

        /// <summary>
        /// 结束事务，确认操作
        /// </summary>
        /// <param name="Transaction">要结束的事务</param>
        public static void endTransactionCommit( SqlTransaction Transaction )
        {
            SqlConnection con = Transaction.Connection;
            Transaction.Commit();
            con.Close();
        }

        /// <summary>
        /// 结束事务，回滚操作
        /// </summary>
        /// <param name="Transaction">要结束的事务</param>
        public static void endTransactionRollback( SqlTransaction Transaction )
        {
            SqlConnection con = Transaction.Connection;
            Transaction.Rollback();
            con.Close();
        }

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        ///  执行ＳＱＬ语句或者存储过程 ,不返回参数,只返回影响行数(通用)
        /// </summary>
        /// <param name="transaction">语句所在的事务</param>
        /// <param name="commandType">ＳＱＬ语句类型</param>
        /// <param name="commandText">ＳＱＬ语句或者存储过程名</param>
        /// <param name="commandParameters">ＳＱＬ语句或者存储过程参数</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteNonQuery( SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            //要检查参数  
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            int retval = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        ///  执行ＳＱＬ语句或者存储过程 ,不返回参数,只返回影响行数
        /// </summary>
        /// <param name="transaction">语句所在的事务</param>
        /// <param name="commandType">ＳＱＬ语句类型</param>
        /// <param name="commandText">ＳＱＬ语句或者存储过程名</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteNonQuery( SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(transaction, commandType, commandText, null);
        }

        /// <summary>
        ///  执行ＳＱＬ语句或者存储过程 ,不返回参数,只返回影响行数(通用)
        /// </summary>
        /// <param name="connection">要执行ＳＱＬ语句的连接</param>
        /// <param name="commandType">ＳＱＬ语句类型</param>
        /// <param name="commandText">ＳＱＬ语句或者存储过程名</param>
        /// <param name="commandParameters">ＳＱＬ语句或者存储过程参数</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            int retval = 0;
            //要检查参数
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
            retval = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }

        /// <summary>
        ///  执行ＳＱＬ语句或者存储过程 ,不返回参数,只返回影响行数
        /// </summary>
        /// <param name="connection">要执行ＳＱＬ语句的连接</param>
        /// <param name="commandType">ＳＱＬ语句类型</param>
        /// <param name="commandText">ＳＱＬ语句或者存储过程名</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteNonQuery(connection, commandType, commandText, null);
        }

        #endregion

        #region ExecuteDataset

        /// <summary>
        /// 执行ＳＱＬ语句或者存储过程 ,返回参数dataset(通用)
        /// </summary>
        /// <param name="connection">要执行ＳＱＬ语句的连接</param>
        /// <param name="commandType">ＳＱＬ语句类型</param>
        /// <param name="commandText">ＳＱＬ语句或者存储过程名</param>
        /// <param name="Table"> 填充的表名 </param>
        /// <param name="commandParameters">ＳＱＬ语句或者存储过程参数</param>
        /// <returns>执行结果集</returns>
        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                if (mustCloseConnection)
                    connection.Close();
                return ds;
            }
        }

        /// <summary>
        /// 执行ＳＱＬ语句或者存储过程 ,返回参数dataset
        /// </summary>
        /// <param name="connection">要执行ＳＱＬ语句的连接</param>
        /// <param name="commandType">ＳＱＬ语句类型</param>
        /// <param name="commandText">ＳＱＬ语句或者存储过程名</param>
        /// <param name="Table">填充的表名</param>
        /// <returns>执行结果集</returns>　
        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText)
        {

            return ExecuteDataset(connection, commandType, commandText, null);
        }
        #endregion

        #region ExecuteReader

        //通用
        private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, bool isClose)
        {
            bool mustCloseConnection = false;
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            SqlDataReader dataReader = null;
            if (isClose)
            {
                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            else
            {
                dataReader = cmd.ExecuteReader();
            }
            bool canClear = true;
            foreach (DbParameter commandParameter in cmd.Parameters)
            {
                if (commandParameter.Direction != ParameterDirection.Input)
                    canClear = false;
            }
            if (canClear)
            {
                cmd.Parameters.Clear();
            }
            return dataReader;
        }

        /// <summary>
        /// 执行ＳＱＬ语句或者存储过程 ,返回参数datareader(通用)
        /// <remarks >
        /// 需要显示关闭连接
        /// </remarks>
        /// </summary>
        /// <param name="connection">要执行ＳＱＬ语句的连接</param>
        /// <param name="commandType">ＳＱＬ语句类型</param>
        /// <param name="commandText">ＳＱＬ语句或者存储过程名</param>
        /// <param name="commandParameters">ＳＱＬ语句或者存储过程参数</param>
        /// <returns>执行结果集</returns>
        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(connection, null, commandType, commandText, commandParameters, true);
        }


        /// <summary>
        /// 执行ＳＱＬ语句或者存储过程 ,返回参数datareader
        /// <remarks >
        /// 需要显示关闭连接
        /// </remarks>
        /// </summary>
        /// <param name="connection">要执行ＳＱＬ语句的连接</param>
        /// <param name="commandType">ＳＱＬ语句类型</param>
        /// <param name="commandText">ＳＱＬ语句或者存储过程名</param>n
        /// <returns>执行结果集</returns>
        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteReader(connection, commandType, commandText, null);
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// 执行ＳＱＬ语句或者存储过程 ,返回参数object．第一行，第一列的值(通用)
        /// </summary>
        /// <param name="connection">要执行ＳＱＬ语句的连接</param>
        /// <param name="commandType">ＳＱＬ语句类型</param>
        /// <param name="commandText">ＳＱＬ语句或者存储过程名</param>
        /// <param name="commandParameters">ＳＱＬ语句或者存储过程参数</param>
        /// <returns>执行结果集第一行，第一列的值</returns>　
        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            object retval = null;
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
            retval = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }

        /// <summary>
        /// 执行ＳＱＬ语句或者存储过程 ,返回参数object．第一行，第一列的值
        /// </summary>
        /// <param name="connection">要执行ＳＱＬ语句的连接</param>
        /// <param name="commandType">ＳＱＬ语句类型</param>
        /// <param name="commandText">ＳＱＬ语句或者存储过程名</param>
        /// <returns>执行结果集第一行，第一列的值</returns>　
        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteScalar(connection, commandType, commandText, null);
        }

        /// <summary>
        ///  执行ＳＱＬ语句或者存储过程 ,返回参数object．第一行，第一列的值
        /// </summary>
        /// <param name="transaction">语句所在的事务</param>
        /// <param name="commandType">ＳＱＬ语句类型</param>
        /// <param name="commandText">ＳＱＬ语句或者存储过程名</param>
        /// <param name="commandParameters">ＳＱＬ语句或者存储过程参数</param>
        /// <returns>影响的行数</returns>
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            object retval = null;
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, null, out mustCloseConnection);
            retval = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        ///  执行ＳＱＬ语句或者存储过程 ,返回参数object．第一行，第一列的值
        /// </summary>
        /// <param name="transaction">语句所在的事务</param>
        /// <param name="commandType">ＳＱＬ语句类型</param>
        /// <param name="commandText">ＳＱＬ语句或者存储过程名</param>
        /// <param name="commandParameters">ＳＱＬ语句或者存储过程参数</param>
        /// <returns>影响的行数</returns>
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            object retval = null;
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            retval = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return retval;
        }

        #endregion
    }
}
