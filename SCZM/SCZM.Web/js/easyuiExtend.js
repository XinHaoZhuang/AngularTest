
/*====================================
 *datagrid扩展方法clientPaging
 *根据页码、排序显示数据
 *loadFilter：pagerFilter
====================================*/
(function ($) {

    function pagerFilter(data) {
        if ($.isArray(data)) {	// is array
            data = {
                total: data.length,
                rows: data
            }
        }
        var target = this;
        var dg = $(target);
        var state = dg.data('datagrid');
        var opts = dg.datagrid('options');
        if (!state.allRows) {
            state.allRows = (data.rows);
        }
        if (!opts.remoteSort && opts.sortName) {
            var names = opts.sortName.split(',');
            var orders = opts.sortOrder.split(',');
            state.allRows.sort(function (r1, r2) {
                var r = 0;
                for (var i = 0; i < names.length; i++) {
                    var sn = names[i];
                    var so = orders[i];
                    var col = $(target).datagrid('getColumnOption', sn);
                    var sortFunc = col.sorter || function (a, b) {
                        return a == b ? 0 : (a > b ? 1 : -1);
                    };
                    r = sortFunc(r1[sn], r2[sn]) * (so == 'asc' ? 1 : -1);
                    if (r != 0) {
                        return r;
                    }
                }
                return r;
            });
        }
        var start = (opts.pageNumber - 1) * parseInt(opts.pageSize);
        var end = start + parseInt(opts.pageSize);
        data.rows = state.allRows.slice(start, end);
        return data;
    }
    

    var loadDataMethod = $.fn.datagrid.methods.loadData;
    var deleteRowMethod = $.fn.datagrid.methods.deleteRow;
    $.extend($.fn.datagrid.methods, {
        clientPaging: function (jq) {
            return jq.each(function () {
                var dg = $(this);
                var state = dg.data('datagrid');
                var opts = state.options;
                opts.loadFilter = pagerFilter;
                var onBeforeLoad = opts.onBeforeLoad;
                opts.onBeforeLoad = function (param) {
                    state.allRows = null;
                    return onBeforeLoad.call(this, param);
                }
                var pager = dg.datagrid('getPager');
                pager.pagination({
                    onSelectPage: function (pageNum, pageSize) {
                        opts.pageNumber = pageNum;
                        opts.pageSize = pageSize;
                        pager.pagination('refresh', {
                            pageNumber: pageNum,
                            pageSize: pageSize
                        });
                        dg.datagrid('loadData', state.allRows);
                    }
                });
                $(this).datagrid('loadData', state.data);
                if (opts.url) {
                    $(this).datagrid('reload');
                }
            });
        }
        ,
        loadData: function (jq, data) {
            jq.each(function () {
                $(this).data('datagrid').allRows = null;
            });
            return loadDataMethod.call($.fn.datagrid.methods, jq, data);
        },
        deleteRow: function (jq, index) {
            return jq.each(function () {
                var row = $(this).datagrid('getRows')[index];
                deleteRowMethod.call($.fn.datagrid.methods, $(this), index);
                var state = $(this).data('datagrid');
                if (state.options.loadFilter == pagerFilter) {
                    for (var i = 0; i < state.allRows.length; i++) {
                        if (state.allRows[i] == row) {
                            state.allRows.splice(i, 1);
                            break;
                        }
                    }
                    $(this).datagrid('loadData', state.allRows);
                }
            });
        },
        getAllRows: function (jq) {
            return jq.data('datagrid').allRows;
        }
    })
    
})(jQuery);
/*====================================
 *EasyUI的自定义日期验证规则 
====================================*/
function checkDateTime(type, datetime, split) {
    var date = datetime.split(" ")[0];
    var time = datetime.split(" ")[1];
    //alert(date + '\n' + time)  
    switch (type) {
        case "time"://检查时分秒的有效性  
            var tempArr = time.split(":");
            if (parseInt(tempArr[0]) > 23) {
                return false;
            }
            if (parseInt(tempArr[1]) > 60 || parseInt(tempArr[2]) > 60) {
                return false;
            }
            break;
        case "date"://检查日期的有效性  
            var tempArr = date.split("-");
            if (parseInt(tempArr[1]) == 0 || parseInt(tempArr[1]) > 12) {//月份  
                return false;
            }
            var lastday = new Date(parseInt(tempArr[0]), parseInt(tempArr[1]), 0);//获取当月的最后一天日期           
            if (parseInt(tempArr[2]) == 0 || parseInt(tempArr[2]) > lastday.getDate()) {//当月的日  
                return false;
            }
            var myDate = new Date(parseInt(tempArr[0]), parseInt(tempArr[1]) - 1, parseInt(tempArr[2]));
            if (myDate == "Invalid Date") {
                return false;
            }
            break;
    }

    return true;
}

