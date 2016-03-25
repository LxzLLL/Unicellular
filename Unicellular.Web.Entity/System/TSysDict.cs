using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicellular.Web.Entity.System
{
    /// <summary>
    /// TSysDict 系统字典表
    /// </summary>
    public class T_Sys_Dict
    {
        /// <summary>
        /// TSysDict 系统字典表构造函数
        /// </summary>
        public T_Sys_Dict()
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
        ///字典编码
        /// </summary>
        public string DICT_CODE
        {
            get;
            set;
        }

        /// <summary>
        ///字典名称
        /// </summary>
        public string DICT_NAME
        {
            get;
            set;
        }

        /// <summary>
        ///字典描述
        /// </summary>
        public string DICT_DES
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
