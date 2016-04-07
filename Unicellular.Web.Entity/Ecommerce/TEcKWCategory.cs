using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicellular.Web.Entity.Ecommerce
{
    /// <summary>
    /// 关键词分类表
    /// </summary>
    public class T_EC_KWCategory
    {

        /// <summary>
        /// 关键词分类表构造函数
        /// </summary>
        public T_EC_KWCategory()
        {
            ///Todo
        }

        /// <summary>
        ///主键
        /// </summary>
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        ///关键词分类编码
        /// </summary>
        public string CATEGORY_CODE
        {
            get;
            set;
        }

        /// <summary>
        ///关键词分类名称
        /// </summary>
        public string CATEGORY_NAME
        {
            get;
            set;
        }

        /// <summary>
        ///此条数据创建日期
        /// </summary>
        public DateTime? C_DATA_TIME
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
        public DateTime? U_DATA_TIME
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

        /// <summary>
        ///父ID，如果是根节点，则为0
        /// </summary>
        public string PARENT_ID
        {
            get;
            set;
        }

        /// <summary>
        ///分类描述
        /// </summary>
        public string CATEGORY_DES
        {
            get;
            set;
        }
    }

}

