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
        public string ID { get; set; }
        public string DICT_CODE { get; set; }
        public string DICT_NAME { get; set; }
        public string DICT_DES { get; set; }
    }
}