using System;
using System.Configuration;
using System.Data;
using System.Collections;
using Oracle.DataAccess.Client;
//using System.Data.OracleClient;
using System.Data.Common;


namespace Unicellular.DataAccess.OracleDBHelper
{

    public class OracleHelper
    {
        #region 私有方法和工具
        //sql
        private static void PrepareCommand(OracleCommand command, OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, OracleParameter[] commandParameters, out bool mustCloseConnection)
        {
            
            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;

            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }


        //通用
        private static void PrepareCommand(OracleCommand command, DbConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, DbParameter[] commandParameters, out bool mustCloseConnection)
        {

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;

                connection.Open();


            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = (OracleConnection)connection;

            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;
            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }



        //sql
        private static void AttachParameters(OracleCommand command, OracleParameter[] commandParameters)
        {

            if (commandParameters != null)
            {
                foreach (OracleParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
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

        //通用
        private static void AttachParameters(OracleCommand command, DbParameter[] commandParameters)
        {

            if (commandParameters != null)
            {
                foreach (DbParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
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
        public static DbTransaction BeginTransaction(OracleConnection conn, IsolationLevel Iso)
        {
            conn.Open();
            return conn.BeginTransaction(Iso);
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <returns>当前事务</returns>
        public static DbTransaction BeginTransaction(OracleConnection conn)
        {

            conn.Open();
            return conn.BeginTransaction();
        }

        /// <summary>
        /// 结束事务，确认操作
        /// </summary>
        /// <param name="Transaction">要结束的事务</param>
        public static void endTransactionCommit(DbTransaction Transaction)
        {
            DbConnection con = (DbConnection)Transaction.Connection;
            Transaction.Commit();
            con.Close();
        }

        /// <summary>
        /// 结束事务，回滚操作
        /// </summary>
        /// <param name="Transaction">要结束的事务</param>
        public static void endTransactionRollback(DbTransaction Transaction)
        {
            DbConnection con = (DbConnection)Transaction.Connection;
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
        public static int ExecuteNonQuery(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            //要检查参数  
            OracleCommand cmd = new OracleCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, ((OracleTransaction)transaction).Connection, (OracleTransaction)transaction, commandType, commandText, commandParameters, out mustCloseConnection);
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
        public static int ExecuteNonQuery(DbTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(transaction, commandType, commandText, (DbParameter[])null);
        }


        /// <summary>
        ///  执行ＳＱＬ语句或者存储过程 ,不返回参数,只返回影响行数(通用)
        /// </summary>
        /// <param name="connection">要执行ＳＱＬ语句的连接</param>
        /// <param name="commandType">ＳＱＬ语句类型</param>
        /// <param name="commandText">ＳＱＬ语句或者存储过程名</param>
        /// <param name="commandParameters">ＳＱＬ语句或者存储过程参数</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteNonQuery(OracleConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            int retval = 0;
            //要检查参数
            OracleCommand cmd = new OracleCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (OracleTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);


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
        public static int ExecuteNonQuery(OracleConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteNonQuery(connection, commandType, commandText, (DbParameter[])null);
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
        public static DataSet ExecuteDataset(OracleConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {

            OracleCommand cmd = new OracleCommand();
            //cmd.InitialLONGFetchSize = -1;
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (OracleTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);
            using (OracleDataAdapter da = new OracleDataAdapter(cmd))
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
        public static DataSet ExecuteDataset(OracleConnection connection, CommandType commandType, string commandText)
        {

            return ExecuteDataset(connection, commandType, commandText, (DbParameter[])null);
        }


        #endregion

        #region ExecuteReader



        //通用
        private static OracleDataReader ExecuteReader(OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, DbParameter[] commandParameters, bool isClose)
        {
            bool mustCloseConnection = false;

            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            OracleDataReader dataReader = null;


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
        public static OracleDataReader ExecuteReader(OracleConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {

            return ExecuteReader(connection, (OracleTransaction)null, commandType, commandText, commandParameters, true);
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
        public static OracleDataReader ExecuteReader(OracleConnection connection, CommandType commandType, string commandText)
        {

            return ExecuteReader(connection, commandType, commandText, (DbParameter[])null);
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
        public static object ExecuteScalar(OracleConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            object retval = null;
            OracleCommand cmd = new OracleCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (OracleTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

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
        public static object ExecuteScalar(OracleConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteScalar(connection, commandType, commandText, (DbParameter[])null);
        }




        /// <summary>
        ///  执行ＳＱＬ语句或者存储过程 ,返回参数object．第一行，第一列的值
        /// </summary>
        /// <param name="transaction">语句所在的事务</param>
        /// <param name="commandType">ＳＱＬ语句类型</param>
        /// <param name="commandText">ＳＱＬ语句或者存储过程名</param>
        /// <param name="commandParameters">ＳＱＬ语句或者存储过程参数</param>
        /// <returns>影响的行数</returns>
        public static object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText)
        {
            object retval = null;
            OracleCommand cmd = new OracleCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, ((OracleTransaction)transaction).Connection, (OracleTransaction)transaction, commandType, commandText, (DbParameter[])null, out mustCloseConnection);
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
        public static object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            object retval = null;
            OracleCommand cmd = new OracleCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, ((OracleTransaction)transaction).Connection, (OracleTransaction)transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            retval = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return retval;
        }

        #endregion


    }
}
