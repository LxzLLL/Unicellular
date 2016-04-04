

//dict的viewmodel对象
var viewModelDict = function () {
    var self = this;
    self.ID = ko.observable();
    self.DICT_CODE = ko.observable();
    self.DICT_NAME = ko.observable();
    self.DICT_DES = ko.observable();
    self.js_Form_Title = ko.observable();
    self.js_Form_Operation = ko.observable();
}
var dictInstence = new viewModelDict();
//dictItem的viewmodel对象
var viewModelDictItem = function(){
    var self = this;
    self.ID = ko.observable();
    self.DICT_ID = ko.observable();
    self.DI_CODE = ko.observable();
    self.DI_NAME = ko.observable();
    self.DI_DES = ko.observable();
    self.js_Form_Title = ko.observable();
    self.js_Form_Operation = ko.observable();
};
var dictItemInstence = new viewModelDictItem();
//to model忽略的属性
var ignorMapping = {
    'ignore': ["js_Form_Title", "js_Form_Operation"]
}

var gridDict = null;
var oButtonDict = null;
var formDictValidator = null;
var formDictItemValidator = null;

var gridDictItem = null;

$(function () {
    //////初始化dict表
    //1.初始化Table
    gridDict = new TableInit();
    gridDict.Init();

    //2.初始化Button的点击事件
    oButtonDict = new ButtonInit();
    oButtonDict.Init();

    //////初始化dictitem
    initGridDictItem();
    initButtonDictItem();

    //初始化验证
    $("#dictForm").bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        excluded: [':disabled']             //初始化时加上，在之后可以restForm
    });
    //获取验证插件实例
    formDictValidator = $("#dictForm").data('bootstrapValidator');

    //初始化验证
    $("#dictItemForm").bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        excluded: [':disabled']             //初始化时加上，在之后可以restForm
    });
    //获取验证插件实例
    formDictItemValidator = $("#dictItemForm").data('bootstrapValidator');
    
    //knockoutjs绑定数据
    BindKoData();

    //绑定表单保存事件
    $("#btnDictSave").on("click", DictSave);
    $("#btnDictItemSave").on("click", DictItemSave);
});

//字典列表填充
var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#table_dict').bootstrapTable({
            url: 'Dict/GetDicts',         //请求后台的URL（*）
            method: 'post',                      //请求方式（*）
            toolbar: '#dict_toolbar',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            //sortable: true,                     //是否启用排序，默认为ture
            sortOrder: "asc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 20, 50, 100, 200, 500,'All'],        //可供选择的每页的行数（*）
            search: true,                       //搜索文本框组件
            //searchTimeOut:1000,                //搜索自动触发时间
            formatSearch: function () { return "名称搜索" },
            searchOnEnterKey:true,            //按下"Enter"键后进行检索
            //strictSearch: true,
            showColumns: true,                  //是否显示所有列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            //height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "ID",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            //cardView: false,                    //是否显示详细视图
            //detailView: false,                   //是否显示父子表
            columns: [
            {
                field: 'DICT_CODE',
                title: '字典编码',
                sortable: true
            }, {
                field: 'DICT_NAME',
                title: '字典名称',
                sortable: true
            }, {
                field: 'DICT_DES',
                title: '字典描述'
            }],
            onClickRow: function (row, $element) {
                $('#table_dict .success').removeClass('success');
                $('#table_dict').find($element).addClass('success');
                //刷新dictItem表格
                LoadDictItem();
            }
        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            pageNumber: params.pageNumber,   //页码
            pageSize: params.limit,                     //页面大小
            sort: params.sort,                             //排序列名
            sortOrder: params.order,                 //排位命令（desc，asc）
            search: params.search                     //搜索文本
            
        };
        return temp;
    };
    return oTableInit;
}

