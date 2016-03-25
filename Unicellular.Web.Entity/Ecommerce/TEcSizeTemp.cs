using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicellular.Web.Entity.Ecommerce
{
    /// <summary>
    /// TEcSizeTemp 尺码描述模板
    /// </summary>
    public class T_EC_Size_Temp
    {

        /// <summary>
        /// TEcSizeTemp 尺码描述模板构造函数
        /// </summary>
        public T_EC_Size_Temp()
        {
            ///Todo
        }

        /// <summary>
        ///表ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        ///国家类型，字典项存储（COUNTRY_TYPE）
        /// </summary>
        public string COUNTRY_TYPE
        {
            get;
            set;
        }

        /// <summary>
        ///尺码
        /// </summary>
        public string SIZE_TEXT
        {
            get;
            set;
        }

        /// <summary>
        ///尺码描述模板
        /// </summary>
        public string SIET_TEMP
        {
            get;
            set;
        }

        /// <summary>
        ///此条数据创建日期
        /// </summary>
        public DateTime C_DATA_TIME
        {
            get;
            set;
        }

        /// <summary>
        ///创建数据的用户ID
        /// </summary>
        public string C_DATA_UID
        {
            get;
            set;
        }

        /// <summary>
        ///此条数据更新日期
        /// </summary>
        public DateTime U_DATA_TIME
        {
            get;
            set;
        }

        /// <summary>
        ///更新数据的用户ID
        /// </summary>
        public string U_DATA_UID
        {
            get;
            set;
        }

        /// <summary>
        ///预留1
        /// </summary>
        public string RESERVE1
        {
            get;
            set;
        }

        /// <summary>
        ///预留2
        /// </summary>
        public string RESERVE2
        {
            get;
            set;
        }

        /// <summary>
        ///预留3
        /// </summary>
        public string RESERVE3
        {
            get;
            set;
        }
    }
}
