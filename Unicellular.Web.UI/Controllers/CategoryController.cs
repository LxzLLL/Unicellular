using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Unicellular.Web.Entity;
using Unicellular.Web.BLL.Ecommerce;
using Unicellular.Web.Entity.Ecommerce;
using XLToolLibrary.Utilities;
namespace Unicellular.Web.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService = new CategoryService();

        // GET: Categroy
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取全部分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetTree()
        {
            List<T_EC_KWCategory> categories = _categoryService.GetList();
            return Json( categories, JsonRequestBehavior.AllowGet );
        }

        /// <summary>
        /// 获取分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get(string ID)
        {
            T_EC_KWCategory category = _categoryService.Get(ID);
            return Json( category, JsonRequestBehavior.AllowGet );
        }

        /// <summary>
        /// 添加分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add( T_EC_KWCategory category )
        {
            category.ID = RandomHelper.GetUUID();
            MsgEntity me = _categoryService.Add(category);
            return Json( me, JsonRequestBehavior.AllowGet );
        }


        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit( T_EC_KWCategory category )
        {
            MsgEntity me = _categoryService.Edit(category);
            return Json( me, JsonRequestBehavior.AllowGet );
        }


        /// <summary>
        /// 删除分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Del( string ID )
        {
            MsgEntity me = _categoryService.Delete(ID);
            return Json( me, JsonRequestBehavior.AllowGet );
        }
    }
}