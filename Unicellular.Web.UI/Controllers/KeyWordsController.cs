using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using Unicellular.Web.BLL.System;
using Unicellular.Web.BLL.Ecommerce;
using Unicellular.Web.Entity.Ecommerce;
using Unicellular.Web.Entity;
using XLToolLibrary.Utilities;
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


        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get( string ID )
        {
            T_EC_KeyWords keyword = _keywordService.Get(ID);
            return Json( keyword, JsonRequestBehavior.AllowGet );
        }

        /// <summary>
        /// 添加分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add( T_EC_KeyWords keyword )
        {
            keyword.ID = RandomHelper.GetUUID();
            MsgEntity me = _keywordService.Add(keyword);
            return Json( me, JsonRequestBehavior.AllowGet );
        }


        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit( T_EC_KeyWords keyword )
        {
            MsgEntity me = _keywordService.Edit(keyword);
            return Json( me, JsonRequestBehavior.AllowGet );
        }


        /// <summary>
        /// 删除分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Del( string ID )
        {
            MsgEntity me = _keywordService.Delete(ID);
            return Json( me, JsonRequestBehavior.AllowGet );
        }

    }
}
