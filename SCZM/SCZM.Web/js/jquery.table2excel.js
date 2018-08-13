/*
 *  jQuery table2excel -王晓平2017-07-10修改 去掉从table内取数，直接传入table数据
 */
//table2excel.js
function table2excel(options) {
    
    if (options.tabledata == "") {
        return;
    }
    var defaults = {
        tabledata:"",
        name: "Table2Excel",
        filename: "table2excel",
        fileext: ".xls",
        exclude_img: true,
        exclude_links: true,
        exclude_inputs: true
    };
    var settings = $.extend({}, defaults, options);
    
    var utf8Heading = "<meta http-equiv=\"content-type\" content=\"application/vnd.ms-excel; charset=UTF-8\">";
    var template = {
        head: "<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\"><head>" + utf8Heading + "<!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets>",
        sheet: {
            head: "<x:ExcelWorksheet><x:Name>",
            tail: "</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet>"
        },
        mid: "</x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body>",
        table: {
            head: "<table>",
            tail: "</table>"
        },
        foot: "</body></html>"
    };
    
    var tableRows = [];
    var tempRows = options.tabledata;
    if (settings.exclude_img) {
        tempRows = exclude_img(tempRows);
    }
    // exclude link tags
    if (settings.exclude_links) {
        tempRows = exclude_links(tempRows);
    }
    // exclude input tags
    if (settings.exclude_inputs) {
        tempRows = exclude_inputs(tempRows);
    }
    tableRows.push(tempRows);
       
    //alert(1);
    tableToExcel(tableRows, settings.name, settings.sheetName);
        

    function tableToExcel(table, name, sheetName) {
        var fullTemplate="", i, link, a;

        format = function (s, c) {
            return s.replace(/{(\w+)}/g, function (m, p) {
                return c[p];
            });
        };

        sheetName = typeof sheetName === "undefined" ? "Sheet" : sheetName;

        var ctx = {
            worksheet: name || "Worksheet",
            table: table,
            sheetName: sheetName
        };
        
        fullTemplate= template.head;

        if ($.isArray(table)) {

            for (i in table) {
                    fullTemplate += template.sheet.head + sheetName + (parseInt(i)+1).toString() + template.sheet.tail;
            }
        }

        fullTemplate += template.mid;

        if ( $.isArray(table) ) {
            for (i in table) {
                fullTemplate += template.table.head + "{table" + i + "}" + template.table.tail;
            }
        }

        fullTemplate += template.foot;

        for (i in table) {
            ctx["table" + i] = table[i];
        }
        delete ctx.table;

        var isIE = /*@cc_on!@*/false || !!document.documentMode; // this works with IE10 and IE11 both :)            
        //if (typeof msie !== "undefined" && msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // this works ONLY with IE 11!!!
        if (isIE) {
            if (typeof Blob !== "undefined") {
                //use blobs if we can
                fullTemplate = format(fullTemplate, ctx); // with this, works with IE
                fullTemplate = [fullTemplate];
                //convert to array
                var blob1 = new Blob(fullTemplate, { type: "text/html" });
                window.navigator.msSaveBlob(blob1, getFileName(settings) );
            } else {
                //otherwise use the iframe and save
                //requires a blank iframe on page called txtArea1
                txtArea1.document.open("text/html", "replace");
                txtArea1.document.write(format(fullTemplate, ctx));
                txtArea1.document.close();
                txtArea1.focus();
                sa = txtArea1.document.execCommand("SaveAs", true, getFileName(settings) );
            }

        } else {
            var blob = new Blob([format(fullTemplate, ctx)], {type: "application/vnd.ms-excel"});
            window.URL = window.URL || window.webkitURL;
            link = window.URL.createObjectURL(blob);
            a = document.createElement("a");
            a.download = getFileName(settings);
            a.href = link;

            document.body.appendChild(a);

            a.click();

            document.body.removeChild(a);
        }

        return true;
    }

    function getFileName(settings) {
        return ( settings.filename ? settings.filename+settings.fileext : "table2excel.xls" );
    }

    // Removes all img tags
    function exclude_img(string) {
        var _patt = /(\s+alt\s*=\s*"([^"]*)"|\s+alt\s*=\s*'([^']*)')/i;
        return string.replace(/<img[^>]*>/gi, function myFunction(x){
            var res = _patt.exec(x);
            if (res !== null && res.length >=2) {
                return res[2];
            } else {
                return "";
            }
        });
    }

    // Removes all link tags
    function exclude_links(string) {
        return string.replace(/<a[^>]*>|<\/a>/gi, "");
    }

    // Removes input params
    function exclude_inputs(string) {
        var _patt = /(\s+value\s*=\s*"([^"]*)"|\s+value\s*=\s*'([^']*)')/i;
        return string.replace(/<input[^>]*>|<\/input>/gi, function myFunction(x){
            var res = _patt.exec(x);
            if (res !== null && res.length >=2) {
                return res[2];
            } else {
                return "";
            }
        });
    }

    

};
