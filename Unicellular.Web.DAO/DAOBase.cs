using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicellular.DataAccess;
using Unicellular.ORM;
namespace Unicellular.Web.DAO
{
    /// <summary>
    /// 数据访问对象层基类
    /// </summary>
    public class DAOBase:RepositoryBase
    {
        //默认设置，换数据库时需更改（可更换为配置处理）
        private DatabaseType _dbTypeDefault = DatabaseType.Sqlite;
        private string _connKey = "SqliteConnection";

        public Database DB { get; private set; }
        public DBSessionBase DataBaseSession { get; set; }

        /// <summary>
        /// 默认构造时，使用默认的数据库类型和连接关键词设置
        /// </summary>
        public DAOBase()
        {
            this.InitDB();
        }
        /// <summary>
        /// 使用数据库类型和连接关键词初始化DAOBase
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connKey"></param>
        public DAOBase(DatabaseType dbType, string connKey)
        {
            this._connKey = connKey;
            this._dbTypeDefault = dbType;
            this.InitDB();
        }
        /// <summary>
        /// 初始化DB信息
        /// </summary>
        private void InitDB()
        {
            this.DB = new Database( this._dbTypeDefault, this._connKey );
            this.DataBaseSession = new DBSessionBase( this.DB );
            base.SetDBSession( this.DataBaseSession );
        }
    }
}
