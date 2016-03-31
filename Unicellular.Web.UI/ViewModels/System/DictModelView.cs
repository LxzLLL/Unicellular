using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Unicellular.Web.UI.ViewModels.System
{
    /// <summary>
    /// 字典模型视图
    /// </summary>
    public class DictModelView
    {
        [Display(Name ="ID")]
        public string ID { get; set; }
        [Display(Name ="DICT_CODE")]
        public string DICT_CODE { get; set; }
        [Display( Name = "DICT_NAME" )]
        public string DICT_NAME { get; set; }
        [Display( Name = "DICT_DES" )]
        public string DICT_DES { get; set; }
    }
}