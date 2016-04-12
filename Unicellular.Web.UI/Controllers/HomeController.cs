using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Unicellular.Web.BLL.System;
using Unicellular.Web.Entity.System;
namespace Unicellular.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Title = "Home Page";
            //SysDictService dictbll = new SysDictService();
            //List<T_Sys_Dict> dicts = dictbll.GetDictAll();
            //ViewBag.Dicts = dicts;
            return View(  );
        }

        public ActionResult Demo2()
        {
            return View();
        }
    }
}
