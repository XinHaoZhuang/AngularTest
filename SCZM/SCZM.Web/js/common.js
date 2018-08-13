//===================================================================基础方法 开始=======================================================================================================
//字符串去除两边空格

function trim(str) {
    if ((str.charAt(0) != ' ') && (str.charAt(str.length - 1) != ' ')) { return str; }
    while (str.charAt(0) == ' ') {
        str = '' + str.substring(1, str.length);
    }
    while (str.charAt(str.length - 1) == ' ') {
        str = '' + str.substring(0, str.length - 1);
    }
    return str;
}
function delLastComma(str) {
    return delLastChar(str, ',');
}
//删除最后一个字符

function delLastChar(str,lastChar) {
    if (str == '' || str == null) {
        return '';
    }
    else {
        var n = str.lastIndexOf(lastChar);
        if (n >= 0) {
            return str.substring(0, n);
        }
        else {
            return str;
        }
    }
}
//判断是否为空
function chkNull(str) {
    if (str == null || trim(str) == '') {
        return false;
    } else {
        return true;
    }
}
//null转空
function nullToStr(str) {
    if (str == null) {
        return '';
    }
    else {
        return str;
    }
}
//数字格式化

//function formatNum(number, decimals) {
//    return parseFloat(number).toString() == "NaN" ? 0 : parseFloat(parseFloat(number).toFixed(decimals));
//}
//数字格式化
function isNumber(val) {
    var regPos = /^\d+(\.\d+)?$/; //非负浮点数
    var regNeg = /^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$/; //负浮点数
    if (regPos.test(val) || regNeg.test(val)) {
        return true;
    } else {
        return false;
    }
}
function formatNum(number, decimals) {
    //return parseFloat(number).toString() == "NaN" ? 0 : parseFloat(parseFloat(number).toFixed(decimals));
    if (isNumber(number)) {
        var tmp = Math.pow(10, decimals);
        return Math.round(number * tmp) / tmp;
    }
    else {
        return 0;
    }
}
//数字去零
function formatNoZero(value) {
    if (value == undefined || value.toString() == '' || value == null) {
        return '';
    }
    else {
        if (!isNaN(value)) {
            return parseFloat(value);
        }
        else {
            return value;
        }
    }
}
//除法函数，用来得到精确的除法结果
//说明：javascript的除法结果会有误差，在两个浮点数相除的时候会比较明显。这个函数返回较为精确的除法结果。
//调用：accDiv(arg1,arg2)
//返回值：arg1除以arg2的精确结果
function accDiv(arg1, arg2) {
    var t1 = 0, t2 = 0, r1, r2;
    try { t1 = arg1.toString().split(".")[1].length } catch (e) { }
    try { t2 = arg2.toString().split(".")[1].length } catch (e) { }
    with (Math) {
        r1 = Number(arg1.toString().replace(".", ""))
        r2 = Number(arg2.toString().replace(".", ""))
        return (r1 / r2) * pow(10, t2 - t1);
    }
}
//给Number类型增加一个div方法，调用起来更加 方便。
Number.prototype.div = function (arg) {
    return accDiv(this, arg);
}
//乘法函数，用来得到精确的乘法结果
//说明：javascript的乘法结果会有误差，在两个浮点数相乘的时候会比较明显。这个函数返回较为精确的乘法结果。
//调用：accMul(arg1,arg2)
//返回值：arg1乘以 arg2的精确结果
function accMul(arg1, arg2) {
    var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
    try { m += s1.split(".")[1].length } catch (e) { }
    try { m += s2.split(".")[1].length } catch (e) { }
    return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m)
}
// 给Number类型增加一个mul方法，调用起来更加方便。
Number.prototype.mul = function (arg) {
    return accMul(arg, this);
}
//加法函数，用来得到精确的加法结果
//说明：javascript的加法结果会有误差，在两个浮点数相加的时候会比较明显。这个函数返回较为精确的加法结果。
//调用：accAdd(arg1,arg2)
// 返回值：arg1加上arg2的精确结果
function accAdd(arg1, arg2) {
    var r1, r2, m;
    try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
    try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
    m = Math.pow(10, Math.max(r1, r2))
    return (arg1 * m + arg2 * m) / m
}
//给Number类型增加一个add方法，调用起来更加方便。
Number.prototype.add = function (arg) {
    return accAdd(arg, this);
}
///////////////日期格式化
//格式化日期

Date.prototype.format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1,                 //月份 
        "d+": this.getDate(),                    //日 
        "h+": this.getHours(),                   //小时 
        "m+": this.getMinutes(),                 //分 
        "s+": this.getSeconds(),                 //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds()             //毫秒 
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}
//格式化为日期
function formatDate(value) {
    if (value == '' || value == null) {
        return '';
    }
    else {
        return new Date(value).format("yyyy-MM-dd");
    }
}
//格式化为日期、时间

function formatDateTime(value) {
    if (value == '' || value == null) {
        return '';
    }
    else {
        return new Date(value).format("yyyy-MM-dd hh:mm:ss");
    }
}

//得到n月前的第一天 n不能大于12
function getMonthFirstDay(n) {
    var now = new Date(),
    year = now.getFullYear(),
    month = now.getMonth() + 1;
    if (n == undefined) {
        n = 0;
    }
    month -= n;
    if (month < 1) {
        year -= 1;
        month += 12;
    }
    year = year.toString();
    month = month < 10 ? '0' + month : month.toString();

    var newDate = year + '-' + month + "-01";
    return newDate;
}
///////////////获取连接字符串

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = decodeURI(window.location.search).substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
///////////////设置cookie
//function setCookie(c_name, value, expiredays) {
//    var exdate = new Date()
//    exdate.setDate(exdate.getDate() + expiredays)
//    document.cookie = c_name + "=" + escape(value) +
//    ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString())
//}
//如果需要设定自定义过期时间
//这是有设定过期时间的使用示例：

