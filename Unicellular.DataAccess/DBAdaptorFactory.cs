using System;
using System.Data;

namespace Unicellular.DataAccess
{
    public class DBAdaptorFactory
    {
        /// <summary>
        /// 根据IDatabase接口，获取IDBAdaptor
        /// </summary>
        public static IDBAdaptor GetDbAdaptor( IDatabase database )
        {
            IDBAdaptor adaptor = null;
            switch ( database.DatabaseType )
            {
                case DatabaseType.SqlServer:
                    adaptor = new SqlDBHelper.SqlAdaptor( database.Connection );
                    break;
                case DatabaseType.MySql:
                    //adaptor = new MySqlDBHelper.MySqlAdaptor( database.Connection );
                    break;
                case DatabaseType.Oracle:
                    adaptor = new OracleDBHelper.OracleAdaptor( database.Connection );
                    break;
                case DatabaseType.DB2:
                //adaptor = new MySqlDBHelper.MySqlAdaptor( database.Connection );
                //break;
                case DatabaseType.Sqlite:
                    //adaptor = new SqliteDBHelper.SqliteAdaptor( database.Connection );
                    break;
            }
            return adaptor;
        }
    }
}
