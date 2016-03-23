using System;
using System.Data;

namespace Unicellular.DataAccess
{
    public class DBAdaptorFactory
    {
        /// <summary>
        /// 获取IDBAdaptor接口工厂
        /// </summary>
        public static IDBAdaptor GetDbAdaptor( DatabaseType dbType, string strKey )
        {
            IDBAdaptor adaptor = null;
            IDbConnection connection = SqlConnectionFactory.CreateSqlConnection(dbType,strKey);
            switch ( dbType )
            {
                case DatabaseType.SqlServer:
                    adaptor = new SqlDBHelper.SqlAdaptor( connection );
                    break;
                case DatabaseType.MySql:
                    //adaptor = new MySqlDBHelper.MySqlAdaptor( connection );
                    break;
                case DatabaseType.Oracle:
                    adaptor = new OracleDBHelper.OracleAdaptor( connection );
                    break;
                case DatabaseType.DB2:
                    //adaptor = new MySqlDBHelper.MySqlAdaptor( connection );
                    //break;
                case DatabaseType.Sqlite:
                    //adaptor = new SqliteDBHelper.SqliteAdaptor( connection );
                    break;
            }
            return adaptor;
        }
    }
}
