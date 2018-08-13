/*====================================
 *datagrid扩展方法clientPaging
 *根据页码、排序显示数据
 *loadFilter：pagerFilter
====================================*/
(function () {
	$.extend($.fn.datagrid.defaults.editors, {
		combogrid: {
			init: function (container, options) {
				var input = $('<input type="text" class="datagrid-editable-input">').appendTo(container);
				input.combogrid(options);
				return input;
			},
			destroy: function (target) {
				$(target).combogrid('destroy');
			},
			getValue: function (target) {
				//return $(target).combogrid('getValue');
				return $(target).combogrid('getText');
			},
			setValue: function (target, value) {
				//$(target).combogrid('setValue', value);
				$(target).combogrid('setText', value);
			},
			resize: function (target, width) {
				$(target).combogrid('resize', width);
			}
		}
	});
})();