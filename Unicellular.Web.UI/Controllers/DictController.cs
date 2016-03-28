using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Unicellular.Web.BLL.System;
using Unicellular.Web.Entity.System;
using XLToolLibrary.Utilities;
namespace Unicellular.Web.UI.Controllers
{
    public class DictController : Controller
    {
        private SysDictService dictService = new SysDictService();

        public ActionResult Views()
        {
            return View();
        }

        public JsonResult GetDict(int pageNumber, int pageSize,string sort=null, string sortOrder=null, string search=null )
        {
            long number =0;
            List<T_Sys_Dict> dicts = dictService.GetDictPages(pageNumber,pageSize,out number,sort,sortOrder,search);
            return this.Json( new { total = number, rows = dicts }, JsonRequestBehavior.AllowGet );
        }

        public JsonResult GetDictItem( int pageNumber, int pageSize, string sort = null, string sortOrder = null, string search = null )
        {
            long number =0;
            List<T_Sys_DictItem> dictitems = dictService.GetDictItemPages(pageNumber,pageSize,out number,sort,sortOrder,search);
            //List<T_Sys_DictItem> dictitems = dictService.GetDictItem();
            return this.Json( new { total = number, rows = dictitems }, JsonRequestBehavior.AllowGet );
            //return JsonHelper.ConvertObject2JsonString( new { data = dictitems } );
        }
    }
}