var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};

    oInit.Init = function () {
        //初始化页面上面的按钮事件
        $("#btn_add_dict").on("click", fn_addDict);
        $("#btn_edit_dict").on("click", fn_editDict);
        $("#btn_delete_dict").on("click", fn_DelDict);
    };
    return oInit;
};
//dict添加按钮触发事件
function fn_addDict() {
    //清空表单上次填写的元素
    formDictValidator.resetForm(true);              //清空上次验证状态
    ko.mapping.fromJS(ko.mapping.toJS(new viewModelDict()), {}, dictInstence);
    dictInstence.js_Form_Title("字典添加");
    dictInstence.js_Form_Operation("add");
    $("#modal_dict").modal("show");
}

//dict编辑按钮触发事件
function fn_editDict() {
    var selectedRow = fn_dictRowSelected();
    if (!selectedRow || !selectedRow.ID) {
        toastr.warning("未选择字典", "警告");
        return;
    }
    //获取选择的数据
    //ajax post
    XLBase.ajaxBackJson(
        "Dict/GetDict",                                                     //url
        "GET",                                                                  //method
        { dictId: selectedRow.ID },                                         //dataJson
        function (data, textStatus) {                                   //successFun
            formDictValidator.resetForm(true);              //清空上次验证状态
            ko.mapping.fromJSON(JSON.stringify(data), {}, dictInstence);
            dictInstence.js_Form_Title("字典编辑");
            dictInstence.js_Form_Operation("edit");
            $("#modal_dict").modal("show");
        });
}
//dict删除
function fn_DelDict() {
    //警告框
    //警告框确认，执行删除
    //结果提示
    var selectedRow = fn_dictRowSelected();
    if (!selectedRow || !selectedRow.ID) {
        toastr.warning("未选择字典", "警告");
        return;
    }
    XLBase.ajaxBackJson(
        "Dict/DelDict",                                                     //url
        "POST",                                                                  //method
        { dictId: selectedRow.ID },                                         //dataJson
        function (data, textStatus) {                                   //successFun
            //提示成功
            if (data.MsgCode == 0) {
                toastr.success(data.MsgDes, "成功");
                //更新表格
                $('#table_dict').bootstrapTable('refresh');
            }
            else {
                //提示失败
                toastr.error(data.MsgDes, "失败");
            }
        });
}
//获取dict表格选择的id，并弹出提示框
function fn_dictRowSelected() {
    var index = $('#table_dict').find('tr.success').data('index');
    var selRow = $('#table_dict').bootstrapTable('getData')[index];
    return selRow;
}


//dict保存按钮
function DictSave() {
    formDictValidator.validate();
    if (!formDictValidator.isValid()) {
        return;
    }
    //form的json化
    var objJson = ko.mapping.toJSON(dictInstence, ignorMapping);
    //var objJson = $("#dictForm").serializeJson();
    if (dictInstence.js_Form_Operation() == "add") {
        //ajax post
        XLBase.ajaxBackJson(
            "Dict/AddDict",                 //url
            "POST",                             //method
            { data: objJson },                //dataJson
            function (data, textStatus) {               //successFun
                //提示成功
                if (data.MsgCode == 0) {
                    toastr.success(data.MsgDes, "成功");
                    //更新表格
                    $('#table_dict').bootstrapTable('refresh');
                }
                else {
                    //提示失败
                    toastr.error(data.MsgDes, "失败");
                }
            });
    }
    else if (dictInstence.js_Form_Operation() == "edit") {
        //编辑
        //ajax post
        XLBase.ajaxBackJson(
            "Dict/EditDict",
            "POST",
            { data: objJson },
            function (data, textStatus) {
                //提示成功
                if (data.MsgCode == 0) {
                    toastr.success(data.MsgDes, "成功");
                    //更新表格
                    $('#table_dict').bootstrapTable('refresh');
                }
                else {
                    //提示失败
                    toastr.error(data.MsgDes, "失败");
                }
            });
    }
}




