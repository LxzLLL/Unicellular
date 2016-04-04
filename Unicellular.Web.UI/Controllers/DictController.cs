using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// mvc5中已实现AttributeRouting的功能（[Route(...)]）
//using AttributeRouting;
//using AttributeRouting.Web.Mvc;

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

        #region 字典
        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetDicts(int pageNumber, int pageSize,string sort=null, string sortOrder=null, string search=null )
        {
            long number =0;
            List<T_Sys_Dict> dicts = dictService.GetDictPages(pageNumber,pageSize,out number,sort,sortOrder,search);
            return this.Json( new { total = number, rows = dicts }, JsonRequestBehavior.AllowGet );
        }


        /// <summary>
        /// 根据id返回字典json类型数据
        /// </summary>
        /// <param name="dictId"></param>
        /// <returns></returns>
        public JsonResult GetDict(string dictId)
        {
            return this.Json( dictService.GetDict( dictId ), JsonRequestBehavior.AllowGet );
        }

        /// <summary>
        /// 新增dict
        /// </summary>
        /// <returns></returns>
        [ HttpPost]
        public JsonResult AddDict(string data)
        {
            T_Sys_Dict dict = JsonHelper.ConvertJsonString2Object<T_Sys_Dict>(data);
            dict.ID = RandomHelper.GetUUID();
            MsgEntity me = dictService.AddDict(dict);
            return this.Json(me,JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 编辑dict
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditDict( string data )
        {
            T_Sys_Dict dict = JsonHelper.ConvertJsonString2Object<T_Sys_Dict>(data);
            MsgEntity me = dictService.EditDict(dict);
            return this.Json( me, JsonRequestBehavior.DenyGet );
        }

        /// <summary>
        /// 编辑dict
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DelDict( string dictId )
        {
            MsgEntity me = dictService.DelDict(dictId);
            return this.Json( me, JsonRequestBehavior.DenyGet );
        }

        #endregion

        #region 字典项
        /// <summary>
        /// 获取字典项列表
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetDictItems( int pageNumber, int pageSize, string sort = null, string sortOrder = null, string search = null,string DICT_ID=null )
        {
            long number =0;
            List<T_Sys_DictItem> dictItems = dictService.GetDictItemPages(pageNumber,pageSize,out number,sort,sortOrder,search,DICT_ID);
            return this.Json( new { total = number, rows = dictItems }, JsonRequestBehavior.AllowGet );
            //return JsonHelper.ConvertObject2JsonString( new { data = dictitems } );
        }


        /// <summary>
        /// 根据id返回字典json类型数据
        /// </summary>
        /// <param name="dictItemId"></param>
        /// <returns></returns>
        public JsonResult GetDictItem( string dictItemId )
        {
            return this.Json( dictService.GetDictItem( dictItemId ), JsonRequestBehavior.AllowGet );
        }

        /// <summary>
        /// 新增dict
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddDictItem( string data )
        {
            T_Sys_DictItem dictItem = JsonHelper.ConvertJsonString2Object<T_Sys_DictItem>(data);
            dictItem.ID = RandomHelper.GetUUID();
            MsgEntity me = dictService.AddDictItem(dictItem);
            return this.Json( me, JsonRequestBehavior.DenyGet );
        }

        /// <summary>
        /// 编辑dict
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditDictItem( T_Sys_DictItem dictItem )
        {
            //T_Sys_DictItem dictItem = JsonHelper.ConvertJsonString2Object<T_Sys_DictItem>(data);
            MsgEntity me = dictService.EditDictItem(dictItem);
            return this.Json( me, JsonRequestBehavior.DenyGet );
        }

        /// <summary>
        /// 编辑dict
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DelDictItem( string dictItemId )
        {
            MsgEntity me = dictService.DelDictItem(dictItemId);
            return this.Json( me, JsonRequestBehavior.DenyGet );
        }


        #endregion
    }
}