//s20是代表20秒

//h是指小时，如12小时则是：h12
//d是天数，30天则：d30
//setCookie("name", "hayden", "s20");
function setCookie(name, value, time) {
    if (time == undefined || time == null) {
        //document.cookie = name + "=" + escape(value) + ";path=/";
        document.cookie = name + "=" + encodeURI(value) + ";path=/";
    }
    else {
        var strsec = getsec(time);
        var exp = new Date();
        exp.setTime(exp.getTime() + strsec * 1);
        //document.cookie = name + "=" + escape(value) + ";path=/;expires=" + exp.toGMTString();
        document.cookie = name + "=" + encodeURI(value) + ";path=/;expires=" + exp.toGMTString();
    }
}
function getsec(str) {
    var str1 = str.substring(1, str.length) * 1;
    var str2 = str.substring(0, 1);
    if (str2 == "s") {
        return str1 * 1000;
    }
    else if (str2 == "h") {
        return str1 * 60 * 60 * 1000;
    }
    else if (str2 == "d") {
        return str1 * 24 * 60 * 60 * 1000;
    }
}
///////////////读取cookie
function getCookie(name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(name + "=")
        if (c_start != -1) {
            c_start = c_start + name.length + 1
            c_end = document.cookie.indexOf(";", c_start)
            if (c_end == -1) c_end = document.cookie.length
            //return unescape(document.cookie.substring(c_start, c_end));
            return decodeURI(document.cookie.substring(c_start, c_end));
        }
    }
    return ""
}
///////////////获取当前季度
function getCurrQuarter() {
    var myDate = new Date();
    var currMonth = myDate.getMonth() + 1;
    var currQuarter = Math.floor((currMonth % 3 == 0 ? (currMonth / 3) : (currMonth / 3 + 1)));
    return currQuarter;
}
///////////////获取时间字符串

function formatDateToString(date) {
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;

    return year.toString() + month.toString() + day.toString();
}
function formateDateAndTimeToString(date) {
    var hours = date.getHours();
    var mins = date.getMinutes();
    var secs = date.getSeconds();
    //var msecs = date.getMilliseconds();
    if (hours < 10) hours = "0" + hours;
    if (mins < 10) mins = "0" + mins;
    if (secs < 10) secs = "0" + secs;
    //if (msecs < 10) secs = "0" + msecs;
    return formatDateToString(date) + hours.toString() + mins.toString() + secs.toString();
}
//===================================================================基础方法 结束=======================================================================================================
//===================================================================页面公共方法 开始=======================================================================================================
//返回系统名称
function getSystemName() {
    return "四川住贸维修管理系统";
}

//页面过期时间
var pageExpires = 3600000;
//var pageExpires = 10000;
//frame内点击事件

function frameClick() {
    if (new Date - parent.lastOperaTime < pageExpires) {
        parent.lastOperaTime = new Date;
    }
}
//检查页面是否合法

function checkPage(loginSalt, menuId) {
    //说明你的页面在框架中显示
    if (top.location != self.location) {
        if (nullToStr(loginSalt) == "" || nullToStr(menuId) == "") {
            window.parent.location.href = "../../../login.aspx";
        }
        if (parent.lastOperaTime == undefined || parent.lastOperaTime == null) {
            window.parent.location.href = "../../../login.aspx";
        }
        else {
            if (new Date - parent.lastOperaTime > pageExpires) {
                window.location.href = "../../Error/relogin.html";
            }
        }
    } else {
        window.location.href = "../../../login.aspx";
    }
}
//设置页面按钮、显示

function initPage(menuId) {
    //实现对字符码的截获，keypress中屏蔽了这些功能按键
    document.onkeypress = banBackSpace;
    //对功能按键的获取
    document.onkeydown = banBackSpace;

    var btnInfo = parent.document.getElementById("hidBtnInfo_" + menuId).value;
    if (btnInfo != "") {
        var url = window.location.toString();
        var btnJson = eval('(' + btnInfo + ')');
        var btn;
        for (var i = 0; i < btnJson.length; i++) {
            btn = $("#" + btnJson[i].ElementName);
            if (url.indexOf(btnJson[i].PageUrl) > 0 && btn.length > 0) {
                btn.css('display', 'none');
            }
        }
    }
    //var obj=$(window.parent.document).find('#menu_' + menuId);
    var navHtml = "";
    var menuObj = $('#menu_' + menuId, window.parent.document);
    menuObj.parents('ul').each(function () {
        var parentObj = $(this).siblings('.item');
        if ($(this).siblings('.item').length > 0) {
            navHtml = "<span>" + $($(this).siblings('.item')[0]).children('span')[0].innerText + "</span><i class='arrow'></i>" + navHtml;
        }
    });
    $('.location').find('.home').after(navHtml);
    if ($('#hidediv').length > 0) {
        $('#hidediv').css('display', 'none');
    }
}
//阻止页面后退
function banBackSpace(e) {
    var ev = e || window.event;
    //各种浏览器下获取事件对象
    var obj = ev.relatedTarget || ev.srcElement || ev.target || ev.currentTarget;
    //按下Backspace键

    if (ev.keyCode == 8) {
        var tagName = obj.nodeName //标签名称
        //如果标签不是input或者textarea则阻止Backspace
        if (tagName != 'INPUT' && tagName != 'TEXTAREA') {
            return stopIt(ev);
        }
        var tagType = obj.type.toUpperCase();//标签类型
        //input标签除了下面几种类型，全部阻止Backspace
        if (tagName == 'INPUT' && (tagType != 'TEXT' && tagType != 'TEXTAREA' && tagType != 'PASSWORD')) {
            return stopIt(ev);
        }
        //input或者textarea输入框如果不可编辑则阻止Backspace
        if ((tagName == 'INPUT' || tagName == 'TEXTAREA') && (obj.readOnly == true || obj.disabled == true)) {
            return stopIt(ev);
        }
    }
}
function stopIt(ev) {
    if (ev.preventDefault) {
        //preventDefault()方法阻止元素发生默认的行为

        ev.preventDefault();
    }
    if (ev.returnValue) {
        //IE浏览器下用window.event.returnValue = false;实现阻止元素发生默认的行为

        ev.returnValue = false;
    }
    return false;
}
//Tab控制函数
function tabs(tabObj) {
    var tabNum = $(tabObj).parent().index("li")
    //设置点击后的切换样式
    $(tabObj).parent().parent().find("li a").removeClass("selected");
    $(tabObj).addClass("selected");
    //根据参数决定显示内容
    $(".tab-content").hide();
    $(".tab-content").eq(tabNum).show();
}
//easyui控件检测 contentObj为控件所在div ，tabObj为如果检测不通过，需要点击的tab，不需要则不传
function easyuiCheck(contentObj, tabObj) {
    var result = true;
    if ($.fn.validatebox) {
        var t = $('#' + contentObj);
        t.find('.validatebox-text:not(:disabled)').each(function () {
            if (!$(this).validatebox('isValid')) {
                result = false;
                if (tabObj != undefined && tabObj != '') {
                    $('#' + tabObj).click();
                }
                $(this).focus();
                return false;
            }
        });
    }
    return result;
}

