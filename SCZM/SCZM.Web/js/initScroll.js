$(document).bind("pageshow", function () {
    function fixed(elm) {


        if (elm.data("iscroll-plugin")) {
            return;
        }

        elm.css({
            overflow: 'hidden'
        });

        var barHeight = 0; // 页头页尾高度
        var loader,
        pullUpEl, pullUpOffset;

        var isRefreshing = false; // 一次滑动事务
        // 设置页头样式

        var $header = elm.find('[data-role="header"]');
        if ($header.length) {
            $header.css({
                "z-index": 1000,
                padding: 0,
                width: "100%"
            });
            barHeight += $header.height();
        }

        // 设置页尾样式
        var $footer = elm.find('[data-role="footer"]');
        if ($footer.length) {
            $footer.css({
                "z-index": 1500,
                padding: 0,
                width: "100%"
            });
            barHeight += $footer.height();
        }

        // 设置内容区域样式、高度
        var $wrapper = elm.find('[data-role="content"]');
        if ($wrapper.length) {
            $wrapper.css({
                "z-index": 1
            });
            $wrapper.height($(window).height() - barHeight);
            $wrapper.bind('touchmove', function (e) { e.preventDefault(); });
        }

        // 设置滚动区域
        var scroller = elm.find('[data-iscroll="scroller"]').get(0);
        if (!scroller) {
            $($wrapper.get(0)).children().wrapAll("<div data-iscroll='scroller'></div>");
        }


        var isInit = 0;
        var myScroll,
        pullDownEl, pullDownOffset,
        pullUpEl, pullUpOffset, generatedCount = 0;


        /**
        * 初始化iScroll控件
        */

        pullDownEl = document.getElementById('pullDown');
        pullDownOffset = pullDownEl.offsetHeight;
        pullUpEl = document.getElementById('pullUp');
        pullUpOffset = pullUpEl.offsetHeight;

        myScroll = new iScroll(
            $wrapper.get(0),
            {
                //scrollbarClass: 'myScrollbar', /* 自定义样式 */
                useTransition: true, //是否使用CSS变换
                topOffset: pullDownOffset + 25,
                hScroll: true,
                vScroll: true,
                hScrollbar: false,
                vScrollbar: true,
                fixedScrollbar: true,
                fadeScrollbar: true,
                hideScrollbar: true,
                bounce: true,
                momentum: true,
                lockDirection: true,
                checkDOMChanges: true,
                onRefresh: function () {
                    if (pullDownEl.className.match('loading')) {
                        pullDownEl.className = '';
                        pullDownEl.querySelector('.pullDownLabel').innerHTML = '下拉刷新...';
                    } else if (pullUpEl.className.match('loading')) {
                        pullUpEl.className = '';
                        pullUpEl.querySelector('.pullUpLabel').innerHTML = '上拉加载更多...';
                    }
                },
                onScrollMove: function () {
                    if (this.y > 15 && !pullDownEl.className.match('flip')) {
                        pullDownEl.className = 'flip';
                        pullDownEl.querySelector('.pullDownLabel').innerHTML = '松手开始更新...';
                        this.minScrollY = 0;
                    } else if (this.y < 15 && pullDownEl.className.match('flip')) {
                        pullDownEl.className = '';
                        pullDownEl.querySelector('.pullDownLabel').innerHTML = '下拉刷新...';
                        this.minScrollY = -pullDownOffset;
                    } else if (this.y < (this.maxScrollY - 15)
                        && !pullUpEl.className.match('flip')) {
                        pullUpEl.className = 'flip';
                        pullUpEl.querySelector('.pullUpLabel').innerHTML = '松手开始更新...';
                        this.maxScrollY = this.maxScrollY;
                    } else if (this.y > (this.maxScrollY + 15)
                        && pullUpEl.className.match('flip')) {
                        pullUpEl.className = '';
                        pullUpEl.querySelector('.pullUpLabel').innerHTML = '上拉加载更多...';
                        this.maxScrollY = pullUpOffset;
                    }
                },
                onScrollEnd: function () {
                    if (pullDownEl.className.match('flip')) {
                        pullDownEl.className = 'loading';
                        pullDownEl.querySelector('.pullDownLabel').innerHTML = '加载中...';
                        pullDownAction();	// Execute custom function (ajax call?)
                    } else if (pullUpEl.className.match('flip')) {
                        pullUpEl.className = 'loading';
                        pullUpEl.querySelector('.pullUpLabel').innerHTML = '加载中...';
                        pullUpAction(); // Execute custom function (ajax call?)
                    }
                },

            });
        //页面初始化
        isInit = 1;
        /**
        * 下拉刷新 
        * myScroll.refresh();		// 数据加载完成后，调用界面更新方法
        */
        function pullDownAction() {
            setTimeout(update, 1000);	// <-- Simulate network congestion, remove setTimeout from production!
        }

        /**
        * 上拉刷新
        * myScroll.refresh();		// 数据加载完成后，调用界面更新方法
        */
        function pullUpAction() {
            //alert(0);
            setTimeout(getIntention, 1000); // <-- Simulate network congestion, remove setTimeout from production!
        }
        var startNum = -1;
        var count = -1;

        function loadData() {
            
            $.ajax({
                async: false,
                url: serverURL, // 跨域URL
                type: 'get',
                data: startNum,
                timeout: 6000,
                success: function (datas) { //客户端jquery预先定义好的callback函数，成功获取跨域服务器上的json数据后，会动态执行这个callback函数 
                    desplay(datas);
                },
                complete: function (XMLHttpRequest, textStatus) {
                    //alert(textStatus);
                },
                error: function (xhr) {
                    //jsonp 方式此方法不被触发
                    //请求出错处理 
                    myAlert("请求出错(请检查相关度网络状况.)");
                    myScroll.refresh();
                }
            });
        }
        //---------------------------------------
        var data = {};
        function getIntention(Data) {
            if (Data == undefined) {
                Data = data;
            }
            //alert(data);
            //console.log("当前页：" + startNum + " || 总页数：" + count)
            //if (startNum != -1 && Number(startNum) >= Number(count)) {
            //    myAlert('已加载完全部信息');
            //    myScroll.refresh();
            //    return false;
            //}
            startNum = startNum + 1;
            //showLoading();
            //console.log("服务器地址：" + serverURL);
            data.startNum = startNum;
            if ($('#loadingToast').css('display') == 'none') {
                $('#loadingToast').fadeIn(100);
            }
            $.ajax({
                type: 'get',
                dataType: 'json',
                data: Data,
                url: "../../Ashx/WX/WX_GetLoginInfo.ashx",
                success: function (data, status, xhr) {
                    //alert("success");
                    if (data.state == "1") {
                        SetData(data);
                        $('#loadingToast').fadeOut(100);
                    }
                    else {
                        $('#loadingToast').fadeOut(100);
                    }
                },
                error: function (xhr, status, errorThrown) {
                    //alert("error");
                    $('#loadingToast').fadeOut(100);
                    warn(status);
                }
            });
            //alert(11);
        }
        function SetData(data) {
            //alert(-1);
            var ListCell = "";
            for (var i = 0; i < data.IntentionList.length; i++) {
                var state = "", CustTypeId = data.IntentionList[i].CustTypeId;
                if (CustTypeId == "1") {
                    state = "外部客户";
                    if (data.IntentionList[i].ActualEnterDate == "") {
                        state += ".未入厂";
                    } else {
                        state += ".已入厂";
                    }
                } else if (CustTypeId == "2") {
                    state = "内部部门";
                }
                ListCell += '<div class="weui-cells list-ul"><div class="weui-cell weui-cell_access list-li"><div class="weui-cell__bd con" onclick="window.location.href=\'WX_Intention_change.html?IntentionId=' + data.IntentionList[i].ID + '\'"><p class="IntentionList"><span class="Customer content_medium">客户：' + data.IntentionList[i].CustName + '</span><br/><span class="content_50 content_smaller">机型：' + data.IntentionList[i].MachineModel + '</span><span class="content_50 content_smaller">机号：' + data.IntentionList[i].MachineCode + '</span><br/><span class="content_smaller content_50">意向担当：' + data.IntentionList[i].Linkman + '</span><span class="content_smaller content_50">提交日期：' + new Date(data.IntentionList[i].OperaTime).format('yyyy-MM-dd') + '</span></p></div><div class="btn" onclick="delIntention(' + data.IntentionList[i].ID + ')"><div>删除</div></div></div></div>';
            }
            //$('.weui-tab__panel').empty().append(ListCell);
            $('.weui-tab__panel').append(ListCell);
        }
        //---------------------------------------

        function desplay(datas) {
            // datas = '<li> <a href="fpmx.html"> <img src="images/ic_wast.png" alt=""> <h3>￥0.00</h3> <p>发票代码：<span class="text-no">11155653322</span></p> <p>发票号码：<span class="text-no">00001234</span></p> </a> <p class="desc-normal desc-wast"><span class="left-aside">开票日期：2013/5/17</span><span class="right-aside">开票员：01</span></p> </li>'
            if (datas != null && datas != "") {
                console.log("加载后的当前页：" + startNum + "|| 获取数据" + datas);

                $("#fpmxList").append(datas);
                $("#fpmxList").listview("refresh");

                setTimeout(function () { // <-- Simulate network congestion, remove setTimeout from production!			
                    myScroll.refresh(); // 数据加载完成后，调用界面更新方法 Remember to refresh when contents are loaded (ie: on ajax completion)
                    hideLoading();
                    if (Number(startNum) >= Number(count)) {
                        myAlert('已加载完全部信息');
                    }
                }, 1500);
            }
        }

        function update() {
            myScroll.refresh(); // 数据加载完成后，调用界面更新方法 Remember to refresh when contents are loaded (ie: on ajax completion)
            window.location.reload();
        }

        function showMyAlert(text) {
            $.mobile.loadingMessageTextVisible = true;
            $.mobile.showPageLoadingMsg("a", text, true);

        }
        function myAlert(text) {
            showMyAlert(text);
            setTimeout(hideLoading, 2000);
        }
        function showLoading() {
            $.mobile.loadingMessageTextVisible = true;
            $.mobile.showPageLoadingMsg("a", "加载中...");
        }

        function hideLoading() {
            $.mobile.hidePageLoadingMsg();
        }


        elm.data("iscroll-plugin", myScroll);

        window.setTimeout(function () { myScroll.refresh(); }, 200);

        try {
            data = { "action": "GetIntentionList", "SearchType": "4" }
            getIntention(data);
            getStyle();
        } catch (e) {
            alert(e.message)
        }
        function getStyle() {

            $('.search').off('click');
            $('.search').on('click', function () {
                $('#iosDialog3').fadeIn(200);
            });

            $('#iosDialog3').off('click', '.weui-dialog__btn_primary');
            $('#iosDialog3').on('click', '.weui-dialog__btn_primary', function () {
                //$(this).parents('#iosDialog3').fadeOut(200);
                var MachineModelId = $('#txtMachineModelId').val();
                var MachineCode = $('#txtMachineCode').val();
                var CustName = $('#txtCustName').val();
                var SearchType = $('#txtSearchType').val();
                data = { "action": "GetIntentionList", "MachineModelId": MachineModelId, "MachineCode": MachineCode, "CustName": CustName, "SearchType": SearchType }
                startNum = -1;
                $('.weui-tab__panel').empty();
                getIntention(data);
                $('#iosDialog3').fadeOut(200);
            });
            $('#iosDialog3').off('click', '.weui-dialog__btn_default');
            $('#iosDialog3').on('click', '.weui-dialog__btn_default', function () {
                //$(this).parents('#iosDialog3').fadeOut(200);
                $('#iosDialog3').fadeOut(200);
            });
            $('#delIntention').off('click', '.weui-dialog__btn_primary');
            $('#delIntention').on('click', '.weui-dialog__btn_primary', function () {
                $(this).parents('#delIntention').fadeOut(200);
                $.ajax({
                    type: 'post',
                    data: { "action": "DelIntention_WX", "IntentionId": IntentionId },
                    dataType: 'json',
                    url: '../../Ashx/WX/WX_GetLoginInfo.ashx',
                    success: function (data, status, xhr) {
                        if (data.state == "1") {
                            $('#SuccessToast').fadeIn(100);
                            setTimeout(function () { $('#SuccessToast').fadeOut(100); }, 2000);
                            data = { "action": "GetIntentionList" }
                            startNum = -1;
                            $('.weui-tab__panel').empty();
                            getIntention(data);
                        } else {
                            $('#ErrorToast').fadeIn(100);
                            setTimeout(function () { $('#ErrorToast').fadeOut(100); }, 2000);
                        }
                    },
                    error: function (xhr, status, errorThrown) {
                        $('#ErrorToast').fadeIn(100);
                        setTimeout(function () { $('#ErrorToast').fadeOut(100); }, 2000);
                    }
                });
            });
            $('#delIntention').off('click', '.weui-dialog__btn_default');
            $('#delIntention').on('click', '.weui-dialog__btn_default', function () {
                $(this).parents('#delIntention').fadeOut(200);
            });
            $.post("../../Ashx/WX/WX_GetLoginInfo.ashx", { "action": "GetMachineModelCombo_WX" }, function (data, status, xhr) {
                var OptionHtml = ""
                for (var i = 0; i < data.length; i++) {
                    OptionHtml += "<option value='" + data[i].MachineModelId + "'>" + data[i].MachineModelName + "</option>";
                }
                OptionHtml += "<option value='' selected>全部</option>"
                $("#txtMachineModelId").empty().append(OptionHtml);
            }, "json");
            $('.weui-mask').on('click', function () {
                $(this).parent().fadeOut(200);
            });


        }
        function delIntention(ID) {
            IntentionId = ID;
            $('#delIntention').fadeIn(100);
        }
        var IntentionId = 0;
        var startX = 0, endX = 0, len = 0, touch = undefined, startY = 0, endY = 0;
        $('.weui-tab__panel').on('touchstart', ".list-li", function (event) {
            console.log(event);
            touch = event.originalEvent.targetTouches[0];
            startX = touch.pageX; //console.log(startX);
            startY = touch.pageY;
        });
        $('.weui-tab__panel').on('touchmove', ".list-li", function (event) {

            touch = event.originalEvent.targetTouches[0];
            endX = touch.pageX; //console.log(endX - startX);
            endY = touch.pageY;
            if (Math.abs(endY - startY) > 5) {
                return;
            }
            len = endX - startX;

            if (len < 0) {
                if (len < -80) {
                    $(this).css("transform", "translateX(-80px)");
                } else {
                    $(this).css("transform", "translateX(" + len + "px)");
                }
            }
        });
        $('.weui-tab__panel').on('touchend', ".list-li", function (event) {
            console.log(startX + ',' + endX + ',' + len);
            if (-len < 40) {
                $(this).css({ "transform": "translateX(0px)", "transition": "transform 1s" });
            } else {
                $(this).css({ "transform": "translateX(-80px)", "transition": "transform 1s" });
            }

        });
    }

    $('[data-role="page"][data-iscroll="enable"]').bind("pageshow", function () {
        fixed($(this));
    });
    if ($.mobile.activePage.data("iscroll") == "enable") {
        fixed($.mobile.activePage);
    }
});