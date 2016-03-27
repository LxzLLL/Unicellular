﻿
$(function () {
    //字典列表填充
    //initGridDict();
    //字典项列表填充
    //initGridDictItem();

    //1.初始化Table
    var gridDict = new TableInit();
    gridDict.Init();

    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();


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
            searchTimeOut:1000,                //搜索自动触发时间
            formatSearch: function () { return "名称搜索" },
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
            }]
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
    };

    return oInit;
};


//字典项列表填充
function initGridDictItem() {
    gridDictItem = initDataTable($("#table_dictitem"), {
        "ajax": "Dict/GetDictItem",
        "columns": [
            { "data": "DI_CODE" },
            { "data": "DI_NAME" },
            { "data": "DI_DES" }
        ],
        "onSelectRow": function (rowdata, rowid) {
            alert("You clicked on " + rowid + " row");
        }
    });
}