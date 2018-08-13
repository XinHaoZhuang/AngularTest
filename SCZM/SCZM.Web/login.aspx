<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SCZM.Web.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <%--<link rel="shortcut icon" href="Images/favicon.ico"/>
    <link rel="bookmark" href="Images/favicon.ico"/>--%>
    <link href="css/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="js/common.js"></script>
    <title>四川住贸维修管理系统</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-screen">
            <div class="login-form">
                <div class="control-group" >
                    <asp:TextBox ID="txtAccount" runat="server" CssClass="login-field" placeholder="请输入账号" title="用户名" ></asp:TextBox>
                    <label class="login-field-icon user" for="txtAccount"></label>
                </div>
                <div class="control-group"> 
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="login-field" TextMode="Password" placeholder="请输入密码" title="密码"></asp:TextBox>
                    <label class="login-field-icon pwd" for="txtPassword"></label>
                </div>
                <div><asp:Button ID="btnSubmit" runat="server" Text="登 录" CssClass="btn-login" OnClientClick="return checkSubmit();" onclick="btnSubmit_Click" /></div>
                <div id="tipsDiv" runat="server" style="display:none;" >
                    <span class="login-tips" runat="server"><i></i><b id="msgtip" runat="server"></b></span>
                </div>
            </div>
            <div id="divMessage" style="position:relative;top:70px;">提示：请使用谷歌浏览器登录系统。

                <a target="_blank" href="http://www.google.cn/chrome/">点击下载</a>
            </div>
        </div>
          
    </form>
    <script type="text/javascript">
        function checkSubmit() {
            if (trim($('#txtAccount').val()) == "") {
                $('#txtAccount').focus();
                $('#msgtip').text("请输入账号！");
                $('#tipsDiv').show();
                return false;
            }
            else if (trim($('#txtPassword').val()) == "") {
                $('#txtPassword').focus();
                $('#msgtip').text("请输入密码！");
                $('#tipsDiv').show();
                return false;
            }
            return true;

        }
    </script>

</body>
</html>
