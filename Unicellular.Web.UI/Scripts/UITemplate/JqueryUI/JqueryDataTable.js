
/*
    基于jqueryUI的datatable封装
    obj：要生成datatable的jquery对象
    options：datatable 配置
    onSelectRow：配置项中增加onSelectRow（参数：第一个rowdata，第二个：rowid）
    isEditAndDel：true or false标定是否需要编辑和删除列（需要在html的thead中最后增加一个th和options的columns项最后增加{ "data": "" } ）
    onEditData：编辑事件（参数：第一个rowdata，第二个：rowid）
    onDelData：删除事件（参数：第一个rowdata，第二个：rowid）
*/
function initDataTable(obj,options) {
    var datatable = null;
    var opt = {
        "autoWidth": false,        //禁止自动向table中添加width属性
        "rowId": "ID",               //指定row的id为后台返回的ID
        //由于表格dom无法对齐，换用bootstrap table
        // "dom": 'i<"toolbar">',
        //"dom": 'T<"clear">lfrtip',
        //"sDom": '<"top"fli>rt<"bottom"p>',
        //"tableTools": {
        //    "sSwfPath": "~/AdminLTE-2.3.0/plugins/datatables/extensions/TableTools/swf/copy_csv_xls_pdf.swf",
        //    "aButtons": [
        //         { "sExtends": "csv", "oSelectorOpts": { "page": "current" },"sFileName":"test.csv" },"print"
        //    ],
        //    "sRowSelect": "os"
        //},
        "stateSave": false,
        "sPaginationType": "full_numbers",
        "oLanguage": {              //国际化配置  
            "sProcessing": "正在获取数据，请稍后...",
            "sLengthMenu": "每页显示 _MENU_ 条",
            "sZeroRecords": "没有您要搜索的内容",
            "sInfo": "从 _START_ 到  _END_/共 _TOTAL_ 条",
            "sInfoEmpty": "记录数为0",
            "sInfoFiltered": "(全部记录数 _MAX_ 条)",
            "sInfoPostFix": "",
            "sSearch": "搜索",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "首页",
                "sPrevious": "上一页",
                "sNext": "下一页",
                "sLast": "尾页"
            }
        }
    };
    //最后一列定义编辑和删除
    var btnEditId = obj.attr("id") + "_row_edit";           //编辑按钮id
    var btnDelId = obj.attr("id") + "_row_del";             //删除按钮id
    var columnDefs =
     [{
         "targets": -1,
         "searchable": false,
         "orderable": false,
         "width": "130px",
         "data": null,
         "defaultContent": "<button title='编辑' id=" + btnEditId + " class='btn .btn-mini btn-primary' type='button'><i class='fa fa-pencil fa-fw'></i>编辑</button>&nbsp<button id=" + btnDelId + " title='删除'  class='btn .btn-mini btn-primary' type='button'><i class='fa fa-trash-o fa-fw'></i>删除</button>"
     }];
    //扩展配置项
    var dtoption = $.extend(opt, options);

    //扩展的配置项中增加columnDefs（如果配置项中标定isEditAndDel为true）
    if (typeof dtoption.isEditAndDel != "undefined" && dtoption.isEditAndDel) {
        if (typeof dtoption.columnDefs != "undefined" && dtoption.columnDefs instanceof Array) {
            dtoption.columnDefs.concat(columnDefs);
        }
        else {
            dtoption.columnDefs = columnDefs;
        }
    }

    //初始化datatable
    datatable = $(obj).DataTable(dtoption);

    //事件处理
    //绑定行单击事件
    if (typeof (dtoption.onSelectRow) == 'function' && dtoption.onSelectRow) {
        //在每一行上绑定click事件
        $(obj).children("tbody").on("click", "tr", function () {
            var rowdata = datatable.row(this).data();
            //点击的行上有数据并且有rowid，才触发；避免无数据时，点击表格生成的提示信息也触发onSelectRow
            if (rowdata && rowdata.ID) {
                if (!$(this).hasClass("info")) {
                    datatable.$('tr.info').removeClass('info');
                    $(this).addClass('info');
                }
                //调用自定义的onSelectRow方法
                dtoption.onSelectRow(rowdata, rowdata.ID);
            }
        });
    }
    //绑定编辑、删除按钮事件
    if (typeof dtoption.isEditAndDel != "undefined" && dtoption.isEditAndDel) {
        $("#table_dict tbody").on("click", "button", function (event) {
            var rowdata = datatable.row($(this).parents("tr")).data();
            //编辑
            if ($(this).attr("id") == btnEditId) {
                if (typeof (dtoption.onEditData) == 'function' && dtoption.onEditData) {
                    dtoption.onEditData(rowdata, rowdata.ID);
                }
            }
            //删除
            else if ($(this).attr("id") == btnDelId) {
                if (typeof (dtoption.onDelData) == 'function' && dtoption.onDelData) {
                    dtoption.onDelData(rowdata, rowdata.ID);
                }
            }
            event.stopPropagation();        //禁止向上传播事件
        });
    }

    $("div.toolbar").html("<button class='btn btn-primary add_server' ><span>自定义按钮</span></button>");
    $(".add_server").click(function () {
        location.href = "/server/import"
    })

    return datatable;
}