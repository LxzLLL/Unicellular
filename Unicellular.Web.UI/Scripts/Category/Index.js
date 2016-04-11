var tree = null;
var formValidator = null;
//表单的knockout 对象
var formObj = function () {
    var self = this;
    self.ID = ko.observable();
    self.CATEGORY_CODE = ko.observable();
    self.CATEGORY_NAME = ko.observable();
    self.PARENT_ID = ko.observable();
    self.CATEGORY_DES = ko.observable();
}
var formInstance = new formObj();

$(function () {

    //初始化Tree
    initTree();
    //初始化Button的点击事件
    initButton();

    ////初始化验证
    initValidate();
    ////获取验证插件实例
    formValidator = $("#Form").data('bootstrapValidator');

    //knockoutjs绑定数据
    BindKoData();
});

//初始化Tree
function initTree() {
    var setting = {
        view: {
            showLine: true,
            selectedMulti: false
        },
        async: {
            enable: true,
            type: "get",
            url: "Category/GetTree"
        },
        data:{
            key: {
                name: "CATEGORY_NAME"
            },
            simpleData: {
                enable:true,
                idKey: "ID",
                pIdKey: "PARENT_ID",
                rootPId:""
            }
        },
        callback: {
            onClick:fn_nodeSelected
        }
    };
    tree = $.fn.zTree.init($("#tree"), setting, null);
    //tree.expandNode()
}
//初始化Button的点击事件
function initButton() {
    //初始化页面上面的CURD按钮事件
    $("#btn_add").on("click", fn_add);
    $("#btn_edit").on("click", fn_edit);
    $("#btn_delete").on("click", fn_del);
}

//初始化表单验证
function initValidate() {
    $("#Form").bootstrapValidator({
        excluded: [':disabled', ':hidden', ':not(:visible)'],               //初始化时加上，在之后可以restForm
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            CATEGORY_CODE: {
                validators: {
                    notEmpty: {
                        message: '不能为空',
                    },
                    stringLength: {
                        max: 32,
                        message: "不能超过32个字符"
                    }
                }
            },
            CATEGORY_NAME: {
                validators: {
                    notEmpty: {
                        message: '不能为空',
                    },
                    stringLength: {
                        max: 64,
                        message: "不能超过64个字符"
                    }
                }
            },
            CATEGORY_DES: {
                validators: {
                    stringLength: {
                        max: 256,
                        message: "不能超过256个字符"
                    }
                }
            }
        }
    });
}

//根据id获取树数据
function fn_load(id) {
    //ajax post
    XLBase.ajaxBackJson(
        "Category/Get",                 //url
        "GET",                             //method
        { ID: id },                //dataJson
        function (data, textStatus) {               //successFun
            //提示成功
            if (data) {
                formValidator.resetForm(true);              //清空上次验证状态
                ko.mapping.fromJS(ko.mapping.toJS(new formObj()), {}, formInstance);//必须使用，否则在清空验证后会出现绑定不上的问题
                ko.mapping.fromJSON(JSON.stringify(data), {}, formInstance);
                
            }
        });
}

//添加
function fn_add() {
    var node = fn_isSelected();
    if (!node || node.length <= 0) {
        return;
    }

    formValidator.validate();
    if (!formValidator.isValid()) {
        return;
    }
    //将Pid赋值到formInstance
    formInstance.ID("");
    formInstance.PARENT_ID(node[0].ID);
    //form的json化
    var objJson = ko.mapping.toJSON(formInstance);
    //ajax post
    XLBase.ajaxBackJson(
        "Category/Add",                 //url
        "POST",                             //method
        JSON.parse(objJson),                //dataJson
        function (data, textStatus) {               //successFun
            //提示成功
            if (data.MsgCode == 0) {
                toastr.success(data.MsgDes, "成功");
                //更新Tree
                tree.reAsyncChildNodes(null, "refresh");
            }
            else {
                //提示失败
                toastr.error(data.MsgDes, "失败");
            }
        });
}
//编辑
function fn_edit() {
    var node = fn_isSelected();
    if (!node || node.length <= 0) {
        return;
    }

    formValidator.validate();
    if (!formValidator.isValid()) {
        return;
    }
    //form的json化
    var objJson = ko.mapping.toJSON(formInstance);
    //ajax post
    XLBase.ajaxBackJson(
        "Category/Edit",                 //url
        "POST",                             //method
        JSON.parse(objJson),                //dataJson
        function (data, textStatus) {               //successFun
            //提示成功
            if (data.MsgCode == 0) {
                toastr.success(data.MsgDes, "成功");
                //更新Tree
                tree.reAsyncChildNodes(null, "refresh");
            }
            else {
                //提示失败
                toastr.error(data.MsgDes, "失败");
            }
        });
}
//删除
function fn_del() {
    var node = fn_isSelected();
    if (!node || node.length <= 0) {
        return;
    }

    //ajax post
    XLBase.ajaxBackJson(
        "Category/Del",                 //url
        "POST",                             //method
        { ID: node[0].ID },                //dataJson
        function (data, textStatus) {               //successFun
            //提示成功
            if (data.MsgCode == 0) {
                toastr.success(data.MsgDes, "成功");
                //更新Tree
                tree.reAsyncChildNodes(null, "refresh");
            }
            else {
                //提示失败
                toastr.error(data.MsgDes, "失败");
            }
        });
}

//是否选中树中节点
function fn_isSelected() {
    var node = tree.getSelectedNodes();
    if (node.length <= 0) {
        toastr.warning("未选择对应分类", "警告");
    }
    return node;
}

//树单击事件
function fn_nodeSelected(event, treeId, treeNode) {
    fn_load(treeNode.ID);
}


//knockoutjs绑定数据
function BindKoData() {
    ko.applyBindings(formInstance, $("#Form").get(0));
}