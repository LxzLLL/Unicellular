using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicellular.Web.Entity.Ecommerce
{
    /// <summary>
    /// TEcKeyWords 电子商务关键词
    /// </summary>
    public class T_EC_KeyWords
    {

        /// <summary>
        /// TEcKeyWords 电子商务关键词构造函数
        /// </summary>
        public T_EC_KeyWords()
        {
            ///Todo
        }

        /// <summary>
        ///ID
        /// </summary>
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        ///平台类型，字典项存储（PLAT_TYPE）
        /// </summary>
        public string PLAT_TYPE
        {
            get;
            set;
        }

        /// <summary>
        ///关键词类型，字典项存储（KEYWORD_TYPE）
        /// </summary>
        public string KEYWORD_TYPE
        {
            get;
            set;
        }

        /// <summary>
        ///分类ID
        /// </summary>
        public string GOODS_TYPE
        {
            get;
            set;
        }

        /// <summary>
        ///关键词
        /// </summary>
        public string KEY_WORD
        {
            get;
            set;
        }

        /// <summary>
        ///关键词的中文描述
        /// </summary>
        public string KW_CN
        {
            get;
            set;
        }

        /// <summary>
        ///关键词最优产品描述
        /// </summary>
        public string KW_DES
        {
            get;
            set;
        }

        /// <summary>
        ///平台检索量
        /// </summary>
        public int KW_VOLUME
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
        ///预留3
        /// </summary>
        public string RESERVE3
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
    }
}