//字典项列表填充
function initGridDictItem() {
    gridDictItem = $('#table_dictitem').bootstrapTable({
        url: 'Dict/GetDictItems',         //请求后台的URL（*）
        method: 'post',                      //请求方式（*）
        toolbar: '#di_toolbar',                //工具按钮用哪个容器
        striped: true,                      //是否显示行间隔色，默认false
        cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
        pagination: true,                   //是否显示分页（*），默认false
        sortOrder: "asc",                   //排序方式，默认“asc”
        queryParams: dictItemParams,//传递参数（*）
        sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*），默认client
        pageNumber: 1,                       //初始化加载第一页，默认第一页
        pageSize: 10,                       //每页的记录行数（*），默认10
        pageList: [10, 20, 50, 100, 200, 500],        //可供选择的每页的行数（*）
        search: true,                       //搜索文本框组件，默认false
        formatSearch: function () { return "名称搜索" },        //格式化搜索框按钮内容
        searchOnEnterKey:true,            //按下"Enter"键后进行检索
        showColumns: true,                  //显示列下拉列表，默认false
        showRefresh: true,                  //是否显示刷新按钮，默认false
        minimumCountColumns: 2,             //最少允许的列数，默认1
        clickToSelect: true,                //是否启用点击选中行，默认false
        uniqueId: "ID",                     //每一行的唯一标识，一般为主键列，默认id
        showToggle: true,                    //是否显示详细视图和列表视图的切换按钮，默认false
        columns: [
        {
            field: 'DI_CODE',
            title: '字典项编码',
            sortable: true
        }, {
            field: 'DI_NAME',
            title: '字典项名称',
            sortable: true
        }, {
            field: 'DI_DES',
            title: '字典项描述'
        }],
        onClickRow: function (row, $element) {
            $('#table_dictitem .success').removeClass('success');
            $('#table_dictitem').find($element).addClass('success');
        }
    });
};
//得到dictItem查询的参数
function dictItemParams(params) {
    var dictId = null;
    var selectedRow = fn_dictRowSelected();
    if (selectedRow && selectedRow.ID) {
        dictId = selectedRow.ID;
    }
    var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
        pageNumber: params.pageNumber,   //页码
        pageSize: params.limit,                     //页面大小
        sort: params.sort,                             //排序列名
        sortOrder: params.order,                 //排位命令（desc，asc）
        search: params.search,                      //搜索文本
        DICT_ID: dictId
    };
    return temp;
};

//初始化 dictitem的按钮
function initButtonDictItem() {
    //初始化页面上面的按钮事件
    $("#btn_add_di").on("click", fn_addDictItem);
    $("#btn_edit_di").on("click", fn_editDictItem);
    $("#btn_delete_di").on("click", fn_DelDictItem);
}


//dictItem添加按钮触发事件
function fn_addDictItem() {
    //先判断dict是否选择
    
    var selectedRow = fn_dictRowSelected();
    if (!selectedRow || !selectedRow.ID) {
        toastr.warning("未选择字典", "警告");
        return;
    }
    //清空表单上次填写的元素
    formDictItemValidator.resetForm(true);              //清空上次验证状态
    ko.mapping.fromJS(ko.mapping.toJS(new viewModelDictItem()), {}, dictItemInstence);
    dictItemInstence.js_Form_Title("字典项添加");
    dictItemInstence.js_Form_Operation("add");
    $("#modal_dictitem").modal("show");
}

