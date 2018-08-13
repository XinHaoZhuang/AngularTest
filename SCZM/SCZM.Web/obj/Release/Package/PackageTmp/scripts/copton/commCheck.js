function shhInfoPlace() {
    shhInfoPlace_SCZM("panelCheck");
}
function shhInfoPlace_SCZM(cSHHInfo) {
    var obj_shhinfo = document.getElementById(cSHHInfo);
    try {
        obj_shhinfo.style.left = document.body.offsetWidth / 2 - 175;
        obj_shhinfo.style.top = document.body.offsetHeight * 0.95 / 2 - 80;
    } catch (e) {
        //alert(e);
    }
}
function checkedCountForFarPoint() {
    return checkedCountForFarPoint_SCZM("Grid1");
}
function checkedCountForFarPoint_SCZM(cCurFarPointID) {
    var i = 0;
    var iCheckedCount = 0;
    var obj_checkbox_tmp;
    for (i = 0; i < 5000; i++) {
        try {
            obj_checkbox_tmp = document.all(cCurFarPointID + "_" + i + ",0");
            if (obj_checkbox_tmp.checked) {
                iCheckedCount++;
            }
        } catch (e) {
            break;
        }
    }
    return iCheckedCount;
}

function selAllForFarPoint() {
    selAllForFarPoint_SCZM("Grid1");
    return false;
}
function CancelSelAllForFarPoint() {
    CancelSelAllForFarPoint_SCZM("Grid1");
    return false;
}

function selAllForFarPoint_SCZM(cCurFarPointID) {
    var spread = document.getElementById(cCurFarPointID);
    //spread.CallBack("SELALL");
    for (var i = 0; i < Grid1_viewport.rows.length; i++) {
        spread.SetValue(i, 0, true, true);
    }

    /*var i = 0;
    var obj_checkbox_tmp;
    var iFlag_success = 0;
    for (i = 0; i < 5000; i++) {
        try {
            obj_checkbox_tmp = document.all(cCurFarPointID + "_" + i + ",0");
            obj_checkbox_tmp.checked = true;
            iFlag_success++;
        } catch (e) {
            if (iFlag_success > 0) {
                break;
            }
        }
    }*/
}
function CancelSelAllForFarPoint_SCZM(cCurFarPointID) {
    var spread = document.getElementById(cCurFarPointID);
    //spread.CallBack("UNSELALL");

    for (var i = 0; i < Grid1_viewport.rows.length; i++) {
        spread.SetValue(i, 0, false, true);
    }
    /*var i = 0;
    var obj_checkbox_tmp;
    var iFlag_success = 0;
    for (i = 0; i < 5000; i++) {
        try {
            obj_checkbox_tmp = document.all(cCurFarPointID + "_" + i + ",0");
            obj_checkbox_tmp.checked = false;
            iFlag_success++;
            alert(i);
        } catch (e) {
            if (iFlag_success > 0) {
                break;
            }
        }
    }*/
}



function GetCurSelPageForFarPoint() {
    var i = 0;
    try {
    var obj_test = document.all("Grid1_pager2");
    var cInitInfo_pageList = obj_test.innerHTML;
        for (i = 0; i < 2000; i++) {
            cInitInfo_pageList = cInitInfo_pageList.replace("&nbsp;", "");
        }
        for (i = 0; i < 2000; i++) {
            cInitInfo_pageList = cInitInfo_pageList.replace(" onclick=\"javascript:Grid1.CallBack('", "");
        }
        for (i = 0; i < 2000; i++) {
            cInitInfo_pageList = cInitInfo_pageList.replace(");return false;\" href=\"javascript:void(0)\"", "");
        }
        for (i = 0; i < 2000; i++) {
            cInitInfo_pageList = cInitInfo_pageList.replace("'", "");
        }
        for (i = 0; i < 2000; i++) {
            cInitInfo_pageList = cInitInfo_pageList.replace("<APage," + i + ">" + (i + 1) + "</A>", "");
        }
        for (i = 0; i < 2000; i++) {
            cInitInfo_pageList = cInitInfo_pageList.replace("<APage,-2>...</A>", "");
        }
        for (i = 0; i < 2000; i++) {
            cInitInfo_pageList = cInitInfo_pageList.replace("<APage,-1>...</A>", "");
        }
        //alert(cInitInfo_pageList);
    } catch (e) {
        //alert(e);
        cInitInfo_pageList = "1";
    }
    return cInitInfo_pageList;
}
function ShowHid() {
	var obj = document.all("table_querydetail");
	if (obj.style.display=="") {
		obj.style.display="none";
		document.all("img_showHid").src = "../images/show.gif";
	}else {
		document.all("img_showHid").src = "../images/hid.gif";
		obj.style.display="";
	}
}




