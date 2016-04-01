﻿

//dict的viewmodel对象
var viewModelDict = {
    ID: ko.observable(""),
    DICT_CODE: ko.observable(""),
    DICT_NAME: ko.observable(""),
    DICT_DES: ko.observable(""),
    js_Form_Title: ko.observable(""),
    js_Form_Operation:ko.observable("")
};

//dictItem的viewmodel对象
var viewModelDictItem = {
    ID: ko.observable(""),
    DI_CODE: ko.observable(""),
    DI_NAME: ko.observable(""),
    DI_DES: ko.observable(""),
    js_Form_Title: ko.observable(""),
    js_Form_Operation:ko.observable("")
};
//to model忽略的属性
//var ignorMapping = {
//    'ignore': ["js_Form_Title", "js_Form_Operation"]
//}

var gridDict = null;
var oButtonDict = null;
var formDictValidator = null;

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
        }
    });
    //获取验证插件实例
    formDictValidator = $("#dictForm").data('bootstrapValidator');
    //knockoutjs绑定数据
    BindKoData();

    //绑定表单保存事件
    $("#btnDictSave").on("click", DictSave);
});

//字典列表填充
var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#table_dict').bootstrapTable({
            url: 'Dict/GetDict',         //请求后台的URL（*）
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
            pageList: [10, 20, 50, 100, 200, 500],        //可供选择的每页的行数（*）
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
                $('.success').removeClass('success');
                $($element).addClass('success');
            }
        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        //debugger;
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            pageNumber: params.pageNumber,   //页码
            pageSize: params.limit,                     //页面大小
            sort: params.sort,                             //排序列名
            sortOrder: params.order,                 //排位命令（desc，asc）
            search:params.search                       //搜索文本
            //departmentname: $("#txt_search_departmentname").val(),
            //statu: $("#txt_search_statu").val()
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
        $("#btn_add_dict").on("click", function () { fn_OperaDict("add") });
        $("#btn_edit_dict").on("click", function () { fn_OperaDict("edit") });
        $("#btn_delete_dict").on("click", fn_DelDict);
    };
    return oInit;
};

//dict按钮操作
function fn_OperaDict(operate) {
    debugger;
    viewModelDict = new viewModelDict();
    if (operate === "add") {
        viewModelDict.js_Form_Title("字典项添加");
        viewModelDict.js_Form_Operation("add");
    }
    else if (operate==="edit") {
        viewModelDict.s_Form_Title("字典项编辑");
        viewModelDict.js_Form_Operation("edit");
    }
    $("#modal_dict").modal("show");
}
//dict删除
function fn_DelDict() {
    //debugger;
    //警告框
    //警告框确认，执行删除
    //结果提示
    toastr["warning"]("警告，不能删除", "警告");
}

//dict保存按钮
function DictSave() {
    if (!formDictValidator.isValid()) {
        return;
    }
    if (viewModelDict.js_Form_Operation() == "add") {
        //form的json化
        //var objJson = ko.mapping.toJSON(viewModelDict, ignorMapping);
        var objJson = $("#dictForm").serializeJson();
        //debugger;
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "Dict/AddDict",
            dataType: "json",
            data: { data: objJson },
            success: function (data, textStatus) {
                //提示成功
                if (data.MsgCode == 0) {
                    toastr["sucess"](data.MsgDes, "成功");
                    //更新表格
                    gridDict.refresh();
                }
                else {
                    //提示失败
                    toastr["failure"](data.MsgDes, "失败");
                }
            }
        });
        //alert(obj);
    }
    else if (viewModelDict.js_Form_Operation() == "edit") {
        //编辑
    }
}




//字典项列表填充
function initGridDictItem() {
    gridDictItem = $('#table_dictitem').bootstrapTable({
        url: 'Dict/GetDictItem',         //请求后台的URL（*）
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
        }]
    });
};
//得到dictItem查询的参数
function dictItemParams (params) {
    //debugger;
    var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
        pageNumber: params.pageNumber,   //页码
        pageSize: params.limit,                     //页面大小
        sort: params.sort,                             //排序列名
        sortOrder: params.order,                 //排位命令（desc，asc）
        search:params.search                       //搜索文本
        //departmentname: $("#txt_search_departmentname").val(),
        //statu: $("#txt_search_statu").val()
    };
    return temp;
};

//初始化 dictitem的按钮
function initButtonDictItem() {

}



//knockoutjs绑定dictForm数据
function BindKoData() {
    ko.applyBindings(viewModelDict, $("#modal_dict").get(0));
}