//dictItem编辑按钮触发事件
function fn_editDictItem() {
    var selectedRow = fn_dictItemRowSelected();
    if (!selectedRow || !selectedRow.ID) {
        return;
    }
    //获取选择的数据
    //ajax post
    XLBase.ajaxBackJson(
        "Dict/GetDictItem",                                                     //url
        "GET",                                                                  //method
        { dictItemId: selectedRow.ID },               //dataJson
        function (data, textStatus) {                                   //successFun
            formDictItemValidator.resetForm(true);              //清空上次验证状态
            ko.mapping.fromJSON(JSON.stringify(data), {}, dictItemInstence);
            dictItemInstence.js_Form_Title("字典项编辑");
            dictItemInstence.js_Form_Operation("edit");
            $("#modal_dictitem").modal("show");
        });
}
//dictItem删除
function fn_DelDictItem() {
    //警告框
    //警告框确认，执行删除
    //结果提示
    var selectedRow = fn_dictItemRowSelected();
    if (!selectedRow || !selectedRow.ID) {
        return;
    }
    XLBase.ajaxBackJson(
        "Dict/DelDictItem",                                                     //url
        "POST",                                                                  //method
        { dictItemId: selectedRow.ID },                                         //dataJson
        function (data, textStatus) {                                   //successFun
            //提示成功
            if (data.MsgCode == 0) {
                toastr.success(data.MsgDes, "成功");
                //更新表格
                $('#table_dictitem').bootstrapTable('refresh');
            }
            else {
                //提示失败
                toastr.error(data.MsgDes, "失败");
            }
        });
}
//获取dictItem表格选择的id，并弹出提示框
function fn_dictItemRowSelected() {
    var selectedRow = null;
    var index = $('#table_dictitem').find('tr.success').data('index');
    var selRow = $('#table_dictitem').bootstrapTable('getData')[index];

    if (!selRow || !selRow.ID) {
        toastr.warning("未选择字典项", "警告");
    }
    else {
        selectedRow = selRow;
    }
    return selectedRow;
}


//dictItem保存按钮
function DictItemSave() {
    formDictItemValidator.validate();
    if (!formDictItemValidator.isValid()) {
        return;
    }
    //form的json化
    //var objJson = ko.mapping.toJSON(dictItemInstence, ignorMapping);
    if (dictItemInstence.js_Form_Operation() == "add") {
        dictItemInstence.DICT_ID(fn_dictRowSelected().ID);              //新增时，需要dict的ID
        var objJson = ko.mapping.toJSON(dictItemInstence, ignorMapping);              
        //ajax post
        XLBase.ajaxBackJson(
            "Dict/AddDictItem",                 //url
            "POST",                             //method
            { data: objJson },                //dataJson
            function (data, textStatus) {               //successFun
                //提示成功
                if (data.MsgCode == 0) {
                    toastr.success(data.MsgDes, "成功");
                    //更新表格
                    $('#table_dictitem').bootstrapTable('refresh');
                }
                else {
                    //提示失败
                    toastr.error(data.MsgDes, "失败");
                }
            });
    }
    else if (dictItemInstence.js_Form_Operation() == "edit") {
        var objJson = ko.mapping.toJSON(dictItemInstence, ignorMapping);
        //编辑
        //ajax post
        XLBase.ajaxBackJson(
            "Dict/EditDictItem",
            "POST",
            //{ data: objJson },
            JSON.parse( objJson),
            function (data, textStatus) {
                //提示成功
                if (data.MsgCode == 0) {
                    toastr.success(data.MsgDes, "成功");
                    //更新表格
                    $('#table_dictitem').bootstrapTable('refresh');
                }
                else {
                    //提示失败
                    toastr.error(data.MsgDes, "失败");
                }
            });
    }
}


//点击字典时，同步刷新字典项列表
function LoadDictItem() {
    var dictSelectedRow = fn_dictRowSelected();
    if (dictSelectedRow && dictSelectedRow.ID) {
        $('#table_dictitem').bootstrapTable('refresh');
        //gridDictItem.refresh({ url: { query: { DICT_CODE: dictSelectedRow.DICT_CODE } } });
    }

}

//knockoutjs绑定dictForm数据
function BindKoData() {
    ko.applyBindings(dictInstence, $("#modal_dict").get(0));
    ko.applyBindings(dictItemInstence, $("#modal_dictitem").get(0));
}