//根据jsonID返回text
function getJsonTextById(json, id) {
    for (var i = 0; i < json.length; i++) {
        if (json[i].id == id) {
            return json[i].text;
        }
    }
    return "";
}
//等待层

/*
* 使用方法: 
 * 开启:MaskUtil.mask(); 
* 关闭:MaskUtil.unmask(); 
* MaskUtil.mask('其它提示文字...'); 
*/
var MaskUtil = (function () {
    var $mask, $maskMsg;
    var defMsg = '正在处理，请稍待。。。';
    function init() {
        if (!$mask) {
            $mask = $("<div class=\"mask\"></div>").appendTo("body");
        }
        if (!$maskMsg) {
            $maskMsg = $("<div class=\"maskmsg\">" + defMsg + "</div>").appendTo("body");
        }
        $mask.css({ width: "100%", height: $(document).height() });
        var scrollTop = $(document.body).scrollTop();
        $maskMsg.css({
            left: ($(document.body).outerWidth(true) - 190) / 2
            , top: (($(window).height() - 45) / 2) + scrollTop
        });
    }
    return {
        mask: function (msg) {
            init();
            $mask.show();
            $maskMsg.html(msg || defMsg).show();
        }
        , unmask: function () {
            $mask.hide();
            $maskMsg.hide();
        }
    }
}());

//获取grid样式参数
function getGridOptions(flag) {
    var gridOptions = {
        fit: true,
        rownumbers: true,
        singleSelect: false,
        autoRowHeight: false,
        striped: true,
        pagination: true,
        pageNumber: 1,
        pageSize: 15,
        pageList: [15, 20, 50, 100],
        remoteSort: false,
        multiSort: false,
        showFooter: false,
        //url: '',
        //loadMsg: '',
        //queryParams: {},
        rowStyler: function (index, row) {
            return 'height:30px;';
        },
        onClickRow: function (rowIndex, rowData) { },
        onDblClickRow: function (rowIndex, rowData) { },
        onSelect: function (rowIndex, rowData) { },
        onUnselect: function (rowIndex, rowData) { },
        onLoadSuccess:function(data){}
    };
    if (flag == "ClientPaging") {
        return gridOptions;
    }
    else if (flag == "ServerPaging") {
        return gridOptions;
    }
    else if (flag == "NOPaging") {
        return gridOptions;
    }
    else {
        return gridOptions;
    }
}
//格式化 0转空
function formatZeroToEmpty(value) {
    if (value != 0) {
        return value;
    }
    else {
        return '';
    }
}
//格式化为百分比

function formatRate(value) {
    if (isNaN(value)) {
        return "";
    }
    else {
        if (value != null && value != "") {
            return parseFloat(parseFloat(value).toFixed(2)) + "%";
        }
        else {
            return value;
        }
    }
}
//格式化为：是、否
function formatBool(value) {
    if (value != null && value != "") {
        if (value == 1) {
            return '是';
        }
        else {
            return '否';
        }
    }
    else {
        return value;
    }
}
//单据状态列style控制
function billStateStyle(value, rowData, rowIndex) {
    if (rowData.BillState == '0') {
        return 'color:#808000;';
    }
    else if (rowData.BillState == '1') {
        return 'color:green;';
    }
    else if (rowData.BillState == '2') {
        return 'color:red;';
    }
    else if (rowData.BillState == '3') {
        return 'color:#00BFFF;';
    }
}
//审批状态列style控制
function auditStateStyle(value, rowData, rowIndex) {
    if (rowData.AuditState == '0') {
        return 'color:red;';
    }
    else if (rowData.AuditState == '1') {
        return 'color:green;';
    }
    else if (rowData.AuditState == '2') {
        return 'color:#FF8C00;';
    }
}
//确认状态列style控制
function confirmStateStyle(value, rowData, rowIndex) {
    if (value == '0') {
        return 'color:red;';
    }
    else if (value == '1') {
        return 'color:green;';
    }
    else if (value == '2') {
        return 'color:#FF8C00;';
    }
}







//加载审核状态页面

