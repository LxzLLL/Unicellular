using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicellular.Web.DAO;
using Unicellular.Web.Entity.System;
namespace Unicellular.Web.BLL.System
{
    /// <summary>
    /// 系统字典处理
    /// </summary>
    public class SysDictBll
    {
        private DAOBase dao;

        public SysDictBll()
        {
            dao = new DAOBase();
        }

        /// <summary>
        /// 获取全部dict列表
        /// </summary>
        /// <returns></returns>
        public List<T_Sys_Dict> GetDictAll()
        {
            List < T_Sys_Dict > dicts = dao.GetList<T_Sys_Dict>().ToList();
            return dicts == null ? new List<T_Sys_Dict>() : dicts;
        }
    }
}
