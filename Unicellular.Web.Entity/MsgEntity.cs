using System;
using System.ComponentModel;

namespace Unicellular.Web.Entity
{
    /// <summary>
    /// 返回前台的消息模型
    /// </summary>
    public class MsgEntity
    {
        public enum MsgCodeEnum
        {
            /// <summary>
            /// 成功
            /// </summary>
            [Description("成功")]
            Success=0,
            /// <summary>
            /// 失败
            /// </summary>
            [Description( "失败" )]
            Failure = 1
        }


        /// <summary>
        /// 代码
        /// </summary>
        public MsgCodeEnum MsgCode { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string MsgDes { get; set; }
    }
}