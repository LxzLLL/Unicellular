using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicellular.Web.Entity.System
{
    /// <summary>
    /// TSysDictItem 系统字典项表
    /// </summary>
    public class T_Sys_DictItem
    {
        /// <summary>
        /// TSysDictItem 系统字典项表构造函数
        /// </summary>
        public T_Sys_DictItem()
        {
            ///Todo
        }

        /// <summary>
        ///表ID
        /// </summary>
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        ///字典表ID
        /// </summary>
        public string DICT_CODE
        {
            get;
            set;
        }

        /// <summary>
        ///字典项编码
        /// </summary>
        public string DI_CODE
        {
            get;
            set;
        }

        /// <summary>
        ///字典项名称
        /// </summary>
        public string DI_NAME
        {
            get;
            set;
        }

        /// <summary>
        ///字典项描述
        /// </summary>
        public string DI_DES
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
