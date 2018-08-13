/*====================================
 *datebox扩展清空按钮
====================================*/
(function () {
    var buttons = $.extend([], $.fn.datebox.defaults.buttons);
    buttons.splice(1, 0, {
        text: '清空',
        handler: function (target) {
            $(target).combo("setValue", "").combo("setText", ""); // 设置空值  
            $(target).combo("hidePanel"); // 点击清空按钮之后关闭日期选择面板  
        }
    });
    $.fn.datebox.defaults.buttons = buttons;
})();