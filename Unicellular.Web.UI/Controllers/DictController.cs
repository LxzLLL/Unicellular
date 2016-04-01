using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Unicellular.Web.BLL.System;
using Unicellular.Web.Entity.System;
using Unicellular.Web.Entity;
using XLToolLibrary.Utilities;
using Unicellular.Web.UI.ViewModels.System;
namespace Unicellular.Web.UI.Controllers
{
    public class DictController : Controller
    {
        private SysDictService dictService = new SysDictService();

        public ActionResult Views()
        {
            return View();
        }
        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetDict(int pageNumber, int pageSize,string sort=null, string sortOrder=null, string search=null )
        {
            long number =0;
            List<T_Sys_Dict> dicts = dictService.GetDictPages(pageNumber,pageSize,out number,sort,sortOrder,search);
            return this.Json( new { total = number, rows = dicts }, JsonRequestBehavior.AllowGet );
        }
        /// <summary>
        /// 获取字典项列表
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetDictItem( int pageNumber, int pageSize, string sort = null, string sortOrder = null, string search = null )
        {
            long number =0;
            List<T_Sys_DictItem> dictitems = dictService.GetDictItemPages(pageNumber,pageSize,out number,sort,sortOrder,search);
            //List<T_Sys_DictItem> dictitems = dictService.GetDictItem();
            return this.Json( new { total = number, rows = dictitems }, JsonRequestBehavior.AllowGet );
            //return JsonHelper.ConvertObject2JsonString( new { data = dictitems } );
        }

        /// <summary>
        /// 新增dict
        /// </summary>
        /// <param name="sDictJson"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddDict()
        {
            string sDictJson = HttpContext.Request.Params["data"];
            T_Sys_Dict dict = JsonHelper.ConvertJsonString2Object<T_Sys_Dict>(sDictJson);
            dict.ID = RandomHelper.GetUUID();
            MsgEntity me = dictService.AddDict(dict);
            return this.Json(me,JsonRequestBehavior.DenyGet);
        }
    }
}
