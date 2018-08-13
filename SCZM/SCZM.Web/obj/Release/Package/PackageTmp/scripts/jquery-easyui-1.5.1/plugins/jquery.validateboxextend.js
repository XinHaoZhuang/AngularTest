
(function ($) {
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
	$.extend($.fn.validatebox.defaults.rules, {
	    //自定义验证规则 名称为myDate  
		date: {
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
		},
	    //验证手机号
		phoneNum: { //验证手机号   
		    validator: function (value, param) {
		        return /^1[3-8]+\d{9}$/.test(value);
		    },
		    message: '请输入正确的手机号码。'
		},
	    //验证电话号码
		telNum: { //既验证手机号，又验证座机号
		    validator: function (value, param) {
		        return /(^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$)|(^((\d3)|(\d{3}\-))?(1[358]\d{9})$)/.test(value);
		    },
		    message: '请输入正确的电话号码，例如：0532-88939999、13900000001。'
		},
	    /*必须和某个字段相等*/
	    equalTo: { validator: function (value, param) { return $(param[0]).val() == value; }, message: '两次输入密码不一致！' }
	});
})(jQuery);