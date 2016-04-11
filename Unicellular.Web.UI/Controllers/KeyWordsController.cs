using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using Unicellular.Web.BLL.System;
using Unicellular.Web.BLL.Ecommerce;
using Unicellular.Web.Entity.Ecommerce;
using Unicellular.Web.Entity.System;
using Unicellular.Web.UI.ViewModels.Ecommerce;

namespace Unicellular.Web.UI.Controllers
{
    public class KeyWordsController : Controller
    {
        private readonly SysDictService _dictService = new SysDictService();
        private readonly CategoryService _categoryService = new CategoryService();
        private readonly KeyWordService _keywordService = new KeyWordService();
        // GET: KeyWords
        public ActionResult Index()
        {
            ViewBag.PlatType = _dictService.GetSelectListItemByObj( new { DICT_CODE = "PLAT_TYPE" } );
            ViewBag.KeyWordType = _dictService.GetSelectListItemByObj( new { DICT_CODE = "KEYWORD_TYPE" } );
            ViewBag.GoodsType = _dictService.GetSelectListItemByObj( new { DICT_CODE = "GOODS_TYPE" } );
            return View();
        }       
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="sortOrder"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public JsonResult GetKeyWords( int pageNumber, int pageSize, string sort = null, string sortOrder = null, T_EC_KeyWords keywordModel = null )
        {
            long number =0;
            List<T_EC_KeyWords> keywords = _keywordService.GetKeyWordsPages(pageNumber,pageSize,out number,sort,sortOrder,keywordModel);
            return this.Json( new { total = number, rows = keywords }, JsonRequestBehavior.AllowGet );
        }
    }
}