var dgdListBgcolor = "";

function ChangInitDateToNull(obj,startRowNum) {
	var i = 0;
	var j = 0;
	for (i=startRowNum; i<obj.rows.length;i++){
		for (j=0; j<obj.rows[i].cells.length;j++){
			if (obj.rows[i].cells[j].innerText=="1900-01-01") {
				obj.rows[i].cells[j].innerText = "";
			}
		}
	}
}
function exitMsg() {
	location.href = "Welcome.aspx";
}
function ShowTbStyle(tbName) {
	var obj = document.all(tbName);
	for (var i = 1; i < obj.rows.length; i++) {
		if (i%2==0) {
			obj.rows[i].bgColor="#f4f4f4";
		}
	}
}

function PageLayout(tbName,iStartRow,cLayOutPros) {
	if (tbName == "") {
		tbName = "dgdList";
	}
	var obj = document.all(tbName);
	var i = 0;
	var j = 0;
	var arrLine = cLayOutPros.split(";");
	var arrCell;
	for (i = iStartRow;i<obj.rows.length;i++) {
		for (j = 0; j < arrLine.length;j++) {
			arrCell = arrLine[j].split(",");
			try{
				obj.rows[i].cells[arrCell[0]].style.textAlign=arrCell[1];
			}catch(e){
			}
		}
	}
}
		
function mm() {
		if (event.srcElement.tagName == "INPUT") {
			//alert(event.srcElement.type);
			if (event.srcElement.type == "button" || event.srcElement.type == "submit") {
				//alert(event.srcElement.className);
				if (event.srcElement.className == "Buttoncss") {
					event.srcElement.className = "Buttoncss_light"
				}else if (event.srcElement.className == "Buttoncss_long") {
					event.srcElement.className = "Buttoncss_light_long"
				}
			}
		}
	}
	
	function mo() {
		if (event.srcElement.tagName == "INPUT") {
			//alert(event.srcElement.type);
			if (event.srcElement.type == "button" || event.srcElement.type == "submit") {
				//alert(event.srcElement.className);
				if (event.srcElement.className == "Buttoncss_light") {
					event.srcElement.className = "Buttoncss"
				}else if (event.srcElement.className == "Buttoncss_light_long") {
					event.srcElement.className = "Buttoncss_long"
				}
			}
		}
	}
function commafy(num){ 
	var re=/(-?\d+)(\d{3})/ 
	
	while(re.test(num)){ 
		num=num.replace(re,"$1,$2") 
	} 
	return num;
}
function exitCurWindow() {
		window.parent.parent.opener.focus();
		window.parent.parent.opener = null;
		window.parent.parent.close();
	}
function exitToHomePage() {
	location.href = "../Welcome.aspx";
}
function exitWindow() {
	try{
	window.opener.focus();
	window.opener = null;
	window.close();
	}catch(e){
		exitToHomePage();
	}
}
function OpenInAnoterWin(str,cName) {
	window.open(str,cName,"width="+(screen.width-10)+",height="+(screen.height-50)+",top=0,left=0,scrollbars=1");
}
function SelCurrentValue(obj_Sel,cValue) {
	try {
		for (var i = 0; i < obj_Sel.options.length; i++) {
			if (obj_Sel.options[i].text == cValue) {
				obj_Sel.options.selectedIndex = i;
				break;
			}
		}
	} catch(e) {
	}
}