function loadProcessPage(divName, billSign, id) {
    var processDiv = $('#' + divName);
    if (processDiv.length > 0) {
        var mainHtml = '<div class="div-content" id="divProcessAudit"><dl><dt>单据状态</dt><dd><span id="spanBillState" style="color:red;"></span></dd></dl>';
        mainHtml += '<dl><dt>审批流状态</dt><dd><table id="execTable" class="border-table" style="width:95%"><thead>';
        mainHtml += '<tr><th style="width:30px;">步骤</th><th style="width:120px;">岗位</th><th style="width:100px;">审批人</th><th style="width:70px;">状态</th><th style="width:120px;">审批时间</th><th>意见</th></tr>';
        mainHtml += '</thead></table></dd></dl></div>';
        processDiv.append(mainHtml);
        if (billSign == "" || id == "") {
            $('#spanBillState').text('页面参数错误');
        }
        else {
            var postData = { "billSign": billSign, "billId": id };
            $.ajax({
                type: "post",
                url: "../../../Ashx/System/sys_Process_BillSet.ashx?action=GetBillProcess&Btn=show&LoginSalt=" + loginSalt + "&MenuId=" + menuId,
                dataType: "json",
                data: postData,
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $.messager.alert(getSystemName(), textStatus, 'info');
                },
                success: function (data, textStatus) {
                    if (data.status == '1') {
                        $('#spanBillState').text(getBillStateMemo(billState));
                        var auditState = "";
                        var style = "";
                        if (data.info.length > 0) {
                            var execHtml = "";
                            for (var i = 0; i < data.info.length; i++) {
                                if (data.info[i].OrderId != "0") {
                                    if (data.info[i].FlagAudit == "0") {
                                        auditState = "未审批";
                                        style = "";
                                    }
                                    else if (data.info[i].FlagAudit == "1") {
                                        auditState = "同意";
                                        style = "";
                                    }
                                    else if (data.info[i].FlagAudit == "2") {
                                        auditState = "不同意";
                                        style = " style='background-color:#FFDEAD;'";
                                    }
                                    execHtml += "<tr" + style + "><th style=' text-align:center'> " + data.info[i].OrderId + "</th><td>" + data.info[i].PostName + "</td><td>" + data.info[i].AuditorName + "</td><td>" + auditState + "</td><td>" + formatDateTime(data.info[i].AuditorTime) + "</td><td>" + data.info[i].AuditorOpinion + "</td></tr>";
                                }
                            }
                            $('#execTable').append(execHtml);
                        }
                        if (!jQuery.isEmptyObject(data.historyInfo)) {
                            var action = "";
                            var historyHtml = '<dl><dt>操作历史</dt>';
                            historyHtml += '<dd><table class="border-table" style="width:95%"><thead><tr>';
                            historyHtml += '<th style="width:30px;">序号</th>';
                            historyHtml += '<th style="width:120px;">岗位</th>';
                            historyHtml += '<th style="width:100px;">操作人</th>';
                            historyHtml += '<th style="width:70px;">动作</th>';
                            historyHtml += '<th style="width:120px;">操作时间</th>';
                            historyHtml += '<th>意见</th></tr></thead>';

                            for (var i = 0; i < data.historyInfo.length; i++) {
                                if (data.historyInfo[i].OrderId == "0") {
                                    if (data.historyInfo[i].FlagAudit == "0") {
                                        action = "取消提交";
                                    }
                                    else if (data.historyInfo[i].FlagAudit == "1") {
                                        action = "提交";
                                    }
                                    historyHtml += "<tr><th style=' text-align:center'>" + (i + 1).toString() + "</th><td>" + data.historyInfo[i].PostName + "</td><td>" + data.historyInfo[i].AuditorName + "</td><td>" + action + "</td><td>" + formatDateTime(data.historyInfo[i].AuditorTime) + "</td><td>" + data.historyInfo[i].AuditorOpinion + "</td></tr>";
                                }
                                else {
                                    if (data.historyInfo[i].FlagAudit == "0") {
                                        action = "取消审批";
                                    }
                                    else if (data.historyInfo[i].FlagAudit == "1") {
                                        action = "审批同意";
                                    }
                                    else if (data.historyInfo[i].FlagAudit == "2") {
                                        action = "审批不同意";
                                    }
                                    historyHtml += "<tr><th style=' text-align:center'> " + (i + 1).toString() + "</th><td>" + data.historyInfo[i].PostName + "</td><td>" + data.historyInfo[i].AuditorName + "</td><td>" + action + "</td><td>" + formatDateTime(data.historyInfo[i].AuditorTime) + "</td><td>" + data.historyInfo[i].AuditorOpinion + "</td></tr>";
                                }
                            }
                            historyHtml += '</table></dd></dl>';
                            $('#divProcessAudit').append(historyHtml);
                        }
                    }
                    else {
                        $.messager.alert(getSystemName(), data.msg, 'info');
                    }
                }
            });
        }
    }
}
//iframe页面退出

function exit() {
    parent.closeSelectTab();
}

//跳转错误页面
function errorJumpPage(error) {
    if (error == '0.1') {
        window.location.href = "../../Error/relogin.html";
    }
    else if (error == '0.2') {
        window.location.href = "../../Error/noPower.html";
    }
    else if (error == '0') {
        window.location.href = "../../Error/systemError.html";
    }
}
//得到页面权限
function getPagePower1(loginSalt, menuId) {
    var postData = { "action": "GetPagePower", "LoginSalt": loginSalt, "MenuId": menuId };
    $.ajax({
        type: "post",
        //async: false,
        url: "../../../Ashx/System/sys_UserPower.ashx?action=GetPagePower&LoginSalt=" + loginSalt + "&MenuId=" + menuId,
        //url: "../../../Ashx/System/sys_UserPower.ashx",
        dataType: "json",
        //data: postData,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert(getSystemName(), errorThrown, 'info');
        },
        success: function (data, textStatus) {
            if (data.status == '1') {
                var btnstr = data.info;
                if (btnstr != '') {
                    var btn = new Array();
                    btn = btnstr.split(",");
                    for (var i = 0; i < btn.length ; i++) {
                        if ($("#" + btn[i]).length > 0) {
                            $("#" + btn[i]).css('display', 'none');
                        }
                    }
                }
                //$('#hidediv').css('display', 'none');
            }
            else {
                //alert(data.msg);
                errorJumpPage(data.status);
            }
        }

    });
}
//附件类导出

