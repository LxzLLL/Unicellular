
var gridKeyWord = null;
var formKeyWordValidator = null;
//表单的knockout 对象
var formObj = function(){
    var self = this;
    self.ID = ko.observable();
    self.PLAT_TYPE = ko.observable();
    self.KEYWORD_TYPE = ko.observable();
    self.GOODS_TYPE = ko.observable();
    self.KEY_WORD = ko.observable();
    self.KW_CN = ko.observable();
    self.KW_DES = ko.observable();
    self.KW_VOLUME = ko.observable();
}
var formInstance = new formObj();


$(function () {
    
    //初始化Table
    initGrid();
    //初始化Button的点击事件
    initButton();

    ////初始化验证
    initValidate();
    ////获取验证插件实例
    formKeyWordValidator = $("#keywordForm").data('bootstrapValidator');

    //knockoutjs绑定数据
    BindKoData();
});

//初始化Table
function initGrid() {
    gridKeyWord = $('#table_keyword').bootstrapTable({
        url: 'KeyWords/GetKeyWords',         //请求后台的URL（*）
        method: 'post',                      //请求方式（*）
        toolbar: '#keyword_toolbar',                //工具按钮用哪个容器
        striped: true,                      //是否显示行间隔色，默认false
        cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
        pagination: true,                   //是否显示分页（*），默认false
        sortOrder: "asc",                   //排序方式，默认“asc”
        queryParams: keywordParams,//传递参数（*）
        sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*），默认client
        pageNumber: 1,                       //初始化加载第一页，默认第一页
        pageSize: 10,                       //每页的记录行数（*），默认10
        pageList: [10, 20, 50, 100, 200, 500],        //可供选择的每页的行数（*）
        search: false,                       //搜索文本框组件，默认false
        //formatSearch: function () { return "名称搜索" },        //格式化搜索框按钮内容
        //searchOnEnterKey: true,            //按下"Enter"键后进行检索
        showColumns: true,                  //显示列下拉列表，默认false
        showRefresh: true,                  //是否显示刷新按钮，默认false
        minimumCountColumns: 2,             //最少允许的列数，默认1
        clickToSelect: true,                //是否启用点击选中行，默认false
        uniqueId: "ID",                     //每一行的唯一标识，一般为主键列，默认id
        showToggle: true,                    //是否显示详细视图和列表视图的切换按钮，默认false
        columns: [
        {
            field: 'PLAT_TYPE_NAME',
            title: '平台类型',
            sortable: true
        }, {
            field: 'KEY_WORD',
            title: '关键词',
            sortable: true
        }, {
            field: 'GOODS_TYPE_NAME',
            title: '分类',
            sortable: true
        } ,{
            field: 'KW_VOLUME',
            title: '平台检索量',
            sortable: true
        }],
        onClickRow: function (row, $element) {
            $('#table_keyword .success').removeClass('success');
            $('#table_keyword').find($element).addClass('success');
        }
    });
}

//表格查询的参数
function keywordParams(params) {
    var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
        pageNumber: params.pageNumber,   //页码
        pageSize: params.limit,                     //页面大小
        sort: params.sort,                             //排序列名
        sortOrder: params.order,                 //排位命令（desc，asc）
        PLAT_TYPE:$("#PlatTypeSearch").val(),
        KEYWORD_TYPE:$("#KeyWordTypeSearch").val(),
        GOODS_TYPE:$("#GoodsTypeSearch").val(),
        KEY_WORD: $("#KeyWordSearch").val()
        //search: params.search,                      //搜索文本
        //DICT_ID: dictId
    };
    return temp;
}

//初始化Button的点击事件
function initButton() {
    //初始化页面上面的CURD按钮事件
    $("#btn_add_kw").on("click", fn_addKWItem);
    $("#btn_edit_kw").on("click", fn_editKWItem);
    $("#btn_delete_kw").on("click", fn_delKWItem);
    $("#btn_query").on("click", fn_searchKWItem);
}



//添加
function fn_addKWItem() {

}
//编辑
function fn_editKWItem() {

}
//删除
function fn_delKWItem() {

}

//查询
function fn_searchKWItem() {
    //更新表格
    $('#table_keyword').bootstrapTable('refresh');
}


//初始化表单验证
function initValidate() {
    $("#keywordForm").bootstrapValidator({
        excluded: [':disabled', ':hidden', ':not(:visible)'],               //初始化时加上，在之后可以restForm
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            KEY_WORD: {
                validators: {
                    notEmpty: {
                        message: '关键字不能为空',
                    },
                    stringLength: {
                        max: 80,
                        message: "关键字不能超过80个字符"
                    }
                }
            },
            KW_VOLUME: {
                validators: {
                    notEmpty: {
                        message: '平台数量不能为空',
                    }
                }
            },
            PLAT_TYPE: {
                validators: {
                    notEmpty: {
                        message: '未选择平台类型',
                    }
                }
            },
            KEYWORD_TYPE: {
                validators: {
                    notEmpty: {
                        message: '未选择关键字类型',
                    }
                }
            },
            GOODS_TYPE: {
                validators: {
                    notEmpty: {
                        message: '未选择分类',
                    }
                }
            }
        }
    });
}


//knockoutjs绑定数据
function BindKoData() {
    ko.applyBindings(formInstance, $("#keywordForm").get(0));
}