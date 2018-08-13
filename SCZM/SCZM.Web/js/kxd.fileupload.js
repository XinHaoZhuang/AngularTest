(function($){
	$.fn.fileupload = function (options, param) {
		if (typeof options == 'string') {
			return $.fn.fileupload.methods[options](this, param);
		}

		options = options || {};
		return this.each(function () {
			var state = $.data(this, 'fileupload');
			if (state) {
				$.extend(state.options, options);
			} else {
				$.data(this, 'fileupload', {
					options: $.extend({}, $.fn.fileupload.defaults, options)
				});
			}
			createUpload(this);


		});
	};
	function createUpload(target) {
		var opts = $.data(target, 'fileupload').options;
		var t = $(target).empty();
		t.addClass('upload');
		t.append('<ul></ul><a class="uploadbtn">上传</a><input type="file" multiple="multiple" onchange="selectedAfter1(this)" style="display:none;" />');
		var a = target.children[1];

	}
	function test(){
		alert(1);
	}
	function selectFile1(target) {
		alert(1);
		//$(target).next().click();
	}
	function selectedAfter1(obj) {

		var target = this.parent();
		var opts = $.data(target, 'fileupload').options;
		var ulobj = target.firstChild();
		var htmlStr = "";
		//for (var i = 0; i < obj.files.length; i++) {
		//    if (ulobj.children().children('a[data-name="' + obj.files[i].name + '"]').length > 0) {
		//        $.messager.alert(getSystemName(), '文件' + obj.files[i].name + '已上传，请重新选择！', 'info');
		//        return;
		//    }
		//}
		for (var i = 0; i < obj.files.length; i++) {
			if (ulobj.children('li').children('a[data-name="' + obj.files[i].name + '"]').length > 0) {
				continue;
			}
			var data = new FormData;
			htmlStr = '<li><span class="icon"></span><a href="" target="_blank" data-id="" data-name="' + obj.files[i].name + '">' + obj.files[i].name + '</a><div class="lilast"><div class="progress_out"><div class="progress_in" ></div></div></div></li>';
			$(ulobj).append(htmlStr);
			var curli = ulobj.children(":last");
			data.append("File", obj.files[i]);
			$.ajax({
				url: '../../../Ashx/System/sys_Upload.ashx?action=UpLoadFile&Btn=show&LoginSalt=' + loginSalt + '&MenuId=' + menuId + '&UpLoadPath=proj_Contract',
				type: "post",
				dataType: "json",
				data: data,
				contentType: false,
				processData: false,
				xhr: function () {
					var xhr = $.ajaxSettings.xhr();
					var lilast = curli.children(".lilast");
					if (xhr.upload) {
						xhr.upload.onprogress = function (progress) {
							if (progress.lengthComputable) {
								var per = (progress.loaded / progress.total * 100);
								lilast.children().children().css("width", per + "%");
							}
						};
					}
					return xhr;
				},
				success: function (data) {
					if (data.error == 0) {
						var aobj = ulobj.children().children('a[data-name="' + data.fileName + '"]')
						aobj.attr("href", data.url);
						aobj.attr("data-id", data.fileId);
						aobj.next().empty();
						aobj.next().append('<a href="javascript:;" class="delbtn" onclick="delAttachment1(this)">删除</a>');
					}
					else {
						$.messager.alert(getSystemName(), data.message, 'info');
					}
				},
				error: function (XMLHttpRequest, textStatus, errorThrown) {
					$.messager.alert(getSystemName(), textStatus + XMLHttpRequest.status + "：" + errorThrown, 'info');
				}

			});
		}

	}
	$.fn.fileupload.defaults = {
		disabled: true,
		url: '',
		getValue: function () { }
	};
})(jQuery);
