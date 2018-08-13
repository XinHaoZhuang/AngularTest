using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCZM.Common;
using SCZM.WebUI;
using System.Net.NetworkInformation;
using System.Management;
namespace SCZM.Web
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.txtAccount.Text = Utils.GetCookie("SCZMAccount");
                if (txtAccount.Text == "")
                {
                    txtAccount.Focus();
                }
                else
                {
                    txtPassword.Focus();
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //判断登录错误次数
            if (Session["LoginNum"] != null && Convert.ToInt32(Session["LoginNum"]) > 5)
            {
                msgtip.InnerHtml = "错误超过5次，请关闭浏览器重新登录！";
                tipsDiv.Style["display"] = "";
                return;
            }

            string userAccount = txtAccount.Text.Trim();
            string userPwd = txtPassword.Text.Trim();

            if (userAccount.Equals(""))
            {
                msgtip.InnerHtml = "请输入账号！";
                tipsDiv.Style["display"] = "";
                txtAccount.Focus();
                CalErrNum();
                return;
            }
            if ( userPwd.Equals(""))
            {
                msgtip.InnerHtml = "请输入密码！";
                tipsDiv.Style["display"] = "";
                txtPassword.Focus();
                CalErrNum();
                return;
            }
            
            
            BLL.System.sys_Person bll = new BLL.System.sys_Person();
            Model.System.sys_LoginUser model = bll.GetModel(userAccount, userPwd, true);
            if (model == null)
            {
                msgtip.InnerHtml = "账号或密码错误！";
                tipsDiv.Style["display"] = "";
                txtPassword.Focus();
                CalErrNum();
                return;
            }
            model.Salt = Utils.GetLetterOrNumberRandom(10);
            model.LoginTime = DateTime.Now;
            model.LoginIP = RequestHelper.GetIP();
            
            // 保存登录人的Sessin
            Session[Keys.SESSION_LoginUser] = model;
            Session.Timeout = 45;
            //写入登录日志
            string operaAction = Enums.ActionEnum.Login.ToString();
            string operaMemo = "用户登录";
            BaseWeb.AddOpera(model, 0, operaAction, operaMemo);

            ////写入Cookies
            Utils.WriteCookie("SCZMLoginSalt", model.Salt);
            Utils.WriteCookie("SCZMAccount", userAccount, 43200);
            Utils.WriteCookie("SCZMUserName", model.PerName);
            Utils.WriteCookie("SCZMUserId", model.ID.ToString());
            Utils.WriteCookie("SCZMDepId", model.DepId.ToString());
            Response.Redirect("index.html");
        }
        protected void CalErrNum()
        {
            if (Session["LoginNum"] == null)
            {
                Session["LoginNum"] = 1;
            }
            else
            {
                Session["LoginNum"] = Convert.ToInt32(Session["LoginNum"]) + 1;
            }
        }
        
    }
}