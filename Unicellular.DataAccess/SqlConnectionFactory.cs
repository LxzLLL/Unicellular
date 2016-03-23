using System;
using System.Configuration;
using System.Data;

namespace Unicellular.DataAccess
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DatabaseType
    {    
        SqlServer,
        MySql,
        Oracle,
        DB2,
        Sqlite,
        Postgresql
    }
    
    public class SqlConnectionFactory
    {
        /// <summary>
        /// 根据数据库类型和配置文件字符串获取连接；主要针对Dapper使用
        /// </summary>
        public static IDbConnection CreateSqlConnection(DatabaseType dbType, string strKey)
        {
            IDbConnection connection = null;
            string strConn = ConfigurationManager.ConnectionStrings[strKey].ConnectionString;

            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    connection = new System.Data.SqlClient.SqlConnection(strConn);
                    break;
                case DatabaseType.MySql:
                    //connection = new MySql.Data.MySqlClient.MySqlConnection(strConn);
                    break;
                case DatabaseType.Oracle:
                    connection = new Oracle.DataAccess.Client.OracleConnection(strConn);
                    //connection = new System.Data.OracleClient.OracleConnection(strConn);
                    break;
                case DatabaseType.DB2:
                    connection = new System.Data.OleDb.OleDbConnection(strConn);
                    break;
                case DatabaseType.Sqlite:
                    connection = new System.Data.SQLite.SQLiteConnection( strConn );
                    break;
                case DatabaseType.Postgresql:
                    //connection = new System.Data.SQLite.SQLiteConnection( strConn );
                    break;
            }
            return connection;
        }
    }
}