/*** 
 @author ganning 
 EasyUI的自定义日期验证规则 
 * */
function initCuntValidateBox() {
    //自定义验证规则 名称为myDate  
    $.extend($.fn.validatebox.defaults.rules, {
        myDate: {
            validator: function (mytime, param) {
                //标准时间格式  
                var regStandard = /^\1|2\d{3}-\d{1,2}-\d{1,2} \d{1,2}:\d{1,2}:\d{1,2}$/;    //匹配标准日期格式  2014-1-20 12:10:00  
                //日期快速输入格式    
                var regA = /^\1|2\d{3}-\d{1,2}-\d{1,2}-\d{1,2}-\d{1,2}-\d{1,2}$/;    //匹配日期 和 时\分\秒  2014-1-20-12-10-00 意思是2014-1-20 12:10:00  
                var regB = /^\1|2\d{3}-\d{1,2}-\d{1,2}-\d{1,2}-\d{1,2}$/;    //匹配日期 和 时\分  2014-1-20-12-10-00 意思是2014-1-20 12:10  
                var regC = /^\1|2\d{3}-\d{1,2}-\d{1,2}$/;    //匹配日期  2014-1-20  
                var x = "";

                if (!regStandard.test(mytime)) {
                    if (regA.test(mytime)) {
                        var tempArr = mytime.split('-');
                        x = tempArr[0] + "-" + tempArr[1] + "-" + tempArr[2] + " " + tempArr[3] + ":" + tempArr[4] + ":" + tempArr[5];
                        /**/
                        if (!(checkDateTime("date", x) && checkDateTime("time", x))) {
                            $.fn.validatebox.defaults.rules.myDate.message = "日期格式错误！";
                            return false;
                        }
                    } else
                        if (regB.test(mytime)) {
                            var tempArr = mytime.split('-');
                            x = tempArr[0] + "-" + tempArr[1] + "-" + tempArr[2] + " " + tempArr[3] + ":" + tempArr[4] + ":00";
                            /**/
                            if (!(checkDateTime("date", x) && checkDateTime("time", x))) {
                                $.fn.validatebox.defaults.rules.myDate.message = "日期格式错误";
                                return false;
                            }
                        } else
                            if (regC.test(mytime)) {
                                x = mytime + " 00:00:00";
                                /**/
                                if (!checkDateTime("date", x)) {
                                    $.fn.validatebox.defaults.rules.myDate.message = "日期格式错误";
                                    return false;
                                }
                            } else {
                                $.fn.validatebox.defaults.rules.myDate.message = "日期格式错误";
                                return false;
                            }
                } else {
                    if (!(checkDateTime("date", mytime) && checkDateTime("time", mytime))) {
                        $.fn.validatebox.defaults.rules.myDate.message = "日期格式错误";
                        return false;
                    }
                }
                return true;
            },
            message: ''
        }
    });
}
/*====================================
 *验证手机号、电话号码
====================================*/
$.extend($.fn.validatebox.defaults.rules, {
    phoneNum: { //验证手机号   
        validator: function (value, param) {
            return /^1[3-8]+\d{9}$/.test(value);
        },
        message: '请输入正确的手机号码。'
    },

    telNum: { //既验证手机号，又验证座机号
        validator: function (value, param) {
            return /(^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$)|(^((\d3)|(\d{3}\-))?(1[358]\d{9})$)/.test(value);
        },
        message: '请输入正确的电话号码，例如：0532-88939999、13900000001。'
    }
});

