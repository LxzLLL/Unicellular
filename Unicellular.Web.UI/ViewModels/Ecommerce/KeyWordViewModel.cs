using System;
using System.ComponentModel.DataAnnotations;

namespace Unicellular.Web.UI.ViewModels.Ecommerce
{
    /// <summary>
    /// 关键词视图模型
    /// </summary>
    public class KeyWordViewModel
    {
        /// <summary>
        ///ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        ///平台类型，字典项存储（PLAT_TYPE）
        /// </summary>
        [Required(ErrorMessage ="必须选择平台类型")]
        [Display(Name ="平台类型")]
        public string PLAT_TYPE { get; set; }

        /// <summary>
        ///关键词类型，字典项存储（KEYWORD_TYPE）
        /// </summary>
        [Required( ErrorMessage = "必须选择关键词类型" )]
        [Display( Name = "关键词类型" )]
        public string KEYWORD_TYPE { get; set; }

        /// <summary>
        ///分类ID
        /// </summary>
        [Required( ErrorMessage = "必须选择关键词分类" )]
        [Display( Name = "关键词分类" )]
        public string GOODS_TYPE { get; set; }

        /// <summary>
        ///关键词
        /// </summary>
        [Required( ErrorMessage = "必须填写关键词" )]
        [StringLength( 80 )]
        [Display( Name = "关键词名称" )]
        public string KEY_WORD { get; set; }

        /// <summary>
        ///关键词的中文描述
        /// </summary>
        [Display( Name = "中文描述" )]
        [StringLength( 128 )]
        public string KW_CN { get; set; }

        /// <summary>
        ///关键词最优产品描述
        /// </summary>
        [Display( Name = "最优描述" )]
        [StringLength( 2048 )]
        public string KW_DES { get; set; }

        /// <summary>
        ///平台检索量
        /// </summary>
        [Required( ErrorMessage = "必须填写关键词平台检索量" )]
        [Display( Name = "平台检索量" )]
        public int KW_VOLUME { get; set; }
    }
}