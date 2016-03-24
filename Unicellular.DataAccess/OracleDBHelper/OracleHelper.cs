using System;
using System.Data;
//using Oracle.DataAccess.Client;
using System.Data.OracleClient;
using System.Data.Common;


namespace Unicellular.DataAccess.OracleDBHelper
{

    public class OracleHelper
    {
        #region ˽�з����͹���
        /// <summary>
        /// sql׼��
        /// </summary>
        /// <param name="command">Sql����</param>
        /// <param name="connection">sql����</param>
        /// <param name="transaction">����</param>
        /// <param name="commandType">���ͣ��ı����Ǵ洢����</param>
        /// <param name="commandText">sql���</param>
        /// <param name="commandParameters">����</param>
        /// <param name="mustCloseConnection">�Ƿ�Ҫ�ر�</param>
        private static void PrepareCommand(OracleCommand command, OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, OracleParameter[] commandParameters, out bool mustCloseConnection)
        {
            // ������
            if ( connection.State != ConnectionState.Open)
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
            // ������������������
            if ( transaction != null)
            {
                command.Transaction = transaction;
            }
            command.CommandType = commandType;
            // ������ڲ�������󶨲���
            if ( commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        /// <summary>
        /// �󶨲���
        /// </summary>
        /// <param name="command">sql����</param>
        /// <param name="commandParameters">�������</param>
        private static void AttachParameters(OracleCommand command, OracleParameter[] commandParameters)
        {
            if (commandParameters != null)
            {
                foreach (OracleParameter p in commandParameters)
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

        #region transaction ������
        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <param name="conn">���ݿ�����</param>
        /// <param name="Iso">ָ�����ӵ�����������Ϊ</param>
        /// <returns>��ǰ����</returns>  
        public static OracleTransaction BeginTransaction(OracleConnection conn, IsolationLevel Iso)
        {
            conn.Open();
            return conn.BeginTransaction(Iso);
        }

        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <param name="conn">���ݿ�����</param>
        /// <returns>��ǰ����</returns>
        public static OracleTransaction BeginTransaction(OracleConnection conn)
        {

            conn.Open();
            return conn.BeginTransaction();
        }

        /// <summary>
        /// ��������ȷ�ϲ���
        /// </summary>
        /// <param name="Transaction">Ҫ����������</param>
        public static void endTransactionCommit( OracleTransaction Transaction )
        {
            OracleConnection con = Transaction.Connection;
            Transaction.Commit();
            con.Close();
        }

        /// <summary>
        /// �������񣬻ع�����
        /// </summary>
        /// <param name="Transaction">Ҫ����������</param>
        public static void endTransactionRollback( OracleTransaction Transaction )
        {
            OracleConnection con =Transaction.Connection;
            Transaction.Rollback();
            con.Close();
        }

        #endregion

        #region ExecuteNonQuery
        /// <summary>
        ///  ִ��SQL�����ߴ洢���� ,�����ز���,ֻ����Ӱ������(ͨ��)
        /// </summary>
        /// <param name="transaction">������ڵ�����</param>
        /// <param name="commandType">SQL�������</param>
        /// <param name="commandText">SQL�����ߴ洢������</param>
        /// <param name="commandParameters">SQL�����ߴ洢���̲���</param>
        /// <returns>Ӱ�������</returns>
        public static int ExecuteNonQuery( OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //Ҫ������  
            OracleCommand cmd = new OracleCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            int retval = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        ///  ִ��SQL�����ߴ洢���� ,�����ز���,ֻ����Ӱ������
        /// </summary>
        /// <param name="transaction">������ڵ�����</param>
        /// <param name="commandType">SQL�������</param>
        /// <param name="commandText">SQL�����ߴ洢������</param>
        /// <returns>Ӱ�������</returns>
        public static int ExecuteNonQuery( OracleTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(transaction, commandType, commandText, null);
        }


        /// <summary>
        ///  ִ��SQL�����ߴ洢���� ,�����ز���,ֻ����Ӱ������(ͨ��)
        /// </summary>
        /// <param name="connection">Ҫִ��SQL��������</param>
        /// <param name="commandType">SQL�������</param>
        /// <param name="commandText">SQL�����ߴ洢������</param>
        /// <param name="commandParameters">SQL�����ߴ洢���̲���</param>
        /// <returns>Ӱ�������</returns>
        public static int ExecuteNonQuery(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            int retval = 0;
            //Ҫ������
            OracleCommand cmd = new OracleCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
            retval = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }
        /// <summary>
        ///  ִ��SQL�����ߴ洢���� ,�����ز���,ֻ����Ӱ������
        /// </summary>
        /// <param name="connection">Ҫִ��SQL��������</param>
        /// <param name="commandType">SQL�������</param>
        /// <param name="commandText">SQL�����ߴ洢������</param>
        /// <returns>Ӱ�������</returns>
        public static int ExecuteNonQuery(OracleConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteNonQuery(connection, commandType, commandText,null);
        }

        #endregion

        #region ExecuteDataset


        /// <summary>
        /// ִ��SQL�����ߴ洢���� ,���ز���dataset(ͨ��)
        /// </summary>
        /// <param name="connection">Ҫִ��SQL��������</param>
        /// <param name="commandType">SQL�������</param>
        /// <param name="commandText">SQL�����ߴ洢������</param>
        /// <param name="Table"> ���ı��� </param>
        /// <param name="commandParameters">SQL�����ߴ洢���̲���</param>
        /// <returns>ִ�н����</returns>
        public static DataSet ExecuteDataset(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
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
        /// ִ��SQL�����ߴ洢���� ,���ز���dataset
        /// </summary>
        /// <param name="connection">Ҫִ��SQL��������</param>
        /// <param name="commandType">SQL�������</param>
        /// <param name="commandText">SQL�����ߴ洢������</param>
        /// <param name="Table">���ı���</param>
        /// <returns>ִ�н����</returns>��
        public static DataSet ExecuteDataset(OracleConnection connection, CommandType commandType, string commandText)
        {

            return ExecuteDataset(connection, commandType, commandText, null);
        }
        #endregion

        #region ExecuteReader
        //ͨ��
        private static OracleDataReader ExecuteReader(OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, OracleParameter[] commandParameters, bool isClose)
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
        /// ִ��SQL�����ߴ洢���� ,���ز���datareader(ͨ��)
        /// <remarks >
        /// ��Ҫ��ʾ�ر�����
        /// </remarks>
        /// </summary>
        /// <param name="connection">Ҫִ��SQL��������</param>
        /// <param name="commandType">SQL�������</param>
        /// <param name="commandText">SQL�����ߴ洢������</param>
        /// <param name="commandParameters">SQL�����ߴ洢���̲���</param>
        /// <returns>ִ�н����</returns>
        public static OracleDataReader ExecuteReader(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            return ExecuteReader(connection, (OracleTransaction)null, commandType, commandText, commandParameters, true);
        }


        /// <summary>
        /// ִ��SQL�����ߴ洢���� ,���ز���datareader
        /// <remarks >
        /// ��Ҫ��ʾ�ر�����
        /// </remarks>
        /// </summary>
        /// <param name="connection">Ҫִ��SQL��������</param>
        /// <param name="commandType">SQL�������</param>
        /// <param name="commandText">SQL�����ߴ洢������</param>n
        /// <returns>ִ�н����</returns>
        public static OracleDataReader ExecuteReader(OracleConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteReader(connection, commandType, commandText,null);
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// ִ��SQL�����ߴ洢���� ,���ز���object����һ�У���һ�е�ֵ(ͨ��)
        /// </summary>
        /// <param name="connection">Ҫִ��SQL��������</param>
        /// <param name="commandType">SQL�������</param>
        /// <param name="commandText">SQL�����ߴ洢������</param>
        /// <param name="commandParameters">SQL�����ߴ洢���̲���</param>
        /// <returns>ִ�н������һ�У���һ�е�ֵ</returns>��
        public static object ExecuteScalar(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            object retval = null;
            OracleCommand cmd = new OracleCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
            retval = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }

        /// <summary>
        /// ִ��SQL�����ߴ洢���� ,���ز���object����һ�У���һ�е�ֵ
        /// </summary>
        /// <param name="connection">Ҫִ��SQL��������</param>
        /// <param name="commandType">SQL�������</param>
        /// <param name="commandText">SQL�����ߴ洢������</param>
        /// <returns>ִ�н������һ�У���һ�е�ֵ</returns>��
        public static object ExecuteScalar(OracleConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteScalar(connection, commandType, commandText,null);
        }

        /// <summary>
        ///  ִ��SQL�����ߴ洢���� ,���ز���object����һ�У���һ�е�ֵ
        /// </summary>
        /// <param name="transaction">������ڵ�����</param>
        /// <param name="commandType">SQL�������</param>
        /// <param name="commandText">SQL�����ߴ洢������</param>
        /// <param name="commandParameters">SQL�����ߴ洢���̲���</param>
        /// <returns>Ӱ�������</returns>
        public static object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText)
        {
            object retval = null;
            OracleCommand cmd = new OracleCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, ((OracleTransaction)transaction).Connection, (OracleTransaction)transaction, commandType, commandText, null, out mustCloseConnection);
            retval = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        ///  ִ��SQL�����ߴ洢���� ,���ز���object����һ�У���һ�е�ֵ
        /// </summary>
        /// <param name="transaction">������ڵ�����</param>
        /// <param name="commandType">SQL�������</param>
        /// <param name="commandText">SQL�����ߴ洢������</param>
        /// <param name="commandParameters">SQL�����ߴ洢���̲���</param>
        /// <returns>Ӱ�������</returns>
        public static object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
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