function AttachmentExport(Btn, loginSalt, menuId, attachmentId) {

    var exportPage = "../../export.aspx?Btn=" + Btn + "&LoginSalt=" + loginSalt + "&MenuId=" + menuId;
    var f = $('<form action="' + exportPage + '" method="post" id="fm2"></form>');
    var i = $('<input type="hidden" id="txtAttachmentId" name="txtAttachmentId" value="' + attachmentId + '" />');
    var l = $('<input type="hidden" id="txtExportType" name="txtExportType" value="1" />');//导出类型 1为附件导出

    i.appendTo(f);
    l.appendTo(f);
    f.appendTo(document.body).submit();
    document.body.removeChild(f[0]);
}
/*====================================
 *datagrid导出EXCEL 废除
 用提交的方式导出
====================================*/

//根据Grid生成动态table
function ChangeToTable(exportGrid) {
    var tableString = '<table id="ExportTable" cellspacing="0" class="pb">';
    var frozenColumns = exportGrid.datagrid("options").frozenColumns;  // 得到frozenColumns对象  
    var columns = exportGrid.datagrid("options").columns;    // 得到columns对象  
    var nameList = new Array();

    // 载入title  
    if (typeof columns != 'undefined' && columns != '') {
        $(columns).each(function (index) {
            tableString += '\n<tr>';
            if (typeof frozenColumns != 'undefined' && typeof frozenColumns[index] != 'undefined') {
                for (var i = 0; i < frozenColumns[index].length; ++i) {
                    if (!frozenColumns[index][i].hidden) {
                        tableString += '\n<th width="' + frozenColumns[index][i].width + '"';
                        if (typeof frozenColumns[index][i].rowspan != 'undefined' && frozenColumns[index][i].rowspan > 1) {
                            tableString += ' rowspan="' + frozenColumns[index][i].rowspan + '"';
                        }
                        if (typeof frozenColumns[index][i].colspan != 'undefined' && frozenColumns[index][i].colspan > 1) {
                            tableString += ' colspan="' + frozenColumns[index][i].colspan + '"';
                        }
                        if (typeof frozenColumns[index][i].field != 'undefined' && frozenColumns[index][i].field != '') {
                            nameList.push(frozenColumns[index][i]);
                        }
                        tableString += '>' + frozenColumns[0][i].title + '</th>';
                    }
                }
            }
            for (var i = 0; i < columns[index].length; ++i) {
                if (!columns[index][i].hidden) {
                    tableString += '\n<th width="' + columns[index][i].width + '"';
                    if (typeof columns[index][i].rowspan != 'undefined' && columns[index][i].rowspan > 1) {
                        tableString += ' rowspan="' + columns[index][i].rowspan + '"';
                    }
                    if (typeof columns[index][i].colspan != 'undefined' && columns[index][i].colspan > 1) {
                        tableString += ' colspan="' + columns[index][i].colspan + '"';
                    }
                    if (typeof columns[index][i].field != 'undefined' && columns[index][i].field != '') {
                        nameList.push(columns[index][i]);
                    }
                    tableString += '>' + columns[index][i].title + '</th>';
                }
            }
            tableString += '\n</tr>';
        });
    }

    // 载入内容  
    //var rows = exportGrid.datagrid("getRows"); // 这段代码是获取当前页的所有行  
    var rows = exportGrid.data('datagrid').allRows;//取Grid本地数据所有行
    for (var i = 0; i < rows.length; ++i) {
        tableString += '\n<tr>';
        for (var j = 0; j < nameList.length; ++j) {
            var e = nameList[j].field.lastIndexOf('_0');

            tableString += '\n<td';
            if (nameList[j].align != 'undefined' && nameList[j].align != '') {
                tableString += ' style="text-align:' + nameList[j].align + ';"';
            }
            tableString += '>';
            if (e + 2 == nameList[j].field.length) {
                tableString += rows[i][nameList[j].field.substring(0, e)];
            }
            else
                tableString += rows[i][nameList[j].field];
            tableString += '</td>';
        }
        tableString += '\n</tr>';
    }
    tableString += '\n</table>';
    return tableString;
}

