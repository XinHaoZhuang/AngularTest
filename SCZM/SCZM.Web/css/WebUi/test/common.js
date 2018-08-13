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
    if (str==undefined || str == null) {
        return '';
    }
    else {
        return str;
    }
}
//是否为数字
function isNumber(val) {
    var regPos = /^\d+(\.\d+)?$/; //非负浮点数
    var regNeg = /^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$/; //负浮点数
    if (regPos.test(val) || regNeg.test(val)) {
        return true;
    } else {
        return false;
    }
}
//数字格式化
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

//除法函数，用来得到精确的除法结果
//说明：javascript的除法结果会有误差，在两个浮点数相除的时候会比较明显。这个函数返回较为精确的除法结果。
//调用：accDiv(arg1,arg2)
//返回值：arg1除以arg2的精确结果
function accDiv(arg1, arg2) {
    var t1 = 0, t2 = 0, r1, r2;
    //try { t1 = arg1.toString().split(".")[1].length } catch (e) { }
    //try { t2 = arg2.toString().split(".")[1].length } catch (e) { }
    if (arg1.toString().indexOf(".") > -1) {
        t1 = arg1.toString().split(".")[1].length;
    }
    if (arg2.toString().indexOf(".") > -1) {
        t2 = arg2.toString().split(".")[1].length;
    }
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
    //try { m += s1.split(".")[1].length } catch (e) { }
    //try { m += s2.split(".")[1].length } catch (e) { }
    if (s1.indexOf(".") > -1) {
        m += s1.split(".")[1].length;
    }
    if (s2.indexOf(".") > -1) {
        m += s2.split(".")[1].length;
    }
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
    var r1=0, r2=0, m=0;
    //try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
    //try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
    if (arg1.toString().indexOf(".") > -1) {
        r1 = arg1.toString().split(".")[1].length;
    }
    if (arg2.toString().indexOf(".") > -1) {
        r2 = arg2.toString().split(".")[1].length;
    }
    m = Math.pow(10, Math.max(r1, r2))
    return (arg1 * m + arg2 * m) / m
}
//给Number类型增加一个add方法，调用起来更加方便。
Number.prototype.add = function (arg) {
    return accAdd(arg, this);
}
//数字去零
function formatNoZero(value) {
    if (value==undefined || value == null || value.toString() == '') {
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
/**
* 使用循环的方式判断一个元素是否存在于一个数组中
* @param {Object} arr 数组
* @param {Object} value 元素值
*/
function isInArray(arr, name, value) {
    for (var i = 0; i < arr.length; i++) {
        if (value == arr[i][name]) {
            return true;
        }
    }
    return false;
}
//===================================================================基础方法 结束=======================================================================================================
//===================================================================页面公共方法 开始=======================================================================================================
//返回系统名称
function getSystemName() {
    return "山东高速汽车贸易管理系统";
}
///////////////////////////////////////页面过期////////////////////////////
//页面过期时间
var pageExpires = 3600000;
//var pageExpires = 2000;
//frame内点击事件


function frameClick() {
    if (new Date - parent.lastOperaTime < pageExpires) {
        parent.lastOperaTime = new Date;
    }
}
///////////////////////////////////////页面处理////////////////////////////
//检查页面是否合法
function checkPage(loginSalt, menuId) {
    //页面在框架中显示
    if (top.location != self.location) {
        if (nullToStr(loginSalt) == "" || trim(nullToStr(menuId)) == "") {
            window.parent.location.href = "../../../login.aspx";
            return false;
        }
        if (!parent.document.getElementById("hidBtnInfo_" + menuId)) {
            window.location.href = "../../Error/noPower.html";
            return false;
        }
        if (parent.lastOperaTime == undefined || parent.lastOperaTime == null) {
            window.parent.location.href = "../../../login.aspx";
            return false;
        }
        else {
            if (new Date - parent.lastOperaTime > pageExpires) {
                window.location.href = "../../Error/relogin.html";
                return false;
            }
        }
        return true;
    } else {
        window.location.href = "../../../login.aspx";
        return false;
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
///////////////////////////////////////Tab点击////////////////////////////
//Tab控制函数
function tabs(tabObj) {
    var tabNum = $(tabObj).parent().index("li")
    //设置点击后的切换样式
    $(tabObj).parent().parent().find("li a").removeClass("selected");
    $(tabObj).addClass("selected");
    //根据参数决定显示内容
    //$(".tab-content").hide();
    //$(".tab-content").eq(tabNum).show();
    $(".tab-content").css("visibility", 'hidden');
    $(".tab-content").eq(tabNum).css("visibility", 'visible');
}
///////////////////////////////////////easyui控件检测////////////////////////////
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


///////////////////////////////////////附件////////////////////////////
//附件初始化
function initUpload() {
    $('div.attach-upload').each(function () {
        var $ul = $(this).children('ul');
        if ($(this).children('input').length == 0) {
            $ul.css("border-bottom", "0px");
        }
        else {
            $ul.css("margin-right", "80px");
            $ul.css("border-bottom", "1px solid #DDDDDD");
            if ($ul.attr("data-required") == "true") {
                $ul.css("border-bottom-color", "#FFA8A8");
                $ul.bind('DOMNodeInserted', function (e) {
                    if (this.style.borderBottomColor == 'rgb(255, 168, 168)') {
                        $ul.css("border-bottom-color", "#DDDDDD");
                    }
                });
            }
        }
    });
}
//附件上传
function upload(obj, UpLoadPath) {
    if (obj.value == '') {
        return;
    }
    MaskUtil.mask('正在上传，请稍候。。。');
    var m = 0, n = obj.files.length;
    for (var i = 0; i < obj.files.length; i++) {
        var formData = new FormData;
        formData.append("File", obj.files[i]);
        $.ajax({
            url: '../../../Ashx/System/sys_Upload.ashx?action=UpLoadFile&Btn=show&LoginSalt=' + loginSalt + '&MenuId=' + menuId + '&UpLoadPath=' + UpLoadPath,
            type: "post",
            dataType: "json",
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.error === 0) {
                    htmlStr = '<li><span class="icon"></span><input type="hidden" value="' + data.fileId + '" /><a href="' + data.url + '" target="_blank" >' + data.fileName + '</a><a href="javascript:;" class="attachbtn" onclick="delAttachment(this)">删除</a></li>';
                    $($(obj).siblings('ul')[0]).append(htmlStr);
                    if (m == n - 1) {
                        MaskUtil.unmask();
                    }
                    m++;
                } else {
                    MaskUtil.unmask();
                    $.messager.alert(getSystemName(), data.message, 'info');
                    return
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                MaskUtil.unmask();
                $.messager.alert(getSystemName(), XMLHttpRequest.responseText, 'info');
                return
            }
        });
    }
    obj.value = '';
}
//删除附件
function delAttachment(obj) {
    var $ul = $(obj).parent().parent();
    $(obj).parent().remove();
    if ($ul.attr("data-required") == "true" && $ul.children().length == 0) {
        $ul.css('border-bottom-color', '#FFA8A8');
    }
}
///////////////////////////////////////等待层////////////////////////////
//等待层
/*
* 使用方法: 
 * 开启:MaskUtil.mask(); 
* 关闭:MaskUtil.unmask(); 
* MaskUtil.mask('其它提示文字...'); 
*/
var MaskUtil = (function () {
    var $mask, $maskMsg;
    var defMsg = '正在处理，请稍候。。。';
    function init() {
        if (!$mask) {
            $mask = $("<div id=\"mask\" class=\"mask\"></div>").appendTo("body");
            $mask.css({ width: "100%", height: $(document).height() });
        }
        if (!$maskMsg) {
            $maskMsg = $("<div id=\"maskmsg\" class=\"maskmsg\">" + defMsg + "</div>").appendTo("body");
            var scrollTop = $(document.body).scrollTop();
            $maskMsg.css({
                left: ($(document.body).outerWidth(true) - 190) / 2
                , top: (($(window).height() - 45) / 2) + scrollTop
            });
        }
    }
    return {
        mask: function (msg) {
            init();
            $mask.css("visibility", 'visible');
            $maskMsg.html(msg || defMsg).css("visibility", 'visible');
        }
        , unmask: function () {
            $mask.css("visibility", 'hidden');
            $maskMsg.css("visibility", 'hidden');
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


function loadProcessPage(divName, billSign,billState, id) {
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
function GridToTable(gridName, textFeild) {

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
    var rows = [];
    if (exportGrid.data('datagrid').allRows == undefined) {
        rows = exportGrid.datagrid("getRows");
    }
    else {
        rows = exportGrid.data('datagrid').allRows;//取Grid本地数据所有行
    }

    for (var i = 0; i < rows.length; ++i) {
        tableString += '<tr>';
        for (var j = 0; j < nameList.length; ++j) {

            var e = nameList[j].field.lastIndexOf('_0');

            tableString += '<td style=\"';
            if (nameList[j].align != 'undefined' && nameList[j].align != '') {
                tableString += 'text-align:' + nameList[j].align + ';';
            }
            if (textFeild != undefined && textFeild.length > 0 && textFeild.indexOf(nameList[j].field) != -1) {
                tableString += "mso-number-format:'\@';";
            }
            tableString += '\">';
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
//===================================================================页面公共方法 开始=======================================================================================================
//===================================================================页面JSON 开始=======================================================================================================

//根据jsonID返回text
function getJsonTextById(json, id) {
    for (var i = 0; i < json.length; i++) {
        if (json[i].id == id) {
            return json[i].text;
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
///////////////////////////////////////////////基础设置
function getStorageStandardJson() {//仓储
    var tempJson = [{ "id": "1", "text": "小型车" }, { "id": "2", "text": "大型车" }];
    return tempJson;
}

function getCostStandardJson() {//收费
    var tempJson = [{ "id": "1", "text": "普通" }, { "id": "2", "text": "大包" }];
    return tempJson;
}

function getClearanceTypeJson() {//清关
    var tempJson = [{ "id": "1", "text": "客户承担" }, { "id": "2", "text": "我方固定金额" }, { "id": "3", "text": "我方承担全部" }];
    return tempJson;
}
function getVenTypeJson() {//收费
    var tempJson = [{ "id": "1", "text": "国外" }, { "id": "2", "text": "国内" }];
    return tempJson;
}

function getPartnerTypeJson() {//合作单位类别
    var tempJson = [{ "id": "1", "text": "报关行" }, { "id": "2", "text": "物流公司" }, { "id": "3", "text": "船运公司" }, { "id": "4", "text": "保险公司" }, { "id": "5", "text": "海关" }];
    return tempJson;
}
function getWarehouseTypeJson() {//仓库类别
    var tempJson = [{ "id": "1", "text": "国内仓" }, { "id": "2", "text": "海外仓" }];
    return tempJson;
}

function getApplyTypeJson() {//申请类型
    var tempJson = [{ "id": "1", "text": "新增" }, { "id": "2", "text": "变更" }, { "id": "3", "text": "终止" }];
    return tempJson;
}


///////////////////////////////////////////////销售业务
function getDepositTypeJson() {//保证金类型
    var tempJson = [{ "id": "1", "text": "无保证金" }, { "id": "2", "text": "定额" }, { "id": "3", "text": "定率" }];
    return tempJson;
}
function getContractTypeJson() {//销售合同类型
    var tempJson = [{ "id": "1", "text": "正式合同" }, { "id": "2", "text": "虚拟合同" }];
    return tempJson;
}
///////////////////////////////////////////////采购业务
function getPurchasingTypeJson() {//采购类别
    var tempJson = [{ "PurchasingTypeId": "1", "PurchasingType": "国内采购" }, { "PurchasingTypeId": "2", "PurchasingType": "海外采购" }];
    return tempJson;
}
//===================================================================页面JSON 结束=======================================================================================================
