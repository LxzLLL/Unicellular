/**
 * @author Arvin
 *  常用方法
 */
var XLBase = null;
(function () {

    /**
   * Array扩展
   */
    //为array对象添加indexOfByObj方法 @param {Object}  obj 
    Array.prototype.indexOfByObj = function (obj) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] === obj)
                return i;
        }
    };
    //为array增加put方法：将指定元素插入到数组指定下标出 
    Array.prototype.putObj = function (obj, index) {
        return this.slice(index, 0, obj);
    };
    //数组删除元素方法
    Array.prototype.removeObj = function (obj) {
        return this.slice(this.indexOfByObj(obj), 1);
    };
    //数组删除元素方法
    Array.prototype.containObj = function (obj) {
        return this.indexOf(obj) >= 0;
    };


    /**
	* string扩展
	*/
    //转成int类型
    String.prototype.intVal = function () {
        return parseInt(this);
    };
    //转成int类型
    String.prototype.floatVal = function () {
        return parseFloat(this);
    };
    //判断是否为数字型
    String.prototype.isNum = function () {
        return isNaN(this * 1);
    };
    //去除首尾、左、右字符
    String.prototype.xlTrim = function (s) {
        s = (s ? s : "\\s");
        s = ("(" + s + ")");
        var reg_trim = new RegExp("(^" + s + "*)|(" + s + "*$)", "g");
        return this.replace(reg_trim, "");
    };
    String.prototype.xlLTrim = function (s) {
        s = (s ? s : "\\s");                            //没有传入参数的，默认去空格
        s = ("(" + s + ")");
        var reg_lTrim = new RegExp("^" + s + "*", "g");     //拼正则
        return this.replace(reg_lTrim, "");
    };
    String.prototype.xlRTrim = function (s) {
        s = (s ? s : "\\s");
        s = ("(" + s + ")");
        var reg_rTrim = new RegExp(s + "*$", "g");
        return this.replace(reg_rTrim, "");
    };

    /**
	* Date扩展
	*/
    Date.prototype.Format = function (format) {
        /**
         * 对Date的扩展，将 Date 转化为指定格式的String 
         * 月(M)、日(d)、12小时(h)、24小时(H)、分(m)、秒(s)、周(E)、季度(q) 可以用 1-2 个占位符
         * 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)
         * eg:
         * (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
         * (new Date()).Format("yyyy-MM-dd E HH:mm:ss") ==> 2009-03-10 二 20:09:04 
         * (new Date()).Format("yyyy-MM-dd EE hh:mm:ss") ==> 2009-03-10 周二 08:09:04 
         * (new Date()).Format("yyyy-MM-dd EEE hh:mm:ss") ==> 2009-03-10 星期二 08:09:04
         * (new Date()).Format("yyyy-M-d h:m:s.S") ==> 2006-7-2 8:9:4.18
         */
        var o = {
            "M+": this.getMonth() + 1, //月份       
            "d+": this.getDate(), //日       
            "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时       
            "H+": this.getHours(), //小时       
            "m+": this.getMinutes(), //分       
            "s+": this.getSeconds(), //秒       
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度       
            "S": this.getMilliseconds() //毫秒       
        };
        var week = {
            "0": "\u65e5",
            "1": "\u4e00",
            "2": "\u4e8c",
            "3": "\u4e09",
            "4": "\u56db",
            "5": "\u4e94",
            "6": "\u516d"
        };
        if (/(y+)/.test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        }
        if (/(E+)/.test(fmt)) {
            fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "\u661f\u671f" : "\u5468") : "") + week[this.getDay() + ""]);
        }
        for (var k in o) {
            if (new RegExp("(" + k + ")").test(fmt)) {
                fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            }
        }
        return fmt;
    }

    XLBase = {
        //获取uuid
        getUid: function () {
            var s = [];
            var hexDigits = "0123456789abcdef";
            for (var i = 0; i < 36; i++) {
                s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
            }
            s[14] = "4"; // bits 12-15 of the time_hi_and_version field to 0010
            s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1); // bits 6-7 of the clock_seq_hi_and_reserved to 01
            s[8] = s[13] = s[18] = s[23] = "";

            var uid = s.join("");
            return uid;
        },
        //验证ip和port，location格式为***.***.***.***:***，如果无端口，则不验证
        isValidIP: function (location) {
            var objRet = {
                error: "",
                isValid: true
            };
            var loca = jQuery.trim(location);
            if (!loca) {
                objRet.error = "IP地址为空！";
                objRet.isValid = false;
            }
            else {
                var strArray = loca.split(":");
                var ipTrim = $.trim(strArray[0]);
                var reg = /^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/
                if (!reg.test(ipTrim)) {
                    objRet.error = "IP地址错误！";
                    objRet.isValid = false;
                }
                else {
                    //端口
                    if (strArray.length > 1) {
                        var portTrim = $.trim(strArray[1]);
                        if (!portTrim || portTrim < 1 || portTrim > 65535) {
                            objRet.error = "端口错误！";
                            objRet.isValid = false;
                        }
                    }
                }
            }
            return objRet;
        },
        //是否是Email
        isEmail: function (e) {
            var re = /^[a-zA-z_][a-zA-Z_0-9]*?@\w{1,}.\[a-zA-Z]{1,}/;
            return re.test(e);
        },
        //是否存在中文字符
        existChinese: function (e) {
            return /^[\x00-\xff]*$/.test(e);
        },
        //是否是URL
        isUrl: function (e) {
            return new RegExp("^http[s]?:\/\/([\w-]+\.)+[\w-]+([\w-./?%&=]*)?$", "i").test(e);
            //return /^http[s]?:\/\/([\w-]+\.)+[\w-]+([\w-./?%&=]*)?$/i.test(this);
        },
        //是否是手机号码
        isPhoneCall: function (e) {
            return /(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)/.test(e);
        },
        /*-------------------- Cookie操作 --------------------*/
        setCookie: function (sName, sValue, oExpires, sPath, sDomain, bSecure) { //-----设置Cookie-----  
            var sCookie = sName + '=' + encodeURIComponent(sValue);
            if (oExpires) {
                var date = new Date();
                date.setTime(date.getTime() + oExpires * 60 * 60 * 1000);
                sCookie += '; expires=' + date.toUTCString();
            }
            if (sPath) {
                sCookie += '; path=' + sPath;
            }
            if (sDomain) {
                sCookie += '; domain=' + sDomain;
            }
            if (bSecure) {
                sCookie += '; secure';
            }
            document.cookie = sCookie;
        },
        getCookie: function (sName) { //-----获得Cookie值-----  
            var sRE = '(?:; )?' + sName + '=([^;]*)';
            var oRE = new RegExp(sRE);
            if (oRE.test(document.cookie)) {
                return decodeURIComponent(RegExp['$1']);
            } else {
                return null;
            }
        },
        deleteCookie: function (sName, sPath, sDomain) { //-----删除Cookie值-----  
            this.setCookie(sName, '', new Date(0), sPath, sDomain);
        },
        clearCookie: function () { //清除所有Cookie  
            var cookies = document.cookie.split(";");
            var len = cookies.length;
            for (var i = 0; i < len; i++) {
                var cookie = cookies[i];
                var eqPos = cookie.indexOf("=");
                var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
                name = name.replace(/^\s*|\s*$/, "");
                document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/"
            }
        },
        //类型转，根据不同类型数据排序，比如，整型，日期，浮点，字符串，接受两个参数，一个是值，一个是排序的数据类型 
        convert: function (sValue, sDataType) {
            switch (sDataType) {
                case "int":
                    return parseInt(sValue);
                case "float":
                    return parseFloat(sValue);
                case "date":
                    return new Date(Date.parse(sValue));
                default:
                    return sValue.toString();
            }
        },
        //获取URL Query值
        GetQueryString: function (name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null)
                return unescape(r[2]);
            return null;
        },
        //escape编码
        HTMLEscape: function (str) {
            var s = "";
            if (str.length == 0) {
                return "";
            }
            s = str.replace(/&/g, "&amp;");
            s = s.replace(/</g, "&lt;");
            s = s.replace(/>/g, "&gt;");
            s = s.replace(/ /g, "&nbsp;");
            s = s.replace(/\'/g, "&#39;");
            s = s.replace(/\"/g, "&quot;");
            return s;
        },
        //动态加载js
        loadJS: function (url) {
            var statu = true; //初始状态  
            var js = document.getElementsByTagName("script");
            var len = js.length;
            for (var i = 0; i < len; i++) {
                if (js[i].getAttribute("src") == url) {
                    statu = false; //如果已经添加，则设置为Flase，不再添加  
                }
            }
            if (statu) {
                var script = document.createElement("script");
                script.type = "text/javascript";
                script.src = url;
                var header = document.getElementsByTagName("head")[0];
                header.appendChild(script);

            }
        },
        //jsonp跨域封装
        getJSONP: function (url, callback) {
            if (!url) {
                return;
            }
            var a = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j']; //定义一个数组以便产生随机函数名
            var r1 = Math.floor(Math.random() * 10);
            var r2 = Math.floor(Math.random() * 10);
            var r3 = Math.floor(Math.random() * 10);
            var name = 'getJSONP' + a[r1] + a[r2] + a[r3];
            var cbname = 'getJSONP.' + name; //作为jsonp函数的属性
            if (url.indexOf('?') === -1) {
                url += '?jsonp=' + cbname;
            } else {
                url += '&jsonp=' + cbname;
            }
            var script = document.createElement('script');
            //定义被脚本执行的回调函数
            getJSONP[name] = function (e) {
                try {
                    //alert(e.name);
                    callback && callback(e);
                } catch (e) {
                    //
                }
                finally {
                    //最后删除该函数与script元素
                    delete getJSOP[name];
                    script.parentNode.removeChild(script);
                }

            }
            script.src = url;
            document.getElementsByTagName('head')[0].appendChild(script);
            //调用方式demo
            //getJSONP('http://localhost:8888/', function (response) {
            //    alert(response.name);
            //});
        },

        /*-------------------- jquery Ajax操作 --------------------*/
        //依赖toastr组件
        //同步ajax，post/get方式(默认post)，返回json对象
        ajaxBackJson: function (urlPath,method, dataJson, successFun) {
            if (!dataJson) {
                dataJson = {};
            }
            if (!method) {
                method = "POST";
            }
            $.ajax({
                cache: false,
                async: false,
                type: method,
                url: urlPath,
                dataType: "json",
                data: dataJson,
                success: function (data, textStatus) {
                    successFun(data, textStatus);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //返回失败提示
                    toastr.error(XMLHttpRequest.statusText, "异常");
                }
            });
        }
    };
})();



/**
 * @author Arvin
 *  Jquery扩展
 */
(function ($) {
    //将表单转化成Json对象
    $.fn.serializeJson = function () {
        var serializeObj = {};
        var array = this.serializeArray();
        var str = this.serialize();
        $(array).each(function () {
            if (serializeObj[this.name]) {
                if ($.isArray(serializeObj[this.name])) {
                    serializeObj[this.name].push(this.value);
                } else {
                    serializeObj[this.name] = [serializeObj[this.name], this.value];
                }
            } else {
                serializeObj[this.name] = this.value;
            }
        });
        return serializeObj;
    };
    //console.log($(this).serializeJson());
})(jQuery)