//用提交的方式导出
function GridExportExcel(strXlsName, gridId, exportPage) {

    if (exportPage == undefined) {
        exportPage = "../../export.aspx";
    }
    if (gridId == undefined) {
        gridId = "dg";
    }
    var exportGrid = $('#' + gridId + '');

    var f = $('<form action="' + exportPage + '" method="post" id="fm1"></form>');
    var i = $('<input type="hidden" id="txtContent" name="txtContent" />');
    var l = $('<input type="hidden" id="txtName" name="txtName" />');
    i.val(ChangeToTable(exportGrid));
    i.appendTo(f);
    l.val(strXlsName);
    l.appendTo(f);
    f.appendTo(document.body).submit();
    document.body.removeChild(f[0]);
}
/*====================================
根据Grid生成table数据 用于table2excelnew js导出
====================================*/
//根据Grid生成table数据 用于table2excelnew js导出
function GridToTable(gridName) {

    var exportGrid = $('#' + gridName);
    var tableString = '';
    var frozenColumns = exportGrid.datagrid("options").frozenColumns;  // 得到frozenColumns对象  
    var columns = exportGrid.datagrid("options").columns;    // 得到columns对象  
    var nameList = new Array();

    // 载入title  
    if (typeof columns != 'undefined' && columns != '') {
        $(columns).each(function (index) {
            tableString += '<tr>';
            if (typeof frozenColumns != 'undefined' && typeof frozenColumns[index] != 'undefined') {
                for (var i = 0; i < frozenColumns[index].length; ++i) {
                    if (!frozenColumns[index][i].hidden && !columns[index][i].checkbox) {
                        tableString += '<th width=\"' + frozenColumns[index][i].width + '\"';
                        if (typeof frozenColumns[index][i].rowspan != 'undefined' && frozenColumns[index][i].rowspan > 1) {
                            tableString += ' rowspan=\"' + frozenColumns[index][i].rowspan + '\"';
                        }
                        if (typeof frozenColumns[index][i].colspan != 'undefined' && frozenColumns[index][i].colspan > 1) {
                            tableString += ' colspan=\"' + frozenColumns[index][i].colspan + '\"';
                        }
                        if (typeof frozenColumns[index][i].field != 'undefined' && frozenColumns[index][i].field != '') {
                            nameList.push(frozenColumns[index][i]);
                        }
                        tableString += '>' + frozenColumns[0][i].title + '</th>';
                    }
                }
            }
            for (var i = 0; i < columns[index].length; ++i) {
                if (!columns[index][i].hidden && !columns[index][i].checkbox) {
                    tableString += '<th width="' + columns[index][i].width + '"';
                    if (typeof columns[index][i].rowspan != 'undefined' && columns[index][i].rowspan > 1) {
                        tableString += ' rowspan=\"' + columns[index][i].rowspan + '\"';
                    }
                    if (typeof columns[index][i].colspan != 'undefined' && columns[index][i].colspan > 1) {
                        tableString += ' colspan=\"' + columns[index][i].colspan + '\"';
                    }
                    if (typeof columns[index][i].field != 'undefined' && columns[index][i].field != '') {
                        nameList.push(columns[index][i]);
                    }
                    tableString += '>' + (columns[index][i].title == undefined ? "" : columns[index][i].title) + '</th>';
                }
            }
            tableString += '</tr>';
        });
    }

    // 载入内容  
    //var rows = exportGrid.datagrid("getRows"); // 这段代码是获取当前页的所有行  
    var rows = exportGrid.data('datagrid').allRows;//取Grid本地数据所有行
    for (var i = 0; i < rows.length; ++i) {
        tableString += '<tr>';
        for (var j = 0; j < nameList.length; ++j) {

            var e = nameList[j].field.lastIndexOf('_0');

            tableString += '<td';
            if (nameList[j].align != 'undefined' && nameList[j].align != '') {
                tableString += ' style=\"text-align:' + nameList[j].align + ';\"';
            }
            tableString += '>';
            if (e + 2 == nameList[j].field.length) {
                //tableString += rows[i][nameList[j].field.substring(0, e)];
                tableString += (nameList[j].formatter == undefined) ? rows[i][nameList[j].field.substring(0, e)] : nameList[j].formatter(rows[i][nameList[j].field.substring(0, e)]);
            }
            else
                tableString += (nameList[j].formatter == undefined) ? rows[i][nameList[j].field] : nameList[j].formatter(rows[i][nameList[j].field]);
            tableString += '</td>';
        }
        tableString += '</tr>';
    }
    return tableString;
}
function GridToTable2(gridName) {

    var exportGrid = $('#' + gridName);
    var tableString = '';
    var frozenColumns = exportGrid.datagrid("options").frozenColumns;  // 得到frozenColumns对象  
    var columns = exportGrid.datagrid("options").columns;    // 得到columns对象  
    var nameList = new Array();

    // 载入title  
    if (typeof columns != 'undefined' && columns != '') {
        $(columns).each(function (index) {
            tableString += '<tr>';
            if (typeof frozenColumns != 'undefined' && typeof frozenColumns[index] != 'undefined') {
                for (var i = 0; i < frozenColumns[index].length; ++i) {
                    if (!frozenColumns[index][i].hidden && !columns[index][i].checkbox) {
                        tableString += '<th width=\"' + frozenColumns[index][i].width + '\"';
                        if (typeof frozenColumns[index][i].rowspan != 'undefined' && frozenColumns[index][i].rowspan > 1) {
                            tableString += ' rowspan=\"' + frozenColumns[index][i].rowspan + '\"';
                        }
                        if (typeof frozenColumns[index][i].colspan != 'undefined' && frozenColumns[index][i].colspan > 1) {
                            tableString += ' colspan=\"' + frozenColumns[index][i].colspan + '\"';
                        }
                        if (typeof frozenColumns[index][i].field != 'undefined' && frozenColumns[index][i].field != '') {
                            nameList.push(frozenColumns[index][i]);
                        }
                        tableString += '>' + frozenColumns[0][i].title + '</th>';
                    }
                }
            }
            for (var i = 0; i < columns[index].length; ++i) {
                if (!columns[index][i].hidden && !columns[index][i].checkbox) {
                    tableString += '<th width="' + columns[index][i].width + '"';
                    if (typeof columns[index][i].rowspan != 'undefined' && columns[index][i].rowspan > 1) {
                        tableString += ' rowspan=\"' + columns[index][i].rowspan + '\"';
                    }
                    if (typeof columns[index][i].colspan != 'undefined' && columns[index][i].colspan > 1) {
                        tableString += ' colspan=\"' + columns[index][i].colspan + '\"';
                    }
                    if (typeof columns[index][i].field != 'undefined' && columns[index][i].field != '') {
                        nameList.push(columns[index][i]);
                    }
                    tableString += '>' + (columns[index][i].title == undefined ? "" : columns[index][i].title) + '</th>';
                }
            }
            tableString += '</tr>';
        });
    }

    // 载入内容  
    //var rows = exportGrid.datagrid("getRows"); // 这段代码是获取当前页的所有行  
    var rows = exportGrid.data('datagrid').allRows;//取Grid本地数据所有行
    for (var i = 0; i < rows.length; ++i) {
        tableString += '<tr>';
        for (var j = 0; j < nameList.length; ++j) {

            var e = nameList[j].field.lastIndexOf('_0');

            tableString += '<td';
            if (nameList[j].align != 'undefined' && nameList[j].align != '') {
                tableString += ' style=\"text-align:' + nameList[j].align + ';\"';
            }
            tableString += '>';
            if (e + 2 == nameList[j].field.length) {
                //tableString += rows[i][nameList[j].field.substring(0, e)];
                tableString += (nameList[j].formatter == undefined) ? rows[i][nameList[j].field.substring(0, e)] : nameList[j].formatter(rows[i][nameList[j].field.substring(0, e)],rows[i],i);
            }
            else
                tableString += (nameList[j].formatter == undefined) ? rows[i][nameList[j].field] : nameList[j].formatter(rows[i][nameList[j].field],rows[i],i);
            tableString += '</td>';
        }
        tableString += '</tr>';
    }
    return tableString;
}
//===================================================================页面公共方法 开始=======================================================================================================
//===================================================================页面JSON 开始=======================================================================================================
function getJsonTextById(json, idName, idValue,textName) {
    for (var i = 0; i < json.length; i++) {
        if (json[i][idName].toString() === idValue) {
            return json[i][textName];
        }
    }
    return "";
}
//返回年度JSON
function getYearJson() {
    var yearJson = '[';

    for (var i = 0; i < 10; i++) {
        yearJson += '{"id": ' + (2017 + i) + ',"text":"' + (2017 + i) + '年"},';
    }
    yearJson = yearJson.substring(0, yearJson.length - 1);
    yearJson += ']';
    return JSON.parse(yearJson);
}
//返回季度JSON
function getQuarterJson() {
    var quarterJson = [{ "id": "0", "text": "全部" }, { "id": "1", "text": "1季度" }, { "id": "2", "text": "2季度" }, { "id": "3", "text": "3季度" }, { "id": "4", "text": "4季度" }];
    return quarterJson;
}
//返回月度JSON
function getMonthJson() {
    var monthJson = [{ "id": "0", "text": "全部" }, { "id": "1", "text": "1月" }, { "id": "2", "text": "2月" }, { "id": "3", "text": "3月" }, { "id": "4", "text": "4月" }, { "id": "5", "text": "5月" }, { "id": "6", "text": "6月" }, { "id": "7", "text": "7月" }, { "id": "8", "text": "8月" }, { "id": "9", "text": "9月" }, { "id": "10", "text": "10月" }, { "id": "11", "text": "11月" }, { "id": "12", "text": "12月" }];
    return monthJson;
}
//返回单据状态JSON
function getBillStateJson() {
    var tempJson = [{ "id": 0, "text": "全部" }, { "id": 7, "text": "所有未完成" }, { "id": 5, "text": "未提交", "group": "提交状态:" }, { "id": 6, "text": "已提交", "group": "提交状态:" }, { "id": 1, "text": "审批完成", "group": "审批状态:" }, { "id": 2, "text": "审批未完成", "group": "审批状态:" }, { "id": 3, "text": "审批中", "group": "未完成状态:" }, { "id": 4, "text": "审批不同意", "group": "未完成状态:" }];
    return tempJson;
}
//返回审核界面单据状态JSON
function getAuditBillStateJson() {
    var tempJson = [{ "id": 0, "text": "全部" }, { "id": 1, "text": "审批完成", "group": "审批状态:" }, { "id": 2, "text": "审批未完成", "group": "审批状态:" }, { "id": 3, "text": "审批中", "group": "未完成状态:" }, { "id": 4, "text": "审批不同意", "group": "未完成状态:" }];
    return tempJson;
}
//返回审核状态JSON
function getAuditStateJson() {
    var tempJson = [{ "id": 0, "text": "全部" }, { "id": 1, "text": "已审核", "group": "审批状态:" }, { "id": 2, "text": "未审核", "group": "审批状态:" }, { "id": 3, "text": "审批同意", "group": "审批结果:" }, { "id": 4, "text": "审批不同意", "group": "审批结果:" }];
    return tempJson;
}
//返回确认状态JSON
function getConfirmStateJson() {
    var confirmStateJson = [{ "id": 0, "text": "全部" }, { "id": 1, "text": "已确认", "group": "确认状态:" }, { "id": 2, "text": "未确认", "group": "确认状态:" }, { "id": 3, "text": "确认通过", "group": "确认结果:" }, { "id": 4, "text": "确认不通过", "group": "确认结果:" }];
    return confirmStateJson;
}
//返回是否JSON
function getYesOrNoJson() {
    var tempJson = [{ "id": "", "text": "全部" }, { "id": "0", "text": "否" }, { "id": "1", "text": "是" }];
    return tempJson;
}
//返回项目类型JSON
function getPorjTypeJson() {
    var tempJson = [{ "id": "0", "text": "全部" }, { "id": "1", "text": "挂靠" }, { "id": "2", "text": "合营" }, { "id": "3", "text": "自营" }];
    return tempJson;
}
//返回进度JSON
function getProgressJson() {
    var tempJson = [{ "text": "预付款" }, { "text": "中期款" }, {"text": "决算款" }, { "text": "尾款" }];
    return tempJson;
}
function getBillStateMemo(billState) {
    if (billState == '0') {
        return '草稿';
    }
    else if (billState == '1') {
        return '审批完成';
    }
    else if (billState == '2') {
        return '审批不同意';
    }
    else if (billState == '3') {
        return '审批中';
    }
    else  {
        return '';
    }
}