function compareDate(DateOne,DateTwo)
{

var OneMonth = DateOne.substring(5,DateOne.lastIndexOf ("-"));
var OneDay = DateOne.substring(DateOne.length,DateOne.lastIndexOf ("-")+1);
var OneYear = DateOne.substring(0,DateOne.indexOf ("-"));

var TwoMonth = DateTwo.substring(5,DateTwo.lastIndexOf ("-"));
var TwoDay = DateTwo.substring(DateTwo.length,DateTwo.lastIndexOf ("-")+1);
var TwoYear = DateTwo.substring(0,DateTwo.indexOf ("-"));

if (Date.parse(OneMonth+"/"+OneDay+"/"+OneYear) > Date.parse(TwoMonth+"/"+TwoDay+"/"+TwoYear))
{
return true;
}
else
{
return false;
}
}


function trim ( str ) {
    if( (str.charAt(0) != ' ') && (str.charAt(str.length-1) != ' ') ) { return str; }
    while( str.charAt(0) == ' ' ) {
        str = '' + str.substring(1,str.length);
    }
    while( str.charAt(str.length-1) == ' ' ) {
        str = '' + str.substring(0,str.length-1);
    }

    return str;
} 

function chkNull (str) {
  if (trim(str) == '') {
    return false;
  } else {
    return true;
  }
}



function chknum(num){
  numstring="0123456789"
  for(iii=0;iii<num.length;iii++){
    if (numstring.indexOf(num.charAt(iii))==-1)
      return false;
  }
  return true;
}


function chkdigital(num){
	if (num.charAt(0) == "-") {
		num = num.substring(1,num.length)
	}
  numstring="0123456789."
  for(iii=0;iii<num.length;iii++){
    if (numstring.indexOf(num.charAt(iii))==-1)
      return false;
  }
  return true;
}
function GetSSHWR(num) {
	return parseFloat(num).toFixed(2)*100/100;
}
function IsDate(str) 
{ 
   var re = /^\d{4}-\d{1,2}-\d{1,2}$/; 
   if(re.test(str)) 
   {  
       var array = str.split('-'); 
       var date = new Date(array[0], parseInt(array[1], 10) - 1, array[2]); 
       if(!((date.getFullYear() == parseInt(array[0], 10)) && ((date.getMonth() + 1) == parseInt(array[1], 10)) && (date.getDate() == parseInt(array[2], 10)))) 
       {  
           return false; 
       } 
       return true; 
   } 

   // 日期格式错误 
   return false; 
} 

function getchecknum(frm) {
	var result = 0;
	var checkboxnum = 0;
	for (var i = 0; i < frm.length; i++) {
		if (frm.elements[i].type == "checkbox") {
			checkboxnum++;
			if (checkboxnum > 1) {
			    if (frm.elements[i].checked) {
				    result++;
			    }
			}
		}
	}
	return result;
}

function checkallorno(o) {
	var a = o;

	while (true) {
		var a = a.parentElement;
		if (a == null) {
			break;
		}
		if ( a == "undefined") {
			a = null;
			break;
		}
		if (a.tagName == "TABLE") {
			break;	
		}
	}
	if (a != null) {
		for (i = 0;i < a.rows.length; i++) {
			for (j = 0;j < a.rows[i].cells[0].children.length; j++) {
				var var1 = a.rows[i].cells[0].children[j];
				if (var1.tagName == "INPUT" ) {
					if (var1.type == "checkbox") {
						var1.checked = o.checked;
					}
				}
			}
		}
	}
}

function ShowColor() {
	try {
		var obj = document.all("dgdList");
		for (var i = 1; i < obj.rows.length;i++){
			if (i%2 > 0) {
				obj.rows[i].className = "gridAltItem1";
			}else {
				obj.rows[i].className = "gridAltItem2";
			}
		}
	}catch(e) {
		alert(e);
	}
}
//去除空格并转化成0
function EmptyToZero(str) {
    try {
        //删除左右两端的空格     
        var str_temp = str.replace(/(^\s*)|(\s*$)/g, "");
        if (str_temp == "") {
            str_temp="0";
        }
        if (isNaN(parseFloat(str_temp))) {
            str_temp = "0";
        }
        else {
            str_temp = parseFloat(str_temp).toString();
        }
        return str_temp;
        
    } catch (e) {
        alert(e);
    }
}