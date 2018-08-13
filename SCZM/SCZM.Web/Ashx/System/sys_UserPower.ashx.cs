using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Text;
using SCZM.Common;
using SCZM.WebUI;

namespace SCZM.Web.Ashx.System
{
    /// <summary>
    /// sys_UserPower 的摘要说明



    /// </summary>
    public class sys_UserPower : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = RequestHelper.GetQueryString("action");
            switch (action)
            {
                case "Login"://登录
                    Login(context);
                    break;
                case "GetPagePower": //获取页面权限
                    GetPagePower(context);
                    break;
                case "GetMenu": //获取登录人菜单

                    GetMenu(context);
                    break;
                case "GetHomePage": //获取首页数据
                    GetHomePage(context);
                    break;
                case "ModifyPwd": //修改密码
                    ModifyPwd(context);
                    break;
                case "GetUserInfo"://获取登录用户信息
                    GetUserInfo(context);
                    break;
            }
        }
        #region 获取页面权限==============================
        private void Login(HttpContext context)
        {
            try
            {
                string account = RequestHelper.GetString("account");
                string pwd = RequestHelper.GetString("pwd");
                string url = RequestHelper.GetUrlReferrer();

                //判断登录错误次数
                if (context.Session["LoginNum"] != null && Convert.ToInt32(context.Session["LoginNum"]) > 5)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"错误超过5次，关闭浏览器重新登录！\"}");
                    return;
                }
                if (account.Trim() == "")
                {
                    WriteError(context);
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"账号不能为空！\"}");
                    return;
                }
                if (pwd.Trim() == "")
                {
                    WriteError(context);
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"密码不能为空！\"}");
                    return;
                }
                if (url.Trim() == "")
                {
                    WriteError(context);
                    context.Response.Write("{\"status\":\"0.1\",\"msg\":\"非法传入页面！\"}");
                    return;
                }
                String domain = Utils.GetUrlDomain(url).ToLower();
                if (domain != "localhost" || RequestHelper.GetIP()!="127.0.0.1")
                {
                    Model.System.sys_Config configModel = new BLL.System.sys_Config().loadConfig();
                    string[] domainArray = (configModel.webinsideurl + "," + configModel.weburl).Split(',');
                    if (domain == "" || !domainArray.Contains(domain))
                    {
                        WriteError(context);
                        context.Response.Write("{\"status\":\"0.1\",\"msg\":\"非法传入页面！\"}");
                        return;
                    }
                }
                BLL.System.sys_Person bll = new BLL.System.sys_Person();
                Model.System.sys_LoginUser model = bll.GetModel(account, pwd, true);
                if (model == null)
                {
                    WriteError(context);
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"用户名或密码有误，请重试！\"}");
                    return;
                }
                model.Salt = Utils.GetLetterOrNumberRandom(10);
                model.LoginTime = DateTime.Now;
                model.LoginIP = RequestHelper.GetIP();
                // 保存登录人的Sessin
                context.Session[Keys.SESSION_LoginUser] = model;
                context.Session.Timeout = 45;
                //写入登录日志
                string operaAction = Enums.ActionEnum.Login.ToString();
                string operaMemo = "用户登录";
                BaseWeb.AddOpera(model, 0, operaAction, operaMemo);


                context.Response.Write("{\"status\":\"1\",\"msg\":\"权限获取成功！\",\"userName\":\"" + model.PerName + "\",\"loginSalt\":\"" + model.Salt + "\"}");
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"" + e.Message + "\"}");
            }
        }
        #endregion
        #region 记录错误==============================
        private void WriteError(HttpContext context)
        {
            if (context.Session["LoginNum"] == null)
            {
                context.Session["LoginNum"] = 1;
            }
            else
            {
                context.Session["LoginNum"] = Convert.ToInt32(context.Session["LoginNum"]) + 1;
            }
        }
        #endregion
        #region 获取页面权限==============================
        private void GetPagePower(HttpContext context)
        {
            try
            {
                string loginSalt = RequestHelper.GetQueryString("LoginSalt");
                string menuId = RequestHelper.GetQueryString("MenuId");
                string url = RequestHelper.GetUrlReferrer();
                
                Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();

                string result =BaseWeb.CheckPageParam(loginSalt, menuId, url, loginUserModel);
                if (result != "")
                {
                    context.Response.Write(result);
                    return;
                }
                if (loginUserModel.IsAdmin == true)
                {
                    context.Response.Write("{\"status\":\"1\",\"msg\":\"系统管理员权限！\",\"info\":\"\"}");
                    return;
                }
                int pageId = BaseWeb.GetPageId(menuId, url);
                if (pageId == 0)
                {
                    context.Response.Write("{\"status\":\"0.1\",\"msg\":\"非法传入页面！\"}");
                    return;
                }
                List<string[]> pageElementPowerList = BaseWeb.GetPageElementListByCache(loginUserModel.ID, pageId);
                string info = "";
                foreach (string[] pageElementPower in pageElementPowerList)
                {
                    if (pageElementPower[0] == "show" && pageElementPower[1] == "0")
                    {
                        context.Response.Write("{\"status\":\"0.2\",\"msg\":\"没有权限！\"}");
                        return;
                    }
                    if (pageElementPower[0] != "show" && pageElementPower[1] == "0")
                    {
                        info += pageElementPower[0] + ",";
                    }
                }
                info = Utils.DelLastComma(info);
                
                context.Response.Write("{\"status\":\"1\",\"msg\":\"权限获取成功！\",\"info\":\"" + info + "\"}");
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\""+e.Message+"\"}");
            }
        }
        #endregion
        #region 获取登录人菜单==============================
        private void GetMenu(HttpContext context)
        {
            try
            {
                string loginSalt = RequestHelper.GetQueryString("LoginSalt");
                if (loginSalt == "")
                {
                    context.Response.Write("身份验证失败");
                    return;
                }
                Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
                if (loginUserModel == null || loginUserModel.Salt != loginSalt)
                {
                    //context.Response.Write("{\"status\":\"0\",\"msg\":\"身份验证失败！\"}");
                    context.Response.Write("身份验证失败");
                    return;
                }

                string htmlStr = "";
                DataTable dt = null;
                if (loginUserModel.IsAdmin == true)
                {
                    dt = new BLL.System.sys_Menu().GetList("").Tables[0];
                }
                else
                {
                    dt = new BLL.System.sys_Person().GetUserMenu(loginUserModel.ID).Tables[0];
                }
                DataTable btnDT = new BLL.System.sys_Menu().GetNoPowerBtn(loginUserModel.ID).Tables[0];
                int levelOpenId = dt.Select("levelId=1").Length > 4 ? 1 : 2;
                htmlStr = AddNode(dt, btnDT, 1,levelOpenId, "0", "ID", "menuName", "supId", "sortId", "linkUrl", "levelId");
                context.Response.Write(htmlStr);
            }
            catch
            {
                context.Response.Write("身份验证失败");
                return;
            }
        }
        /// <summary>
        /// 添加某一节点下的DOM
        /// </summary>
        /// <param name="menuData">menu DT</param>
        /// <param name="levelBeginID">开始的节点级次</param>
        /// <param name="levelBeginID">打开的层级 从开始节点第几层 如 打开开始节点为1</param>
        /// <param name="nodeID">本次的上级节点</param>
        /// <param name="feildMenuID">菜单ID字段名</param>
        /// <param name="feildMenuName">菜单名称字段名</param>
        /// <param name="feildMenuSupID">上级ID字段名</param>
        /// <param name="feildMenuOrder">排序字段名</param>
        /// <param name="feildMenuURL">链接URL字段名</param>
        /// <param name="feildLevelID">层级字段名</param>
        /// <returns></returns>
        private string AddNode(DataTable menuData,DataTable noPowerBtnData, int levelBeginID,int levelOpenId, string nodeID, string feildMenuID, string feildMenuName, string feildMenuSupID, string feildMenuOrder, string feildMenuURL, string feildLevelID)
        {
            StringBuilder htmlStr = new StringBuilder();
            int levelID = 0;
            bool node = true;
            bool openState = true;
            DataRow[] drs = menuData.Select(feildMenuSupID + "=" + nodeID + "", feildMenuOrder + " asc");
            if (drs.Length != 0)
            {

                foreach (DataRow dr in drs)
                {
                    levelID = Utils.ObjToInt(dr[feildLevelID], 0);
                    node = menuData.Select(feildMenuSupID + "=" + dr[feildMenuID].ToString() + "").Length != 0 ? true : false;

                    if (levelID < levelBeginID + levelOpenId)
                    {
                        htmlStr.Append("<ul style=\"display: block;\">");
                    }
                    else
                    {
                        htmlStr.Append("<ul style=\"display: none;\">");
                    }
                    htmlStr.Append("<li>");
                    openState = (levelID < levelBeginID + levelOpenId - 1) ? true : false;
                    if (node)
                    {
                        htmlStr.Append(Node(dr[feildMenuID].ToString(), true,openState, levelID - levelBeginID + 1, dr[feildMenuName].ToString(), dr[feildMenuURL].ToString(),"[]"));
                        htmlStr.Append(AddNode(menuData, noPowerBtnData, 1,levelOpenId, dr[feildMenuID].ToString(), "ID", "menuName", "supId", "sortId", "linkUrl", "levelId"));
                    }
                    else
                    {  
                        DataRow[] btnDR = noPowerBtnData.Select("MenuId=" + dr[feildMenuID].ToString());
                        string[] arrColumnName={"PageUrl","ElementName"};
                        string btnInfo = Utils.ToJson(btnDR, arrColumnName);
                        htmlStr.Append(Node(dr[feildMenuID].ToString(), false, false,levelID - levelBeginID + 1, dr[feildMenuName].ToString(), dr[feildMenuURL].ToString(), btnInfo));
                    }
                    htmlStr.Append("</li></ul>");
                }

            }
            return htmlStr.ToString();

        }
        /// <summary>
        /// 添加节点DOM，分为中间节点、末级节点两种情况



        /// </summary>
        /// <param name="feildMenuID">菜单ID</param>
        /// <param name="node">是否中间节点</param>
        /// <param name="openState">中间节点是否打开</param>
        /// <param name="levelCount">节点的级次，从菜单开始节点算起</param>
        /// <param name="txt">菜单名</param>
        /// <param name="url">菜单URL</param>
        /// <param name="btnInfo">按钮信息</param>
        /// <returns></returns>
        private string Node(string feildMenuID, bool node, bool openState, int levelCount, string txt, string url,string btnInfo)
        {
            url = url.Trim();
            if (url != "")
            {
                if (url.IndexOf("?") == -1)
                {
                    url += "?vt=" + new BLL.System.sys_Config().loadConfig().webversiontime;
                }
                else
                {
                    url += "&vt=" + new BLL.System.sys_Config().loadConfig().webversiontime;
                }
                url += "&MenuId=" + feildMenuID.ToString();
            }
            StringBuilder htmlStr =new StringBuilder();
            int paddingLeft = 20 * levelCount;
            if (node)
            {
                htmlStr.Append("<div class=\"item\" style=\" padding:10px 0 10px " + paddingLeft.ToString() + "px;\" >");
                if (openState == true)
                {
                    htmlStr.Append("<div class=\"icons folder open\"></div>");
                }
                else
                {
                    htmlStr.Append("<div class=\"icons folder\"></div>");
                }
                htmlStr.Append("<span>" + txt + "</span></div>");
            }
            else
            {
                htmlStr.Append("<div id=\"menu_" + feildMenuID + "\" class=\"item\" style=\" padding:10px 0 10px " + paddingLeft.ToString() + "px;\" MenuID=\"" + feildMenuID + "\" MenuURL=\"" + url + "\">");
                htmlStr.Append("<div class=\"icons last\"></div>");
                htmlStr.Append("<span>" + txt + "</span><input type=\"hidden\" id=\"hidBtnInfo_" + feildMenuID + "\" value=\"" + btnInfo.Replace("\"", "\'") + "\" /></div>");
            }

            return htmlStr.ToString();
        }
        #endregion
        #region 获取首页数据==============================
        private void GetHomePage(HttpContext context)
        {
            try
            {
                string loginSalt = RequestHelper.GetQueryString("LoginSalt"); 
                Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
                if (loginUserModel == null || loginUserModel.Salt != loginSalt)
                {
                    context.Response.Write("{\"status\":\"0.1\",\"msg\":\"对不起，登录超时，请重新登录！\"}");
                    return;
                }
                string vt = new BLL.System.sys_Config().loadConfig().webversiontime;
                BLL.System.sys_Mail_Send mailBll = new BLL.System.sys_Mail_Send();
                int receiveNoRead = mailBll.GetReceiveNoRead(loginUserModel.ID);


                BLL.sys_Bulletin bll = new BLL.sys_Bulletin();
                int recordNumBulletin = RequestHelper.GetInt("recordNumBulletin", 0);
                string strWhere = "";
                DataTable bulletinDT = bll.GetListByPower(strWhere, recordNumBulletin, loginUserModel.DepId).Tables[0];
                string bulletinInfo = Utils.ToJson(bulletinDT);

                BLL.System.sys_Menu menuBll = new BLL.System.sys_Menu();
                //DataTable todoDT = menuBll.GetTodoList(loginUserModel.ID).Tables[0];
                //string todoInfo = Utils.ToJson(todoDT);
                string todoInfo = "[]";
                //DataTable nodoDT = menuBll.GetNodoList(loginUserModel.ID).Tables[0];
                //string nodoInfo = Utils.ToJson(nodoDT);
                string nodoInfo = "[]";
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"vt\":\"" + vt + "\"");
                jsonStr.Append(",\"receiveNoRead\":" + receiveNoRead);
                jsonStr.Append(",\"bulletinInfo\":" + bulletinInfo);
                jsonStr.Append(",\"todoInfo\":" + todoInfo);
                jsonStr.Append(",\"nodoInfo\":" + nodoInfo);
                jsonStr.Append("}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"" + e.Message + "\"}");
            }
        }
        #endregion
        #region 修改密码==============================
        private void ModifyPwd(HttpContext context)
        {
            try
            {
                string loginSalt = RequestHelper.GetQueryString("LoginSalt");
                if (loginSalt == "")
                {
                    context.Response.Write("{\"status\":\"0.1\",\"msg\":\"Salt不能为空！\"}");
                    return;
                }
                Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
                if (loginUserModel == null || loginUserModel.Salt != loginSalt)
                {
                    context.Response.Write("{\"status\":\"0.1\",\"msg\":\"登录超时，请重新登录！\"}");
                    return;
                }

                string oldPwd = RequestHelper.GetString("oldPwd");
                string newPwd = RequestHelper.GetString("newPwd");
                BLL.System.sys_Person bll = new BLL.System.sys_Person();


                string errMessage = bll.UpdatePwd(loginUserModel.Account, oldPwd, newPwd);
                if (errMessage != "")
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"" + errMessage + "\"}");
                    return;
                }
                //写入操作日志
                
                Model.System.sys_OperaLog operaModel = BaseWeb.GetOperaModel(loginUserModel);
                operaModel.OperaType = Enums.ActionEnum.Login.ToString();
                operaModel.Memo = "修改密码";
                new BLL.System.sys_OperaLog().Add(operaModel);
                
                context.Response.Write("{\"status\":\"1\",\"msg\":\"密码修改成功！\"}");

            }
            catch(Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        #region 获取登录用户信息==============================
        private void GetUserInfo(HttpContext context)
        {
            try
            {
                string loginSalt = RequestHelper.GetQueryString("LoginSalt");
                if (loginSalt == "")
                {
                    context.Response.Write("{\"status\":\"0.1\",\"msg\":\"Salt不能为空！\"}");
                    return;
                }
                Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
                if (loginUserModel == null || loginUserModel.Salt != loginSalt)
                {
                    context.Response.Write("{\"status\":\"0.1\",\"msg\":\"登录超时，请重新登录！\"}");
                    return;
                }

                context.Response.Write("{\"status\":\"1\",\"userName\":\"" + loginUserModel.PerName + "！\"}");

            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}