//function getUnitJson() {
//    var tempJson = [{ "id": "0", "text": "未设置" }, { "id": "1", "text": "件" }, { "id": "2", "text": "升" }, { "id": "3", "text": "桶" }, { "id": "4", "text": "盒" }, { "id": "5", "text": "箱" }];
//    return tempJson;
//}
function getRepairAddressJson() {
    var tempJson = [{ 'Id': '0', 'RepairAddress': '现场' }, { 'Id': 1, 'RepairAddress': '车间' }];
    return tempJson;
}
function getCustTypeJson() {
    var tempJson = [{ 'CustomerTypeId': '', 'CustomerTypeName': '全部' },{ 'CustomerTypeId': 1, 'CustomerTypeName': '外部客户' }, { 'CustomerTypeId': 2, 'CustomerTypeName': '内部部门' }];
    return tempJson;
}
function getIntentionTypeJson() {
    var tempJson = [{ 'IntentionTypeId': '', 'IntentionTypeName': '全部' }, { 'IntentionTypeId': 1, 'IntentionTypeName': '新报修' }, { 'IntentionTypeId': 2, 'IntentionTypeName': '返修' }];
    return tempJson;
}
function getFlagSchemeJson() {
    var tempJson = [{ 'FlagSchemeId': 0, 'FlagSchemeName': '全部' }, { 'FlagSchemeId': 1, 'FlagSchemeName': '已提交' }, { 'FlagSchemeId': 2, 'FlagSchemeName': '未提交' }];
    return tempJson;
}
function getScheduleTypeJson() {
    var tempJson = [{ 'ScheduleTypeId': 0, 'ScheduleTypeName': '未开始', 'ScheduleGroup': '维修状态' }, { 'ScheduleTypeId': 5, 'ScheduleTypeName': '维修中', 'ScheduleGroup': '维修状态' }, { 'ScheduleTypeId': 3, 'ScheduleTypeName': '已完成', 'ScheduleGroup': '维修状态' }, { 'ScheduleTypeId': 1, 'ScheduleTypeName': '开始', 'ScheduleGroup': '维修中' }, { 'ScheduleTypeId': 2, 'ScheduleTypeName': '暂停', 'ScheduleGroup': '维修中' }];
    return tempJson;
}
function getPayTypeJson() {
    var tempJson = [{ 'PayTypeId': 1, 'PayTypeName': '一次性付清' }, { 'PayTypeId': 2, 'PayTypeName': '赊销' }];
    return tempJson;
}
function getPauseReasonJson() {
    var tempJson = [{ 'PauseReasonId': 0, 'PauseReason': '人为原因' }, { 'PauseReasonId': 5, 'PauseReason': '等件' }, { 'PauseReasonId': 1, 'PauseReason': '零件原因' }, { 'PauseReasonId': 2, 'PauseReason': '外协原因' }, { 'PauseReasonId': 3, 'PauseReason': '客户原因' }, { 'PauseReasonId': 4, 'PauseReason': '其它' }];
    return tempJson;
}
function getBillType() {
    var tempJson = [{ "BillTypeId": 0, "BillTypeName": '辅料明细' }, { "BillTypeId": 1, "BillTypeName": '辅料加工' }, { "BillTypeId": 2, "BillTypeName": '吊车费' }, { "BillTypeId": 3, "BillTypeName": '柴油明细' }, { "BillTypeId": 4, "BillTypeName": '其他明细' },{ "BillTypeId": 5, "BillTypeName": '洗车费' }];
    return tempJson;
}
function getLeaveTypeJson() {
    var tempJson = [{ "LeaveTypeId": 0, "LeaveTypeName": "完工出厂" }, { "LeaveTypeId": 1, "LeaveTypeName": "未维修出厂" }];
    return tempJson;
}
function getMachineLevelJson() {
    var tempJson = [{ "Level": "1", "Describe": "MINI-160" }, { "Level": "2", "Describe": "200-270" }, { "Level": "3", "Describe": "300-360" }, { "Level": "4", "Describe": "400-460" }, { "Level": "5", "Describe": "650及以上" }];
    return tempJson;
}
function getNumTypeJson() {
    var tempJson = [{ "NumTypeId": "0", "NumTypeName": "固定" }, { "NumTypeId": "1", "NumTypeName": "按数量" }]
    return tempJson;
}
function getRepairModeJson() {
    var tempJson = [{ "ModeId": "1", "ModeName": "整机" }, { "ModeId": "2", "ModeName": "部件" }]
    return tempJson;
}
function getAuditStateJson() {
    var tempJson = [{ "StateId": "0", "StateName": "未审核" }, { "StateId": "1", "StateName": "审核通过" }, { "StateId": "2", "StateName": "审核不同意" }];
    return tempJson;
}
function getDateTypeJson() {
    var tempJson = [{ "DateTypeId": "0", "DateTypeName": "时间段" }, { "DateTypeId": "1", "DateTypeName": "月" }, { "DateTypeId": "2", "DateTypeName": "周" }, { "DateTypeId": "3", "DateTypeName": "日" }];
    return tempJson;
}
function getRepairPartJson() {
    var tempJson = [{ "RepairPartId": "FlagENGKC", "RepairPartName": "ENG KC大修" }, { "RepairPartId": "FlagPPMKC", "RepairPartName": "PPM KC大修" }, { "RepairPartId": "FlagENG", "RepairPartName": "发动机" }, { "RepairPartId": "FlagPPM", "RepairPartName": "主泵" }, { "RepairPartId": "FlagMCV", "RepairPartName": "主控阀" }, { "RepairPartId": "FlagELE", "RepairPartName": "电器" }, { "RepairPartId": "FlagVM", "RepairPartName": "行走马达" }, { "RepairPartId": "FlagRM", "RepairPartName": "回转马达" }, { "RepairPartId": "FlagSM", "RepairPartName": "钣金" }
        , { "RepairPartId": "FlagUM", "RepairPartName": "底盘" }, { "RepairPartId": "FlagVR", "RepairPartName": "焊修" }, { "RepairPartId": "FlagSP", "RepairPartName": "喷漆" }, { "RepairPartId": "FlagOther", "RepairPartName": "其他" }];
    return tempJson;
}
function getFlagRecordJson() {
    var tempJson = [{ "RecordId": "1", "RecordName": "已登记" }, {"RecordId":"2","RecordName":"未登记"}];
    return tempJson;
}
function getFlagStandardJson() {
    var tempJson = [{ "StandardId": "1", "StandardName": "正常" }, { "StandardId": "2", "StandardName": "超时" }, { "StandardId": "3", "StandardName": "工时异常" }, { "StandardId": "4", "StandardName": "未设置" }];
    return tempJson;
}
//===================================================================页面JSON 结束=======================================================================================================
